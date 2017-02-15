using System;
using System.Reflection;

using NUnit.Framework;
using Moq;

using DogeNews.Web.Mvp.News.Category;
using DogeNews.Web.Services.Contracts;
using DogeNews.Services.Http.Contracts;

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
