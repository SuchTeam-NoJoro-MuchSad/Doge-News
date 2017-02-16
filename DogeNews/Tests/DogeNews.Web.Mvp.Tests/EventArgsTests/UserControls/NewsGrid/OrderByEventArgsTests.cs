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
            OrderByEventArgs eventArgs = new OrderByEventArgs();
            OrderByType orderBy = OrderByType.Ascending;

            eventArgs.OrderBy = orderBy;
            Assert.AreEqual(orderBy, eventArgs.OrderBy);
        }

        [Test]
        public void ViewStateShouldReturnTheSetValue()
        {
            OrderByEventArgs eventArgs = new OrderByEventArgs();
            StateBag viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }
    }
}
