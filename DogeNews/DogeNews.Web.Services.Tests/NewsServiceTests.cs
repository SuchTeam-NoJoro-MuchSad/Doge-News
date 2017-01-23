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

        [SetUp]
        public void Init()
        {
            this.mockUserRepo = new Mock<IRepository<User>>();
            this.mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            this.mockNewsData = new Mock<INewsData>();
            this.mockMapperProvider = new Mock<IMapperProvider>();
            this.mockImageRepo = new Mock<IRepository<Image>>();
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
                    this.mockImageRepo.Object));
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
                    this.mockImageRepo.Object));
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
                    this.mockImageRepo.Object));
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
                    this.mockImageRepo.Object));
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
                    null));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Add_IfNullOrEmptyUsernameIsPassedArgumentNullExceptionShouldBeThrown(string username)
        {
            var mockUserRepo = new Mock<IRepository<User>>();
            var mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            var mockNewsData = new Mock<INewsData>();
            var mockMapperProvider = new Mock<IMapperProvider>();
            var mockImageRepo = new Mock<IRepository<Image>>();

            var newsService = new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => newsService.Add(username, new NewsWebModel()));
            Assert.AreEqual("username", exception.ParamName);
        }

        [Test]
        public void Add_IfNullOrEmptyNewsItemIsPassedArgumentNullExceptionShouldBeThrown()
        {
            var newsService = new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => newsService.Add("username", null));
            Assert.AreEqual("newsItem", exception.ParamName);
        }

        [Test]
        public void Add_ShouldCallImageRepositoryAddOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            var newsService = new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object);

            newsService.Add(testUsername, testWebNewsModel);
            this.mockImageRepo.Verify(x => x.Add(It.IsAny<Image>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallNewsRepositoryAddOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            var newsService = new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object);

            newsService.Add(testUsername, testWebNewsModel);
            this.mockNewsItemsRepo.Verify(x => x.Add(It.IsAny<NewsItem>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallUnitOfWorkCommitOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());


            var newsService = new NewsService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object);

            newsService.Add(testUsername, testWebNewsModel);
            this.mockNewsData.Verify(x => x.Commit(), Times.Once);
        }
    }
}