using System;
using System.Linq.Expressions;
using System.Web;

using AutoMapper;

using NUnit.Framework;
using Moq;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Services.Contracts;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private Mock<IRepository<User>> mockedUserRepository;
        private Mock<INewsData> mockedData;
        private Mock<ICryptographicService> mockedCrypthographicService;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;
        private Mock<IEncryptionProvider> mockedEncryptionProvider;
        private Mock<IAppConfigurationProvider> mockedConfigProvider;

        [SetUp]
        public void Init()
        {
            this.mockedUserRepository = new Mock<IRepository<User>>();
            this.mockedData = new Mock<INewsData>();
            this.mockedCrypthographicService = new Mock<ICryptographicService>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapper = new Mock<IMapper>();
            this.mockedEncryptionProvider = new Mock<IEncryptionProvider>();
            this.mockedConfigProvider = new Mock<IAppConfigurationProvider>();
        }

        [Test]
        public void Cnstructor_WhenNullUserRepositoryIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                null,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object));
            Assert.AreEqual("userRepository", exception.ParamName);
        }

        [Test]
        public void Cnstructor_WhenNullNewsDataIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                this.mockedUserRepository.Object,
                null,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object));
            Assert.AreEqual("newsData", exception.ParamName);
        }

        [Test]
        public void Cnstructor_WhenNullCryptographicServiceIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                null,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object));
            Assert.AreEqual("cryptographicService", exception.ParamName);
        }

        [Test]
        public void Cnstructor_WhenNullMapperProviderIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                null,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [Test]
        public void Cnstructor_WhenNullEncryptionProviderIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                null,
                this.mockedConfigProvider.Object));
            Assert.AreEqual("encryptionProvider", exception.ParamName);
        }

        [Test]
        public void Cnstructor_WhenNullConfigProviderIsPassedArgumentNullExceptionShouldBeCalled()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                null));
            Assert.AreEqual("configProvider", exception.ParamName);
        }

        [Test]
        public void Register_IfThePassedUserWebModelIsNullArgumentNullExceptionShouldBeThrown()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => authService.RegisterUser(null));
            Assert.AreEqual("user", exception.ParamName);
        }

        [Test]
        public void RegisterUser_UserRepositoryGetFirstShouldBeCalledOnce()
        {
            var userModel = new UserWebModel { Username = "username" };

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedUserRepository.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void RegisterUser_IfUserWithProvidedUsernameAlreadyExistsItShouldReturnFalse()
        {
            var userModel = new UserWebModel { Username = "username" };

            this.mockedUserRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isAdded = authService.RegisterUser(userModel);
            Assert.IsFalse(isAdded);
        }

        [Test]
        public void RegisterUser_CryptographicServiceGetSalShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var userModel = new UserWebModel { Username = "username" };

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedCrypthographicService.Verify(x => x.GetSalt(), Times.Once);
        }

        [Test]
        public void RegisterUser_CryptographicServiceByteArrayToStringShouldBeCalledTwiceWhenTheUserDoesNotExist()
        {
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedCrypthographicService.Verify(x => x.ByteArrayToString(It.IsAny<byte[]>()), Times.Exactly(2));
        }

        [Test]
        public void RegisterUser_CryptographicServiceHashPasswordShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var userModel = new UserWebModel { Username = "username", Password = "123456" };
            var salt = new byte[10];

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());
            this.mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedCrypthographicService.Verify(x => x.HashPassword(
                It.Is<string>(a => a == userModel.Password),
                It.Is<byte[]>(a => a == salt)), Times.Once);
        }

        [Test]
        public void RegisterUser_MapperProviderInstanceMapShouldBeCalledWithTheUserModelWhenTheUserDoesNotExist()
        {
            var userModel = new UserWebModel { Username = "username", Password = "123456" };
            var salt = new byte[10];

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());
            mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedMapper.Verify(x => x.Map<User>(It.Is<UserWebModel>(u => u.Username == userModel.Username)), Times.Once);

        }

        [Test]
        public void RegisterUser_UserRepositoryAddShouldBeCalledOnceWithTheNewUserWhenTheUserDoesNotExist()
        {
            var salt = new byte[10];
            var passHash = new byte[10];
            var saltString = Convert.ToBase64String(salt);
            var passHashString = Convert.ToBase64String(passHash);
            var userModel = new UserWebModel { Username = "username", Password = "123456", Salt = Convert.ToBase64String(salt) };
            var newUser = new User { Username = "username", PassHash = passHashString, Salt = saltString };

            this.mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passHash);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(newUser);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedUserRepository.Verify(x => x.Add(It.Is<User>(u =>
                u.Username == newUser.Username &&
                u.Salt == saltString &&
                u.PassHash == passHashString)),
                Times.Once);
        }

        [Test]
        public void RegisterUser_NewsDataCommitShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var userModel = new UserWebModel { Username = "username" };

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.RegisterUser(userModel);
            this.mockedData.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void RegisterUser_ShouldReturnTrueWhenTheNewUserIsAdded()
        {
            var userModel = new UserWebModel { Username = "username" };

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isAdded = authService.RegisterUser(userModel);
            Assert.IsTrue(isAdded);
        }
        
        [TestCase(null)]
        [TestCase("")]
        public void LoginUser_IfThePassedUsernameIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string username)
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => authService.LoginUser(username, "123"));
            Assert.AreEqual("username", exception.ParamName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void LoginUser_IfThePassedPasswordIsNullOrEmptyArgumentNullExceptionShouldBeThrown(string password)
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => authService.LoginUser("username", password));
            Assert.AreEqual("password", exception.ParamName);
        }

        [Test]
        public void LoginUser_ShouldCallUserRepositoryGetFirstOnce()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.LoginUser("username", "123456");
            this.mockedUserRepository.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void LoginUser_ShouldReturnNullWhenTheUserDoesNotExists()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);
            var user = authService.LoginUser("username", "123456");

            Assert.AreEqual(null, user);
        }

        [Test]
        public void LoginUser_ShouldCallCrypthographicServiceIsValidPassword()
        {
            string username = "username";
            string password = "123456";

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<UserWebModel>(It.IsAny<User>())).Returns(new UserWebModel());
            this.mockedUserRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            this.mockedCrypthographicService
                .Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.LoginUser(username, password);
            this.mockedCrypthographicService.Verify(x => x.IsValidPassword(
                    It.Is<string>(a => a == password),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void LoginUser_ShouldReturnNullWhenThePasswordDoesNotMatch()
        {
            string username = "username";
            string password = "123456";

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<UserWebModel>(It.IsAny<User>())).Returns(new UserWebModel());
            this.mockedUserRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            this.mockedCrypthographicService
                .Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);
            var user = authService.LoginUser(username, password);

            Assert.AreEqual(null, user);
        }

        [Test]
        public void LoginUser_MapperProviderInstanceMapShouldBeCalledOnce()
        {
            string username = "username";
            string password = "123456";

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<UserWebModel>(It.IsAny<User>())).Returns(new UserWebModel());
            this.mockedUserRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            this.mockedCrypthographicService
                .Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);
            authService.LoginUser(username, password);

            this.mockedMapper.Verify(x => x.Map<UserWebModel>(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void LoginUser_ShouldReturnTheLoggedInUser()
        {
            string username = "username";
            string password = "123456";
            var resultUser = new UserWebModel();

            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            this.mockedMapper.Setup(x => x.Map<UserWebModel>(It.IsAny<User>())).Returns(resultUser);
            this.mockedUserRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            this.mockedCrypthographicService
                .Setup(x => x.IsValidPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);
            var user = authService.LoginUser(username, password);

            Assert.AreEqual(resultUser, user);
        }

        [Test]
        public void IsUserLoggedIn_IfThePassedCookiesParamIsNullOrEmptyArgumentNullExceptionShouldBeThrown()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => authService.IsUserLoggedIn(null));
            Assert.AreEqual("cookies", exception.ParamName);
        }

        [Test]
        public void IsUserLoggedIn_ShouldReturnFalseWhenTheCookieDoesNotExist()
        {
            var cookieCollection = new HttpCookieCollection();
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isUserLoggedIn = authService.IsUserLoggedIn(cookieCollection);
            Assert.IsFalse(isUserLoggedIn);
        }

        [Test]
        public void IsUserLoggedIn_ShouldReturnFalseWhenTheCookieIsEmpty()
        {
            var cookieCollection = new HttpCookieCollection();
            var cookie = new HttpCookie("aaaaa");

            cookieCollection.Add(cookie);
            this.mockedEncryptionProvider
                .Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedEncryptionProvider
                .Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedConfigProvider.SetupGet(x => x.AuthCookieName).Returns("aaa");
            this.mockedConfigProvider.SetupGet(x => x.EncryptionKey).Returns("aaa");

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isUserLoggedIn = authService.IsUserLoggedIn(cookieCollection);
            Assert.IsFalse(isUserLoggedIn);
        }

        [Test]
        public void IsUserLoggedIn_ShouldReturnTrueWhenTheCookieIsValid()
        {
            var cookieCollection = new HttpCookieCollection();
            var cookie = new HttpCookie("aaa");

            cookie["Id"] = "Id";
            cookie["Username"] = "Username";

            cookieCollection.Add(cookie);
            this.mockedEncryptionProvider
                .Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedEncryptionProvider
                .Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedConfigProvider.SetupGet(x => x.AuthCookieName).Returns("aaa");
            this.mockedConfigProvider.SetupGet(x => x.EncryptionKey).Returns("aaa");

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isUserLoggedIn = authService.IsUserLoggedIn(cookieCollection);
            Assert.IsTrue(isUserLoggedIn);
        }

        [Test]
        public void IsUserLoggedIn_ShouldReturnFalseWhenTheCookieHasInvalidParamaters()
        {
            var cookieCollection = new HttpCookieCollection();
            var cookie = new HttpCookie("aaaaa");

            cookieCollection.Add(cookie);
            this.mockedEncryptionProvider
                .Setup(x => x.Encrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedEncryptionProvider
                .Setup(x => x.Decrypt(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string a, string b) => a);
            this.mockedConfigProvider.SetupGet(x => x.AuthCookieName).Returns("aaaaa");
            this.mockedConfigProvider.SetupGet(x => x.EncryptionKey).Returns("aaaaa");

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            bool isUserLoggedIn = authService.IsUserLoggedIn(cookieCollection);
            Assert.IsFalse(isUserLoggedIn);
        }

        [Test]
        public void LogoutUser_IfThePassedCookiesParamIsNullOrEmptyArgumentNullExceptionShouldBeThrown()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var exception = Assert.Throws<ArgumentNullException>(() => authService.LogoutUser(null));
            Assert.AreEqual("cookies", exception.ParamName);
        }

        [Test]
        public void LogoutUser_ShouldSetFoundCookieExpireToDateNow()
        {
            this.mockedConfigProvider.Setup(x => x.AuthCookieName).Returns("aaaaaa");

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var cookieName = this.mockedConfigProvider.Object.AuthCookieName;
            var cookieCollection = new HttpCookieCollection();
            var cookie = new HttpCookie(cookieName);
            cookieCollection.Add(cookie);

            authService.LogoutUser(cookieCollection);

            Assert.AreEqual(cookie.Expires.Day, DateTime.Now.Day);
            Assert.AreEqual(cookie.Expires.Month, DateTime.Now.Month);
            Assert.AreEqual(cookie.Expires.Year, DateTime.Now.Year);
            Assert.AreEqual(cookie.Expires.Hour, DateTime.Now.Hour);
            Assert.AreEqual(cookie.Expires.Minute, DateTime.Now.Minute);
            Assert.AreEqual(cookie.Expires.Second, DateTime.Now.Second);
        }

        [Test]
        public void LogoutUser_ShouldNotThrowWhenCookieIsNull()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            var cookieCollection = new HttpCookieCollection();

            Assert.DoesNotThrow(() => authService.LogoutUser(cookieCollection));
        }

        [Test]
        public void SeedAdminUser_UserRepositoryGetFirstShouldBeCalled()
        {
            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedUserRepository
                .Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void SeedAdminUser_CryptographicServiceGetSaltShouldBeCalledWhenAdminUserIsNull()
        {
            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedCrypthographicService.Verify(x => x.GetSalt(), Times.Once);
        }

        [Test]
        public void SeedAdminUser_CryptographicServiceHashPasswordShouldBeCalledWhenAdminUserIsNullWithCorrectParams()
        {
            string adminPassword = "aaa";
            byte[] salt = new byte[10];

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(salt);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedConfigProvider.SetupGet(x => x.AdminPassword).Returns(adminPassword);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedCrypthographicService
                .Verify(x => x.HashPassword(
                        It.Is<string>(a => a == adminPassword),
                        It.Is<byte[]>(a => a == salt)),
                    Times.Once);
        }

        [Test]
        public void SeedAdminUser_CryptographicServiceByteArrayToStringShouldBeCalledWhenAdminUserIsNullWithSalt()
        {
            string adminPassword = "aaa";
            byte[] salt = new byte[10];

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(salt);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedConfigProvider.SetupGet(x => x.AdminPassword).Returns(adminPassword);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedCrypthographicService.Verify(x => x.ByteArrayToString(It.Is<byte[]>(a => a == salt)), Times.Once);
        }

        [Test]
        public void SeedAdminUser_CryptographicServiceByteArrayToStringShouldBeCalledWhenAdminUserIsNullWithPassHash()
        {
            string adminPassword = "aaa";
            byte[] passHash = new byte[10];

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passHash);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedConfigProvider.SetupGet(x => x.AdminPassword).Returns(adminPassword);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedCrypthographicService.Verify(x => x.ByteArrayToString(It.Is<byte[]>(a => a == passHash)), Times.Once);
        }

        [Test]
        public void SeedAdminUser_UserRepositoryAddShouldBeCalled()
        {
            string adminPassword = "aaa";
            byte[] passHash = new byte[10];

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passHash);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedConfigProvider.SetupGet(x => x.AdminPassword).Returns(adminPassword);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedUserRepository.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void SeedAdminUser_NewsDataCommitShouldBeCalled()
        {
            string adminPassword = "aaa";
            byte[] passHash = new byte[10];

            this.mockedUserRepository
                .Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns<User>(null);
            this.mockedCrypthographicService
                .Setup(x => x.GetSalt())
                .Returns(new byte[10]);
            this.mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passHash);
            this.mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            this.mockedConfigProvider.SetupGet(x => x.AdminPassword).Returns(adminPassword);

            var authService = new AuthService(
                this.mockedUserRepository.Object,
                this.mockedData.Object,
                this.mockedCrypthographicService.Object,
                this.mockedMapperProvider.Object,
                this.mockedEncryptionProvider.Object,
                this.mockedConfigProvider.Object);

            authService.SeedAdminUser();
            this.mockedData.Verify(x => x.Commit(), Times.Once);
        }
    }
}
