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
        private Mock<INewsService> mockedNewsService;
        private Mock<IDefaultView> mockedView;

        [SetUp]
        public void Init()
        {
            this.mockedNewsService = new Mock<INewsService>();
            this.mockedView = new Mock<IDefaultView>();
        }

        [Test]
        public void Constructor_ShouldSetNewsServices()
        {
            var presenter = new DefaultPresenter(this.mockedView.Object, this.mockedNewsService.Object);
            var newsServiceFiled = typeof(DefaultPresenter)
                .GetField("newsService", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(presenter);

            Assert.AreEqual(this.mockedNewsService.Object, newsServiceFiled);
        }

        [Test]
        public void PageLoad_NewsServiceGetSliderNewsShouldBeCalled()
        {
            this.mockedView.SetupGet(x => x.Model).Returns(new DefaultViewModel());

            var presenter = new DefaultPresenter(this.mockedView.Object, this.mockedNewsService.Object);
            var eventArgs = new EventArgs();

            presenter.PageLoad(null, eventArgs);
            this.mockedNewsService.Verify(x => x.GetSliderNews(), Times.Once);
        }
    }
}
