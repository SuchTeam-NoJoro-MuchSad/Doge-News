using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Name_ShouldMaxLengthAttributeWithValue100()
        {
            Type imageType = typeof(Image);
            PropertyInfo propertyInfo = imageType.GetProperty("Name");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 100;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void FullName_ShouldMaxLengthAttributeWithValue200()
        {
            Type imageType = typeof(Image);
            PropertyInfo propertyInfo = imageType.GetProperty("FullName");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 200;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }
        
        [Test]
        public void FileExtension_ShouldMaxLengthAttributeWithValue10()
        {
            Type imageType = typeof(Image);
            PropertyInfo propertyInfo = imageType.GetProperty("FileExtention");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 10;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            Image image = new Image();

            image.Id = 1;
            Assert.AreEqual(id, image.Id);
        }

        [Test]
        public void Name_GetShouldReturnTheSetValue()
        {
            string name = "Name";
            Image image = new Image();

            image.Name = name;
            Assert.AreEqual(name, image.Name);
        }

        [Test]
        public void FullName_GetShouldReturnTheSetValue()
        {
            string fullName = "FullName";
            Image image = new Image();

            image.FullName = fullName;
            Assert.AreEqual(fullName, image.FullName);
        }

        [Test]
        public void FileExtension_GetShouldReturnTheSetValue()
        {
            string fileExtension = ".exe";
            Image image = new Image();

            image.FileExtention = fileExtension;
            Assert.AreEqual(fileExtension, image.FileExtention);
        }

        [Test]
        public void NewsItems_GetShouldReturnTheSetValue()
        {
            List<NewsItem> items = new List<NewsItem>();
            Image image = new Image();

            image.NewsItems = items;
            Assert.AreEqual(items, image.NewsItems);
        }
    }
}