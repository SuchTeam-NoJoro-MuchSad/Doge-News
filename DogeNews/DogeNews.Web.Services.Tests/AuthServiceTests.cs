using System;
using System.Linq.Expressions;

using AutoMapper;

using NUnit.Framework;
using Moq;

using DogeNews.Data.Contracts;
using DogeNews.Web.Services.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class AuthServiceTests
    {
        [Test]
        public void RegisterUser_UserRepositoryGetFirstShouldBeCalledOnce()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedRepository.Verify(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Test]
        public void RegisterUser_IfUserWithProvidedUsernameAlreadyExistsItShouldReturnFalse()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var userModel = new UserWebModel { Username = "username" };

            mockedRepository.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(new User());
            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            bool isAdded = authService.RegisterUser(userModel);
            Assert.IsFalse(isAdded);
        }

        [Test]
        public void RegisterUser_CryptographicServiceGetSalShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedCrypthographicService.Verify(x => x.GetSalt(), Times.Once);
        }

        [Test]
        public void RegisterUser_CryptographicServiceByteArrayToStringShouldBeCalledTwiceWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedCrypthographicService.Verify(x => x.ByteArrayToString(It.IsAny<byte[]>()), Times.Exactly(2));
        }

        [Test]
        public void RegisterUser_CryptographicServiceHashPasswordShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username", Password = "123456" };
            var salt = new byte[10];

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());
            mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedCrypthographicService.Verify(x => x.HashPassword(
                It.Is<string>(a => a == userModel.Password),
                It.Is<byte[]>(a => a == salt)), Times.Once);
        }

        [Test]
        public void RegisterUser_MapperProviderInstanceMapShouldBeCalledWithTheUserModelWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username", Password = "123456" };
            var salt = new byte[10];

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());
            mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedMapper.Verify(x => x.Map<User>(It.Is<UserWebModel>(u => u.Username == userModel.Username)), Times.Once);

        }

        [Test]
        public void RegisterUser_UserRepositoryAddShouldBeCalledOnceWithTheNewUserWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var salt = new byte[10];
            var passHash = new byte[10];
            var saltString = Convert.ToBase64String(salt);
            var passHashString = Convert.ToBase64String(passHash);
            var userModel = new UserWebModel { Username = "username", Password = "123456", Salt = salt };
            var newUser = new User { Username = "username", PassHash = passHashString, Salt = saltString };

            mockedCrypthographicService.Setup(x => x.GetSalt()).Returns(salt);
            mockedCrypthographicService
                .Setup(x => x.HashPassword(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Returns(passHash);
            mockedCrypthographicService
                .Setup(x => x.ByteArrayToString(It.IsAny<byte[]>()))
                .Returns<byte[]>(x => Convert.ToBase64String(x));
            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(newUser);

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedRepository.Verify(x => x.Add(It.Is<User>(u =>
                u.Username == newUser.Username &&
                u.Salt == saltString &&
                u.PassHash == passHashString)),
                Times.Once);
        }

        [Test]
        public void RegisterUser_NewsDataCommitShouldBeCalledOnceWhenTheUserDoesNotExist()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            authService.RegisterUser(userModel);
            mockedData.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void RegisterUser_ShouldReturnTrueWhenTheNewUserIsAdded()
        {
            var mockedRepository = new Mock<IRepository<User>>();
            var mockedData = new Mock<INewsData>();
            var mockedCrypthographicService = new Mock<ICryptographicService>();
            var mockedMapperProvider = new Mock<IMapperProvider>();
            var mockedMapper = new Mock<IMapper>();
            var userModel = new UserWebModel { Username = "username" };

            mockedMapperProvider.SetupGet(x => x.Instance).Returns(mockedMapper.Object);
            mockedMapper.Setup(x => x.Map<User>(It.IsAny<UserWebModel>())).Returns(new User());

            var authService = new AuthService(
                mockedRepository.Object,
                mockedData.Object,
                mockedCrypthographicService.Object,
                mockedMapperProvider.Object);

            bool isAdded = authService.RegisterUser(userModel);
            Assert.IsTrue(isAdded);
        }
    }
}
