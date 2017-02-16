using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Web.UI;
using System.Reflection;

using Moq;
using NUnit.Framework;

using DogeNews.Web.Mvp.UserControls.NewsGrid;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;
using DogeNews.Web.Models;
using DogeNews.Data.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Common.Enums;
using DogeNews.Services.Data.Contracts;
using DogeNews.Services.Http.Contracts;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.UserControls
{
    [TestFixture]
    public class NewsGridPresenterTests
    {
        private Mock<INewsGridView> view;
        private Mock<INewsDataSource<NewsItem, NewsWebModel>> datSource;
        private Mock<IHttpUtilityService> httpUtilService;
        private Mock<IArticleManagementService> articleManagementService;

        [SetUp]
        public void Init()
        {
            this.view = new Mock<INewsGridView>();

            this.httpUtilService = new Mock<IHttpUtilityService>();
            this.httpUtilService.Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection());

            this.articleManagementService = new Mock<IArticleManagementService>();

            this.datSource = new Mock<INewsDataSource<NewsItem, NewsWebModel>>();
            this.datSource
                .Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.datSource
                .Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.datSource
                .Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.datSource
                .Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.datSource
                .Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.datSource
                .Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenNewsDataSourceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsGridPresenter(
                    this.view.Object,
                    null,
                    this.httpUtilService.Object,
                    this.articleManagementService.Object));

            Assert.AreEqual("newsDataSource", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenHttpUtilityIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsGridPresenter(
                    this.view.Object,
                    this.datSource.Object,
                    null,
                    this.articleManagementService.Object));

            Assert.AreEqual("httpUtilityService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenArticleManagementServiceIsNUll()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsGridPresenter(
                    this.view.Object,
                    this.datSource.Object,
                    this.httpUtilService.Object,
                    null));

            Assert.AreEqual("articleManagementService", exception.ParamName);
        }

        [Test]
        public void PageLoad_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object, 
                this.httpUtilService.Object, 
                this.articleManagementService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void PageLoad_WhenItIsNotPostbackViewStateCurrentPageShouldBeSetTo1()
        {
            PageLoadEventArgs eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });
            NewsGridPresenter presenter = new NewsGridPresenter(this.view.Object,
                this.datSource.Object, this.httpUtilService.Object, this.articleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(1, (int)eventArgs.ViewState["CurrentPage"]);
        }

        [Test]
        public void PageLoad_NewsDataSourceGetPageItemsShouldBeCalledWithOneAndPageSize()
        {
            PageLoadEventArgs eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });
            NewsGridPresenter presenter = new NewsGridPresenter(this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            this.datSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == 1), It.Is<int>(a => a == 6), It.IsAny<bool>(), It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void PageLoad_ViewModelNewsCountShouldBeSetToViewModelNewsDataSourceCount()
        {
            PageLoadEventArgs eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };
            int count = 6;

            this.datSource.SetupGet(x => x.Count).Returns(count);
            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(count, presenter.View.Model.NewsCount);
        }

        [Test]
        public void PageLoad_PageSizeShouldBeSetToTheConstantPageSize()
        {
            PageLoadEventArgs eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };
            int count = 6;

            this.datSource.SetupGet(x => x.Count).Returns(count);
            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);


            int pageSizeConstant = (int)typeof(NewsGridPresenter)
                .GetField("PageSize", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(presenter);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(pageSizeConstant, 6);
        }

        [Test]
        public void PageLoad_ShouldCallHttpUtilityServiceParseQueryString_WhenEventArgsQueryStringIsNotNull()
        {
            OrderByEventArgs eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Descending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.view.SetupGet(x => x.Model).Returns(new NewsGridViewModel { });

            string queryString = "name=Sports";

            PageLoadEventArgs pageLoadEventArgs = new PageLoadEventArgs
            {
                QueryString = queryString,
                IsPostBack = false,
                ViewState = new StateBag()
            };

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.PageLoad(null, pageLoadEventArgs);

            httpUtilService.Verify(x => x.ParseQueryString(queryString), Times.Once);
        }

        [Test]
        public void ChangePage_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.ChangePage(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void ChangePage_ShouldSetViewStateCurrentPageToThePassedPageFromTheEventArgs()
        {
            ChangePageEventArgs eventArgs = new ChangePageEventArgs { Page = 3, ViewState = new StateBag() };

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.ChangePage(null, eventArgs);
            Assert.AreEqual(3, (int)eventArgs.ViewState["CurrentPage"]);
        }

        [Test]
        public void ChangePage_ShouldViewModelNewsDataSourceGetPageItemsWithPageAndPageSize()
        {
            int page = 3;
            ChangePageEventArgs eventArgs = new ChangePageEventArgs { Page = page, ViewState = new StateBag() };

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            int pageSizeConstant = (int)typeof(NewsGridPresenter)
               .GetField("PageSize", BindingFlags.NonPublic | BindingFlags.Static)
               .GetValue(presenter);

            presenter.ChangePage(null, eventArgs);
            this.datSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == page), It.Is<int>(a => a == pageSizeConstant), It.IsAny<bool>(), null),
                Times.Once);
        }

        [Test]
        public void OrderByDate_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.OrderByDate(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }

        [Test]
        public void OrderByDate_WhenOrderTypeIsAscendingNewsDataSourceOrderByAscendingShouldBeCalled()
        {
            OrderByEventArgs eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Ascending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel());

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.OrderByDate(null, eventArgs);
            this.datSource.Verify(x =>
                x.OrderByAscending(
                        It.IsAny<Expression<Func<NewsItem, DateTime?>>>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<bool>(),
                        It.IsAny<string>()),
                    Times.Once);
        }

        [Test]
        public void OrderByDate_WhenOrderTypeIsDescendingNewsDataSourceOrderByDescendingShouldBeCalled()
        {
            OrderByEventArgs eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Descending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.view
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            NewsGridPresenter presenter = new NewsGridPresenter(
                this.view.Object,
                this.datSource.Object,
                this.httpUtilService.Object,
                this.articleManagementService.Object);

            presenter.OrderByDate(null, eventArgs);
            this.datSource.Verify(x =>
                x.OrderByDescending(
                    It.IsAny<Expression<Func<NewsItem, DateTime?>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
