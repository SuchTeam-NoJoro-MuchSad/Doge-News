using System;
using System.Reflection;
using System.Collections.Specialized;
using System.Web;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit;
using DogeNews.Web.Mvp.News.Edit.EventArguments;
using DogeNews.Common.Enums;
using DogeNews.Web.Models;
using DogeNews.Services.Http.Contracts;
using DogeNews.Services.Common.Contracts;
using DogeNews.Services.Data.Contracts;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class EditArticlePresenterTests
    {
        private Mock<IArticleManagementService> articleManagementService;
        private Mock<IEditArticleView> view;
        private Mock<INewsService> newsService;
        private Mock<IHttpUtilityService> httpUtilService;
        private Mock<IHttpContextService> httpContextService;
        private Mock<IFileService> fileService;
        private Mock<IHttpServerUtilityService> serverUtilService;
        private Mock<IHttpPostedFileService> httpPostedFileService;

        [SetUp]
        public void Init()
        {
            this.articleManagementService = new Mock<IArticleManagementService>();
            this.view = new Mock<IEditArticleView>();
            this.newsService = new Mock<INewsService>();
            this.httpUtilService = new Mock<IHttpUtilityService>();
            this.httpContextService = new Mock<IHttpContextService>();
            this.fileService = new Mock<IFileService>();
            this.serverUtilService = new Mock<IHttpServerUtilityService>();
            this.httpPostedFileService = new Mock<IHttpPostedFileService>();
        }

        [Test]
        public void Constructor_ShouldSetArticleManagementService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IArticleManagementService articleManagementServiceField = (IArticleManagementService)typeof(EditArticlePresenter)
                .GetField("articleManagementService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.articleManagementService.Object, articleManagementServiceField);
        }

        [Test]
        public void Constructor_ShouldSetNewsService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            INewsService newsServiceField = (INewsService)typeof(EditArticlePresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.newsService.Object, newsServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpUtilityService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IHttpUtilityService httpUtilServiceField = (IHttpUtilityService)typeof(EditArticlePresenter)
                .GetField("httpUtilityService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.httpUtilService.Object, httpUtilServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IHttpContextService httpContextServiceField = (IHttpContextService)typeof(EditArticlePresenter)
                .GetField("httpContextService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.httpContextService.Object, httpContextServiceField);
        }

        [Test]
        public void Constructor_ShouldSetFileService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IFileService fileServiceField = (IFileService)typeof(EditArticlePresenter)
                .GetField("fileService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.fileService.Object, fileServiceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpServerService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IHttpServerUtilityService serverServiceField = (IHttpServerUtilityService)typeof(EditArticlePresenter)
                .GetField("httpServerService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.serverUtilService.Object, serverServiceField);
        }

        [Test]
        public void Constructor_ShouldSetPostedFileService()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            IHttpPostedFileService postedFileServiceField = (IHttpPostedFileService)typeof(EditArticlePresenter)
                .GetField("httpPostedFileService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.httpPostedFileService.Object, postedFileServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenArticleManagementServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                null,
                this.newsService.Object,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                this.fileService.Object,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                null,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                this.fileService.Object,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpUtilServiceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                null,
                this.httpContextService.Object,
                this.fileService.Object,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object));

            Assert.AreEqual("httpUtilityService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                this.httpUtilService.Object,
                null,
                this.fileService.Object,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenFileServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                null,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object));

            Assert.AreEqual("fileService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenServerServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                this.fileService.Object,
                null,
                this.httpPostedFileService.Object));

            Assert.AreEqual("httpServerService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpPostedFileServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                this.fileService.Object,
                this.serverUtilService.Object,
                null));

            Assert.AreEqual("httpPostedFileService", exception.ParamName);
        }

        [Test]
        public void PagePreInit_HttpUtilityServiceParseQueryShouldBeCalled()
        {
            PreInitPageEventArgs eventArgs = new PreInitPageEventArgs { QueryString = "?id=3" };

            this.httpUtilService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "id", "3" } });
            this.view.SetupGet(x => x.Model).Returns(new EditArticleViewModel());

            EditArticlePresenter presenter = this.GetPresenter();
            presenter.PagePreInt(null, eventArgs);

            this.httpUtilService.Verify(x => x.ParseQueryString(It.Is<string>(a => a == eventArgs.QueryString)), Times.Once);
        }

        [Test]
        public void PagePreInit_NewsServiceGetItemByIdShouldBeCalled()
        {
            PreInitPageEventArgs eventArgs = new PreInitPageEventArgs { QueryString = "?name=name" };
            string id = "3";

            this.httpUtilService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "id", id } });
            this.view.SetupGet(x => x.Model).Returns(new EditArticleViewModel());

            EditArticlePresenter presenter = this.GetPresenter();
            presenter.PagePreInt(null, eventArgs);

            this.newsService.Verify(x => x.GetItemById(3), Times.Once);
        }

        [Test]
        public void PagePreInit_ArgumentNullExceptionShouldBeCalledWhenEventArgsIsNull()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.PagePreInt(null, null));

            Assert.AreEqual("preInitPageEventArgs", exception.ParamName);
        }

        [Test]
        public void EditArticle_HttpContextServiceGetUsernameShouldBeCalled()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            string fileName = "FileName.png";
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs { Image = image };

            presenter.EditArticle(null, eventArgs);
            this.httpContextService.Verify(x => x.GetUsername(It.IsAny<HttpContextBase>()), Times.Once);
        }

        [Test]
        public void EditArticle_ArticleManagementServiceUpdateShouldBeCalledWhenImageIsNullAndEverythingIsOk()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            string fileName = "FileName.png";
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.articleManagementService.Verify(
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
            EditArticlePresenter presenter = this.GetPresenter();
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.fileService.Verify(x => x.GetFileExtension(It.Is<string>(a => a == eventArgs.FileName)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedFileServiceGetUniqueFileNameShouldBeCalled()
        {
            string username = "username";
            this.httpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            EditArticlePresenter presenter = this.GetPresenter();
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.fileService.Verify(x => x.GetUniqueFileName(It.Is<string>(a => a == username)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedHttpServerServiceMapFileShouldBeCalled()
        {
            string username = "username";
            this.httpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            EditArticlePresenter presenter = this.GetPresenter();
            string basePath = "~\\Resources\\Images";
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.serverUtilService.Verify(x => x.MapPath(It.Is<string>(a => a == basePath)), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedFileServiceCreateFileShouldBeCalled()
        {
            string username = "username";
            this.httpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            EditArticlePresenter presenter = this.GetPresenter();
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.fileService.Verify(x => x.CreateFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void EditArticle_WhenImageIsChangedHttpPostedFileServiceSaveShouldBeCalled()
        {
            string username = "username";
            this.httpContextService
                .Setup(x => x.GetUsername(It.IsAny<HttpContextBase>()))
                .Returns(username);

            string fileName = "FileName.png";
            EditArticlePresenter presenter = this.GetPresenter();
            HttpPostedFile image = HttpPostedFileCreator.ConstructHttpPostedFile(new byte[10], fileName, "png");
            EditArticleEventArgs eventArgs = new EditArticleEventArgs
            {
                Title = "Title",
                Category = NewsCategoryType.Breaking,
                Content = "Content",
                FileName = fileName,
                Image = image
            };

            presenter.EditArticle(null, eventArgs);
            this.httpPostedFileService.Verify(x => x.SaveAs(It.Is<HttpPostedFile>(a => a == eventArgs.Image), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void EditArticle_ArgumentNullExceptionShouldBeThrownWhenEventArgsIsNull()
        {
            EditArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.EditArticle(null, null));

            Assert.AreEqual("editArticleEventArgs", exception.ParamName);
        }

        private EditArticlePresenter GetPresenter()
        {
            return new EditArticlePresenter(
                this.view.Object,
                this.articleManagementService.Object,
                this.newsService.Object,
                this.httpUtilService.Object,
                this.httpContextService.Object,
                this.fileService.Object,
                this.serverUtilService.Object,
                this.httpPostedFileService.Object);
        }
    }
}
