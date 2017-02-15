using System;
using System.Reflection;

using DogeNews.Web.Mvp.Default;
using DogeNews.Web.Services.Contracts;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.PresenterTests.Default
{
    [TestFixture]
    public class DefaultPresenterTests
    {
        private Mock<INewsService> newsService;
        private Mock<IDefaultView> view;

        [SetUp]
        public void Init()
        {
            this.newsService = new Mock<INewsService>();
            this.view = new Mock<IDefaultView>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenNewsServiceIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new DefaultPresenter(this.view.Object, null));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldSetNewsServices()
        {
            var presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            var newsServiceFiled = typeof(DefaultPresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.newsService.Object, newsServiceFiled);
        }

        [Test]
        public void PageLoad_NewsServiceGetSliderNewsShouldBeCalled()
        {
            this.view.SetupGet(x => x.Model).Returns(new DefaultViewModel());

            var presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            var eventArgs = new EventArgs();

            presenter.PageLoad(null, eventArgs);
            this.newsService.Verify(x => x.GetSliderNews(), Times.Once);
        }

        [Test]
        public void PageLoad_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            var presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            var exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }
    }
}
