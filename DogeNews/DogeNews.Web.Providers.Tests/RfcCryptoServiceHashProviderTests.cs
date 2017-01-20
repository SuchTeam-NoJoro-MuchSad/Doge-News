using Moq;
using NUnit.Framework;
using System.Security.Cryptography;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class RfcCryptoServiceHashProviderTests
    {
        [Test]
        public void GetHashBytes_ShouldReturnProperByteArrayOfTheHasedText()
        {
            string stringToHash = "123";
            byte[] salt = new byte[10];
            int iterationsCount = 10;
            int hashBytesCount = 10;
            
            var provider = new RfcCryptoServiceHashProvider();
            var expected = (new Rfc2898DeriveBytes(stringToHash, salt, iterationsCount)).GetBytes(hashBytesCount);
            var actual = provider.GetHashBytes(stringToHash, salt, iterationsCount, hashBytesCount);

            Assert.AreEqual(expected, actual);
        }
    }
}
