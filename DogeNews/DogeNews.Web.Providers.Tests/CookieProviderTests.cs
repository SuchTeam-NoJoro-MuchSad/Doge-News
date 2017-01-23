using System;
using System.Collections.Generic;

using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Auth;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class CookieProviderTests
    {
        [Test]
        public void GetAuthenticationCookie_ShouldReturnCookieWithCorrectProperties()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var now = DateTime.Now;
            string cookieName = "aaa";
            int daysUntilExpiration = 1;
            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Username", "Username")
            };

            mockedDateTimeProvider.SetupGet(x => x.Now).Returns(now);

            var cookieProvider = new CookieProvider(mockedDateTimeProvider.Object);
            var cookie = cookieProvider.GetAuthenticationCookie(cookieName, daysUntilExpiration, values);

            Assert.AreEqual(cookieName, cookie.Name);
            Assert.AreEqual(now.AddDays(1), cookie.Expires);
            Assert.AreEqual("Username", cookie["Username"]);
        }
    }
}
