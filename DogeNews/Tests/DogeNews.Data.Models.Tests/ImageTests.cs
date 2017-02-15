using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

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

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            var image = new Image();

            image.Id = 1;
            Assert.AreEqual(id, image.Id);
        }

        [Test]
        public void Name_GetShouldReturnTheSetValue()
        {
            string name = "Name";
            var image = new Image();

            image.Name = name;
            Assert.AreEqual(name, image.Name);
        }

        [Test]
        public void FullName_GetShouldReturnTheSetValue()
        {
            string fullName = "FullName";
            var image = new Image();

            image.FullName = fullName;
            Assert.AreEqual(fullName, image.FullName);
        }

        [Test]
        public void FileExtension_GetShouldReturnTheSetValue()
        {
            string fileExtension = ".exe";
            var image = new Image();

            image.FileExtention = fileExtension;
            Assert.AreEqual(fileExtension, image.FileExtention);
        }

        [Test]
        public void NewsItems_GetShouldReturnTheSetValue()
        {
            var items = new List<NewsItem>();
            var image = new Image();

            image.NewsItems = items;
            Assert.AreEqual(items, image.NewsItems);
        }
    }
}