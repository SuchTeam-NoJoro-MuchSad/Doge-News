using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

using NUnit.Framework;


namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class NewsItemTests
    {
        [Test]
        public void Title_ShouldHaveMinLengthAttributeWithValue5()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Title");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 5;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Title_ShouldHaveMaxLengthAttributeWithValue30()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Title");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 30;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Title_ShouldHaveIndexAttributeWithIsUniqueSetToTrue()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Title");
            var indexAttribute = (IndexAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as IndexAttribute != null);

            Assert.IsTrue(indexAttribute.IsUnique);
        }

        [Test]
        public void Subtitle_ShouldHaveMinLengthAttributeWithValue5()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Subtitle");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 5;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Subtitle_ShouldHaveMaxLengthAttributeWithValue30()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Subtitle");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 30;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Content_ShouldHaveRequiredAttribute()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Content");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Category_ShouldHaveRequiredAttribute()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Category");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }
    }
}
