using System;
using System.Reflection;
using DogeNews.Services.Data.Contracts;
using DogeNews.Web.Mvp.Default;
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
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new DefaultPresenter(this.view.Object, null));

            Assert.AreEqual("newsService", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldSetNewsServices()
        {
            DefaultPresenter presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            object newsServiceFiled = typeof(DefaultPresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.newsService.Object, newsServiceFiled);
        }

        [Test]
        public void PageLoad_NewsServiceGetSliderNewsShouldBeCalled()
        {
            this.view.SetupGet(x => x.Model).Returns(new DefaultViewModel());

            DefaultPresenter presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            EventArgs eventArgs = new EventArgs();

            presenter.PageLoad(null, eventArgs);
            this.newsService.Verify(x => x.GetSliderNews(), Times.Once);
        }

        [Test]
        public void PageLoad_ShouldThrowArgumentNullExceptionWhenEventArgsIsNull()
        {
            DefaultPresenter presenter = new DefaultPresenter(this.view.Object, this.newsService.Object);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => presenter.PageLoad(null, null));

            Assert.AreEqual("e", exception.ParamName);
        }
    }
}
