using System.Reflection;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;
using System;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class EditArticlePresenterTests
    {
        private Mock<IArticleManagementService> mockArticleManageService;
        private Mock<IEditArticleView> mockView;
        private Mock<INewsService> mockNewsService;
        private Mock<IHttpUtilityService> mockHttpUtilService;
        private Mock<IHttpContextService> mockHttpContextService;
        private Mock<IFileService> mockFileService;
        private Mock<IHttpServerUtilityService> mockServerUtilService;
        private Mock<IHttpPostedFileService> mockHttpPostedFileService;

        [SetUp]
        public void Init()
        {
            this.mockArticleManageService = new Mock<IArticleManagementService>();
            this.mockView = new Mock<IEditArticleView>();
            this.mockNewsService = new Mock<INewsService>();
            this.mockHttpUtilService = new Mock<IHttpUtilityService>();
            this.mockHttpContextService = new Mock<IHttpContextService>();
            this.mockFileService = new Mock<IFileService>();
            this.mockServerUtilService = new Mock<IHttpServerUtilityService>();
            this.mockHttpPostedFileService = new Mock<IHttpPostedFileService>();
        }

        [Test]
        public void Constructor_ShouldSetArticleManagementService()
        {
            var presenter = this.GetPresenter();
            var articleManagementServiceField = (IArticleManagementService)typeof(EditArticlePresenter)
                .GetField("articleManagementService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockArticleManageService.Object, articleManagementServiceField);
        }

        [Test]
        public void Constructor_ShouldSetNewsService()
        {
            var presenter = this.GetPresenter();
            var newsServiceField = (INewsService)typeof(EditArticlePresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockNewsService.Object, newsServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpUtilityService()
        {
            var presenter = this.GetPresenter();
            var httpUtilServiceField = (IHttpUtilityService)typeof(EditArticlePresenter)
                .GetField("httpUtilityService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockHttpUtilService.Object, httpUtilServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextService()
        {
            var presenter = this.GetPresenter();
            var httpContextServiceField = (IHttpContextService)typeof(EditArticlePresenter)
                .GetField("httpContextService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockHttpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetFileService()
        {
            var presenter = this.GetPresenter();
            var fileServiceField = (IFileService)typeof(EditArticlePresenter)
                .GetField("fileService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockFileService.Object, fileServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpServerService()
        {
            var presenter = this.GetPresenter();
            var serverServiceField = (IHttpServerUtilityService)typeof(EditArticlePresenter)
                .GetField("httpServerService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockServerUtilService.Object, serverServiceField);
        }

        [Test]
        public void Constructor_ShouldSetPostedFileService()
        {
            var presenter = this.GetPresenter();
            var postedFileServiceField = (IHttpPostedFileService)typeof(EditArticlePresenter)
                .GetField("httpPostedFileService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockHttpPostedFileService.Object, postedFileServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenArticleManagementServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                null,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                null,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpUtilServiceIsNUll()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                null,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("httpUtilityService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                null,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                null,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("fileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenServerServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                null,
                this.mockHttpPostedFileService.Object));

            Assert.AreEqual("httpServerService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpPostedFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                null));

            Assert.AreEqual("httpPostedFileService", exception.ParamName);
        }

        private EditArticlePresenter GetPresenter()
        {
            return new EditArticlePresenter(
                this.mockView.Object,
                this.mockArticleManageService.Object,
                this.mockNewsService.Object,
                this.mockHttpUtilService.Object,
                this.mockHttpContextService.Object,
                this.mockFileService.Object,
                this.mockServerUtilService.Object,
                this.mockHttpPostedFileService.Object);
        }
    }
}
