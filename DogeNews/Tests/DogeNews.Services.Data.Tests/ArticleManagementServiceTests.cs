using System;
using System.Linq.Expressions;
using AutoMapper;
using DogeNews.Common.Enums;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Models;
using Moq;
using NUnit.Framework;

namespace DogeNews.Services.Data.Tests
{
    [TestFixture]
    public class ArticleManagementServiceTests
    {
        private Mock<IRepository<User>> userRepo;
        private Mock<IRepository<NewsItem>> newsItemRepo;
        private Mock<INewsData> newsData;
        private Mock<IMapperProvider> mapperProvider;
        private Mock<IRepository<Image>> imageRepo;
        private Mock<IDateTimeProvider> dateTimeProvider;
        private Mock<IMapper> mockMapper;

        [SetUp]
        public void Init()
        {
            this.userRepo = new Mock<IRepository<User>>();
            this.newsItemRepo = new Mock<IRepository<NewsItem>>();
            this.newsData = new Mock<INewsData>();
            this.mapperProvider = new Mock<IMapperProvider>();
            this.imageRepo = new Mock<IRepository<Image>>();
            this.dateTimeProvider = new Mock<IDateTimeProvider>();
            this.mockMapper = new Mock<IMapper>();
        }

        [Test]
        public void Constructor_IfUserRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleManagementService(
                    null,
                    this.newsItemRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.imageRepo.Object,
                    this.dateTimeProvider.Object)
                );
            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsItemRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleManagementService(
                    this.userRepo.Object,
                    null,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    this.imageRepo.Object,
                    this.dateTimeProvider.Object));
            Assert.AreEqual("newsRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_IfNewsDataIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleManagementService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    null,
                    this.mapperProvider.Object,
                    this.imageRepo.Object,
                    this.dateTimeProvider.Object));
            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Constructor_IfMapperProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleManagementService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.newsData.Object,
                    null,
                    this.imageRepo.Object,
                    this.dateTimeProvider.Object));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_IfImageRepositoryIsNullArgumentNullExceptionShouldBeThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleManagementService(
                    this.userRepo.Object,
                    this.newsItemRepo.Object,
                    this.newsData.Object,
                    this.mapperProvider.Object,
                    null,
                    this.dateTimeProvider.Object));
            Assert.AreEqual("imageRepository", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Add_IfNullOrEmptyUsernameIsPassedArgumentNullExceptionShouldBeThrown(string username)
        {
            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => articleManagementService.Add(username, new NewsWebModel()));
            Assert.AreEqual("username", exception.ParamName);
        }

        [Test]
        public void Add_IfNullOrEmptyNewsItemIsPassedArgumentNullExceptionShouldBeThrown()
        {
            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => articleManagementService.Add("username", null));
            Assert.AreEqual("newsItem", exception.ParamName);
        }

        [Test]
        public void Add_ShouldCallImageRepositoryAddOnce()
        {
            NewsWebModel testWebNewsModel = new NewsWebModel();
            string testUsername = "junka";
            User testUser = new User { UserName = testUsername, Id = "1" };

            this.userRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            ArticleManagementService articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.imageRepo.Verify(x => x.Add(It.IsAny<Image>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallNewsRepositoryAddOnce()
        {
            NewsWebModel testWebNewsModel = new NewsWebModel();
            string testUsername = "junka";
            User testUser = new User { UserName = testUsername, Id = "1" };

            this.userRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            ArticleManagementService articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.newsItemRepo.Verify(x => x.Add(It.IsAny<NewsItem>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallUnitOfWorkCommitOnce()
        {
            NewsWebModel testWebNewsModel = new NewsWebModel();
            string testUsername = "junka";
            User testUser = new User { UserName = testUsername, Id = "1" };

            this.userRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);
            this.mapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            this.mapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());

            ArticleManagementService articleManagementService = this.GetArticleManagementService();

            articleManagementService.Add(testUsername, testWebNewsModel);
            this.newsData.Verify(x => x.Commit(), Times.Once);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Restore_ShouldThrowArgumentOutOfRangeExceptionWhenIdIsNotPositive(int id)
        {
            ArticleManagementService service = this.GetArticleManagementService();
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.Restore(id));

            Assert.AreEqual("id", exception.ParamName);
        }

        [Test]
        public void Restore_NewsRepositoryGetByIdShouldBeCalledWithParsedNewsItemId()
        {
            NewsItem newsItem = new NewsItem();
            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            int newsItemId = 1;

            articleManagementService.Restore(newsItemId);
            this.newsItemRepo.Verify(x => x.GetById(It.Is<int>(a => a == newsItemId)), Times.Once);
        }

        [Test]
        public void Restore_NewsRepositoryUpdateShouldBeCalledWithTheFoundItem()
        {
            NewsItem newsItem = new NewsItem();
            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            int newsItemId = 1;

            articleManagementService.Restore(newsItemId);
            this.newsItemRepo.Verify(x => x.Update(It.Is<NewsItem>(a => a == newsItem)), Times.Once);
        }

        [Test]
        public void Restore_NewsDataCommitShouldBeCalled()
        {
            NewsItem newsItem = new NewsItem();
            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            int newsItemId = 1;

            articleManagementService.Restore(newsItemId);
            this.newsData.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void Update_NewsRepositoryGetByIdShouldBeCalledWithTheModelId()
        {
            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new NewsItem());

            int id = 3;
            NewsWebModel newsItem = new NewsWebModel { Id = id };
            ArticleManagementService articleManagementService = this.GetArticleManagementService();

            articleManagementService.Update(newsItem);
            this.newsItemRepo.Verify(x => x.GetById(It.Is<int>(a => a == id)), Times.Once);
        }

        [Test]
        public void Update_TheEntityShouldBeUpdatedWithoutTheImageWhenTheImageIsNull()
        {
            NewsItem entityToUpdate = new NewsItem();
            NewsWebModel newsItem = new NewsWebModel
            {
                Id = 3,
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content"
            };

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(entityToUpdate);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            articleManagementService.Update(newsItem);

            Assert.AreEqual(newsItem.Title, entityToUpdate.Title);
            Assert.AreEqual(newsItem.Category, entityToUpdate.Category);
            Assert.AreEqual(newsItem.Content, entityToUpdate.Content);
        }

        [Test]
        public void Update_WhenTheImageIsNotNullImageRepositoryAddShouldBeCalled()
        {
            NewsItem entityToUpdate = new NewsItem();
            Image image = new Image { FullName = "Full Name", FileExtention = ".png", Name = "Name" };
            NewsWebModel newsItem = new NewsWebModel
            {
                Id = 3,
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = new ImageWebModel { }
            };

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(entityToUpdate);
            this.mockMapper.Setup(x => x.Map<Image>(It.IsAny<ImageWebModel>())).Returns(image);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            articleManagementService.Update(newsItem);

            this.imageRepo.Verify(x => x.Add(It.Is<Image>(a => a == image)), Times.Once);
        }

        [Test]
        public void Update_WhenTheImageIsNotNullTheNewsItemImageShouldBeUpdatedToo()
        {
            NewsItem entityToUpdate = new NewsItem();
            Image image = new Image { FullName = "Full Name", FileExtention = ".png", Name = "Name" };
            NewsWebModel newsItem = new NewsWebModel
            {
                Id = 3,
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = new ImageWebModel { }
            };

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(entityToUpdate);
            this.mockMapper.Setup(x => x.Map<Image>(It.IsAny<ImageWebModel>())).Returns(image);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            articleManagementService.Update(newsItem);

            Assert.AreEqual(image.FileExtention, entityToUpdate.Image.FileExtention);
            Assert.AreEqual(image.Name, entityToUpdate.Image.Name);
            Assert.AreEqual(image.FullName, entityToUpdate.Image.FullName);
        }

        [Test]
        public void Update_NewsRepositoryUpdateShouldBeCalledWhenEverythingIsOk()
        {
            NewsItem entityToUpdate = new NewsItem();
            Image image = new Image { FullName = "Full Name", FileExtention = ".png", Name = "Name" };
            NewsWebModel newsItem = new NewsWebModel
            {
                Id = 3,
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = new ImageWebModel { }
            };

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(entityToUpdate);
            this.mockMapper.Setup(x => x.Map<Image>(It.IsAny<ImageWebModel>())).Returns(image);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            articleManagementService.Update(newsItem);

            this.newsItemRepo.Verify(x => x.Update(It.Is<NewsItem>(a => a == entityToUpdate)), Times.Once);
        }

        [Test]
        public void Update_NewsDataCommitShouldBeCalledWhenEverythingIsOk()
        {
            NewsItem entityToUpdate = new NewsItem();
            Image image = new Image { FullName = "Full Name", FileExtention = ".png", Name = "Name" };
            NewsWebModel newsItem = new NewsWebModel
            {
                Id = 3,
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = new ImageWebModel { }
            };

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(entityToUpdate);
            this.mockMapper.Setup(x => x.Map<Image>(It.IsAny<ImageWebModel>())).Returns(image);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);

            ArticleManagementService articleManagementService = this.GetArticleManagementService();
            articleManagementService.Update(newsItem);

            this.newsData.Verify(x => x.Commit(), Times.Once);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Delete_ShouldThrowArgumentOutOfRangeExceptionWhenIdIsNotPositive(int id)
        {
            ArticleManagementService service = this.GetArticleManagementService();
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.Delete(id));

            Assert.AreEqual("id", exception.ParamName);
        }

        [Test]
        public void Delete_NewsRepoGetByIdShouldBeCalledWithTheRightId()
        {
            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new NewsItem());

            ArticleManagementService service = this.GetArticleManagementService();
            int id = 1;

            service.Delete(id);
            this.newsItemRepo.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void Delete_FoundItemDeletedOnShouldBeSetToNow()
        {
            DateTime now = DateTime.Now;
            NewsItem newsItem = new NewsItem();

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);
            this.dateTimeProvider.SetupGet(x => x.Now).Returns(now);

            ArticleManagementService service = this.GetArticleManagementService();
            int id = 1;

            service.Delete(id);
            Assert.AreEqual(now, newsItem.DeletedOn);
        }

        [Test]
        public void Delete_NewsRepoUpdateShouldBeCalledWithTheRightNewsItem()
        {
            DateTime now = DateTime.Now;
            NewsItem newsItem = new NewsItem();

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);
            this.dateTimeProvider.SetupGet(x => x.Now).Returns(now);

            ArticleManagementService service = this.GetArticleManagementService();
            int id = 1;

            service.Delete(id);
            this.newsItemRepo.Verify(x => x.Update(newsItem), Times.Once);
        }


        [Test]
        public void Delete_NewsDataCommitShouldBeCalled()
        {
            DateTime now = DateTime.Now;
            NewsItem newsItem = new NewsItem();

            this.newsItemRepo.Setup(x => x.GetById(It.IsAny<int>())).Returns(newsItem);
            this.dateTimeProvider.SetupGet(x => x.Now).Returns(now);

            ArticleManagementService service = this.GetArticleManagementService();
            int id = 1;

            service.Delete(id);
            this.newsData.Verify(x => x.Commit(), Times.Once);
        }

        private ArticleManagementService GetArticleManagementService()
        {
            return new ArticleManagementService(
                this.userRepo.Object,
                this.newsItemRepo.Object,
                this.newsData.Object,
                this.mapperProvider.Object,
                this.imageRepo.Object,
                this.dateTimeProvider.Object);
        }
    }
}