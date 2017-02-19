using System;

using NUnit.Framework;

namespace DogeNews.Services.Http.Tests
{
    [TestFixture]
    public class HttpContextServiceTests
    {
        [Test]
        public void GetUsername_ShouldThrowNullReferenceException_WhenHttpContextBaseIsNull()
        {
            HttpContextService service = new HttpContextService();

            Assert.Throws<NullReferenceException>(() => service.GetLoggedInUserUsername());
        }
    }
}