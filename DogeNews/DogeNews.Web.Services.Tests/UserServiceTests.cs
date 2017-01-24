using System;

using DogeNews.Data.Models;
using DogeNews.Data.Contracts;
using DogeNews.Web.Services.Contracts;

using NUnit.Framework;
using Moq;
using System.Linq.Expressions;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IRepository<User>> mockedUserRepository;
        private Mock<ICryptographicService> mockedCryptographicService;
        private Mock<INewsData> mockedNewsData;

        [SetUp]
        public void Init()
        {
            this.mockedCryptographicService = new Mock<ICryptographicService>();
            this.mockedNewsData = new Mock<INewsData>();
            this.mockedUserRepository = new Mock<IRepository<User>>();
        }

        [Test]
        public void Constructor_WhenUserRepositoryIsNullArgumentNullExceptionsShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(
                    null,
                    this.mockedCryptographicService.Object,
                    this.mockedNewsData.Object));
            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_WhenCryptographicServiceIsNullArgumentNullExceptionsShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(
                    this.mockedUserRepository.Object,
                    null,
                    this.mockedNewsData.Object));
            Assert.AreEqual("cryptographicService", exception.ParamName);
        }

        [Test]
        public void Constructor_WhenNewsDataIsNullArgumentNullExceptionsShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(
                    this.mockedUserRepository.Object,
                    this.mockedCryptographicService.Object,
                    null));
            Assert.AreEqual("newsData", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ChangePassword_WhenUsernameIsNullOrEmptyArgumentNullExceptionsShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(
                    this.mockedUserRepository.Object,
                    this.mockedCryptographicService.Object,
                    this.mockedNewsData.Object));
            Assert.AreEqual("username", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void ChangePassword_WhenPasswordIsNullOrEmptyArgumentNullExceptionsShouldBeThrown()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new UserService(
                    this.mockedUserRepository.Object,
                    this.mockedCryptographicService.Object,
                    this.mockedNewsData.Object));
            Assert.AreEqual("password", exception.ParamName);
        }

        [Test]
        public void ChangePassword_UserRepositoryGetFirstShouldBeCalledOnce()
        {
            var salt = Convert.ToBase64String(new byte[10]);
            string username = "username";
            string password = "password";

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Salt = salt });

            var userService = new UserService(
                this.mockedUserRepository.Object,
                this.mockedCryptographicService.Object,
                this.mockedNewsData.Object);

            userService.ChangePassword(username, password);
            this.mockedUserRepository
                .Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void ChangePassword_CryptographicServiceBase64StringToByteArrayShouldBeCalledOnceWithUserSalt()
        {
            var saltBytes = new byte[10];
            var salt = Convert.ToBase64String(saltBytes);
            string username = "username";
            string password = "password";

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Salt = salt });

            var userService = new UserService(
                this.mockedUserRepository.Object,
                this.mockedCryptographicService.Object,
                this.mockedNewsData.Object);

            userService.ChangePassword(username, password);
            this.mockedCryptographicService
                .Verify(x => x.Base64StringToByteArray(It.Is<string>(a => a == salt)), Times.Once);
        }

        [Test]
        public void ChangePassword_CryptographicServiceHashPasswordShouldBeCalledOnceWithPasswordAndSalt()
        {
            var saltBytes = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var salt = Convert.ToBase64String(saltBytes);
            string username = "username";
            string password = "password";

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Salt = salt });
            this.mockedCryptographicService
                .Setup(x => x.Base64StringToByteArray(It.IsAny<string>()))
                .Returns(saltBytes);

            var userService = new UserService(
                this.mockedUserRepository.Object,
                this.mockedCryptographicService.Object,
                this.mockedNewsData.Object);

            userService.ChangePassword(username, password);
            this.mockedCryptographicService
                .Verify(x => x.HashPassword(
                        It.Is<string>(a => a == password),
                        It.Is<byte[]>(a => a == saltBytes)),
                    Times.Once);
        }

        [Test]
        public void ChangePassword_NewsDataCommitShouldBeCalledOnce()
        {
            var saltBytes = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var salt = Convert.ToBase64String(saltBytes);
            string username = "username";
            string password = "password";

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new User { Salt = salt });

            var userService = new UserService(
                this.mockedUserRepository.Object,
                this.mockedCryptographicService.Object,
                this.mockedNewsData.Object);

            userService.ChangePassword(username, password);
            this.mockedNewsData.Verify(x => x.Commit(), Times.Once);
        }
    }
}
