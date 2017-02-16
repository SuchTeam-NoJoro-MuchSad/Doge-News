using NUnit.Framework;

namespace DogeNews.Services.Http.Tests
{
    [TestFixture]
    public class HttpResponseServiceTests
    {
        [Test, Ignore("HttpContext is not instanced by the test (only by ASP), and this test will aways throw NullArgumentException")]
        public void Clear_ShouldNotThrow()
        {
            HttpResponseService service = new HttpResponseService();
            Assert.DoesNotThrow(()=> service.Clear());
        }
    }
}