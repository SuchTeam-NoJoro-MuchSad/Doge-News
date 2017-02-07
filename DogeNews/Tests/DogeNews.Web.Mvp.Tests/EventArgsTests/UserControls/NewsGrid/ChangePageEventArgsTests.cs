using System.Web.UI;

using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.UserControls.NewsGrid
{
    [TestFixture]
    public class ChangePageEventArgsTests
    {
        [Test]
        public void PageShouldReturnTheSetValue()
        {
            var eventArgs = new ChangePageEventArgs();
            int page = 3;

            eventArgs.Page = page;
            Assert.AreEqual(page, eventArgs.Page);
        }

        [Test]
        public void ViewStateShouldReturnTheSetValue()
        {
            var eventArgs = new ChangePageEventArgs();
            var viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }
    }
}
