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
            EditArticleEventArgs eventArgs = new EditArticleEventArgs();
            int id = 1;

            eventArgs.Id = id;
            Assert.AreEqual(id, eventArgs.Id);
        }

        [Test]
        public void Title_ShouldReturnTheSetValue()
        {
            EditArticleEventArgs eventArgs = new EditArticleEventArgs();
            string title = "title";

            eventArgs.Title = title;
            Assert.AreEqual(title, eventArgs.Title);
        }

        [Test]
        public void FileName_ShouldReturnTheSetValue()
        {
            EditArticleEventArgs eventArgs = new EditArticleEventArgs();
            string fileName = "fileName";

            eventArgs.FileName = fileName;
            Assert.AreEqual(fileName, eventArgs.FileName);
        }

        [Test]
        public void Content_ShouldReturnTheSetValue()
        {
            EditArticleEventArgs eventArgs = new EditArticleEventArgs();
            string content = "content";

            eventArgs.Content = content;
            Assert.AreEqual(content, eventArgs.Content);
        }

        [Test]
        public void Category_ShouldReturnTheSetValue()
        {
            EditArticleEventArgs eventArgs = new EditArticleEventArgs();
            NewsCategoryType category = NewsCategoryType.Breaking;

            eventArgs.Category = category;
            Assert.AreEqual(category, eventArgs.Category);
        }
    }
}
