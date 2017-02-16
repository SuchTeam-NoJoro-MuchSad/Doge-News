using System.Web.UI;

using DogeNews.Web.Mvp.News.Article.EventArguments;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.News
{
    [TestFixture]
    public class ArticlePageLoadEventArgsTests
    {
        [Test]
        public void IsPostBackShouldReturnTheSetValue()
        {
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs();
            bool isPostBack = true;

            eventArgs.IsPostBack = isPostBack;
            Assert.AreEqual(isPostBack, eventArgs.IsPostBack);
        }

        [Test]
        public void QueryStringShouldReturnTheSetValue()
        {
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs();
            string queryString = "?title=omfg";

            eventArgs.QueryString = queryString;
            Assert.AreEqual(queryString, eventArgs.QueryString);
        }

        [Test]
        public void ViewStateShouldReturnTheSetValue()
        {
            ArticlePageLoadEventArgs eventArgs = new ArticlePageLoadEventArgs();
            StateBag viewState = new StateBag();

            eventArgs.ViewState = viewState;
            Assert.AreEqual(viewState, eventArgs.ViewState);
        }
    }
}
