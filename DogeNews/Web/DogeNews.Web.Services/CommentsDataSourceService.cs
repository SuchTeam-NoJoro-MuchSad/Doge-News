using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;
using System.Linq;
using System.Net;
using DogeNews.Data;

namespace DogeNews.Web.Services
{
    public class CommentsDataSourceService : ICommentDataSourceService
    {
        private readonly IRepository<Comment> commentItemRepository;
        private readonly IRepository<NewsItem> newsItemRepository;
        private readonly IMapperProvider mapperProvider;
        private readonly IRepository<User> userRepository;
        private readonly INewsData newsData;
        private int count;


        public CommentsDataSourceService(IRepository<Comment> commentsRepository,
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

        public IEnumerable<CommentWebModel> GetPageItems(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.OrderByDateDescending(page, pageSize);
            return items;
        }

        public IEnumerable<CommentWebModel> OrderByDateAscending(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.commentItemRepository
                .All
                .OrderBy(x => x.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<CommentWebModel>(x));

            return items;
        }

        public IEnumerable<CommentWebModel> OrderByDateDescending(int page, int pageSize)
        {
            this.ValidatePage(page);
            this.ValidatePageSize(pageSize);

            var items = this.commentItemRepository
                .All
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(x => this.mapperProvider.Instance.Map<CommentWebModel>(x));

            return items;
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

        private void ValidatePage(int page)
        {
            if (page <= 0)
            {
                throw new ArgumentOutOfRangeException("page");
            }
        }

        private void ValidatePageSize(int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }
        }
    }
}