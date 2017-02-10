using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit;
using DogeNews.Web.Models;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.News
{
    [TestFixture]
    public class EditArticleViewModelTests
    {
        [Test]
        public void NewsItem_ShouldReturnTheSetValue()
        {
            var model = new EditArticleViewModel();
            var newsItem = new NewsWebModel();

            model.NewsItem = newsItem;
            Assert.AreEqual(newsItem, model.NewsItem);
        }
    }
}
