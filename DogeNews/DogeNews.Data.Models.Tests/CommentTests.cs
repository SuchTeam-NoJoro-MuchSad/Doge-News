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
    }
}
