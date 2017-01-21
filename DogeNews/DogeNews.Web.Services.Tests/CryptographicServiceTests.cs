using System;

using NUnit.Framework;
using Moq;

using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class CryptographicServiceTests
    {
        [Test]
        public void GetSalt_ShouldCallSaltServiceGetBytesWithEmptySaltArrayWithSize251()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            cryptographicService.GetSalt();
            mockedSaltProvider.Verify(x => x.GetBytes(It.Is<byte[]>(a => a.Length == 23)), Times.Once);
        }

        [Test]
        public void GetSalt_ShouldReturnSaltByteArray()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
            var saltByteArray = new byte[23];

            mockedSaltProvider.Setup(x => x.GetBytes(It.IsAny<byte[]>())).Returns(saltByteArray);

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            var actualSaltByteArray = cryptographicService.GetSalt();
            Assert.AreEqual(saltByteArray, actualSaltByteArray);
        }

        [Test]
        public void HashPassword_ShouldCallHashProviderGetHashBytesWithPasswordSaltIterationsCountAndHashSize()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
            string password = "123456";
            var salt = new byte[23];

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            cryptographicService.HashPassword(password, salt);
            mockedHashProvider.Verify(x => x.GetHashBytes(
                    It.Is<string>(a => a == password),
                    It.Is<byte[]>(a => a == salt),
                    It.Is<int>(a => a == 10000),
                    It.Is<int>(a => a == 29)),
                Times.Once);
        }

        [Test]
        public void HashPassword_ShouldReturnHashedPasswordByteArray()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
            var passHashByteArray = new byte[23];
            var salt = new byte[23];

            mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(passHashByteArray);

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);
            var actualPassHashByteArray = cryptographicService.HashPassword(string.Empty, salt);

            Assert.AreEqual(passHashByteArray, actualPassHashByteArray);
        }

        [Test]
        public void ByteArrayToString_ShouldReturnBase64StringOfThePassedByteArray()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
            var byteArray = new byte[10];

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            var expected = Convert.ToBase64String(byteArray);
            var actual = cryptographicService.ByteArrayToString(byteArray);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsValidPassword_HashProviderGetHashBytesShouldBeCalledOnec()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
            string password = "123456";
            string passHashString = Convert.ToBase64String(new byte[257]);
            string saltString = Convert.ToBase64String(new byte[10]);
            byte[] saltBytes = Convert.FromBase64String(saltString);

            mockedHashProvider.Setup(x => x.GetHashBytes(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(new byte[29]);

            var cryptographicService = new CryptographicService(
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            cryptographicService.IsValidPassword(password, passHashString, saltString);
            mockedHashProvider.Verify(x => x.GetHashBytes(
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
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
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
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            bool isValidPassword = cryptographicService.IsValidPassword(password, passHashString, saltString);
            Assert.IsFalse(isValidPassword);
        }

        [Test]
        public void IsValidPassword_ShouldReturnTrueWhenThePasswordDoesNotMatch()
        {
            var mockedSaltProvider = new Mock<ICryptoServiceSaltProvider>();
            var mockedHashProvider = new Mock<ICryptoServiceHashProvider>();
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
                mockedSaltProvider.Object,
                mockedHashProvider.Object);

            bool isValidPassword = cryptographicService.IsValidPassword(password, passHashString, saltString);
            Assert.IsTrue(isValidPassword);
        }
    }
}