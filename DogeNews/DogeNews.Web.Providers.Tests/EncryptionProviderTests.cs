using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Encryption;

using NUnit.Framework;
using System;

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

        [TestCase(null)]
        [TestCase("")]
        public void Encrypt_ShouldThrowArgumentNullExceptionWhenTextIsNullOrEmpty(string text)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.encryptionProvider.Encrypt(text, "key"));
            Assert.AreEqual("text", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Encrypt_ShouldThrowArgumentNullExceptionWhenKeyIsNullOrEmpty(string key)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.encryptionProvider.Encrypt("text", key));
            Assert.AreEqual("key", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Decrypt_ShouldThrowArgumentNullExceptionWhenTextIsNullOrEmpty(string text)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.encryptionProvider.Encrypt(text, "key"));
            Assert.AreEqual("text", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Decrypt_ShouldThrowArgumentNullExceptionWhenKeyIsNullOrEmpty(string key)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => this.encryptionProvider.Encrypt("text", key));
            Assert.AreEqual("key", exception.ParamName);
        }
    }
}