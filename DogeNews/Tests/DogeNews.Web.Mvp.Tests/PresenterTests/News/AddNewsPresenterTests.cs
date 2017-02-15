using System;
using System.Web;
using System.Reflection;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Mvp.News.Add;
using DogeNews.Web.Mvp.News.Add.EventArguments;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Models;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Http.Contracts;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class AddNewsPresenterTests
    {
        private Mock<IFileService> fileService;
        private Mock<IArticleManagementService> articleManagementService;
        private Mock<IHttpContextService> httpContextService;
        private Mock<IHttpPostedFileService> httpPostedFileService;
        private Mock<IAddNewsView> view;
        private Mock<IHttpServerUtilityService> httpServerService;

        [SetUp]
        public void Init()
        {
            this.fileService = new Mock<IFileService>();
            this.articleManagementService = new Mock<IArticleManagementService>();
            this.httpContextService = new Mock<IHttpContextService>();
            this.httpPostedFileService = new Mock<IHttpPostedFileService>();
            this.httpServerService = new Mock<IHttpServerUtilityService>();
            this.view = new Mock<IAddNewsView>();
        }

        [Test]
        public void Constructor_ShouldSetFileServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpContextServiceField = (IHttpContextService)typeof(AddNewsPresenter)
                .GetField("httpContextService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetArticleManagementServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var newsServiceField = (IArticleManagementService)typeof(AddNewsPresenter)
                .GetField("articleManagementService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.articleManagementService.Object, newsServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpContextServiceField = (IHttpContextService)typeof(AddNewsPresenter)
                .GetField("httpContextService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpPostedFileServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpPostedFileServiceField = (IHttpPostedFileService)typeof(AddNewsPresenter)
                .GetField("httpPostedFileService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpPostedFileService.Object, httpPostedFileServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpServerServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpServerServiceField = (IHttpServerUtilityService)typeof(AddNewsPresenter)
                .GetField("httpServerService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpServerService.Object, httpServerServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.view.Object,
                    null,
                    this.articleManagementService.Object,
                    this.httpContextService.Object,
                    this.httpPostedFileService.Object,
                    this.httpServerService.Object));

            Assert.AreEqual("fileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.view.Object,
                    this.fileService.Object,
                    null,
                    this.httpContextService.Object,
                    this.httpPostedFileService.Object,
                    this.httpServerService.Object));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.view.Object,
                    this.fileService.Object,
                    this.articleManagementService.Object,
                    null,
                    this.httpPostedFileService.Object,
                    this.httpServerService.Object));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpPostedFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.view.Object,
                    this.fileService.Object,
                    this.articleManagementService.Object,
                    this.httpContextService.Object,
                    null,
                    this.httpServerService.Object));

            Assert.AreEqual("httpPostedFileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpServerServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.view.Object,
                    this.fileService.Object,
                    this.articleManagementService.Object,
                    this.httpContextService.Object,
                    this.httpPostedFileService.Object,
                    null));

            Assert.AreEqual("httpServerService", exception.ParamName);
        }

        [Test]
        public void AddNews_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            var presenter = this.GetNewsPresenter();
            var exception = Assert.Throws<ArgumentNullException>(() => presenter.AddNews(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void AddNews_ShouldCallHttpContextServiceGetUsername()
        {
            var presenter = this.GetNewsPresenter();

            presenter.AddNews(null, new AddNewsEventArgs());
            this.httpContextService
                .Verify(x => x.GetUsername(It.IsAny<HttpContextBase>()), Times.Once);
        }

        [Test]
        public void AddNews_FileServiceGetUniqueFileNameShouldBeCalledWithUsername()
        {
            string username = "username";

            this.httpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            var presenter = this.GetNewsPresenter();

            presenter.AddNews(null, new AddNewsEventArgs());
            this.fileService
                .Verify(x => x.GetUniqueFileName(It.Is<string>(a => a == username)), Times.Once);
        }

        [Test]
        public void AddNews_HttpServerServiceMapPathShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.httpServerService.Verify(x => x.MapPath(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_FileServiceCreateFileShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.fileService
                .Verify(x => x.CreateFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_HttpPostedFileServiceSaveAsShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.httpPostedFileService
                .Verify(x => x.SaveAs(It.IsAny<HttpPostedFile>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_NewsServiceAddShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.articleManagementService.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<NewsWebModel>()), Times.Once);
        }

        public AddNewsPresenter GetNewsPresenter()
        {
            var presenter = new AddNewsPresenter(
                this.view.Object,
                this.fileService.Object,
                this.articleManagementService.Object,
                this.httpContextService.Object,
                this.httpPostedFileService.Object,
                this.httpServerService.Object);

            return presenter;
        }
    }
}
