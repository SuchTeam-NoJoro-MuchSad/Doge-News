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
    }
}
