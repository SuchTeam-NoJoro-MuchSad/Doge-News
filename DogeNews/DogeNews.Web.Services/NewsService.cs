using System;

using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<NewsItem> newsRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly INewsData newsData;
        private readonly IMapperProvider mapperProvider;

        public NewsService(
            IRepository<User> userRepository,
            IRepository<NewsItem> newsRepository,
            INewsData newsData,
            IMapperProvider mapperProvider,
            IRepository<Image> imageRepository)
        {
            this.ValidateConstructorParams(userRepository, newsRepository, newsData, mapperProvider, imageRepository);

            this.userRepository = userRepository;
            this.newsRepository = newsRepository;
            this.imageRepository = imageRepository;
            this.newsData = newsData;
            this.mapperProvider = mapperProvider;
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

            var author = this.userRepository.GetFirst(x => x.Username == username);
            var image = this.mapperProvider.Instance.Map<Image>(newsItem.Image);
            var news = this.mapperProvider.Instance.Map<NewsItem>(newsItem);

            news.Author = author;
            news.AuthorId = author.Id;
            news.Image = image;
            news.ImageId = image.Id;
            this.imageRepository.Add(image);
            this.newsRepository.Add(news);
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
