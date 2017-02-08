using System;
using System.Runtime.Serialization;
using System.Web;
using DogeNews.Web.Services.Http;
using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests.HttpTests
{
    [TestFixture]
    public class HttpPostedFileServiceTests
    {
        [Test]
        public void SaveAs_ShouldThrowNullReferenceException_WhenBothArgumentsAreNull()
        {
            var service = new HttpPostedFileService();

            Assert.Throws<NullReferenceException>(() => service.SaveAs(null, null));
        }

        [Test]
        public void SaveAs_ShouldThrowNullReferenceException_WhenOnlyHttpPostedFileIsNull()
        {
            var service = new HttpPostedFileService();

            Assert.Throws<NullReferenceException>(() => service.SaveAs(null, It.IsAny<string>()));
        }

        [Test]
        public void SaveAs_ShouldThsrowNullReferenceException_WhenOnlyStringFileNameIsNull()
        {
            var expectedErrorMessage = @"The SaveAs method is configured to require a rooted path, and the path '' is not rooted.";
            var fakeHttpPostedFile = (HttpPostedFile)FormatterServices
                .GetSafeUninitializedObject(typeof(HttpPostedFile));

            var service = new HttpPostedFileService();

            var message = Assert.Throws<HttpException>(() => service.SaveAs(fakeHttpPostedFile, null));
            Assert.AreEqual(message.Message, expectedErrorMessage);
        }
    }
}