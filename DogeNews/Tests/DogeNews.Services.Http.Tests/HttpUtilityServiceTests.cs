using System;
using System.Collections.Specialized;
using NUnit.Framework;

namespace DogeNews.Services.Http.Tests
{
    [TestFixture]
    public class HttpUtilityServiceTests
    {
        [Test]
        public void ParseQueryString_ShouldReturnCorrectHttpValueCollection()
        {
            int expectedCount = 2;

            HttpUtilityService service = new HttpUtilityService();
            NameValueCollection result = service.ParseQueryString("name=Sports&page=2");

            Assert.AreEqual(expectedCount, result.Count);
        }

        [Test]
        public void ParseQueryString_ShouldThrowArgumentNullException_WhenThePassedParameterIsNull()
        {
            HttpUtilityService service = new HttpUtilityService();

            Assert.Throws<ArgumentNullException>(() => service.ParseQueryString(null));
        }
    }
}