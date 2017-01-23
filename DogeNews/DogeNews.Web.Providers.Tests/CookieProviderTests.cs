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
        private Mock<IDateTimeProvider> mockedDateTimeProvider;
        private string cookieName;
        private int daysUntilExpiration;
        private IEnumerable<KeyValuePair<string, string>> values;


        [SetUp]
        public void Init()
        {
            this.mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            this.cookieName = "cookieName";
            this.daysUntilExpiration = 1;
            this.values = new List<KeyValuePair<string, string>>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new CookieProvider(null));
            Assert.AreEqual("dateTimeProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenInitiatedIDateTimeProviderIsPassed()
        {
            Assert.DoesNotThrow(() => new CookieProvider(this.mockedDateTimeProvider.Object));
        }

        [Test]
        public void GetAuthenticationCookie_ShouldReturnCookieWithCorrectProperties()
        {
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

        [Test]
        public void GetAuthenticationCookie_ShouldThrowArgumentNullExeption_WhenCookieNameIsNull()
        {
            var cookieProvider = new CookieProvider(this.mockedDateTimeProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() =>
                {
                    cookieProvider.GetAuthenticationCookie(null, this.daysUntilExpiration, this.values);
                });

            Assert.AreEqual("cookieName", exception.ParamName);
        }

        [Test]
        public void GetAuthenticationCookie_ShouldThrowArgumentNullExeption_WhenValuesIsNull()
        {
            var cookieProvider = new CookieProvider(this.mockedDateTimeProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                cookieProvider.GetAuthenticationCookie(this.cookieName, this.daysUntilExpiration, null);
            });

            Assert.AreEqual("values", exception.ParamName);
        }


        [Test]
        public void GetAuthenticationCookie_ShouldNotThrow_WhenAllParametersAreNotNull()
        {
            var cookieProvider = new CookieProvider(this.mockedDateTimeProvider.Object);

            Assert.DoesNotThrow(() =>
                {
                    cookieProvider.GetAuthenticationCookie(this.cookieName, this.daysUntilExpiration, this.values);
                }
            );
        }
    }
}
