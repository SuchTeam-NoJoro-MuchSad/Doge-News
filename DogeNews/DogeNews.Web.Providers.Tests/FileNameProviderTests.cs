using System;

using NUnit.Framework;
using Moq;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Common;
using System.IO;

namespace DogeNews.Web.Providers.Tests
{
    public class FileNameProviderTests
    {
        [Test]
        public void GetRandomFileName_ShouldReturnStringBeginningWithUsernameAndEndingWithDateTimeNow()
        {
            var mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            var now = DateTime.Now;
            string username = "username";
            string dateString = now.ToString()
                .Replace(' ', '-')
                .Replace(':', '-')
                .Replace('/', '-');
            
            mockedDateTimeProvider.SetupGet(x => x.Now).Returns(now);

            var fileNameProvider = new FileProvider(mockedDateTimeProvider.Object);
            string fileName = fileNameProvider.GetUnique(username);

            Assert.IsTrue(fileName.StartsWith(username));
            Assert.IsTrue(fileName.EndsWith(dateString));
        }
    }
}
