using System;
using System.Linq.Expressions;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Web.Providers.Contracts;
using Moq;
using NUnit.Framework;

namespace DogeNews.Web.Services.Tests
{
    [TestFixture]
    public class NewsServiceTests
    {
        [Test]
        public void Add_ShouldCallImageRepositoryAddOnce()
        {
            //Arange
            var testWebNewsModel = new NewsWebModel();

            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };
            var mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);

            var mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            var mockImageRepo = new Mock<IRepository<Image>>();

            var mockNewsData = new Mock<INewsData>();

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());
            

            var newsService = new NewsService(mockUserRepo.Object,
                mockNewsItemsRepo.Object,
                mockNewsData.Object,
                mockMapperProvider.Object,
                mockImageRepo.Object);

            //Act
            newsService.Add(testUsername, testWebNewsModel);

            //Assert
            mockImageRepo.Verify(x=>x.Add(It.IsAny<Image>()),Times.Once);
        }

        [Test]
        public void Add_ShouldCallNewsRepositoryAddOnce()
        {
            //Arange
            var testWebNewsModel = new NewsWebModel();

            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };
            var mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);

            var mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            var mockImageRepo = new Mock<IRepository<Image>>();

            var mockNewsData = new Mock<INewsData>();

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());


            var newsService = new NewsService(mockUserRepo.Object,
                mockNewsItemsRepo.Object,
                mockNewsData.Object,
                mockMapperProvider.Object,
                mockImageRepo.Object);

            //Act
            newsService.Add(testUsername, testWebNewsModel);

            //Assert
            mockNewsItemsRepo.Verify(x => x.Add(It.IsAny<NewsItem>()), Times.Once);
        }

        [Test]
        public void Add_ShouldCallUnitOfWorkCommitOnce()
        {
            //Arange
            var testWebNewsModel = new NewsWebModel();

            var testUsername = "junka";
            var testUser = new User { Username = testUsername, Id = 1 };
            var mockUserRepo = new Mock<IRepository<User>>();
            mockUserRepo.Setup(x => x.GetFirst(It.IsAny<Expression<Func<User, bool>>>())).Returns(testUser);

            var mockNewsItemsRepo = new Mock<IRepository<NewsItem>>();
            var mockImageRepo = new Mock<IRepository<Image>>();

            var mockNewsData = new Mock<INewsData>();

            var mockMapperProvider = new Mock<IMapperProvider>();
            mockMapperProvider.Setup(x => x.Instance.Map<Image>(It.IsAny<NewsWebModel>())).Returns(new Image());
            mockMapperProvider.Setup(x => x.Instance.Map<NewsItem>(It.IsAny<NewsWebModel>())).Returns(new NewsItem());


            var newsService = new NewsService(mockUserRepo.Object,
                mockNewsItemsRepo.Object,
                mockNewsData.Object,
                mockMapperProvider.Object,
                mockImageRepo.Object);

            //Act
            newsService.Add(testUsername, testWebNewsModel);

            //Assert
            mockNewsData.Verify(x => x.Commit(), Times.Once);
        }
    }
}