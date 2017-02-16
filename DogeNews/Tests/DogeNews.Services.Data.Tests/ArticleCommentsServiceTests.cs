using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

using NUnit.Framework;
using Moq;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Models;

using AutoMapper;

namespace DogeNews.Services.Data.Tests
{
    [TestFixture]
    public class ArticleCommentsServiceTests
    {
        private Mock<IProjectableRepository<Comment>> commentsRepo;
        private Mock<IProjectableRepository<NewsItem>> newsItemRepo;
        private Mock<IProjectableRepository<User>> userRepo;
        private Mock<IMapperProvider> mapperProvider;
        private Mock<INewsData> newsData;
        private Mock<IMapper> mapper;

        [SetUp]
        public void Init()
        {
            this.commentsRepo = new Mock<IProjectableRepository<Comment>>();
            this.newsItemRepo = new Mock<IProjectableRepository<NewsItem>>();
            this.userRepo = new Mock<IProjectableRepository<User>>();
            this.mapperProvider = new Mock<IMapperProvider>();
            this.newsData = new Mock<INewsData>();
            this.mapper = new Mock<IMapper>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfCommentsRepoIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    null,
                    this.newsItemRepo.Object,
                    this.userRepo.Object,
                    this.mapperProvider.Object,
                    this.newsData.Object));

