using System;
using System.Reflection;
using System.Collections.Specialized;
using DogeNews.Services.Data.Contracts;
using Moq;
using NUnit.Framework;
using DogeNews.Web.Mvp.News.Article;
using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Services.Http.Contracts;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class ArticlePresenterTests
    {
        private Mock<IArticleView> view;
        private Mock<INewsService> newService;
        private Mock<IHttpUtilityService> httpUtilityService;
        private Mock<IHttpResponseService> httpResponseService;
        private Mock<IArticleManagementService> articleManagementService;

        [SetUp]
        public void Init()
        {
            this.view = new Mock<IArticleView>();
            this.view.Setup(x => x.Model).Returns(new ArticleViewModel());

            this.newService = new Mock<INewsService>();
            this.httpUtilityService = new Mock<IHttpUtilityService>();
            this.httpResponseService = new Mock<IHttpResponseService>();
            this.articleManagementService = new Mock<IArticleManagementService>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenNewsServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticlePresenter(
                    this.view.Object,
                    null,
                    this.httpUtilityService.Object,
                    this.httpResponseService.Object,
                    this.articleManagementService.Object));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenHttpUtilityServiceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticlePresenter(
                    this.view.Object,
                    this.newService.Object,
                    null,
                    this.httpResponseService.Object,
                    this.articleManagementService.Object));

            Assert.AreEqual("httpUtilityService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenHttpResponseServiceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticlePresenter(
                    this.view.Object,
                    this.newService.Object,
                    this.httpUtilityService.Object,
                    null,
                    this.articleManagementService.Object));

            Assert.AreEqual("httpResponseService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenArticleManagementServiceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new ArticlePresenter(
                    this.view.Object,
                    this.newService.Object,
                    this.httpUtilityService.Object,
                    this.httpResponseService.Object,
                    null));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheDataSourceService()
        {
            ArticlePresenter presenter = this.GetPresenter();
            INewsService field = (INewsService)typeof(ArticlePresenter)
                .GetField("newsService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.newService.Object, field);
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheHttpUtilityService()
        {
            ArticlePresenter presenter = this.GetPresenter();
            IHttpUtilityService field = (IHttpUtilityService)typeof(ArticlePresenter)
                .GetField("httpUtilityService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpUtilityService.Object, field);
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheHttpResponseService()
        {
            ArticlePresenter presenter = this.GetPresenter();
            IHttpResponseService field = (IHttpResponseService)typeof(ArticlePresenter)
                .GetField("httpResponseService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.httpResponseService.Object, field);
        }

        [Test]
        public void PageLoad_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void PageLoad_HttpUtilityServiceParseQueryStringShouldBeCalledWithTheEventArgsQueryString()
        {
            string queryString = "?title=someTitle";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.httpUtilityService.Verify(x =>
                x.ParseQueryString(It.Is<string>(a => a == queryString)),
                Times.Once);
        }

        [Test]
        public void PageLoad_WhenTheQueryStringDoesNotHaveTitleTheRequestShouldSend404CodeAndEnd()
        {
            string queryString = "?NOT_TITLE=someTitle";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "NOT_TITLE", "someTitle" } });

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.httpResponseService.Verify(x => x.Clear(), Times.Once);
            this.httpResponseService.Verify(x => x.SetStatusCode(It.Is<int>(a => a == 404)), Times.Once);
            this.httpResponseService.Verify(x => x.End(), Times.Once);
        }

        [Test]
        public void PageLoad_WhenThereIsTitleInTheQueryStringNewsServiceGetItemByTitleShouldBeCalledWithTheTitle()
        {
            string queryString = "?title=someTitle";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.newService.Verify(x => x.GetItemByTitle(It.Is<string>(a => a == "someTitle")), Times.Once);
        }

        [Test]
        public void PageLoad_WhenTheModelIsNullTheRequestShouldSend404CodeAndEnd()
        {
            string queryString = "?title=someTitle";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });
            this.newService
                .Setup(x => x.GetItemByTitle(It.IsAny<string>()))
                .Returns<NewsWebModel>(null);

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.httpResponseService.Verify(x => x.Clear(), Times.Once);
            this.httpResponseService.Verify(x => x.SetStatusCode(It.Is<int>(a => a == 404)), Times.Once);
            this.httpResponseService.Verify(x => x.End(), Times.Once);
        }

        [Test]
        public void PageLoad_WhenTheQueryStringIsEmptyTheRequestShouldSend404CodeAndEnd()
        {
            string queryString = "";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { });
            this.newService
                .Setup(x => x.GetItemByTitle(It.IsAny<string>()))
                .Returns<NewsWebModel>(null);

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.httpResponseService.Verify(x => x.Clear(), Times.Once);
            this.httpResponseService.Verify(x => x.SetStatusCode(It.Is<int>(a => a == 404)), Times.Once);
            this.httpResponseService.Verify(x => x.End(), Times.Once);
        }

        [Test]
        public void PageLoad_ViewNewsModelShouldBeSetToTheReturnedModelFromTheNewsService()
        {
            string queryString = "?title=someTitle";
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };
            NewsWebModel model = new NewsWebModel { Title = "Some Title" };

            this.view.SetupGet(x => x.Model).Returns(new ArticleViewModel());
            this.httpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });
            this.newService
                .Setup(x => x.GetItemByTitle(It.IsAny<string>()))
                .Returns(model);

            ArticlePresenter presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            Assert.AreEqual(model, presenter.View.Model.NewsModel);
        }

        [Test]
        public void ArticleRestore_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.ArticleRestore(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void ArticleRestore_ShouldCallArticleManagementServiceRestoreOnceWithCorrectParameters()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            int newsItemId = 1111;

            OnArticleRestoreEventArgs eventArgs = new OnArticleRestoreEventArgs
            {
                NewsItemId = newsItemId.ToString()
            };

            this.newService.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(new NewsWebModel());

            //Act
            presenter.ArticleRestore(null, eventArgs);

            //Assert
            articleManagementService.Verify(x => x.Restore(newsItemId), Times.Once);
        }

        [Test]
        public void ArticleRestore_ShouldCallNewsServiceGetItemByIdOnceWithCorrectParameters()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            string newsItemId = "1111";

            OnArticleRestoreEventArgs eventArgs = new OnArticleRestoreEventArgs
            {
                NewsItemId = newsItemId
            };

            this.newService.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(new NewsWebModel());

            //Act
            presenter.ArticleRestore(null, eventArgs);

            //Assert
            this.newService.Verify(x => x.GetItemById(1111), Times.Once);
        }

        [Test]
        public void ArticleEdit_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.ArticleEdit(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void ArticleEdit_ShouldCallHttpResponseRedirectServiceOnceWithCorrectParametersAndWhenAdminIsLoggedIn()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            string newsItemId = "1111";

            OnArticleEditEventArgs eventArgs = new OnArticleEditEventArgs
            {
                NewsItemId = newsItemId,
                IsAdminUser = true
            };

            //Act
            presenter.ArticleEdit(null, eventArgs);

            //Assert
            httpResponseService.Verify(x => x.Redirect($"~/News/Edit?id={eventArgs.NewsItemId}"), Times.Once);
        }

        [Test]
        public void ArticleEdit_ShouldNotCallHttpResponseRedirectServiceWithCorrectParametersAndWhenAdminIsNotLoggedIn()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            string newsItemId = "1111";

            OnArticleEditEventArgs eventArgs = new OnArticleEditEventArgs
            {
                NewsItemId = newsItemId,
                IsAdminUser = false
            };

            //Act
            presenter.ArticleEdit(null, eventArgs);

            //Assert
            httpResponseService.Verify(x => x.Redirect($"~/News/Edit?id={eventArgs.NewsItemId}"), Times.Never);
        }

        [Test]
        public void ArticleDelete_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            ArticlePresenter presenter = this.GetPresenter();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.ArticleDelete(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void ArticleDelete_ShouldCallArticleManagementServiceDeleteOnceWithCorrectParameters()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            int newsItemId = 1111;

            OnArticleDeleteEventArgs eventArgs = new OnArticleDeleteEventArgs
            {
                NewsItemId = newsItemId.ToString()
            };

            this.newService.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(new NewsWebModel());

            //Act
            presenter.ArticleDelete(null, eventArgs);

            //Assert
            articleManagementService.Verify(x => x.Delete(newsItemId), Times.Once);
        }

        [Test]
        public void ArticleDelete_ShouldCallNewsServiceGetItemByIdOnceWithCorrectParameters()
        {
            //Arange
            ArticlePresenter presenter = this.GetPresenter();

            string newsItemId = "1111";

            OnArticleDeleteEventArgs eventArgs = new OnArticleDeleteEventArgs
            {
                NewsItemId = newsItemId
            };

            this.newService.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(new NewsWebModel());

            //Act
            presenter.ArticleDelete(null, eventArgs);

            //Assert
            this.newService.Verify(x => x.GetItemById(1111), Times.Once);
        }


        private ArticlePresenter GetPresenter()
        {
            return new ArticlePresenter(
                this.view.Object,
                this.newService.Object,
                this.httpUtilityService.Object,
                this.httpResponseService.Object,
                this.articleManagementService.Object);
        }
    }
}
