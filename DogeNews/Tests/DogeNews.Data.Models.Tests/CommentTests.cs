using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class CommentTests
    {
        [Test]
        public void User_ShouldHaveRequiredAttribute()
        {
            var commentType = typeof(Comment);
            var propertyInfo = commentType.GetProperty("User");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Content_ShouldHaveRequiredAttribute()
        {
            var commentType = typeof(Comment);
            var propertyInfo = commentType.GetProperty("Content");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Content_ShouldHaveMinLengthAttributeWithValue2()
        {
            var commentType = typeof(Comment);
            var propertyInfo = commentType.GetProperty("Content");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 2;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Content_ShouldHaveMaxLengthAttributeWithValue250()
        {
            var commentType = typeof(Comment);
            var propertyInfo = commentType.GetProperty("Content");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 250;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            var comment = new Comment();

            comment.Id = 1;
            Assert.AreEqual(id, comment.Id);
        }

        [Test]
        public void User_GetShouldReturnTheSetValue()
        {
            var user = new User();
            var comment = new Comment();

            comment.User = user;
            Assert.AreEqual(user, comment.User);
        }

        [Test]
        public void Content_GetShouldReturnTheSetValue()
        {
            string content = "Content";
            var comment = new Comment();

            comment.Content = content;
            Assert.AreEqual(content, comment.Content);
        }
    }
}
