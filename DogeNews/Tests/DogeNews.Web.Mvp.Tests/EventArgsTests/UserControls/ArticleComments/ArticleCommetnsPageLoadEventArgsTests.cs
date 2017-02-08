using System.Web.UI;

using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.UserControls.ArticleComments
{
    [TestFixture]
    public class ArticleCommetnsPageLoadEventArgsTests
    {
        [Test]
        public void IsPostBackShouldReturnSetValue()
        {
            var eventArgs = new ArticleCommetnsPageLoadEventArgs();

            eventArgs.IsPostBack = true;
            Assert.IsTrue(eventArgs.IsPostBack);
        }

        [Test]
        public void ViewStateShouldReturnSetValue()
        {
            var eventArgs = new ArticleCommetnsPageLoadEventArgs();
            var viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }

        [Test]
        public void TitleShouldReturnSetValue()
        {
            var eventArgs = new ArticleCommetnsPageLoadEventArgs();
            var title = "title";

            eventArgs.Title = title;
            Assert.AreEqual(title, eventArgs.Title);
        }
    }
}
