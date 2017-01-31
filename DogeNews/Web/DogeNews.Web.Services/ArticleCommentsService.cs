using System;
using System.Collections.Generic;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;
using System.Linq;

namespace DogeNews.Web.Services
{
    public class ArticleCommentsService : IArticleCommentsService
    {
        private readonly IRepository<Comment> commentItemRepository;
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private int count;


        public ArticleCommentsService(IRepository<Comment> commentsRepository,
            IRepository<NewsItem> newsItemRepository,
            IRepository<User> userRepository,
            IMapperProvider mapperProvider,
            INewsData newsData)
        {
            this.ValidateConstructorParams(commentsRepository, mapperProvider);

            this.commentItemRepository = commentsRepository;
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
            var newsItem = newsItemRepository.GetFirst(x => x.Title == title);
            var comments = newsItem.Comments;
            IEnumerable<CommentWebModel> mappedModels = this.mapperProvider.Instance.Map<IEnumerable<CommentWebModel>>(comments);

            this.count = mappedModels.Count();

            return mappedModels;
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

        private void ValidateConstructorParams(IRepository<Comment> commentsRepository, IMapperProvider mapperProvider)
        {
            if (commentsRepository == null)
            {
                throw new ArgumentNullException("commentsRepository");
            }

            if (mapperProvider == null)
            {
                throw new ArgumentNullException("mapperProvider");
            }
        }
    }
}