using System;
using System.Runtime.Serialization;
using System.Web;
using Moq;
using NUnit.Framework;

namespace DogeNews.Services.Http.Tests
{
    [TestFixture]
    public class HttpPostedFileServiceTests
    {
        [Test]
        public void SaveAs_ShouldThrowNullReferenceException_WhenBothArgumentsAreNull()
        {
            HttpPostedFileService service = new HttpPostedFileService();

            Assert.Throws<NullReferenceException>(() => service.SaveAs(null, null));
        }

        [Test]
        public void SaveAs_ShouldThrowNullReferenceException_WhenOnlyHttpPostedFileIsNull()
        {
            HttpPostedFileService service = new HttpPostedFileService();

            Assert.Throws<NullReferenceException>(() => service.SaveAs(null, It.IsAny<string>()));
        }

        [Test]
        public void SaveAs_ShouldThsrowNullReferenceException_WhenOnlyStringFileNameIsNull()
        {
            string expectedErrorMessage = @"The SaveAs method is configured to require a rooted path, and the path '' is not rooted.";
            HttpPostedFile fakeHttpPostedFile = (HttpPostedFile)FormatterServices
                .GetSafeUninitializedObject(typeof(HttpPostedFile));

            HttpPostedFileService service = new HttpPostedFileService();

            HttpException message = Assert.Throws<HttpException>(() => service.SaveAs(fakeHttpPostedFile, null));
            Assert.AreEqual(message.Message, expectedErrorMessage);
        }
    }
}