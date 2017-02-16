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
            AddCommentEventArguments eventArgs = new AddCommentEventArguments();
            string username = "username";

            eventArgs.Username = username;
            Assert.AreEqual(username, eventArgs.Username);
        }

        [Test]
        public void ContentShouldReturnSetValue()
        {
            AddCommentEventArguments eventArgs = new AddCommentEventArguments();
            string content = "content";

            eventArgs.Content = content;
            Assert.AreEqual(content, eventArgs.Content);
        }

        [Test]
        public void ArticleTitleShouldReturnSetValue()
        {
            AddCommentEventArguments eventArgs = new AddCommentEventArguments();
            string title = "title";

            eventArgs.ArticleTitle = title;
            Assert.AreEqual(title, eventArgs.ArticleTitle);
        }
    }
}
