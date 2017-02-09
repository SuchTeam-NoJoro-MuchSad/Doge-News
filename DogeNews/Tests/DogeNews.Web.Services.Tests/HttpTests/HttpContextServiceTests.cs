using System;
using System.Web;

using DogeNews.Web.Services.Http;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests.HttpTests
{
    [TestFixture]
    public class HttpContextServiceTests
    {
        [Test]
        public void GetUsername_ShouldThrowNullReferenceException_WhenHttpContextBaseIsNull()
        {
            var service = new HttpContextService();

            Assert.Throws<NullReferenceException>(() => service.GetUsername(null));
        }

        [Test]
        public void GetUsername_ShouldNotThrow_WhenTheContextIsInCorrectState()
        {
            var mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns("someUserName");

            var service = new HttpContextService();
            
            Assert.DoesNotThrow(() => service.GetUsername(mockContextBase.Object));
        }

        [Test]
        public void GetUsername_ShouldReturnString_WhenTheContextIsInCorrectState()
        {
            var mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns("someUserName");

            var service = new HttpContextService();
            var result = service.GetUsername(mockContextBase.Object);

            Assert.AreEqual(result.GetType(),typeof(string));
        }

        [Test]
        public void GetUsername_ShouldReturnCorrectString_WhenTheContextIsInCorrectState()
        {
            var expecteUsername = "someUserName";
            var mockContextBase = new Mock<HttpContextBase>();
            mockContextBase.Setup(x => x.User.Identity.Name).Returns(expecteUsername);

            var service = new HttpContextService();
            var result = service.GetUsername(mockContextBase.Object);

            Assert.AreEqual(result.GetType(), typeof(string));
            Assert.AreEqual(result, expecteUsername);
        }
    }
}