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
        public void Constructor_ShouldNotThrowWhenIDateTimeProviderParamIsNotNull()
        {
            Assert.DoesNotThrow(() => new FileService());
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetUniqueFileName_ShouldThrowArgumentNullException_WhenUsernameParamIsNull(string value)
        {
            var fileProvider = new FileService(); ;
            var exeption = Assert.Throws<ArgumentNullException>(() =>
            {
                fileProvider.GetUniqueFileName(value);
            });

            Assert.AreEqual("username", exeption.ParamName);
        }

        [Test]
        public void GetUniqueFileName_ShouldReturnStringBeginningWithUsername()
        {
            var now = DateTime.Now;
            string username = "username";
            string dateString = now.ToString()
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('/', '-');

            mockedDateTimeProvider.SetupGet(x => x.Now).Returns(now);

            var fileNameProvider = new FileService();
            string fileName = fileNameProvider.GetUniqueFileName(username);

            Assert.IsTrue(fileName.StartsWith(username));
        }
    }
}
