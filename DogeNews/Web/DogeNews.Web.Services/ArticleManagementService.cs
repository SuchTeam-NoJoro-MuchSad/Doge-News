using System;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services
{
    public class ArticleManagementService : IArticleManagementService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<NewsItem> newsRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly INewsData newsData;
        private readonly IMapperProvider mapperProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public ArticleManagementService(IRepository<User> userRepository,
            IRepository<NewsItem> newsRepository,
            INewsData newsData,
            IMapperProvider mapperProvider,
            IRepository<Image> imageRepository,
            IDateTimeProvider dateTimeProvider)
        {
            this.ValidateConstructorParams(userRepository, newsRepository, newsData, mapperProvider, imageRepository);

            this.userRepository = userRepository;
            this.newsRepository = newsRepository;
            this.imageRepository = imageRepository;
            this.newsData = newsData;
            this.mapperProvider = mapperProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Add(string username, NewsWebModel newsItem)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }

            if (newsItem == null)
            {
                throw new ArgumentNullException("newsItem");
            }

            var author = this.userRepository.GetFirst(x => x.UserName == username);
            var image = this.mapperProvider.Instance.Map<Image>(newsItem.Image);
            var news = this.mapperProvider.Instance.Map<NewsItem>(newsItem);

            news.Author = author;
            news.AuthorId = author.Id;
            news.Image = image;
            news.ImageId = image.Id;
            news.CreatedOn = this.dateTimeProvider.Now;

            this.imageRepository.Add(image);
            this.newsRepository.Add(news);
            this.newsData.Commit();
        }

        public void Update(NewsWebModel model)
        {
            var entryToUpdate = this.newsRepository.GetById(model.Id);
            entryToUpdate.Title = model.Title;
            entryToUpdate.Category = model.Category;
            entryToUpdate.Content = model.Content;

            if (model.Image != null)
            {
                var image = this.mapperProvider.Instance.Map<Image>(model.Image);
                this.imageRepository.Add(image);
                entryToUpdate.Image = image;
                entryToUpdate.ImageId = image.Id;
            }

            newsRepository.Update(entryToUpdate);
            newsData.Commit();
        }

        public void Restore(string newsItemId)
        {
            if (string.IsNullOrEmpty(newsItemId))
            {
                throw new ArgumentNullException(nameof(newsItemId));
            }

            var id = int.Parse(newsItemId);
            var foundItem = this.newsRepository.GetById(id);

            foundItem.DeletedOn = null;
            this.newsRepository.Update(foundItem);
            this.newsData.Commit();
        }

        public void Delete(string newsItemId)
        {
            if (string.IsNullOrEmpty(newsItemId))
            {
                throw new ArgumentNullException(nameof(newsItemId));
            }

            var id = int.Parse(newsItemId);
            var foundItem = this.newsRepository.GetById(id);
            foundItem.DeletedOn = this.dateTimeProvider.Now;
            this.newsRepository.Update(foundItem);
            this.newsData.Commit();
        }

        private void ValidateConstructorParams(
            IRepository<User> userRepository,
            IRepository<NewsItem> newsRepository,
            INewsData newsData,
            IMapperProvider mapperProvider,
            IRepository<Image> imageRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (newsRepository == null)
            {
                throw new ArgumentNullException("newsRepository");
            }

            if (newsData == null)
            {
                throw new ArgumentNullException("newsData");
            }

            if (mapperProvider == null)
            {
                throw new ArgumentNullException("mapperProvider");
            }

            if (imageRepository == null)
            {
                throw new ArgumentNullException("imageRepository");
            }
        }
    }
}