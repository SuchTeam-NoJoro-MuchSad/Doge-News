using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Models;
using Moq;
using NUnit.Framework;

namespace DogeNews.Services.Data.Tests
{
    [TestFixture]
    public class NewsServiceTests
    {
        private Mock<IProjectableRepository<User>> userRepo;
        private Mock<IProjectableRepository<NewsItem>> newsItemRepo;
        private Mock<IProjectableRepository<Image>> imageRepo;
        private Mock<INewsData> newsData;
        private Mock<IMapperProvider> mapperProvider;
        private Mock<IDateTimeProvider> dateTimeProvider;
        private Mock<IMapper> mapper;
        private Mock<IProjectionService> projectionService;

        [SetUp]
        public void Init()
        {
            this.userRepo = new Mock<IProjectableRepository<User>>();
            this.newsItemRepo = new Mock<IProjectableRepository<NewsItem>>();
            this.imageRepo = new Mock<IProjectableRepository<Image>>();
            this.newsData = new Mock<INewsData>();
            this.mapperProvider = new Mock<IMapperProvider>();
            this.dateTimeProvider = new Mock<IDateTimeProvider>();
            this.mapper = new Mock<IMapper>();
            this.projectionService = new Mock<IProjectionService>();
        }

        [Test]
        public void Constructor_IfUserRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    null,
                    this.newsItemRepo.Object,
                    this.imageRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.dateTimeProvider.Object,
                    this.projectionService.Object)
                );

            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsItemRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    null,
                    this.imageRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.dateTimeProvider.Object,
                    this.projectionService.Object));
            Assert.AreEqual("newsRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfImageRepoIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    null,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.dateTimeProvider.Object,
                    this.projectionService.Object));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsDataIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.imageRepo.Object,
                    null,
                    this.mapperProvider.Object,
                    this.dateTimeProvider.Object,
                    this.projectionService.Object));
            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Constructor_IfMapperProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.imageRepo.Object,
                    this.newsData.Object,
                    null,
                    this.dateTimeProvider.Object,
                    this.projectionService.Object));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_IfDateTimeProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.imageRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    null,
                    this.projectionService.Object));
            Assert.AreEqual("dateTimeProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_IfProjectionServiceIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                 new NewsService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.imageRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.dateTimeProvider.Object,
                    null));
            Assert.AreEqual("projectionService", exception.ParamName);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetItemByTitle_ShouldThrowArgumentNullExceptionWhenTitleIsNullOrEmpty(string title)
        {
            NewsService service = this.GetNewsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.GetItemByTitle(title));
            string expectedParamName = "title";

            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        [Test]
        public void GetItemByTitle_ShouldCallGetFirstMappedNewsRepositoryGetFirst()
        {
            NewsWebModel newsWebModel = new NewsWebModel();
            string title = "title";
            NewsItem newsItem = new NewsItem { Title = title };

            this.mapper.Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>())).Returns(newsWebModel);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mapper.Object);
            this.newsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            NewsService service = this.GetNewsService();

            service.GetItemByTitle(title);
            this.newsItemRepo.Verify(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemByTitle_ShouldReturnCorrectObject()
        {
            string title = "title";
            NewsWebModel newsWebModel = new NewsWebModel { Title = title };
            NewsItem newsItem = new NewsItem { Title = title };

            this.newsItemRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsWebModel);

            NewsService service = this.GetNewsService();
            NewsWebModel foundItem = service.GetItemByTitle(title);

            Assert.AreEqual(newsWebModel, foundItem);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void GetItemById_ShouldThrowArgumentOutOfRangeExceptionWhenIdIsNotGreaterThanZero(int id)
        {
            NewsService service = this.GetNewsService();
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetItemById(id));
            string expectedParamName = "id";

            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        [Test]
        public void GetItemById_ShouldCallNewsRepositoryGetFirstMapped()
        {
            NewsService service = this.GetNewsService();
            int id = 1;

            service.GetItemById(id);
            this.newsItemRepo.Verify(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemById_ShouldReturnTheCorrectNewsItem()
        {
            NewsWebModel newsItem = new NewsWebModel();

            this.newsItemRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            NewsService service = this.GetNewsService();
            int id = 1;

            NewsWebModel foundItem = service.GetItemById(id);
            Assert.AreEqual(newsItem, foundItem);
        }

        [Test]
        public void GetSliderNews_NewsRepositoryAllShouldBeCalled()
        {
            IQueryable<NewsItem> newsItems = new List<NewsItem>().AsQueryable();
            Mock<IConfigurationProvider> configProvider = new Mock<IConfigurationProvider>();

            this.newsItemRepo.SetupGet(x => x.All).Returns(newsItems);
            this.mapperProvider
                .SetupGet(x => x.Configuration)
                .Returns(configProvider.Object);

            NewsService service = this.GetNewsService();

            service.GetSliderNews();
            this.newsItemRepo.VerifyGet(x => x.All, Times.Once);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetNewsItemsByCategory_ShouldThrowArgumentNullExceptionWhenCategoryIsNullOrEmpty(string category)
        {
            NewsService service = this.GetNewsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.GetNewsItemsByCategory(category));

            Assert.AreEqual("category", exception.ParamName);
        }

        [Test]
        public void GetNewsItemsByCategory_NewsRepositoryGetAllMappedShouldBeCalled()
        {
            NewsWebModel newsWebModel = new NewsWebModel();

            this.mapper.Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>())).Returns(newsWebModel);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mapper.Object);

            NewsService service = this.GetNewsService();

            service.GetNewsItemsByCategory("Breaking");
            this.newsItemRepo.Verify(x => x.GetAllMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        private NewsService GetNewsService()
        {
            return new NewsService(
                this.userRepo.Object,
                this.newsItemRepo.Object,
                this.imageRepo.Object,
                this.newsData.Object,
                this.mapperProvider.Object,
                this.dateTimeProvider.Object,
                this.projectionService.Object);
        }
    }
}
