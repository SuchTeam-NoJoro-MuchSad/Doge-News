using System;
using System.Linq.Expressions;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;

using Moq;

using NUnit.Framework;

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
        private Mock<IDateTimeProvider> dateTimeProvider;

        [SetUp]
        public void Init()
        {
            this.mockUserRepo = new Mock<IRepository<User>>();
            this.mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            this.mockNewsData = new Mock<INewsData>();
            this.mockMapperProvider = new Mock<IMapperProvider>();
            this.mockImageRepo = new Mock<IRepository<Image>>();
            this.dateTimeProvider = new Mock<IDateTimeProvider>();
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
                    this.dateTimeProvider.Object)
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
                    this.dateTimeProvider.Object));
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
                    this.dateTimeProvider.Object));
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
                    this.dateTimeProvider.Object));
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
                    this.dateTimeProvider.Object));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }


    }
}
