using System;
using System.Reflection;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Models;

using AutoMapper;
using System.Linq.Expressions;
using System.Linq;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class ArticleCommentsServiceTests
    {
        private Mock<IRepository<Comment>> mockCommentsRepo;
        private Mock<IRepository<NewsItem>> mockNewsItemRepo;
        private Mock<IRepository<User>> mockUserRepo;
        private Mock<IMapperProvider> mockMapperProvider;
        private Mock<INewsData> mockNewsData;
        private Mock<IMapper> mockMapper;

        [SetUp]
        public void Init()
        {
            this.mockCommentsRepo = new Mock<IRepository<Comment>>();
            this.mockNewsItemRepo = new Mock<IRepository<NewsItem>>();
            this.mockUserRepo = new Mock<IRepository<User>>();
            this.mockMapperProvider = new Mock<IMapperProvider>();
            this.mockNewsData = new Mock<INewsData>();
            this.mockMapper = new Mock<IMapper>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfCommentsRepoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    null,
                    this.mockNewsItemRepo.Object,
                    this.mockUserRepo.Object,
                    this.mockMapperProvider.Object,
                    this.mockNewsData.Object));

            Assert.AreEqual("commentsRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfNewsItemRepoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.mockCommentsRepo.Object,
                    null,
                    this.mockUserRepo.Object,
                    this.mockMapperProvider.Object,
                    this.mockNewsData.Object));

            Assert.AreEqual("newsItemRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfUserRepoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.mockCommentsRepo.Object,
                    this.mockNewsItemRepo.Object,
                    null,
                    this.mockMapperProvider.Object,
                    this.mockNewsData.Object));

            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfMapperProviderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.mockCommentsRepo.Object,
                    this.mockNewsItemRepo.Object,
                    this.mockUserRepo.Object,
                    null,
                    this.mockNewsData.Object));

            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionIfNewsDataIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticleCommentsService(
                    this.mockCommentsRepo.Object,
                    this.mockNewsItemRepo.Object,
                    this.mockUserRepo.Object,
                    this.mockMapperProvider.Object,
                    null));

            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Constructor_CommentsRepositoryShouldBeSet()
        {
            var service = this.GetCommentsService();
            var commentsRepoField = typeof(ArticleCommentsService)
                .GetField("commentsRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mockCommentsRepo.Object, commentsRepoField);
        }

        [Test]
        public void Constructor_NewsItemRepositoryShouldBeSet()
        {
            var service = this.GetCommentsService();
            var newsItemRepoField = typeof(ArticleCommentsService)
                .GetField("newsItemRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mockNewsItemRepo.Object, newsItemRepoField);
        }

        [Test]
        public void Constructor_UserRepositoryShouldBeSet()
        {
            var service = this.GetCommentsService();
            var userRepoField = typeof(ArticleCommentsService)
                .GetField("userRepository", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mockUserRepo.Object, userRepoField);
        }

        [Test]
        public void Constructor_MapperProviderShouldBeSet()
        {
            var service = this.GetCommentsService();
            var mapperProviderField = typeof(ArticleCommentsService)
                .GetField("mapperProvider", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mockMapperProvider.Object, mapperProviderField);
        }

        [Test]
        public void Constructor_NewsDataShouldBeSet()
        {
            var service = this.GetCommentsService();
            var newsDataField = typeof(ArticleCommentsService)
                .GetField("newsData", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(service);

            Assert.AreEqual(this.mockNewsData.Object, newsDataField);
        }

        [Test]
        public void GetCommentsForArticleByTitle_NewsRepoGetFirstShouldBeCalled()
        {
            var comments = new List<CommentWebModel>();

            this.mockMapper
                .Setup(x => x.Map<IEnumerable<CommentWebModel>>(It.IsAny<object>()))
                .Returns(comments);
            this.mockMapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            var service = this.GetCommentsService();
            string title = "title";

            service.GetCommentsForArticleByTitle(title);
            this.mockNewsItemRepo.Verify(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void GetCommentsForArticleByTitle_ShouldReturnMappedModels()
        {
            var comments = new List<CommentWebModel>();

            this.mockMapper
                .Setup(x => x.Map<IEnumerable<CommentWebModel>>(It.IsAny<object>()))
                .Returns(comments);
            this.mockMapperProvider.SetupGet(x => x.Instance).Returns(this.mockMapper.Object);
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            var service = this.GetCommentsService();
            string title = "title";

            var models = service.GetCommentsForArticleByTitle(title);
            Assert.AreEqual(comments, models);
        }

        [Test]
        public void AddComment_UserRepositoryGetFirstShouldBeCalled()
        {
            this.mockUserRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            var service = this.GetCommentsService();
            service.AddComment(string.Empty, string.Empty, string.Empty);

            this.mockUserRepo.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void AddComment_NewsItemRepoGetFirstShouldBeCalled()
        {
            this.mockUserRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            var service = this.GetCommentsService();
            service.AddComment(string.Empty, string.Empty, string.Empty);

            this.mockNewsItemRepo.Verify(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()), Times.Once);
        }

        [Test]
        public void AddComment_CommentShouldBeAddedToNewsItemComments()
        {
            var user = new User();
            var newsItem = new NewsItem();

            this.mockUserRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(user);
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(newsItem);

            var service = this.GetCommentsService();
            string title = "title";
            string content = "content";
            string username = "username";

            service.AddComment(title, content, username);

            var addedComment = newsItem.Comments.First();

            Assert.AreEqual(content, addedComment.Content);
            Assert.AreEqual(user, addedComment.User);
        }

        [Test]
        public void AddComment_NewsDataCommitShouldBeCalled()
        {
            this.mockUserRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User());
            this.mockNewsItemRepo
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<NewsItem, bool>>>()))
                .Returns(new NewsItem());

            var service = this.GetCommentsService();
            service.AddComment(string.Empty, string.Empty, string.Empty);

            this.mockNewsData.Verify(x => x.Commit(), Times.Once);
        }

        private ArticleCommentsService GetCommentsService()
        {
            return new ArticleCommentsService(
                this.mockCommentsRepo.Object,
                this.mockNewsItemRepo.Object,
                this.mockUserRepo.Object,
                this.mockMapperProvider.Object,
                this.mockNewsData.Object);
        }
    }
}
