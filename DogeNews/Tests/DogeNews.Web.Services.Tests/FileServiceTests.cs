using System;

using DogeNews.Web.Providers.Contracts;

using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class FileServiceTests
    {
        private Mock<IDateTimeProvider> mockedDateTimeProvider;

        [SetUp]
        public void Init()
        {
            this.mockedDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Test]
        public void Constructor_ShouldThrowArgmentNullExeption_WhenIDateTimeProviderParamIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new FileService(null);
            });

            Assert.AreEqual("dateTimeProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldNotThrowWhenIDateTimeProviderParamIsNotNull()
        {
            Assert.DoesNotThrow(() => new FileService(this.mockedDateTimeProvider.Object));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetUniqueFileName_ShouldThrowArgumentNullException_WhenUsernameParamIsNull(string value)
        {
            var fileProvider = new FileService(this.mockedDateTimeProvider.Object); ;
            var exeption = Assert.Throws<ArgumentNullException>(() =>
            {
                fileProvider.GetUniqueFileName(value);
            });

            Assert.AreEqual("username", exeption.ParamName);
        }

        [Test]
        public void GetUniqueFileName_ShouldReturnStringBeginningWithUsernameAndEndingWithDateTimeNow()
        {
            var now = DateTime.Now;
            string username = "username";
            string dateString = now.ToString()
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('/', '-');

            mockedDateTimeProvider.SetupGet(x => x.Now).Returns(now);

            var fileNameProvider = new FileService(mockedDateTimeProvider.Object);
            string fileName = fileNameProvider.GetUniqueFileName(username);

            Assert.IsTrue(fileName.StartsWith(username));
            Assert.IsTrue(fileName.EndsWith(dateString));
        }
    }
}
