using DogeNews.Web.Services.Http;

using NUnit.Framework;

namespace DogeNews.Web.Services.Tests.HttpTests
{
    [TestFixture]
    public class HttpResponseServiceTests
    {
        [Test, Ignore("HttpContext is not instanced by the test (only by ASP), and this test will aways throw NullArgumentException")]
        public void Clear_ShouldNotThrow()
        {
            var service = new HttpResponseService();
            Assert.DoesNotThrow(()=> service.Clear());
        }
    }
}