using System.Collections.Generic;

using DogeNews.Data.Models;
using DogeNews.Web.DataSources.Contracts;
using DogeNews.Web.Models;
using DogeNews.Web.Mvp.UserControls.NewsGrid;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.UserControls
{
    [TestFixture]
    public class NewsGridViewModelTests
    {
        [Test]
        public void NewsCountShouldReturnTheSetValue()
        {
            var model = new NewsGridViewModel();
            int newsCount = 5;

            model.NewsCount = newsCount;
            Assert.AreEqual(newsCount, model.NewsCount);
        }

        [Test]
        public void PageSizeShouldReturnTheSetValue()
        {
            var model = new NewsGridViewModel();
            int pageSize = 5;

            model.PageSize = pageSize;
            Assert.AreEqual(pageSize, model.PageSize);
        }

        [Test]
        public void CurrentPageNewsShouldReturnTheSetValue()
        {
            var model = new NewsGridViewModel();
            IEnumerable<NewsWebModel> currentPageNews = new List<NewsWebModel>();

            model.CurrentPageNews = currentPageNews;
            Assert.AreEqual(currentPageNews, model.CurrentPageNews);
        }
    }
}
