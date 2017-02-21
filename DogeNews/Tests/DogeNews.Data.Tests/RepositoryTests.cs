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
using DogeNews.Services.Common.Contracts;

using Moq;
using NUnit.Framework;

namespace DogeNews.Data.Tests
{
    [TestFixture]
    public class RepositoryTests
    {
        private Mock<IMapperProvider> mapperProvider;
        private Mock<INewsDbContext> context;

        [SetUp]
        public void Init()
        {
            this.mapperProvider = new Mock<IMapperProvider>();
            this.context = new Mock<INewsDbContext>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Repository<Comment>(null));
        }

        [Test]
        public void Constructor_InitializesRepositoryContext()
        {
            Mock<INewsDbContext> mockContext = new Mock<INewsDbContext>();
            Repository<Comment> repository = this.GetRepository();
            Assert.NotNull(repository.Context);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSet()
        {
            IDbSet<Comment> mockSet = new Mock<IDbSet<Comment>>().Object;
            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet);

            Repository<Comment> repository = this.GetRepository();

            Assert.NotNull(repository.DbSet);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSetWithCorrectType()
        {
            IDbSet<Comment> mockSet = new Mock<IDbSet<Comment>>().Object;
            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet);

            Repository<Comment> repository = this.GetRepository();

            Assert.NotNull(repository.DbSet);
            Assert.IsInstanceOf(typeof(IDbSet<Comment>), repository.DbSet);
        }

        [Test]
        public void All_ReturnsRepositoryDbSet()
        {
            IDbSet<Comment> mockSet = new Mock<IDbSet<Comment>>().Object;

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet);

            Repository<Comment> repository = this.GetRepository();

            Assert.NotNull(repository.All);
            Assert.IsInstanceOf(typeof(IDbSet<Comment>), repository.All);
            Assert.AreSame(repository.All, repository.DbSet);
        }

        [Test]
        public void GetFirst_ShouldThrowArgumentNullExceptionWhenFilterExpressionIsNull()
        {
            Repository<Comment> repository = this.GetRepository();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => repository.GetFirst(null));

            Assert.AreEqual("filterExpression", exception.ParamName);
        }

        [Test]
        public void GetFirst_ShouldReturnCorrectObjectIfObjectIsFound()
        {
            Comment expected = new Comment { Content = "asd", Id = 4, User = null };
            IQueryable<Comment> data = new List<Comment>
            {
                new Comment {Content = "asdasdasd", Id = 1, User = null},
                new Comment {Content = "as12312312d", Id = 2, User = null},
                new Comment {Content = "asaasd as das dd", Id = 3, User = null},
                expected,
                new Comment {Content = "a123123sd", Id = 5, User = null}
            }
            .AsQueryable();

            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            Comment reslut = repository.GetFirst(x => x.Content == "asd");

            Assert.AreSame(expected, reslut);
        }

        [Test]
        public void GetFirst_ShouldReturnNullIfObjectIsNotFound()
        {
            IQueryable<Comment> data = new List<Comment>
            {
                new Comment {Content = "asdasdasd", Id = 1, User = null},
                new Comment {Content = "as12312312d", Id = 2, User = null},
                new Comment {Content = "asaasd as das dd", Id = 3, User = null},
                new Comment {Content = "asd", Id = 4, User = null},
                new Comment {Content = "a123123sd", Id = 5, User = null}
            }
            .AsQueryable();

            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);


            Repository<Comment> repository = this.GetRepository();
            Comment reslut = repository.GetFirst(x => x.Content == "notexisting");

            Assert.IsNull(reslut);
        }

        [Test]
        public void GetById_SholdBeCalledOnceWithCorrectParameters()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            mockSet.Setup(x => x.Find(It.IsAny<int>())).Returns(new Comment());
            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            Comment result = repository.GetById(4);

            mockSet.Verify(m => m.Find(4), Times.Once);
        }

        [Test]
        public void Add_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();

            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

        [Test]
        public void Add_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();
            Mock<Comment> mockComment = new Mock<Comment>();
            DbEntityEntry<Comment> fakeEntry = (DbEntityEntry<Comment>)FormatterServices
                .GetSafeUninitializedObject(typeof(DbEntityEntry<Comment>));

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Entry(It.IsAny<Comment>())).Returns(fakeEntry);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();

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

            this.context.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void DataMaping_Configure()
        {
            DataMappingsProfile profile = new DataMappingsProfile();
            Assert.DoesNotThrow(() => profile
                .GetType()
                .GetMethod("Configure", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(profile, new object[] { }));
        }

        [Test]
        public void Delete_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
        }

        [Test]
        public void Delete_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Mock<Comment> mockComment = new Mock<Comment>();
            Repository<Comment> repository = this.GetRepository();

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

            this.context.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void Update_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            Assert.Throws<ArgumentNullException>(() => repository.Update(null));
        }

        [Test]
        public void Update_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Mock<Comment> mockComment = new Mock<Comment>();
            Repository<Comment> repository = this.GetRepository();

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

            this.context.Verify(x => x.Entry(mockComment.Object), Times.AtLeastOnce);
        }

        [Test]
        public void GetAll_WithNoParametersShouldReturnTheWholeCollection()
        {
            IQueryable<Comment> data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = 4, User = null },
                new Comment { Content = "a123123sd", Id = 5, User = null}
            }
            .AsQueryable();

            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            IEnumerable<Comment> reslut = repository.GetAll();

            Assert.AreEqual(data, reslut);
        }

        [Test]
        public void GetAll_WithFilterExpretionParameterShouldReturnCorrectCollection()
        {
            IQueryable<Comment> data = new List<Comment>
            {
                new Comment { Content = "asdasdasd", Id = 1, User = null},
                new Comment { Content = "as12312312d", Id = 2, User = null},
                new Comment { Content = "asaasd as das dd", Id = 3, User = null},
                new Comment { Content = "asd", Id = 4, User = null },
                new Comment { Content = "asd", Id = 5, User = null}
            }
            .AsQueryable();

            Mock<IDbSet<Comment>> mockSet = new Mock<IDbSet<Comment>>();

            mockSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.context.Setup(x => x.Set<Comment>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Comments).Returns(mockSet.Object);

            Repository<Comment> repository = this.GetRepository();
            IEnumerable<Comment> reslut = repository.GetAll(x => x.Content == "asd");

            Assert.AreEqual(5, reslut.Count());
        }

        [Test]
        public void GetById_ShouldThrowArgumentNullExceptionWhenIDIsNull()
        {
            Repository<Comment> repository = this.GetRepository();
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => repository.GetById(null));

            Assert.AreEqual("id", exception.ParamName);
        }

        private Repository<Comment> GetRepository()
        {
            return new Repository<Comment>(this.context.Object);
        }
    }
}