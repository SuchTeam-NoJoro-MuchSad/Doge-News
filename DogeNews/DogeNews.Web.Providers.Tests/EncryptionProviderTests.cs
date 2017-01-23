using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Providers.Encryption;

using NUnit.Framework;
using System;
using System.Text;
using System.Web.Security;

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

        [Test]
        public void Encrypt_ShouldReturnCorrectlyEncryptedString()
        {
            string text = "text";
            string key = "key";
            byte[] encryptedTextBytes = MachineKey.Protect(Encoding.UTF8.GetBytes(text), key);

            string expected = Convert.ToBase64String(encryptedTextBytes);
            string actual = this.encryptionProvider.Encrypt(text, key);

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

        [Test]
        public void Decrypt_ShouldReturnCorrectlyDecryptedString()
        {
            string text = "text";
            string key = "key";
            byte[] encryptedTextBytes = MachineKey.Protect(Encoding.UTF8.GetBytes(text), key);
            string encryptedText = Convert.ToBase64String(encryptedTextBytes);
            byte[] decryptedBytes = MachineKey.Unprotect(Convert.FromBase64String(encryptedText), key);
            
            string expected = Encoding.UTF8.GetString(decryptedBytes);
            string actual = this.encryptionProvider.Decrypt(encryptedText, key);

            Assert.AreEqual(expected, actual);
        }
    }
}