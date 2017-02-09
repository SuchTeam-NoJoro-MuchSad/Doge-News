using System;

using DogeNews.Web.Services.Http;

using NUnit.Framework;

namespace DogeNews.Web.Services.Tests.HttpTests
{
    [TestFixture]
    public class HttpUtilityServiceTests
    {
        [Test]
        public void ParseQueryString_ShouldReturnCorrectHttpValueCollection()
        {
            var expectedCount = 2;

            var service = new HttpUtilityService();
            var result = service.ParseQueryString("name=Sports&page=2");

            Assert.AreEqual(expectedCount, result.Count);
        }

        [Test]
        public void ParseQueryString_ShouldThrowArgumentNullException_WhenThePassedParameterIsNull()
        {
            var service = new HttpUtilityService();

            Assert.Throws<ArgumentNullException>(() => service.ParseQueryString(null));
        }
    }
}