using System.Web;
using NUnit.Framework;

namespace DogeNews.Services.Http.Tests
{
    [TestFixture]
    public class HttpServerUtilityServiceTests
    {
        [Test,Ignore("HttpContext is not instanced by the test (only by ASP), and this test will aways throw NullArgumentException")]
        public void HttpUtilityService_ShouldThrowHttpException_WhenInputArgumentIsNull()
        {
            HttpServerUtilityService service = new HttpServerUtilityService();
            Assert.Throws<HttpException>(() => service.MapPath(null));
        }
    }
}