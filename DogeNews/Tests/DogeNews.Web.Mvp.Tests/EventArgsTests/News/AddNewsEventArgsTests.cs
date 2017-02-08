using DogeNews.Common.Enums;
using NUnit.Framework;

using DogeNews.Web.Mvp.News.Add.EventArguments;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.News
{
    [TestFixture]
    public class AddNewsEventArgsTests
    {
        [Test]
        public void TitleShouldReturnTheSetValue()
        {
            var eventArgs = new AddNewsEventArgs();
            var title = "title";

            eventArgs.Title = title;
            Assert.AreEqual(title, eventArgs.Title);
        }

        [Test]
        public void FileNameShouldReturnTheSetValue()
        {
            var eventArgs = new AddNewsEventArgs();
            var fileName = "fileName";

            eventArgs.FileName = fileName;
            Assert.AreEqual(fileName, eventArgs.FileName);
        }

        [Test]
        public void ContentShouldReturnTheSetValue()
        {
            var eventArgs = new AddNewsEventArgs();
            var content = "content";

            eventArgs.Content = content;
            Assert.AreEqual(content, eventArgs.Content);
        }

        [Test]
        public void NewscategoryShouldReturnTheSetValue()
        {
            var eventArgs = new AddNewsEventArgs();
            var category = NewsCategoryType.Breaking;

            eventArgs.Category = category;
            Assert.AreEqual(category, eventArgs.Category);
        }
    }
}
