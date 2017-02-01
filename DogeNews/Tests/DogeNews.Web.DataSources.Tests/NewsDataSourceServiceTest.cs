using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Providers.Contracts;
using DogeNews.Web.Models;

using NUnit.Framework;
using Moq;
using AutoMapper;

namespace DogeNews.Web.DataSources.Tests
{
    [TestFixture]
    public class NewsDataSourceServiceTest
    {
        private class CreatedOnComparer : IComparer
        {
            public int Compare(object x, object y)
            {
                var a = x as NewsWebModel;
                var b = x as NewsWebModel;

                if (a.CreatedOn < b.CreatedOn)
                {
                    return -1;
                }
                else if (a.CreatedOn > b.CreatedOn)
                {
                    return 1;
                }

                return 0;
            }
        }

        private Mock<IRepository<NewsItem>> mockedNewsRepo;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;
        private IEnumerable<NewsItem> newsItems = new List<NewsItem>
        {
            new NewsItem { CreatedOn = DateTime.Now },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(5) },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(2) },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(6) },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(4) },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(1) },
            new NewsItem { CreatedOn = DateTime.Now.AddMinutes(3) },
        };

        [SetUp]
        public void Init()
        {
            this.mockedNewsRepo = new Mock<IRepository<NewsItem>>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapper = new Mock<IMapper>();
            mockedNewsRepo.SetupGet(x => x.All).Returns(this.newsItems.AsQueryable());
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenNewsItemRepoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsDataSource(null, mockedMapperProvider.Object));
            Assert.AreEqual("newsItemRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenMapperProviderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsDataSource(mockedNewsRepo.Object, null));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateDescending_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByDescending(x => x.CreatedOn, page, 20));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateDescending_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByDescending(x => x.CreatedOn, 20, pageSize));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void OrderByDateDescending_ShouldReturnCorrectElements()
        {
            this.mockedMapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mockedMapper.Object);

            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var expected = this.newsItems
                .OrderByDescending(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.OrderByDescending(x => x.CreatedOn, 1, 6);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateAscending_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByAscending(x => x.CreatedOn, page, 20));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateAscending_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByAscending(x => x.CreatedOn, 20, pageSize));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void OrderByDateAscending_ShouldReturnCorrectElements()
        {
            this.mockedMapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mockedMapper.Object);

            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var expected = this.newsItems
                .OrderBy(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.OrderByAscending(x => x.CreatedOn, 1, 6);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetPageItems_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPageItems(page, 20));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetPageItems_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPageItems(20, pageSize));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void GetPageItems_ShouldReturnsItemsSortedByDateDescending()
        {
            this.mockedMapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mockedMapper.Object);

            var service = new NewsDataSource(this.mockedNewsRepo.Object, this.mockedMapperProvider.Object);
            var expected = this.newsItems
                .OrderByDescending(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.GetPageItems(1, 6);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }
    }
}
