using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

using DogeNews.Common.Enums;

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
        public void Title_ShouldHaveMaxLengthAttributeWithValue200()
        {
            var newsItemType = typeof(NewsItem);
            var propertyInfo = newsItemType.GetProperty("Title");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 200;

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

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            var item = new NewsItem();

            item.Id = id;
            Assert.AreEqual(id, item.Id);
        }

        [Test]
        public void Title_GetShouldReturnTheSetValue()
        {
            string title = "title";
            var item = new NewsItem();

            item.Title = title;
            Assert.AreEqual(title, item.Title);
        }

        [Test]
        public void SubTitle_GetShouldReturnTheSetValue()
        {
            string subtitle = "subtitle";
            var item = new NewsItem();

            item.Subtitle = subtitle;
            Assert.AreEqual(subtitle, item.Subtitle);
        }

        [Test]
        public void Contetn_GetShouldReturnTheSetValue()
        {
            string content = "title";
            var item = new NewsItem();

            item.Content = content;
            Assert.AreEqual(content, item.Content);
        }

        [Test]
        public void Category_GetShouldReturnTheSetValue()
        {
            var category = NewsCategoryType.Breaking;
            var item = new NewsItem();

            item.Category = category;
            Assert.AreEqual(category, item.Category);
        }

        [Test]
        public void AuthorId_GetShouldReturnTheSetValue()
        {
            var authorId = "1";
            var item = new NewsItem();

            item.AuthorId = authorId;
            Assert.AreEqual(authorId, item.AuthorId);
        }

        [Test]
        public void ImageId_GetShouldReturnTheSetValue()
        {
            var imageID = 1;
            var item = new NewsItem();

            item.ImageId = imageID;
            Assert.AreEqual(imageID, item.ImageId);
        }

        [Test]
        public void CreatedOn_GetShouldReturnTheSetValue()
        {
            var createdOn = DateTime.Now;
            var item = new NewsItem();

            item.CreatedOn = createdOn;
            Assert.AreEqual(createdOn, item.CreatedOn);
        }

        [Test]
        public void DeletedOn_GetShouldReturnTheSetValue()
        {
            var deletedOn = DateTime.Now;
            var item = new NewsItem();

            item.DeletedOn = deletedOn;
            Assert.AreEqual(deletedOn, item.DeletedOn);
        }

        [Test]
        public void IsApproved_GetShouldReturnTheSetValue()
        {
            var isApproved = true;
            var item = new NewsItem();

            item.IsApproved = isApproved;
            Assert.AreEqual(isApproved, item.IsApproved);
        }

        [Test]
        public void IsAddedByAdmin_GetShouldReturnTheSetValue()
        {
            var isAddedByAdmin = true;
            var item = new NewsItem();

            item.IsAddedByAdmin = isAddedByAdmin;
            Assert.AreEqual(isAddedByAdmin, item.IsAddedByAdmin);
        }

        [Test]
        public void Author_GetShouldReturnTheSetValue()
        {
            var author = new User();
            var item = new NewsItem();

            item.Author = author;
            Assert.AreEqual(author, item.Author);
        }

        [Test]
        public void Image_GetShouldReturnTheSetValue()
        {
            var image = new Image();
            var item = new NewsItem();

            item.Image = image;
            Assert.AreEqual(image, item.Image);
        }

        [Test]
        public void Comments_GetShouldReturnTheSetValue()
        {
            var comments = new List<Comment>();
            var item = new NewsItem();

            item.Comments = comments;
            Assert.AreEqual(comments, item.Comments);
        }
    }
}
