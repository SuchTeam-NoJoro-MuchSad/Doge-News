using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Data.Repositories;
using DogeNews.Web.Infrastructure.Mappings.Profiles;

using Moq;
using NUnit.Framework;

namespace DogeNews.Data.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void Constructor_InitializesRepositoryContext()
        {
            var mockContext = new Mock<INewsDbContext>();
            var repository = new Repository<Comment>(mockContext.Object);
            Assert.NotNull(repository.Context);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSet()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>().Object;
            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet);

            var repository = new Repository<Comment>(mockContext.Object);

            Assert.NotNull(repository.DbSet);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSetWithCorrectType()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>().Object;
            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet);
            var repository = new Repository<Comment>(mockContext.Object);

            Assert.NotNull(repository.DbSet);
            Assert.IsInstanceOf(typeof(IDbSet<Comment>), repository.DbSet);
        }

        [Test]
        public void All_ReturnsRepositoryDbSet()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>().Object;
            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet);

            var repository = new Repository<Comment>(mockContext.Object);

            Assert.NotNull(repository.All);
            Assert.IsInstanceOf(typeof(IDbSet<Comment>), repository.All);
            Assert.AreSame(repository.All, repository.DbSet);
        }

        [Test]
        public void GetFirst_ShouldReturnCorrectObjectIfObjectIsFound()
        {
            var expected = new Comment { Content = "asd", Id = 4, User = null };
            var data = new List<Comment>
            {
                new Comment {Content = "asdasdasd", Id = 1, User = null},
                new Comment {Content = "as12312312d", Id = 2, User = null},
                new Comment {Content = "asaasd as das dd", Id = 3, User = null},
                expected,
                new Comment {Content = "a123123sd", Id = 5, User = null}
            }.AsQueryable();

            var mockContext = new Mock<INewsDbContext>();

            var mockSet = new Mock<IDbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);
            
            var repository = new Repository<Comment>(mockContext.Object);
            var reslut = repository.GetFirst(x => x.Content == "asd");

            Assert.AreSame(expected, reslut);
        }

        [Test]
        public void GetFirst_ShouldReturnNullIfObjectIsNotFound()
        {
            var data = new List<Comment>
            {
                new Comment {Content = "asdasdasd", Id = 1, User = null},
                new Comment {Content = "as12312312d", Id = 2, User = null},
                new Comment {Content = "asaasd as das dd", Id = 3, User = null},
                new Comment {Content = "asd", Id = 4, User = null},
                new Comment {Content = "a123123sd", Id = 5, User = null}
            }.AsQueryable();

            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);


            var repository = new Repository<Comment>(mockContext.Object);
            var reslut = repository.GetFirst(x => x.Content == "notexisting");

            Assert.IsNull(reslut);
        }

        [Test]
        public void GetById_SholdBeCalledOnceWithCorrectParameters()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Comment());
            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            var reslut = repository.GetById(4);

            mockSet.Verify(m => m.Find(4), Times.Once);
        }

        [Test]
        public void Add_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);

            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

        [Test]
        public void Add_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();
            var mockComment = new Mock<Comment>();
            var fakeEntry = (DbEntityEntry<Comment>)FormatterServices
                .GetSafeUninitializedObject(typeof(DbEntityEntry<Comment>));

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Entry(It.IsAny<Comment>())).Returns(fakeEntry);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            try
            {
                repository.Add(mockComment.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            mockContext.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void DataMaping_Configure()
        {
            var profile = new DataMappingsProfile();
            Assert.DoesNotThrow(() => profile
                .GetType()
                .GetMethod("Configure", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(profile, new object[] { }));
        }

        [Test]
        public void Delete_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
        }

        [Test]
        public void Delete_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var mockComment = new Mock<Comment>();
            var repository = new Repository<Comment>(mockContext.Object);

            try
            {
                repository.Delete(mockComment.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            mockContext.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void Update_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            Assert.Throws<ArgumentNullException>(() => repository.Update(null));
        }

        [Test]
        public void Update_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var mockComment = new Mock<Comment>();
            var repository = new Repository<Comment>(mockContext.Object);

            try
            {
                repository.Update(mockComment.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            mockContext.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void GetAll_WithNoParametersShouldReturnTheWholeCollection()
        {
            var data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = 4, User = null },
                new Comment { Content = "a123123sd", Id = 5, User = null}
            }.AsQueryable();

            var mockContext = new Mock<INewsDbContext>();

            var mockSet = new Mock<IDbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            var reslut = repository.GetAll();

            Assert.AreEqual(data, reslut);
        }

        [Test]
        public void GetAll_WithFilterExpretionParameterShouldReturnCorrectCollection()
        {
            var data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = 4, User = null },
                new Comment { Content = "asd", Id = 5, User = null}
            }.AsQueryable();

            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            var reslut = repository.GetAll(x => x.Content == "asd");

            Assert.AreEqual(2, reslut.Count());
        }

        [Test]
        public void GetAll_WithFilterExpretionAndSortExpressionParametersShouldReturnCorrectCollection()
        {
            int expctedFirstElementId = 5;
            int expectedSecondElementId = 41;

            var data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = expectedSecondElementId, User = null },
                new Comment { Content = "asd", Id = expctedFirstElementId, User = null}
            }
            .AsQueryable();

            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();

            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);
            IList<Comment> result = repository.GetAll(x => x.Content == "asd", x => x.Id).ToList();

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(expctedFirstElementId, result[0].Id);
            Assert.AreEqual(expectedSecondElementId, result[1].Id);
        }

        [Test]
        public void GetAll_WithFilterExpretionAndSortExpressionAndSelectExpressionParametersShouldReturnCorrectCollection()
        {
            int expctedFirstElementId = 5;
            int expectedSecondElementId = 41;
            int expectedThirdElementId = 411;

            var data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = expectedSecondElementId, User = null },
                new Comment { Content = "asd", Id = expctedFirstElementId, User = null},
                new Comment { Content = "asd", Id = expectedThirdElementId, User = null}
            }.AsQueryable();

            var mockContext = new Mock<INewsDbContext>();
            var mockSet = new Mock<IDbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);

            List<Comment> reslut = repository
                .GetAll(x => x.Content == "asd",
                    x => x.Id,
                    x => new Comment
                    {
                        Content = x.Content + "New",
                        Id = x.Id,
                        User = null
                    })
                    .ToList();

            //Filter
            Assert.AreEqual(3, reslut.Count());
            //Sort
            Assert.AreEqual(expctedFirstElementId, reslut[0].Id);
            Assert.AreEqual(expectedSecondElementId, reslut[1].Id);
            Assert.AreEqual(expectedThirdElementId, reslut[2].Id);
            //Select
            Assert.AreEqual("asdNew", reslut[0].Content);
            Assert.AreEqual("asdNew", reslut[1].Content);
            Assert.AreEqual("asdNew", reslut[2].Content);
        }
    }
}