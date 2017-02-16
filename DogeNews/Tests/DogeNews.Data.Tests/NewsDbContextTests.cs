using System;
using System.Data.Entity;
using System.Reflection;

using DogeNews.Data.Models;

using NUnit.Framework;

namespace DogeNews.Data.Tests
{
    [TestFixture]
    public class NewsDbContextTests
    {
        private NewsDbContext context;

        [SetUp]
        public void Init()
        {
            this.context = new NewsDbContext();
        }
        
        [Test]
        public void Images_GetShouldReturnSetValue()
        {
            IDbSet<Image> set = (IDbSet<Image>)typeof(DbSet<Image>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.Images = set;
            Assert.AreEqual(set, this.context.Images);
        }

        [Test]
        public void NewsItems_GetShouldReturnSetValue()
        {
            IDbSet<NewsItem> set = (IDbSet<NewsItem>)typeof(DbSet<NewsItem>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.NewsItems = set;
            Assert.AreEqual(set, this.context.NewsItems);
        }

        [Test]
        public void Comments_GetShouldReturnSetValue()
        {
            IDbSet<Comment> set = (IDbSet<Comment>)typeof(DbSet<Comment>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.Comments = set;
            Assert.AreEqual(set, this.context.Comments);
        }

        [Test]
        public void Set_ShouldReturnIDbSetInstanceOfTheRequiredType()
        {
            string expected = typeof(DbSet<User>).Name;
            string actual = this.context.Set<User>().GetType().Name;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SaveChanges_ShouldReturn0WhenEverythingIsOk()
        {
            int expected = 0;
            int actual = this.context.SaveChanges();

            Assert.AreEqual(expected, actual);
        }
    }
}