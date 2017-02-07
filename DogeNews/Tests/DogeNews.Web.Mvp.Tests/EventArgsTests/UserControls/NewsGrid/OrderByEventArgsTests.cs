using System.Web.UI;

using NUnit.Framework;

using DogeNews.Common.Enums;
using DogeNews.Web.Mvp.UserControls.NewsGrid.EventArguments;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.UserControls.NewsGrid
{
    [TestFixture]
    public class OrderByEventArgsTests
    {
        [Test]
        public void OrderByShouldReturnTheSetValue()
        {
            var eventArgs = new OrderByEventArgs();
            var orderBy = OrderByType.Ascending;

            eventArgs.OrderBy = orderBy;
            Assert.AreEqual(orderBy, eventArgs.OrderBy);
        }

        [Test]
        public void ViewStateShouldReturnTheSetValue()
        {
            var eventArgs = new OrderByEventArgs();
            var viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }
    }
}
