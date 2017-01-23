using System;

using NUnit.Framework;
using Moq;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Common;

namespace DogeNews.Web.Providers.Tests
{
    public class FileProviderTests
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
                new FileProvider(null);
            });

            Assert.AreEqual("dateTimeProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldNotThrowWhenIDateTimeProviderParamIsNotNull()
        {
            Assert.DoesNotThrow(() => new FileProvider(this.mockedDateTimeProvider.Object));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetUniqueFileName_ShouldThrowArgumentNullException_WhenUsernameParamIsNull(string value)
        {
            var fileProvider = new FileProvider(this.mockedDateTimeProvider.Object);;
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

            var fileNameProvider = new FileProvider(mockedDateTimeProvider.Object);
            string fileName = fileNameProvider.GetUniqueFileName(username);

            Assert.IsTrue(fileName.StartsWith(username));
            Assert.IsTrue(fileName.EndsWith(dateString));
        }
    }
}
