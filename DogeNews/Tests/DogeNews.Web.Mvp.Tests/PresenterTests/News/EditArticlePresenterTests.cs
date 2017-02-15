using System;
using System.Reflection;
using System.Collections.Specialized;
using System.Web;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Mvp.News.Edit.EventArguments;
using DogeNews.Common.Enums;
using DogeNews.Web.Models;
using DogeNews.Services.Http.Contracts;
using DogeNews.Services.Common.Contracts;

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

        [Test]
        public void PagePreInit_HttpUtilityServiceParseQueryShouldBeCalled()
        {
            var eventArgs = new PreInitPageEventArgs { QueryString = "?id=3" };

            this.mockHttpUtilService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "id", "3" } });
            this.mockView.SetupGet(x => x.Model).Returns(new EditArticleViewModel());

            var presenter = this.GetPresenter();
            presenter.PagePreInt(null, eventArgs);

            this.mockHttpUtilService.Verify(x => x.ParseQueryString(It.Is<string>(a => a == eventArgs.QueryString)), Times.Once);
        }

        [Test]
        public void PagePreInit_NewsServiceGetItemByIdShouldBeCalled()
        {
            var eventArgs = new PreInitPageEventArgs { QueryString = "?name=name" };
            string id = "3";

            this.mockHttpUtilService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "id", id } });
            this.mockView.SetupGet(x => x.Model).Returns(new EditArticleViewModel());

            var presenter = this.GetPresenter();
            presenter.PagePreInt(null, eventArgs);

            this.mockNewsService.Verify(x => x.GetItemById(3), Times.Once);
        }

        [Test]
        public void PagePreInit_ArgumentNullExceptionShouldBeCalledWhenEventArgsIsNull()
        {
            var presenter = this.GetPresenter();
            var exception = Assert.Throws<ArgumentNullException>(() => presenter.PagePreInt(null, null));

            Assert.AreEqual("preInitPageEventArgs", exception.ParamName);
        }

        [Test]
        public void EditArticle_HttpContextServiceGetUsernameShouldBeCalled()
        {
            var presenter = this.GetPresenter();
            string fileName = "FileName.png";
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs { Image = image };

            presenter.EditArticle(null, eventArgs);
            this.mockHttpContextService.Verify(x => x.GetUsername(It.IsAny<HttpContextBase>()), Times.Once);
        }

        [Test]
        public void EditArticle_ArticleManagementServiceUpdateShouldBeCalledWhenImageIsNullAndEverythingIsOk()
        {
            var presenter = this.GetPresenter();
            string fileName = "FileName.png";
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockArticleManageService.Verify(
                x => x.Update(It.Is<NewsWebModel>(a =>
                    a.Title == eventArgs.Title &&
                    a.Category == eventArgs.Category &&
                    a.Content == eventArgs.Content)),
                Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedFileServiceGetFileExtensionShouldBeCalled()
        {
            string fileName = "FileName.png";
            var presenter = this.GetPresenter();
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockFileService.Verify(x => x.GetFileExtension(It.Is<string>(a => a == eventArgs.FileName)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedFileServiceGetUniqueFileNameShouldBeCalled()
        {
            string username = "username";
            this.mockHttpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            var presenter = this.GetPresenter();
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockFileService.Verify(x => x.GetUniqueFileName(It.Is<string>(a => a == username)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedHttpServerServiceMapFileShouldBeCalled()
        {
            string username = "username";
            this.mockHttpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            var presenter = this.GetPresenter();
            string basePath = "~\\Resources\\Images";
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockServerUtilService.Verify(x => x.MapPath(It.Is<string>(a => a == basePath)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedFileServiceCreateFileShouldBeCalled()
        {
            string username = "username";
            this.mockHttpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            var presenter = this.GetPresenter();
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockFileService.Verify(x => x.CreateFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedHttpPostedFileServiceSaveShouldBeCalled()
        {
            string username = "username";
            this.mockHttpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            var presenter = this.GetPresenter();
            var image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            var eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.mockHttpPostedFileService.Verify(x => x.SaveAs(It.Is<HttpPostedFile>(a => a == eventArgs.Image), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void EditArticle_ArgumentNullExceptionShouldBeThrownWhenEventArgsIsNull()
        {
            var presenter = this.GetPresenter();
            var exception = Assert.Throws<ArgumentNullException>(() => presenter.EditArticle(null, null));

            Assert.AreEqual("editArticleEventArgs", exception.ParamName);
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
