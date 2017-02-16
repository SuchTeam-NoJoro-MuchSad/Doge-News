using System.Collections.Generic;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.NewsGrid;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.UserControls
{
    [TestFixture]
    public class NewsGridViewModelTests
    {
        [Test]
        public void NewsCountShouldReturnTheSetValue()
        {
            NewsGridViewModel model = new NewsGridViewModel();
            int newsCount = 5;

            model.NewsCount = newsCount;
            Assert.AreEqual(newsCount, model.NewsCount);
        }

        [Test]
        public void PageSizeShouldReturnTheSetValue()
        {
            NewsGridViewModel model = new NewsGridViewModel();
            int pageSize = 5;

            model.PageSize = pageSize;
            Assert.AreEqual(pageSize, model.PageSize);
        }

        [Test]
        public void CurrentPageNewsShouldReturnTheSetValue()
        {
            NewsGridViewModel model = new NewsGridViewModel();
            IEnumerable<NewsWebModel> currentPageNews = new List<NewsWebModel>();

            model.CurrentPageNews = currentPageNews;
            Assert.AreEqual(currentPageNews, model.CurrentPageNews);
        }
    }
}
