using DogeNews.Common.Attributes;
using DogeNews.Common.Validators;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Data.Contracts;
using DogeNews.Web.Interception;
using DogeNews.Web.Models;

namespace DogeNews.Services.Data
{
    [Interceptable(typeof(ExceptionInterceptor), typeof(AdminActionsInterceptor))]
    public class ArticleManagementService : IArticleManagementService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<NewsItem> newsRepository;
        private readonly IRepository<Image> imageRepository;
        private readonly INewsData newsData;
        private readonly IMapperProvider mapperProvider;
        private readonly IDateTimeProvider dateTimeProvider;

        public ArticleManagementService(
            IRepository<User> userRepository,
            IRepository<NewsItem> newsRepository,
            INewsData newsData,
            IMapperProvider mapperProvider,
            IRepository<Image> imageRepository,
            IDateTimeProvider dateTimeProvider)
        {
            Validator.ValidateThatObjectIsNotNull(userRepository, nameof(userRepository));
            Validator.ValidateThatObjectIsNotNull(newsRepository, nameof(newsRepository));
            Validator.ValidateThatObjectIsNotNull(newsData, nameof(newsData));
            Validator.ValidateThatObjectIsNotNull(mapperProvider, nameof(mapperProvider));
            Validator.ValidateThatObjectIsNotNull(imageRepository, nameof(imageRepository));
            Validator.ValidateThatObjectIsNotNull(dateTimeProvider, nameof(dateTimeProvider));

            this.userRepository = userRepository;
            this.newsRepository = newsRepository;
            this.imageRepository = imageRepository;
            this.newsData = newsData;
            this.mapperProvider = mapperProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void Add(string username, NewsWebModel newsItem)
        {
            Validator.ValidateThatStringIsNotNullOrEmpty(username, nameof(username));
            Validator.ValidateThatObjectIsNotNull(newsItem, nameof(newsItem));

            User author = this.userRepository.GetFirst(x => x.UserName == username);
            Image image = this.mapperProvider.Instance.Map<Image>(newsItem.Image);
            NewsItem news = this.mapperProvider.Instance.Map<NewsItem>(newsItem);

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
            Validator.ValidateThatObjectIsNotNull(model, nameof(model));

            NewsItem entityToUpdate = this.newsRepository.GetById(model.Id);

            entityToUpdate.Title = model.Title;
            entityToUpdate.Category = model.Category;
            entityToUpdate.Content = model.Content;

            if (model.Image != null)
            {
                Image image = this.mapperProvider.Instance.Map<Image>(model.Image);
                this.imageRepository.Add(image);
                entityToUpdate.Image = image;
                entityToUpdate.ImageId = image.Id;
            }

            newsRepository.Update(entityToUpdate);
            newsData.Commit();
        }

        public void Restore(int id)
        {
            Validator.ValidateThatNumberIsNotNegative(id, nameof(id));

            NewsItem foundItem = this.newsRepository.GetById(id);

            foundItem.DeletedOn = null;
            this.newsRepository.Update(foundItem);
            this.newsData.Commit();
        }

        public void Delete(int id)
        {
            Validator.ValidateThatNumberIsNotNegative(id, nameof(id));

            NewsItem foundItem = this.newsRepository.GetById(id);

            foundItem.DeletedOn = this.dateTimeProvider.Now;
            this.newsRepository.Update(foundItem);
            this.newsData.Commit();
        }
    }
}