using System;
using System.Web;

using Moq;
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

            Assert.Throws<NullReferenceException>(() => service.GetUsername(null));
        }

        [Test]
        public void GetUsername_ShouldNotThrow_WhenTheContextIsInCorrectState()
        {
            Mock<HttpContextBase> mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns("someUserName");

            HttpContextService service = new HttpContextService();
            
            Assert.DoesNotThrow(() => service.GetUsername(mockContextBase.Object));
        }

        [Test]
        public void GetUsername_ShouldReturnString_WhenTheContextIsInCorrectState()
        {
            Mock<HttpContextBase> mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns("someUserName");

            HttpContextService service = new HttpContextService();
            string result = service.GetUsername(mockContextBase.Object);

            Assert.AreEqual(result.GetType(),typeof(string));
        }

        [Test]
        public void GetUsername_ShouldReturnCorrectString_WhenTheContextIsInCorrectState()
        {
            string expecteUsername = "someUserName";
            Mock<HttpContextBase> mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns(expecteUsername);

            HttpContextService service = new HttpContextService();
            string result = service.GetUsername(mockContextBase.Object);

            Assert.AreEqual(result.GetType(), typeof(string));
            Assert.AreEqual(result, expecteUsername);
        }
    }
}