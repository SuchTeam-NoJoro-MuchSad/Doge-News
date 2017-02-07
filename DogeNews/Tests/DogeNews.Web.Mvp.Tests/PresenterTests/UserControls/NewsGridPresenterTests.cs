using System;
using System.Collections.Generic;
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

namespace DogeNews.Web.Mvp.Tests.PresenterTests.UserControls
{
    [TestFixture]
    public class NewsGridPresenterTests
    {
        private Mock<INewsGridView> mockedView;
        private Mock<INewsDataSource<NewsItem, NewsWebModel>> mockedDataSource;

        [SetUp]
        public void Init()
        {
            this.mockedView = new Mock<INewsGridView>();

            this.mockedDataSource = new Mock<INewsDataSource<NewsItem, NewsWebModel>>();

            this.mockedDataSource.Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.GetPageItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());

            this.mockedDataSource.Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByDescending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());

            this.mockedDataSource.Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<NewsWebModel>());
            this.mockedDataSource.Setup(x => x.OrderByAscending(It.IsAny<Expression<Func<NewsItem, NewsWebModel>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new List<NewsWebModel>());
        }

        [Test]
        public void PageLoad_WhenItIsNotPostbackViewStateCurrentPageShouldBeSetTo1()
        {
            var eventArgs = new PageLoadEventArgs { IsPostBack = false, ViewState = new StateBag() };

            this.mockedView
                .SetupGet(x => x.Model)
                .Returns(new NewsGridViewModel { });
            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

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
            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

            presenter.PageLoad(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == 1), It.Is<int>(a => a == 6),It.IsAny<string>()),
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
            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

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
            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);
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

            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

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

            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);
            int pageSizeConstant = (int)typeof(NewsGridPresenter)
               .GetField("PageSize", BindingFlags.NonPublic | BindingFlags.Static)
               .GetValue(presenter);

            presenter.ChangePage(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.GetPageItems(It.Is<int>(a => a == page), It.Is<int>(a => a == pageSizeConstant), null),
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

            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

            presenter.OrderByDate(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.OrderByAscending(
                        It.IsAny<Expression<Func<NewsItem, DateTime?>>>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
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

            var presenter = new NewsGridPresenter(this.mockedView.Object, this.mockedDataSource.Object);

            presenter.OrderByDate(null, eventArgs);
            this.mockedDataSource.Verify(x =>
                x.OrderByDescending(
                    It.IsAny<Expression<Func<NewsItem, DateTime?>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
