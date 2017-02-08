using System.Web.UI;

using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.UserControls.NewsGrid
{
    [TestFixture]
    public class PageLoadEventArgsTests
    {
        [Test]
        public void IsPostBackShouldReturnTheSetValue()
        {
            var eventArgs = new PageLoadEventArgs();
            var isPostBack = true;

            eventArgs.IsPostBack = isPostBack;
            Assert.IsTrue(eventArgs.IsPostBack);
        }

        [Test]
        public void ViewStateShouldReturnTheSetValue()
        {
            var eventArgs = new PageLoadEventArgs();
            var viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }
    }
}
