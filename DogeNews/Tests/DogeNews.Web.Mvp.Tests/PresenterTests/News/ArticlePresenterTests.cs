using System.Reflection;
using System.Collections.Specialized;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;
using DogeNews.Web.Mvp.News.Article;
using DogeNews.Web.Mvp.News.Article.EventArguments;
using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class ArticlePresenterTests
    {
        private Mock<IArticleView> mockedView;
        private Mock<INewsService> mockedNewsService;
        private Mock<IHttpUtilityService> mockedHttpUtilityService;
        private Mock<IHttpResponseService> mockedHttpResponseService;

        [SetUp]
        public void Init()
        {
            this.mockedView = new Mock<IArticleView>();
            this.mockedNewsService = new Mock<INewsService>();
            this.mockedHttpUtilityService = new Mock<IHttpUtilityService>();
            this.mockedHttpResponseService = new Mock<IHttpResponseService>();
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheDataSourceService()
        {
            var presenter = this.GetPresenter();
            var field = (INewsService)typeof(ArticlePresenter)
                .GetField("newsService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedNewsService.Object, field);
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheHttpUtilityService()
        {
            var presenter = this.GetPresenter();
            var field = (IHttpUtilityService)typeof(ArticlePresenter)
                .GetField("httpUtilityService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpUtilityService.Object, field);
        }

        [Test]
        public void Constructor_ShouldSetProperlyTheHttpResponseService()
        {
            var presenter = this.GetPresenter();
            var field = (IHttpResponseService)typeof(ArticlePresenter)
                .GetField("httpResponseService", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedHttpResponseService.Object, field);
        }

        [Test]
        public void PageLoad_HttpUtilityServiceParseQueryStringShouldBeCalledWithTheEventArgsQueryString()
        {
            string queryString = "?title=someTitle";
            var eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.mockedHttpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });

            var presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.mockedHttpUtilityService.Verify(x =>
                x.ParseQueryString(It.Is<string>(a => a == queryString)),
                Times.Once);
        }

        [Test]
        public void PageLoad_WhenTheQueryStringDoesNotHaveTitleTheRequestShouldSend404CodeAndEnd()
        {
            string queryString = "?NOT_TITLE=someTitle";
            var eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.mockedHttpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "NOT_TITLE", "someTitle" } });

            var presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.mockedHttpResponseService.Verify(x => x.Clear(), Times.Once);
            this.mockedHttpResponseService.Verify(x => x.SetStatusCode(It.Is<int>(a => a == 404)), Times.Once);
            this.mockedHttpResponseService.Verify(x => x.End(), Times.Once);
        }

        [Test]
        public void PageLoad_WhenThereIsTitleInTheQueryStringNewsServiceGetItemByTitleShouldBeCalledWithTheTitle()
        {
            string queryString = "?title=someTitle";
            var eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.mockedHttpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });

            var presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.mockedNewsService.Verify(x => x.GetItemByTitle(It.Is<string>(a => a == "someTitle")), Times.Once);
        }

        [Test]
        public void PageLoad_WhenTheModelIsNullTheRequestShouldSend404CodeAndEnd()
        {
            string queryString = "?title=someTitle";
            var eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };

            this.mockedHttpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });
            this.mockedNewsService
                .Setup(x => x.GetItemByTitle(It.IsAny<string>()))
                .Returns<NewsWebModel>(null);

            var presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            this.mockedHttpResponseService.Verify(x => x.Clear(), Times.Once);
            this.mockedHttpResponseService.Verify(x => x.SetStatusCode(It.Is<int>(a => a == 404)), Times.Once);
            this.mockedHttpResponseService.Verify(x => x.End(), Times.Once);
        }

        [Test]
        public void PageLoad_ViewNewsModelShouldBeSetToTheReturnedModelFromTheNewsService()
        {
            string queryString = "?title=someTitle";
            var eventArgs = new ArticlePageLoadEventArgs { QueryString = queryString };
            var model = new NewsWebModel { Title = "Some Title" };

            this.mockedView.SetupGet(x => x.Model).Returns(new ArticleViewModel());
            this.mockedHttpUtilityService
                .Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection { { "title", "someTitle" } });
            this.mockedNewsService
                .Setup(x => x.GetItemByTitle(It.IsAny<string>()))
                .Returns(model);

            var presenter = this.GetPresenter();
            presenter.PageLoad(null, eventArgs);

            Assert.AreEqual(model, presenter.View.Model.NewsModel);
        }

        private ArticlePresenter GetPresenter()
        {
            return new ArticlePresenter(
                this.mockedView.Object,
                this.mockedNewsService.Object,
                this.mockedHttpUtilityService.Object,
                this.mockedHttpResponseService.Object);
        }
    }
}
