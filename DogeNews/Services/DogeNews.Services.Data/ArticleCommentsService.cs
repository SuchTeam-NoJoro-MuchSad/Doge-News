using System.Collections.Generic;
using System.Linq;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Services.Common.Contracts;
using DogeNews.Common.Validators;
using DogeNews.Web.Models;

namespace DogeNews.Web.Services
{
    public class ArticleCommentsService : IArticleCommentsService
    {
        private readonly IProjectableRepository<Comment> commentsRepository;
        private readonly IProjectableRepository<NewsItem> newsItemRepository;
        private readonly IProjectableRepository<User> userRepository;
        private readonly IMapperProvider mapperProvider;
        private readonly INewsData newsData;

        private int count;

        public ArticleCommentsService(
            IProjectableRepository<Comment> commentsRepository,
            IProjectableRepository<NewsItem> newsItemRepository,
            IProjectableRepository<User> userRepository,
            IMapperProvider mapperProvider,
            INewsData newsData)
        {
            Validator.ValidateThatObjectIsNotNull(commentsRepository, nameof(commentsRepository));
            Validator.ValidateThatObjectIsNotNull(newsItemRepository, nameof(newsItemRepository));
            Validator.ValidateThatObjectIsNotNull(userRepository, nameof(userRepository));
            Validator.ValidateThatObjectIsNotNull(mapperProvider, nameof(mapperProvider));
            Validator.ValidateThatObjectIsNotNull(newsData, nameof(newsData));

            this.commentsRepository = commentsRepository;
            this.newsItemRepository = newsItemRepository;
            this.userRepository = userRepository;
            this.newsData = newsData;
            this.mapperProvider = mapperProvider;
        }

        public int Count
        {
            get { return this.count; }
        }

        public IEnumerable<CommentWebModel> GetCommentsForArticleByTitle(string title)
        {
            var newsItem = newsItemRepository.GetFirstMapped<NewsWebModel>(x => x.Title == title);

            this.count = newsItem.Comments.Count();

            return newsItem.Comments;
        }

        public void AddComment(string newsItemTitle, string commentContent, string userName)
        {
            var foundUser = this.userRepository.GetFirst(x => x.UserName == userName);
            var newsItem = newsItemRepository.GetFirst(x => x.Title == newsItemTitle);
            var commentToAdd = new Comment
            {
                User = foundUser,
                Content = commentContent
            };

            newsItem.Comments.Add(commentToAdd);
            this.newsData.Commit();
        }
    }
}