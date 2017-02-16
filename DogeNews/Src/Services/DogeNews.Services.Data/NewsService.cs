using System;
using System.Collections.Generic;
using System.Linq;

using DogeNews.Common.Enums;
using DogeNews.Web.Models;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Models;
using DogeNews.Data.Contracts;
using DogeNews.Common.Validators;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Common;

namespace DogeNews.Web.Services
{
    public class NewsService : INewsService
    {
        private const int SliderNewsCount = 3;

        private readonly IProjectableRepository<User> userRepository;
        private readonly IProjectableRepository<NewsItem> newsRepository;
        private readonly IProjectableRepository<Image> imageRepository;
        private readonly INewsData newsData;
        private readonly IMapperProvider mapperProvider;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IProjectionService projectionService;

        public NewsService(
            IProjectableRepository<User> userRepository,
            IProjectableRepository<NewsItem> newsRepository,
            IProjectableRepository<Image> imageRepository,
            INewsData newsData,
            IMapperProvider mapperProvider,
            IDateTimeProvider dateTimeProvider,
            IProjectionService projectionService)
        {
            Validator.ValidateThatObjectIsNotNull(userRepository, nameof(userRepository));
            Validator.ValidateThatObjectIsNotNull(newsRepository, nameof(newsRepository));
            Validator.ValidateThatObjectIsNotNull(newsData, nameof(newsData));
            Validator.ValidateThatObjectIsNotNull(mapperProvider, nameof(mapperProvider));
            Validator.ValidateThatObjectIsNotNull(imageRepository, nameof(imageRepository));
            Validator.ValidateThatObjectIsNotNull(dateTimeProvider, nameof(dateTimeProvider));
            Validator.ValidateThatObjectIsNotNull(projectionService, nameof(projectionService));

            this.userRepository = userRepository;
            this.newsRepository = newsRepository;
            this.imageRepository = imageRepository;
            this.newsData = newsData;
            this.mapperProvider = mapperProvider;
            this.dateTimeProvider = dateTimeProvider;
            this.projectionService = projectionService;
        }

        public NewsWebModel GetItemByTitle(string title)
        {
            Validator.ValidateThatStringIsNotNullOrEmpty(title, nameof(title));

            var newsWebModel = this.newsRepository.GetFirstMapped<NewsWebModel>(x => x.Title == title);
            return newsWebModel;
        }

        public NewsWebModel GetItemById(int id)
        {
            Validator.ValidateThatNumberIsNotNegative(id, nameof(id));

            var newsWebModel = this.newsRepository.GetFirstMapped<NewsWebModel>(x => x.Id == id);
            return newsWebModel;
        }

        public IEnumerable<NewsWebModel> GetSliderNews()
        {
            var query = this.newsRepository
                .All
                .OrderByDescending(x => x.CreatedOn)
                .Take(SliderNewsCount);
            var news = this.projectionService.ProjectToList<NewsItem, NewsWebModel>(query);

            return news;
        }

        public IEnumerable<NewsWebModel> GetNewsItemsByCategory(string category)
        {
            Validator.ValidateThatStringIsNotNullOrEmpty(category, nameof(category));

            var enumeration = (NewsCategoryType)Enum.Parse(typeof(NewsCategoryType), category);
            var news = this.newsRepository.GetAllMapped<NewsWebModel>(x => x.Category == enumeration);

            return news;
        }
    }
}
