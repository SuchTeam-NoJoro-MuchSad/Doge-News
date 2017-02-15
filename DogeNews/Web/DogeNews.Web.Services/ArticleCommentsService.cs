using System.Collections.Generic;
using System.Linq;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;
using DogeNews.Common.Validators;

namespace DogeNews.Web.Services
{
    public class ArticleCommentsService : IArticleCommentsService
    {
        private readonly IRepository<Comment> commentsRepository;
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;

        private int count;
        
        public ArticleCommentsService(
            IRepository<Comment> commentsRepository,
            IRepository<NewsItem> newsItemRepository,
            IRepository<User> userRepository,
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