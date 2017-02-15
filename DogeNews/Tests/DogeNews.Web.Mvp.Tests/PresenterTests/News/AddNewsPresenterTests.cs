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
        private Mock<IFileService> mockedFileService;
        private Mock<IArticleManagementService> mockedArticleManagementService;
        private Mock<IHttpContextService> mockedHttpContextService;
        private Mock<IHttpPostedFileService> mockedHttpPostedFileService;
        private Mock<IAddNewsView> mockedView;
        private Mock<IHttpServerUtilityService> mockedHttpServerService;

        [SetUp]
        public void Init()
        {
            this.mockedFileService = new Mock<IFileService>();
            this.mockedArticleManagementService = new Mock<IArticleManagementService>();
            this.mockedHttpContextService = new Mock<IHttpContextService>();
            this.mockedHttpPostedFileService = new Mock<IHttpPostedFileService>();
            this.mockedHttpServerService = new Mock<IHttpServerUtilityService>();
            this.mockedView = new Mock<IAddNewsView>();
        }

        [Test]
        public void Constructor_ShouldSetFileServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpContextServiceField = (IHttpContextService)typeof(AddNewsPresenter)
                .GetField("httpContextService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetArticleManagementServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var newsServiceField = (IArticleManagementService)typeof(AddNewsPresenter)
                .GetField("articleManagementService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedArticleManagementService.Object, newsServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpContextServiceField = (IHttpContextService)typeof(AddNewsPresenter)
                .GetField("httpContextService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpPostedFileServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpPostedFileServiceField = (IHttpPostedFileService)typeof(AddNewsPresenter)
                .GetField("httpPostedFileService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpPostedFileService.Object, httpPostedFileServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpServerServiceField()
        {
            var presenter = this.GetNewsPresenter();
            var httpServerServiceField = (IHttpServerUtilityService)typeof(AddNewsPresenter)
                .GetField("httpServerService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpServerService.Object, httpServerServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.mockedView.Object,
                    null,
                    this.mockedArticleManagementService.Object,
                    this.mockedHttpContextService.Object,
                    this.mockedHttpPostedFileService.Object,
                    this.mockedHttpServerService.Object));

            Assert.AreEqual("fileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.mockedView.Object,
                    this.mockedFileService.Object,
                    null,
                    this.mockedHttpContextService.Object,
                    this.mockedHttpPostedFileService.Object,
                    this.mockedHttpServerService.Object));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.mockedView.Object,
                    this.mockedFileService.Object,
                    this.mockedArticleManagementService.Object,
                    null,
                    this.mockedHttpPostedFileService.Object,
                    this.mockedHttpServerService.Object));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpPostedFileServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.mockedView.Object,
                    this.mockedFileService.Object,
                    this.mockedArticleManagementService.Object,
                    this.mockedHttpContextService.Object,
                    null,
                    this.mockedHttpServerService.Object));

            Assert.AreEqual("httpPostedFileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpServerServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new AddNewsPresenter(
                    this.mockedView.Object,
                    this.mockedFileService.Object,
                    this.mockedArticleManagementService.Object,
                    this.mockedHttpContextService.Object,
                    this.mockedHttpPostedFileService.Object,
                    null));

            Assert.AreEqual("httpServerService", exception.ParamName);
        }

        [Test]
        public void AddNews_ShouldCallHttpContextServiceGetUsername()
        {
            var presenter = this.GetNewsPresenter();

            presenter.AddNews(null, new AddNewsEventArgs());
            this.mockedHttpContextService
                .Verify(x => x.GetUsername(It.IsAny<HttpContextBase>()), Times.Once);
        }

        [Test]
        public void AddNews_FileServiceGetUniqueFileNameShouldBeCalledWithUsername()
        {
            string username = "username";

            this.mockedHttpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            var presenter = this.GetNewsPresenter();

            presenter.AddNews(null, new AddNewsEventArgs());
            this.mockedFileService
                .Verify(x => x.GetUniqueFileName(It.Is<string>(a => a == username)), Times.Once);
        }

        [Test]
        public void AddNews_HttpServerServiceMapPathShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.mockedHttpServerService.Verify(x => x.MapPath(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_FileServiceCreateFileShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.mockedFileService
                .Verify(x => x.CreateFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_HttpPostedFileServiceSaveAsShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.mockedHttpPostedFileService
                .Verify(x => x.SaveAs(It.IsAny<HttpPostedFile>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddNews_NewsServiceAddShouldBeCalled()
        {
            var presenter = this.GetNewsPresenter();
            presenter.AddNews(null, new AddNewsEventArgs());

            this.mockedArticleManagementService.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<NewsWebModel>()), Times.Once);
        }

        public AddNewsPresenter GetNewsPresenter()
        {
            var presenter = new AddNewsPresenter(
                this.mockedView.Object,
                this.mockedFileService.Object,
                this.mockedArticleManagementService.Object,
                this.mockedHttpContextService.Object,
                this.mockedHttpPostedFileService.Object,
                this.mockedHttpServerService.Object);

            return presenter;
        }
    }
}