            Assert.AreEqual("commentsRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfNewsItemRepoIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.commentsRepo.Object,
                    null,
                    this.userRepo.Object,
                    this.mapperProvider.Object,
                    this.newsData.Object));

            Assert.AreEqual("newsItemRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfUserRepoIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.commentsRepo.Object,
                    this.newsItemRepo.Object,
                    null,
                    this.mapperProvider.Object,
                    this.newsData.Object));

            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfMapperProviderIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.commentsRepo.Object,
                    this.newsItemRepo.Object,
                    this.userRepo.Object,
                    null,
                    this.newsData.Object));

            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfNewsDataIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.commentsRepo.Object,
                    this.newsItemRepo.Object,
                    this.userRepo.Object,
                    this.mapperProvider.Object,
                    null));

            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Constructor_CommentsRepositoryShouldBeSet()
        {
            ArticleCommentsService service = this.GetCommentsService();
            object commentsRepoField = typeof(ArticleCommentsService)
                .GetField("commentsRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.commentsRepo.Object, commentsRepoField);
        }

        [Test]
        public void Constructor_NewsItemRepositoryShouldBeSet()
        {
            ArticleCommentsService service = this.GetCommentsService();
            object newsItemRepoField = typeof(ArticleCommentsService)
                .GetField("newsItemRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.newsItemRepo.Object, newsItemRepoField);
        }

        [Test]
        public void Constructor_UserRepositoryShouldBeSet()
        {
            ArticleCommentsService service = this.GetCommentsService();
            object userRepoField = typeof(ArticleCommentsService)
                .GetField("userRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.userRepo.Object, userRepoField);
        }

        [Test]
        public void Constructor_MapperProviderShouldBeSet()
        {
            ArticleCommentsService service = this.GetCommentsService();
            object mapperProviderField = typeof(ArticleCommentsService)
                .GetField("mapperProvider", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mapperProvider.Object, mapperProviderField);
        }

        [Test]
        public void Constructor_NewsDataShouldBeSet()
        {
            ArticleCommentsService service = this.GetCommentsService();
            object newsDataField = typeof(ArticleCommentsService)
                .GetField("newsData", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.newsData.Object, newsDataField);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetCommentsForArticleByTitle_ShouldThrowArgumentNullExceptionWhenTitleIsNullOrEmpty(string title)
        {
            ArticleCommentsService service = this.GetCommentsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.GetCommentsForArticleByTitle(title));

            Assert.AreEqual("title", exception.ParamName);
        }

        [Test]
        public void GetCommentsForArticleByTitle_NewsRepoGetFirstMappedShouldBeCalled()
        {
            List<CommentWebModel> comments = new List<CommentWebModel>();

            this.mapper
                .Setup(x => x.Map<IEnumerable<CommentWebModel>>(It.IsAny<object>()))
                .Returns(comments);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mapper.Object);
            this.newsItemRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsWebModel { Comments = new List<CommentWebModel>() });

            ArticleCommentsService service = this.GetCommentsService();
            string title = "title";

            service.GetCommentsForArticleByTitle(title);
            this.newsItemRepo.Verify(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetCommentsForArticleByTitle_ShouldReturnMappedModels()
        {
            List<CommentWebModel> comments = new List<CommentWebModel>();

            this.mapper
                .Setup(x => x.Map<IEnumerable<CommentWebModel>>(It.IsAny<object>()))
                .Returns(comments);
            this.mapperProvider.SetupGet(x => x.Instance).Returns(this.mapper.Object);
            this.newsItemRepo
                .Setup(x => x.GetFirstMapped<NewsWebModel>(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsWebModel { Comments = comments });

            ArticleCommentsService service = this.GetCommentsService();
            string title = "title";

            IEnumerable<CommentWebModel> models = service.GetCommentsForArticleByTitle(title);
            Assert.AreEqual(comments, models);
        }

        [TestCase("")]
        [TestCase(null)]
        public void AddComemnt_ShouldThrowArgumentNullExceptionWhenNewsItemTitleIsNullOrEmpty(string title)
        {
            ArticleCommentsService service = this.GetCommentsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.AddComment(title, "content", "username"));

            Assert.AreEqual("newsItemTitle", exception.ParamName);
        }

        [TestCase("")]
        [TestCase(null)]
        public void AddComemnt_ShouldThrowArgumentNullExceptionWhenCommentContentIsNullOrEmpty(string content)
        {
            ArticleCommentsService service = this.GetCommentsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.AddComment("title", content, "username"));

            Assert.AreEqual("commentContent", exception.ParamName);
        }

        [TestCase("")]
        [TestCase(null)]
        public void AddComemnt_ShouldThrowArgumentNullExceptionWhenUsernameIsNullOrEmpty(string username)
        {
            ArticleCommentsService service = this.GetCommentsService();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.AddComment("title", "content", username));

            Assert.AreEqual("userName", exception.ParamName);
        }

        [Test]
        public void AddComment_UserRepositoryGetFirstShouldBeCalled()
        {
            this.userRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.newsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            ArticleCommentsService service = this.GetCommentsService();
            service.AddComment("title", "content", "username");

            this.userRepo.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void AddComment_NewsItemRepoGetFirstShouldBeCalled()
        {
            this.userRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.newsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            ArticleCommentsService service = this.GetCommentsService();
            service.AddComment("title", "content", "username");

            this.newsItemRepo.Verify(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void AddComment_CommentShouldBeAddedToNewsItemComments()
        {
            User user = new User();
            NewsItem newsItem = new NewsItem();

            this.userRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(user);
            this.newsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            ArticleCommentsService service = this.GetCommentsService();
            string title = "title";
            string content = "content";
            string username = "username";

            service.AddComment(title, content, username);

            Comment addedComment = newsItem.Comments.First();

            Assert.AreEqual(content, addedComment.Content);
            Assert.AreEqual(user, addedComment.User);
        }

        [Test]
        public void AddComment_NewsDataCommitShouldBeCalled()
        {
            this.userRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.newsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            ArticleCommentsService service = this.GetCommentsService();
            service.AddComment("title", "content", "username");

            this.newsData.Verify(x => x.Commit(), Times.Once);
        }

        private ArticleCommentsService GetCommentsService()
        {
            return new ArticleCommentsService(
                this.commentsRepo.Object,
                this.newsItemRepo.Object,
                this.userRepo.Object,
                this.mapperProvider.Object,
                this.newsData.Object);
        }
    }
}
