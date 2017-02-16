using System;
using System.Reflection;
using DogeNews.Services.Data.Contracts;
using NUnit.Framework;
using Moq;

using DogeNews.Web.Mvp.News.Category;
using DogeNews.Services.Http.Contracts;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.News
{
    [TestFixture]
    public class CategoryPresenterTests
    {
        private Mock<ICategoryView> view;
        private Mock<INewsService> newsService;
        private Mock<IHttpContextService> httpContextService;

        [SetUp]
        public void Init()
        {
            this.view = new Mock<ICategoryView>();
            this.newsService = new Mock<INewsService>();
            this.httpContextService = new Mock<IHttpContextService>();
        }

        [Test]
        public void Constructor_ShouldSetNewsServiceFiled()
        {
            CategoryPresenter presenter = this.GetPresenter();
            object serviceField = typeof(CategoryPresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.newsService.Object, serviceField);
        }

        [Test]
        public void Constructor_ShouldSetHttpContextServiceField()
        {
            CategoryPresenter presenter = this.GetPresenter();
            object servichttpContextServiceField = typeof(CategoryPresenter)
                .GetField("httpContextService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.httpContextService.Object, servichttpContextServiceField);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenNewsServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => 
                new CategoryPresenter(
                    this.view.Object, 
                    null, 
                    this.httpContextService.Object));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ArgumentNullExceptionShouldBeThrownWhenHttpContextServiceIsNull()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
                new CategoryPresenter(
                    this.view.Object,
                    this.newsService.Object,
                    null));

            Assert.AreEqual("httpContextService", exception.ParamName);
        }

        private CategoryPresenter GetPresenter()
        {
            CategoryPresenter presenter = new CategoryPresenter(
                this.view.Object,
                this.newsService.Object,
                this.httpContextService.Object);

            return presenter;
        }
    }
}
