using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class CommentTests
    {
        [Test]
        public void User_ShouldHaveRequiredAttribute()
        {
            Type commentType = typeof(Comment);
            PropertyInfo propertyInfo = commentType.GetProperty("User");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Content_ShouldHaveRequiredAttribute()
        {
            Type commentType = typeof(Comment);
            PropertyInfo propertyInfo = commentType.GetProperty("Content");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Content_ShouldHaveMinLengthAttributeWithValue2()
        {
            Type commentType = typeof(Comment);
            PropertyInfo propertyInfo = commentType.GetProperty("Content");
            MinLengthAttribute minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            int expectedLength = 2;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Content_ShouldHaveMaxLengthAttributeWithValue250()
        {
            Type commentType = typeof(Comment);
            PropertyInfo propertyInfo = commentType.GetProperty("Content");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 250;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            Comment comment = new Comment();

            comment.Id = 1;
            Assert.AreEqual(id, comment.Id);
        }

        [Test]
        public void User_GetShouldReturnTheSetValue()
        {
            User user = new User();
            Comment comment = new Comment();

            comment.User = user;
            Assert.AreEqual(user, comment.User);
        }

        [Test]
        public void Content_GetShouldReturnTheSetValue()
        {
            string content = "Content";
            Comment comment = new Comment();

            comment.Content = content;
            Assert.AreEqual(content, comment.Content);
        }
    }
}
