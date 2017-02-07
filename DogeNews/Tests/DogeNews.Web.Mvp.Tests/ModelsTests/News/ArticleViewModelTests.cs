using DogeNews.Web.Models;
using DogeNews.Web.Mvp.News.Article;

using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.ModelsTests.News
{
    [TestFixture]
    public class ArticleViewModelTests
    {
        [Test]
        public void NewsModelShouldReturnTheSetValue()
        {
            var articleModel = new ArticleViewModel();
            var newsModel = new NewsWebModel();

            articleModel.NewsModel = newsModel;
            Assert.AreEqual(newsModel, articleModel.NewsModel);
        }
    }
}
