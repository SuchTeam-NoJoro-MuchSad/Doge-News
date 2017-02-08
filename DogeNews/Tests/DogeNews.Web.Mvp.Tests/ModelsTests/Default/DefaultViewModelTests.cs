using System.Collections.Generic;

using NUnit.Framework;

using DogeNews.Web.Models;
using DogeNews.Web.Mvp.Default;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.Default
{
    [TestFixture]
    public class DefaultViewModelTests
    {
        [Test]
        public void SliderNewsShouldReturnSetValue()
        {
            var model = new DefaultViewModel();
            var sliderNews = new List<NewsWebModel>();

            model.SliderNews = sliderNews;
            Assert.AreEqual(sliderNews, model.SliderNews);
        }
    }
}
