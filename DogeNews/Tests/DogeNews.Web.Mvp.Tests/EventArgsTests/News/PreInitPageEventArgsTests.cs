using NUnit.Framework;

using DogeNews.Web.Mvp.News.Edit.EventArguments;

namespace DogeNews.Web.Mvp.Tests.EventArgsTests.News
{
    [TestFixture]
    public class PreInitPageEventArgsTests
    {
        [Test]
        public void QueryString_ShouldReturnTheSetValue()
        {
            var eventArgs = new PreInitPageEventArgs();
            var queryString = "?Name=Name";

            eventArgs.QueryString = queryString;
            Assert.AreEqual(queryString, eventArgs.QueryString);
        }

        [Test]
        public void IsAdminUser_ShouldReturnTheSetValue()
        {
            var eventArgs = new PreInitPageEventArgs();

            eventArgs.IsAdminUser = true;
            Assert.IsTrue(eventArgs.IsAdminUser);
        }
    }
}
