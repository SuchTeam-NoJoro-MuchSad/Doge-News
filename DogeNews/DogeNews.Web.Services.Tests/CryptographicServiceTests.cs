using System;

using NUnit.Framework;
using Moq;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class CryptographicServiceTests
    {
        private Mock<ICryptoServiceSaltProvider> mockedSaltProvider;
        private Mock<ICryptoServiceHashProvider> mockedHashProvider;

        [SetUp]
        public void Init()
        {
            this.mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            this.mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
        }

        [Test]
        public void Constructor_WhenSaltProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert
                .Throws<ArgumentNullException>(() => new CryptographicService(null, this.mockedHashProvider.Object));
            Assert.AreEqual("saltProvider", exception.ParamName);
        }

        [Test]
        public void Constructor_WhenHashProviderIsNullArgumentNullExceptionShouldBeThrown()
        {
            var exception = Assert
                .Throws<ArgumentNullException>(() => new CryptographicService(this.mockedSaltProvider.Object, null));
            Assert.AreEqual("hashProvider", exception.ParamName);
        }

        [Test]
        public void GetSalt_ShouldCallSaltServiceGetBytesWithEmptySaltArrayWithSize251()
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            cryptographicService.GetSalt();
            this.mockedSaltProvider.Verify(x => x.GetBytes(It.Is<byte[]>(a => a.Length == 23)), Times.Once);
        }

        [Test]
        public void GetSalt_ShouldReturnSaltByteArray()
        {
            var saltByteArray = new byte[23];

            this.mockedSaltProvider.Setup(x => x.GetBytes(It.IsAny<byte[]>())).Returns(saltByteArray);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var actualSaltByteArray = cryptographicService.GetSalt();
            Assert.AreEqual(saltByteArray, actualSaltByteArray);
        }

        [TestCase(null)]
        [TestCase("")]
        public void HashPassword_IfThePassedPasswordIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string password)
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert
                .Throws<ArgumentNullException>(() => cryptographicService.HashPassword(password, new byte[10]));
            Assert.AreEqual("password", exception.ParamName);
        }

        [Test]
        public void HashPassword_IfThePassedSaltIsNullArgumentNullExceptionShouldBeThrown()
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert
                .Throws<ArgumentNullException>(() => cryptographicService.HashPassword("123", null));
            Assert.AreEqual("salt", exception.ParamName);
        }

        [Test]
        public void HashPassword_ShouldCallHashProviderGetHashBytesWithPasswordSaltIterationsCountAndHashSize()
        {
            string password = "123456";
            var salt = new byte[23];

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            cryptographicService.HashPassword(password, salt);
            this.mockedHashProvider.Verify(x => x.GetHashBytes(
                    It.Is<string>(a => a == password),
                    It.Is<byte[]>(a => a == salt),
                    It.Is<int>(a => a == 10000),
                    It.Is<int>(a => a == 29)),
                Times.Once);
        }

        [Test]
        public void HashPassword_ShouldReturnHashedPasswordByteArray()
        {
            var passHashByteArray = new byte[23];
            var salt = new byte[23];

            this.mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(passHashByteArray);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);
            var actualPassHashByteArray = cryptographicService.HashPassword("pass", salt);

            Assert.AreEqual(passHashByteArray.Length, actualPassHashByteArray.Length);
        }

        [Test]
        public void HashPassword_IfThePassedArrayIsNullArgumentNullExceptionShouldBeThrown()
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert
                .Throws<ArgumentNullException>(() => cryptographicService.ByteArrayToString(null));
            Assert.AreEqual("array", exception.ParamName);
        }

        [Test]
        public void ByteArrayToString_ShouldReturnBase64StringOfThePassedByteArray()
        {
            var byteArray = new byte[10];

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var expected = Convert.ToBase64String(byteArray);
            var actual = cryptographicService.ByteArrayToString(byteArray);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(null)]
        [TestCase("")]
        public void IsValidPassword_IfThePassedPasswordToCheckIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string passwordToCheck)
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() =>
            cryptographicService.IsValidPassword(passwordToCheck, "passhash", "salt"));
            Assert.AreEqual("passwordToCheck", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void IsValidPassword_IfThePassedUserPassHashToCheckIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string userPassHash)
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() =>
            cryptographicService.IsValidPassword("password", userPassHash, "salt"));
            Assert.AreEqual("userPassHash", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void IsValidPassword_IfThePassedUserSaltToCheckIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string userSalt)
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() =>
            cryptographicService.IsValidPassword("password", "passHash", userSalt));
            Assert.AreEqual("userSalt", exception.ParamName);
        }

        [Test]
        public void IsValidPassword_HashProviderGetHashBytesShouldBeCalledOnec()
        {
            string password = "123456";
            string passHashString = Convert.ToBase64String(new byte[257]);
            string saltString = Convert.ToBase64String(new byte[10]);
            byte[] saltBytes = Convert.FromBase64String(saltString);

            this.mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(new byte[29]);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            cryptographicService.IsValidPassword(password, passHashString, saltString);
            this.mockedHashProvider.Verify(x => x.GetHashBytes(
                    It.Is<string>(a => a == password),
                    It.Is<byte[]>(a => a.Length == saltBytes.Length),
                    It.Is<int>(a => a == 10000),
                    It.Is<int>(a => a == 29)
                ),
                Times.Once);
        }

        [Test]
        public void IsValidPassword_ShouldReturnFalseWhenThePasswordDoesNotMatch()
        {
            string password = "123456";
            var passHash = new byte[29];
            passHash[0] = (byte)1; // This will make the to hash arrays different
            string passHashString = Convert.ToBase64String(passHash);
            string saltString = Convert.ToBase64String(new byte[10]);
            byte[] saltBytes = Convert.FromBase64String(saltString);

            mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(new byte[29]);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            bool isValidPassword = cryptographicService.IsValidPassword(password, passHashString, saltString);
            Assert.IsFalse(isValidPassword);
        }

        [Test]
        public void IsValidPassword_ShouldReturnTrueWhenThePasswordDoesNotMatch()
        {
            string password = "123456";
            var passHash = new byte[29];
            string passHashString = Convert.ToBase64String(passHash);
            string saltString = Convert.ToBase64String(new byte[10]);
            byte[] saltBytes = Convert.FromBase64String(saltString);

            mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(new byte[29]);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            bool isValidPassword = cryptographicService.IsValidPassword(password, passHashString, saltString);
            Assert.IsTrue(isValidPassword);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Base64StringToByteArray_ShouldThrowArgumentNullExceptionWhenThePassedStringIsNull(string str)
        {
            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var exception = Assert
                .Throws<ArgumentNullException>(() => cryptographicService.Base64StringToByteArray(str));
            Assert.AreEqual("base64string", exception.ParamName);
        }


        [Test]
        public void Base64StringToByteArray_ShouldReturnCorrectByteArray()
        {
            var str = Convert.ToBase64String(new byte[20]);
            var bytes = Convert.FromBase64String(str);

            var cryptographicService = new CryptographicService(
                this.mockedSaltProvider.Object,
                this.mockedHashProvider.Object);

            var result = cryptographicService.Base64StringToByteArray(str);
            Assert.AreEqual(bytes, result);
        }
    }
}