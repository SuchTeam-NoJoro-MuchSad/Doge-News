using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Data.Repositories;
using DogeNews.Services.Common.Contracts;
using DogeNews.Web.Models;
using Moq;
using NUnit.Framework;

namespace DogeNews.Data.Tests
{
    [TestFixture]
    public class ProjectableRepositoryTests
    {
        private Mock<IDbSet<Comment>> mockDbSet;
        private Mock<INewsDbContext> mockNewsDbContext;
        private Mock<IProjectionService> mockProjectionService;

        [SetUp]
        public void Init()
        {
            IQueryable<Comment> data = new List<Comment>
            {
                new Comment {Content = "asdasdasd", Id = 1, User = null},
                new Comment {Content = "as12312312d", Id = 2, User = null},
                new Comment {Content = "asaasd as das dd", Id = 3, User = null},
                new Comment {Content = "a123123sd", Id = 5, User = null}
            }.AsQueryable();

            this.mockDbSet = new Mock<IDbSet<Comment>>();
            this.mockDbSet.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(data.Provider);
            this.mockDbSet.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(data.Expression);
            this.mockDbSet.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(data.ElementType);
            this.mockDbSet.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.mockNewsDbContext = new Mock<INewsDbContext>();
            this.mockNewsDbContext.Setup(
                x => x.Set<Comment>()).Returns(this.mockDbSet.Object);

            this.mockProjectionService = new Mock<IProjectionService>();
            this.mockProjectionService.Setup(
                x => x.ProjectToList<Comment, CommentWebModel>(It.IsAny<IQueryable<Comment>>()));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenProjectionServiceIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
               new ProjectableRepository<Comment>(this.mockNewsDbContext.Object, null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDbContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
               new ProjectableRepository<Comment>(null, this.mockProjectionService.Object));
        }

        [Test]
        public void GetFirstMapped_ShouldCallProjectionServiceProjectToFirstOrDefaultOnce()
        {
            IProjectableRepository<Comment> repository = new ProjectableRepository<Comment>(
                this.mockNewsDbContext.Object, this.mockProjectionService.Object);

            IQueryable<Comment> query = repository.All.Where(x => x.Id == 1);

            this.mockProjectionService.Setup(
                x => x.ProjectToFirstOrDefault<Comment, CommentWebModel>(query));


            repository.GetFirstMapped<CommentWebModel>(x => x.Id == 1);

            this.mockProjectionService.Verify(
                x => x.ProjectToFirstOrDefault<Comment, CommentWebModel>(query), Times.Once);
        }

        [Test]
        public void GetAllMapped_WithNoParameters_ShouldCallProjectionServiceProjectToListOnce()
        {
            IProjectableRepository<Comment> repository = new ProjectableRepository<Comment>(
               this.mockNewsDbContext.Object, this.mockProjectionService.Object);

            IQueryable<Comment> query = repository.All;

            repository.GetAllMapped<CommentWebModel>();

            this.mockProjectionService.Verify(
                x => x.ProjectToList<Comment, CommentWebModel>(query), Times.Once);
        }

        [Test]
        public void GetAllMapped_WithParameters_ShouldCallProjectionServiceProjectToListOnceWithCorrectQuery()
        {
            IProjectableRepository<Comment> repository = new ProjectableRepository<Comment>(
               this.mockNewsDbContext.Object, this.mockProjectionService.Object);

            IQueryable<Comment> query = repository.All.Where(x=>x.Id ==1);

            repository.GetAllMapped<CommentWebModel>(x => x.Id == 1);

            this.mockProjectionService.Verify(
                x => x.ProjectToList<Comment, CommentWebModel>(query), Times.Once);
        }
    }
}