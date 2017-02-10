using System;
using System.Reflection;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using DogeNews.Web.Mvp.News.Category;
using DogeNews.Web.Services.Contracts;
using DogeNews.Web.Services.Contracts.Http;
using DogeNews.Web.Mvp.News.Category.EventArguments;
using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class CategoryPresenterTests
    {
        private Mock<ICategoryView> mockView;
        private Mock<INewsService> mockNewsService;
        private Mock<IHttpContextService> mockHttpContextService;

        [SetUp]
        public void Init()
        {
            this.mockView = new Mock<ICategoryView>();
            this.mockNewsService = new Mock<INewsService>();
            this.mockHttpContextService = new Mock<IHttpContextService>();
        }

        [Test]
        public void Constructor_ShouldSetNewsServiceFiled()
        {
            var presenter = this.GetPresenter();
            var serviceField = typeof(CategoryPresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockNewsService.Object, serviceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextServiceField()
        {
            var presenter = this.GetPresenter();
            var servichttpContextServiceField = typeof(CategoryPresenter)
                .GetField("httpContextService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockHttpContextService.Object, servichttpContextServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => 
                new CategoryPresenter(
                    this.mockView.Object, 
                    null, 
                    this.mockHttpContextService.Object));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new CategoryPresenter(
                    this.mockView.Object,
                    this.mockNewsService.Object,
                    null));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        [Test]
        public void PageLoad_ArgumentNullExceptionShouldBeThrownWhenEventArgsIsNull()
        {
            var presenter = this.GetPresenter();
            var exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("categoryPageLoadEventArgs", exception.ParamName);
        }

        [Test]
        public void PageLoad_HttpContextStringGetQueryStringPairValueShouldBeCalled()
        {
            this.mockView.Setup(x => x.Model).Returns(new CategoryViewModel());

            var presenter = this.GetPresenter();
            var eventArgs = new CategoryPageLoadEventArgs();

            presenter.PageLoad(null, eventArgs);
            this.mockHttpContextService.Verify(x => x.GetQueryStringPairValue(It.Is<string>(a => a == "name")), Times.Once);
        }

        [Test]
        public void PageLoad_NewsServiceGetNewsItemsByCategoryShouldBeCalled()
        {
            this.mockView.Setup(x => x.Model).Returns(new CategoryViewModel());

            string category = "category";
            this.mockHttpContextService.Setup(x => x.GetQueryStringPairValue(It.IsAny<string>())).Returns(category);

            var presenter = this.GetPresenter();
            var eventArgs = new CategoryPageLoadEventArgs();

            presenter.PageLoad(null, eventArgs);
            this.mockNewsService.Verify(x => x.GetNewsItemsByCategory(It.Is<string>(a => a == category)), Times.Once);
        }

        private CategoryPresenter GetPresenter()
        {
            var presenter = new CategoryPresenter(
                this.mockView.Object,
                this.mockNewsService.Object,
                this.mockHttpContextService.Object);

            return presenter;
        }
    }
}
