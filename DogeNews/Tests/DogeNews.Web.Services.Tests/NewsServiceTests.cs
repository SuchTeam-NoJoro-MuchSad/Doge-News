using System;
using System.Linq.Expressions;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;

using Moq;
using NUnit.Framework;

using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Configuration;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class NewsServiceTests
    {
        private Mock<IRepository<User>> mockUserRepo;
        private Mock<IRepository<NewsItem>> mockNewsItemsRepo;
        private Mock<INewsData> mockNewsData;
        private Mock<IMapperProvider> mockMapperProvider;
        private Mock<IRepository<Image>> mockImageRepo;
        private Mock<IDateTimeProvider> mockDateTimeProvider;
        private Mock<IMapper> mockMapper;

        [SetUp]
        public void Init()
        {
            this.mockUserRepo = new Mock<IRepository<User>>();
            this.mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            this.mockNewsData = new Mock<INewsData>();
            this.mockMapperProvider = new Mock<IMapperProvider>();
            this.mockImageRepo = new Mock<IRepository<Image>>();
            this.mockDateTimeProvider = new Mock<IDateTimeProvider>();
            this.mockMapper = new Mock<IMapper>();
        }

        [Test]
        public void Constructor_IfUserRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    null,
                    this.mockNewsItemsRepo.Object,
                    this.mockNewsData.Object,
                    this.mockMapperProvider.Object,
                    this.mockImageRepo.Object,
                    this.mockDateTimeProvider.Object)
                );
            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsItemRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    this.mockUserRepo.Object,
                    null,
                    this.mockNewsData.Object,
                    this.mockMapperProvider.Object,
                    this.mockImageRepo.Object,
                    this.mockDateTimeProvider.Object));
            Assert.AreEqual("newsRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsDataIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    this.mockUserRepo.Object,
                    this.mockNewsItemsRepo.Object,
                    null,
                    this.mockMapperProvider.Object,
                    this.mockImageRepo.Object,
                    this.mockDateTimeProvider.Object));
            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Constructor_IfMapperProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    this.mockUserRepo.Object,
                    this.mockNewsItemsRepo.Object,
                    this.mockNewsData.Object,
                    null,
                    this.mockImageRepo.Object,
                    this.mockDateTimeProvider.Object));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_IfImageRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsService(
                    this.mockUserRepo.Object,
                    this.mockNewsItemsRepo.Object,
                    this.mockNewsData.Object,
                    this.mockMapperProvider.Object,
                    null,
                    this.mockDateTimeProvider.Object));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetItemByTitle_ShouldThrowArgumentNullExceptionWhenTitleIsNullOrEmpty(string title)
        {
            var service = this.GetNewsService();
            var exception = Assert.Throws<ArgumentNullException>(() => service.GetItemByTitle(title));
            string expectedParamName = "title";

            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        [Test]
        public void GetItemByTitle_ShouldCallGetFirstMappedNewsRepositoryGetFirst()
        {
            var newsWebModel = new NewsWebModel();
            string title = "title";
            var newsItem = new NewsItem { Title = title };

            this.mockMapper.Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>())).Returns(newsWebModel);
            this.mockMapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);
            this.mockNewsItemsRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            var service = this.GetNewsService();

            service.GetItemByTitle(title);
            this.mockNewsItemsRepo.Verify(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemByTitle_ShouldReturnCorrectObject()
        {
            string title = "title";
            var newsWebModel = new NewsWebModel { Title = title };
            var newsItem = new NewsItem { Title = title };

            this.mockNewsItemsRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsWebModel);

            var service = this.GetNewsService();
            var foundItem = service.GetItemByTitle(title);

            Assert.AreEqual(newsWebModel, foundItem);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void GetItemById_ShouldThrowArgumentOutOfRangeExceptionWhenIdIsNotGreaterThanZero(int id)
        {
            var service = this.GetNewsService();
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetItemById(id));
            string expectedParamName = "id";

            Assert.AreEqual(expectedParamName, exception.ParamName);
        }

        [Test]
        public void GetItemById_ShouldCallNewsRepositoryGetFirstMapped()
        {
            var service = this.GetNewsService();
            int id = 1;

            service.GetItemById(id);
            this.mockNewsItemsRepo.Verify(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetItemById_ShouldReturnTheCorrectNewsItem()
        {
            var newsItem = new NewsWebModel();

            this.mockNewsItemsRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            var service = this.GetNewsService();
            int id = 1;

            var foundItem = service.GetItemById(id);
            Assert.AreEqual(newsItem, foundItem);
        }
        
        [Test]
        public void GetSliderNews_NewsRepositoryAllShouldBeCalled()
        {
            var newsItems = new List<NewsItem>().AsQueryable();
            var configProvider = new Mock<IConfigurationProvider>();
            
            this.mockNewsItemsRepo.SetupGet(x => x.All).Returns(newsItems);
            this.mockMapperProvider
                .SetupGet(x => x.Configuration)
                .Returns(configProvider.Object);

            var service = this.GetNewsService();

            service.GetSliderNews();
            this.mockNewsItemsRepo.VerifyGet(x => x.All, Times.Once);
        }

        [Test]
        public void GetNewsItemsByCategory_NewsRepositoryGetAllShouldBeCalled()
        {
            var newsWebModel = new NewsWebModel();

            this.mockMapper.Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>())).Returns(newsWebModel);
            this.mockMapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);

            var service = this.GetNewsService();

            service.GetNewsItemsByCategory("Breaking");
            this.mockNewsItemsRepo.Verify(x => x.GetAll(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        private NewsService GetNewsService()
        {
            return new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object,
                this.mockDateTimeProvider.Object);
        }
    }
}
