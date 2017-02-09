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
    public class ArticleManagementServiceTests
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
                new ArticleManagementService(
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
                new ArticleManagementService(
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
                new ArticleManagementService(
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
                new ArticleManagementService(
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
                new ArticleManagementService(
                    this.mockUserRepo.Object,
                    this.mockNewsItemsRepo.Object,
                    this.mockNewsData.Object,
                    this.mockMapperProvider.Object,
                    null,
                    this.dateTimeProvider.Object));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Add_IfNullOrEmptyUsernameIsPassedArgumentNullExceptionShouldBeThrown(string username)
        {
            var articleManagementService = this.GetArticleManagementService();
            var exception = Assert.Throws<ArgumentNullException>(() => articleManagementService.Add(username, new NewsWebModel()));
            Assert.AreEqual("username", exception.ParamName);
        }

        [Test]
        public void Add_IfNullOrEmptyNewsItemIsPassedArgumentNullExceptionShouldBeThrown()
        {
            var articleManagementService = this.GetArticleManagementService();
            var exception = Assert.Throws<ArgumentNullException>(() => articleManagementService.Add("username", null));
            Assert.AreEqual("newsItem", exception.ParamName);
        }

        [Test]
        public void Add_ShouldCallImageRepositoryAddOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { UserName = testUsername, Id = "1" };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            var articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.mockImageRepo.Verify(x => x.Add(It.IsAny<Image>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallNewsRepositoryAddOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { UserName = testUsername, Id = "1" };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            var articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.mockNewsItemsRepo.Verify(x => x.Add(It.IsAny<NewsItem>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallUnitOfWorkCommitOnce()
        {
            var testWebNewsModel = new NewsWebModel();
            var testUsername = "junka";
            var testUser = new User { UserName = testUsername, Id = "1" };

            this.mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());
            
            var articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.mockNewsData.Verify(x => x.Commit(), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Restore_ShouldThrowArgumentNullExceptionWhenItemIdIsNullOrEmpty(string newsItemId)
        {
            var articleManagementService = this.GetArticleManagementService();
            var exception = Assert.Throws<ArgumentNullException>(() => articleManagementService.Restore(newsItemId));

            Assert.AreEqual(nameof(newsItemId), exception.ParamName);
        }

        [Test]
        public void Restore_NewsRepositoryGetByIdShouldBeCalledWithParsedNewsItemId()
        {
            var newsItem = new NewsItem();
            this.mockNewsItemsRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            var articleManagementService = this.GetArticleManagementService();
            string newsItemId = "1";

            articleManagementService.Restore(newsItemId);
            this.mockNewsItemsRepo.Verify(x => x.GetById(It.Is<int>(a => a == int.Parse(newsItemId))), Times.Once);
        }

        [Test]
        public void Restore_NewsRepositoryUpdateShouldBeCalledWithTheFoundItem()
        {
            var newsItem = new NewsItem();
            this.mockNewsItemsRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            var articleManagementService = this.GetArticleManagementService();
            string newsItemId = "1";

            articleManagementService.Restore(newsItemId);
            this.mockNewsItemsRepo.Verify(x => x.Update(It.Is<NewsItem>(a => a == newsItem)), Times.Once);
        }

        [Test]
        public void Restore_NewsDataCommitShouldBeCalled()
        {
            var newsItem = new NewsItem();
            this.mockNewsItemsRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            var articleManagementService = this.GetArticleManagementService();
            string newsItemId = "1";

            articleManagementService.Restore(newsItemId);
            this.mockNewsData.Verify(x => x.Commit(), Times.Once);
        }

        private ArticleManagementService GetArticleManagementService()
        {
            return new ArticleManagementService(
                this.mockUserRepo.Object,
                this.mockNewsItemsRepo.Object,
                this.mockNewsData.Object,
                this.mockMapperProvider.Object,
                this.mockImageRepo.Object,
                this.dateTimeProvider.Object);
        }
    }
}