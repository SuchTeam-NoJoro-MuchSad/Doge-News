using DogeNews.Web.Mvp.UserControls.ArticleComments.EventArguments;
using NUnit.Framework;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.UserControls.ArticleComments
{
    [TestFixture]
    public class AddCommentEventArgumentsTests
    {
        [Test]
        public void UsernameShouldReturnSetValue()
        {
            var eventArgs = new AddCommentEventArguments();
            var username = "username";

            eventArgs.Username = username;
            Assert.AreEqual(username, eventArgs.Username);
        }

        [Test]
        public void ContentShouldReturnSetValue()
        {
            var eventArgs = new AddCommentEventArguments();
            var content = "content";

            eventArgs.Content = content;
            Assert.AreEqual(content, eventArgs.Content);
        }

        [Test]
        public void ArticleTitleShouldReturnSetValue()
        {
            var eventArgs = new AddCommentEventArguments();
            var title = "title";

            eventArgs.ArticleTitle = title;
            Assert.AreEqual(title, eventArgs.ArticleTitle);
        }
    }
}
