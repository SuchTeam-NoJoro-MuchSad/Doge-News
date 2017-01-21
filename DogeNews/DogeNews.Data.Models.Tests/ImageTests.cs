using System.ComponentModel.DataAnnotations;
using System.Linq;

using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Name_ShouldMaxLengthAttributeWithValue100()
        {
            var imageType = typeof(Image);
            var propertyInfo = imageType.GetProperty("Name");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 100;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void FullName_ShouldMaxLengthAttributeWithValue200()
        {
            var imageType = typeof(Image);
            var propertyInfo = imageType.GetProperty("FullName");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 200;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Url_ShouldMaxLengthAttributeWithValue200()
        {
            var imageType = typeof(Image);
            var propertyInfo = imageType.GetProperty("Url");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 200;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void FileExtension_ShouldMaxLengthAttributeWithValue10()
        {
            var imageType = typeof(Image);
            var propertyInfo = imageType.GetProperty("FileExtention");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 10;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }
    }
}