using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Encryption;

using NUnit.Framework;

namespace DogeNews.Web.Providers.Tests
{
    [TestFixture]
    public class EncryptionProviderTests
    {
        private IEncryptionProvider encryptionProvider;

        [SetUp]
        public void Init()
        {
            this.encryptionProvider = new EncryptionProvider();
        }

        [Test]
        public void Encrypt_ShouldReturnStringWithCorrectLength()
        {
            string text = "aaa";
            string key = "bbb";

            int expected = 128;
            int actual = this.encryptionProvider.Encrypt(text, key).Length;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Decrypt_ShouldReturnCorrectDecryptedString()
        {
            string text = "wPQ4UYvEcuGF60jfCdSmsz + kDvkcp357JajRWXnktGPA9DhRi8Ry4YXrSN8J1KazP6QO + RynfnslqNFZeeS0Y77lbBKk0SyBtkrgpgJmX / VNYgdIkVOlH6Qupo42i + m5";
            string key = "bbb";
            
            string expected = "aaa";
            string actual = this.encryptionProvider.Decrypt(text, key);

            Assert.AreEqual(expected, actual);
        }
    }
}