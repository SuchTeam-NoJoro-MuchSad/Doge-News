using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DogeNews.Data.Contracts;
using DogeNews.Data.Models;
using DogeNews.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace DogeNews.Data.Tests
{
    [TestFixture]
    public class UnitOfWorkTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NewsData(null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenContextIsCorrect()
        {
            var mockContext = new Mock<INewsDbContext>();

            Assert.DoesNotThrow(() => new NewsData(mockContext.Object));
        }

        [Test]
        public void Commit_SouldCall_ContextSaveChanges()
        {
            var mockContext = new Mock<INewsDbContext>();
            var newsData = new NewsData(mockContext.Object);

            newsData.Commit();

            mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Dispose_SouldCall_ContextDispose()
        {
            var mockContext = new Mock<INewsDbContext>();
            var newsData = new NewsData(mockContext.Object);

            newsData.Dispose();

            mockContext.Verify(x => x.Dispose(), Times.Once);
        }
    }
}