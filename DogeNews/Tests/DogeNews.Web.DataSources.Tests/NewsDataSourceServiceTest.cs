using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Web.Models;
using DogeNews.Services.Common.Contracts;

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

        private Mock<IRepository<NewsItem>> newsRepo;
        private Mock<IMapperProvider> mapperProvider;
        private Mock<IMapper> mapper;

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
            this.newsRepo = new Mock<IRepository<NewsItem>>();
            this.mapperProvider = new Mock<IMapperProvider>();
            this.mapper = new Mock<IMapper>();
            newsRepo.SetupGet(x => x.All).Returns(this.newsItems.AsQueryable());
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenNewsItemRepoIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsDataSource(null, mapperProvider.Object));
            Assert.AreEqual("newsItemRepository", exception.ParamName);
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullExceptionWhenMapperProviderIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new NewsDataSource(newsRepo.Object, null));
            Assert.AreEqual("mapperProvider", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateDescending_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByDescending(x => x.CreatedOn, page, 20, false));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateDescending_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByDescending(x => x.CreatedOn, 20, pageSize, false));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void OrderByDateDescending_ShouldReturnCorrectElements()
        {
            this.mapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mapper.Object);

            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var expected = this.newsItems
                .OrderByDescending(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.OrderByDescending(x => x.CreatedOn, 1, 6, false);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateAscending_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByAscending(x => x.CreatedOn, page, 20, false));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void OrderByDateAscending_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.OrderByAscending(x => x.CreatedOn, 20, pageSize, false));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void OrderByDateAscending_ShouldReturnCorrectElements()
        {
            this.mapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mapper.Object);

            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var expected = this.newsItems
                .OrderBy(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.OrderByAscending(x => x.CreatedOn, 1, 6, false);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetPageItems_ShouldThrowArgumentOutOfRangeExceptionWhenPageIsLessThanOrEqualToZero(int page)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPageItems(page, 20, false));

            Assert.AreEqual("page", exception.ParamName);
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void GetPageItems_ShouldThrowArgumentOutOfRangeExceptionWhenPageSizeIsLessThanOrEqualToZero(int pageSize)
        {
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPageItems(20, pageSize, false));

            Assert.AreEqual("pageSize", exception.ParamName);
        }

        [Test]
        public void GetPageItems_ShouldReturnsItemsSortedByDateDescending()
        {
            this.mapper
                .Setup(x => x.Map<NewsWebModel>(It.IsAny<NewsItem>()))
                .Returns<NewsItem>(x => new NewsWebModel { CreatedOn = x.CreatedOn });
            this.mapperProvider
                .SetupGet(x => x.Instance)
                .Returns(mapper.Object);
            
            var service = new NewsDataSource(this.newsRepo.Object, this.mapperProvider.Object);
            var expected = this.newsItems
                .OrderByDescending(x => x.CreatedOn)
                .Take(6)
                .Select(x => new NewsWebModel { CreatedOn = x.CreatedOn })
                .ToList();
            var actual = service.GetPageItems(1, 6, false);

            CollectionAssert.AreEqual(expected, actual, new CreatedOnComparer());
        }
    }
}
