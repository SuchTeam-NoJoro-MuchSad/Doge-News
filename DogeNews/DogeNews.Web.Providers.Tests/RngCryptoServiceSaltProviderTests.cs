using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class RngCryptoServiceSaltProviderTests
    {
        [Test]
        public void GetBytes_ShouldReturnByteArrayWithRandomValue()
        {
            var bytes = new byte[10];
            var provider = new RngCryptoServiceSaltProvider();

            var result = provider.GetBytes(bytes);
            Assert.IsTrue(result[0] != default(byte));
            Assert.IsTrue(result[result.Length - 1] != default(byte));
        }
    }
}
