using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit.EventArguments;
using DogeNews.Common.Enums;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.News
{
    [TestFixture]
    public class EditArticleEventArgsTests
    {
        [Test]
        public void Id_ShouldReturnTheSetValue()
        {
            var eventArgs = new EditArticleEventArgs();
            int id = 1;

            eventArgs.Id = id;
            Assert.AreEqual(id, eventArgs.Id);
        }

        [Test]
        public void Title_ShouldReturnTheSetValue()
        {
            var eventArgs = new EditArticleEventArgs();
            var title = "title";

            eventArgs.Title = title;
            Assert.AreEqual(title, eventArgs.Title);
        }

        [Test]
        public void FileName_ShouldReturnTheSetValue()
        {
            var eventArgs = new EditArticleEventArgs();
            var fileName = "fileName";

            eventArgs.FileName = fileName;
            Assert.AreEqual(fileName, eventArgs.FileName);
        }

        [Test]
        public void Content_ShouldReturnTheSetValue()
        {
            var eventArgs = new EditArticleEventArgs();
            var content = "content";

            eventArgs.Content = content;
            Assert.AreEqual(content, eventArgs.Content);
        }

        [Test]
        public void Category_ShouldReturnTheSetValue()
        {
            var eventArgs = new EditArticleEventArgs();
            var category = NewsCategoryType.Breaking;

            eventArgs.Category = category;
            Assert.AreEqual(category, eventArgs.Category);
        }
    }
}
