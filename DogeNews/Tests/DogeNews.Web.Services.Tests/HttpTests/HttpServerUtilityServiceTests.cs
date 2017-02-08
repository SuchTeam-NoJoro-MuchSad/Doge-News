using System.Web;
using DogeNews.Web.Services.Http;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests.HttpTests
{
    [TestFixture]
    public class HttpServerUtilityServiceTests
    {
        [Test,Ignore("HttpContext is not instanced by the test (only by ASP), and this test will aways throw NullArgumentException")]
        public void HttpUtilityService_ShouldThrowHttpException_WhenInputArgumentIsNull()
        {
            var service = new HttpServerUtilityService();
            Assert.Throws<HttpException>(() => service.MapPath(null));
        }
    }
}