using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Ports;
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
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.UserControls
{
    [TestFixture]
    public class NewsGridPresenterTests
    {
        private Mock<INewsGridView> mockedView;
        private Mock<INewsDataSource<NewsItem, NewsWebModel>> mockedDataSource;
        private Mock<IHttpUtilityService> mockedHttpUtilitySevice;
        private Mock<IArticleManagementService> mockArticleManagementService;

        [SetUp]
        public void Init()
        {
            this.mockedView = new Mock<INewsGridView>();

            this.mockedHttpUtilitySevice = new Mock<IHttpUtilityService>();
            this.mockedHttpUtilitySevice.Setup(x => x.ParseQueryString(It.IsAny<string>()))
                .Returns(new NameValueCollection());

            this.mockArticleManagementService = new Mock<IArticleManagementService>();

            this.mockedDataSource = new Mock<INewsDataSource<NewsItem, NewsWebModel>>();
            this.mockedDataSource.Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
        }

        [Test]
        public void PageLoad_WhenItIsNotPostbackViewStateCurrentPageShouldBeSetTo1()
        {
            var eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });
            var presenter = new NewsGridPresenter(this.mockedView.Object,
                this.mockedDataSource.Object, this.mockedHttpUtilitySevice.Object, this.mockArticleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(1, (int)eventArgs.ViewState["CurrentPage"]);
        }

        [Test]
        public void PageLoad_NewsDataSourceGetPageItemsShouldBeCalledWithOneAndPageSize()
        {
            var eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });
            var presenter = new NewsGridPresenter(this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == 1), It.Is<int>(a => a == 6), It.IsAny<bool>(), It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void PageLoad_ViewModelNewsCountShouldBeSetToViewModelNewsDataSourceCount()
        {
            var eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };
            int count = 6;

            this.mockedDataSource.SetupGet(x => x.Count).Returns(count);
            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(count, presenter.View.Model.NewsCount);
        }

        [Test]
        public void PageLoad_PageSizeShouldBeSetToTheConstantPageSize()
        {
            var eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };
            int count = 6;

            this.mockedDataSource.SetupGet(x => x.Count).Returns(count);
            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);


            int pageSizeConstant = (int)typeof(NewsGridPresenter)
                .GetField("PageSize", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(presenter);

            presenter.PageLoad(null, eventArgs);
            Assert.AreEqual(pageSizeConstant, 6);
        }

        [Test]
        public void ChangePage_ShouldSetViewStateCurrentPageToThePassedPageFromTheEventArgs()
        {
            var eventArgs = new ChangePageEventArgs { Page = 3, ViewState = new StateBag() };

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.ChangePage(null, eventArgs);
            Assert.AreEqual(3, (int)eventArgs.ViewState["CurrentPage"]);
        }

        [Test]
        public void ChangePage_ShouldViewModelNewsDataSourceGetPageItemsWithPageAndPageSize()
        {
            int page = 3;
            var eventArgs = new ChangePageEventArgs { Page = page, ViewState = new StateBag() };

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            int pageSizeConstant = (int)typeof(NewsGridPresenter)
               .GetField("PageSize", BindingFlags.NonPublic | BindingFlags.Static)
               .GetValue(presenter);

            presenter.ChangePage(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == page), It.Is<int>(a => a == pageSizeConstant),It.IsAny<bool>(), null),
                Times.Once);
        }

        [Test]
        public void OrderByDate_WhenOrderTypeIsAscendingNewsDataSourceOrderByAscendingShouldBeCalled()
        {
            var eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Ascending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel());

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.OrderByDate(null, eventArgs);
            this.mockedDataSource.Verify(x =>
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
            var eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Descending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.OrderByDate(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.OrderByDescending(
                    It.IsAny<Expression<Func<NewsItem, DateTime?>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void PageLoad_ShouldCallHttpUtilityServiceParseQueryString_WhenEventArgsQueryStringIsNotNull()
        {
            var eventArgs = new OrderByEventArgs { OrderBy = OrderByType.Descending, ViewState = new StateBag() };
            eventArgs.ViewState["CurrentPage"] = 5;

            this.mockedView.SetupGet(x => x.Model).Returns(new NewsGridViewModel { });

            var queryString = "name=Sports";

            var pageLoadEventArgs = new PageLoadEventArgs
            {
                QueryString = queryString,
                IsPostBack = false,
                ViewState = new StateBag()
            };

            var presenter = new NewsGridPresenter(
                this.mockedView.Object,
                this.mockedDataSource.Object,
                this.mockedHttpUtilitySevice.Object,
                this.mockArticleManagementService.Object);

            presenter.PageLoad(null, pageLoadEventArgs);

            mockedHttpUtilitySevice.Verify(x => x.ParseQueryString(queryString), Times.Once);
        }
    }
}
