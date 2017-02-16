using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Reflection;
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
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Title");
            MinLengthAttribute minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            int expectedLength = 5;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Title_ShouldHaveMaxLengthAttributeWithValue200()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Title");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 200;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Title_ShouldHaveIndexAttributeWithIsUniqueSetToTrue()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Title");
            IndexAttribute indexAttribute = (IndexAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as IndexAttribute != null);

            Assert.IsTrue(indexAttribute.IsUnique);
        }

        [Test]
        public void Subtitle_ShouldHaveMinLengthAttributeWithValue5()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Subtitle");
            MinLengthAttribute minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            int expectedLength = 5;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Subtitle_ShouldHaveMaxLengthAttributeWithValue30()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Subtitle");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 30;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Content_ShouldHaveRequiredAttribute()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Content");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Category_ShouldHaveRequiredAttribute()
        {
            Type newsItemType = typeof(NewsItem);
            PropertyInfo propertyInfo = newsItemType.GetProperty("Category");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Id_GetShouldReturnTheSetValue()
        {
            int id = 1;
            NewsItem item = new NewsItem();

            item.Id = id;
            Assert.AreEqual(id, item.Id);
        }

        [Test]
        public void Title_GetShouldReturnTheSetValue()
        {
            string title = "title";
            NewsItem item = new NewsItem();

            item.Title = title;
            Assert.AreEqual(title, item.Title);
        }

        [Test]
        public void SubTitle_GetShouldReturnTheSetValue()
        {
            string subtitle = "subtitle";
            NewsItem item = new NewsItem();

            item.Subtitle = subtitle;
            Assert.AreEqual(subtitle, item.Subtitle);
        }

        [Test]
        public void Contetn_GetShouldReturnTheSetValue()
        {
            string content = "title";
            NewsItem item = new NewsItem();

            item.Content = content;
            Assert.AreEqual(content, item.Content);
        }

        [Test]
        public void Category_GetShouldReturnTheSetValue()
        {
            NewsCategoryType category = NewsCategoryType.Breaking;
            NewsItem item = new NewsItem();

            item.Category = category;
            Assert.AreEqual(category, item.Category);
        }

        [Test]
        public void AuthorId_GetShouldReturnTheSetValue()
        {
            string authorId = "1";
            NewsItem item = new NewsItem();

            item.AuthorId = authorId;
            Assert.AreEqual(authorId, item.AuthorId);
        }

        [Test]
        public void ImageId_GetShouldReturnTheSetValue()
        {
            int imageID = 1;
            NewsItem item = new NewsItem();

            item.ImageId = imageID;
            Assert.AreEqual(imageID, item.ImageId);
        }

        [Test]
        public void CreatedOn_GetShouldReturnTheSetValue()
        {
            DateTime createdOn = DateTime.Now;
            NewsItem item = new NewsItem();

            item.CreatedOn = createdOn;
            Assert.AreEqual(createdOn, item.CreatedOn);
        }

        [Test]
        public void DeletedOn_GetShouldReturnTheSetValue()
        {
            DateTime deletedOn = DateTime.Now;
            NewsItem item = new NewsItem();

            item.DeletedOn = deletedOn;
            Assert.AreEqual(deletedOn, item.DeletedOn);
        }

        [Test]
        public void IsApproved_GetShouldReturnTheSetValue()
        {
            bool isApproved = true;
            NewsItem item = new NewsItem();

            item.IsApproved = isApproved;
            Assert.AreEqual(isApproved, item.IsApproved);
        }

        [Test]
        public void IsAddedByAdmin_GetShouldReturnTheSetValue()
        {
            bool isAddedByAdmin = true;
            NewsItem item = new NewsItem();

            item.IsAddedByAdmin = isAddedByAdmin;
            Assert.AreEqual(isAddedByAdmin, item.IsAddedByAdmin);
        }

        [Test]
        public void Author_GetShouldReturnTheSetValue()
        {
            User author = new User();
            NewsItem item = new NewsItem();

            item.Author = author;
            Assert.AreEqual(author, item.Author);
        }

        [Test]
        public void Image_GetShouldReturnTheSetValue()
        {
            Image image = new Image();
            NewsItem item = new NewsItem();

            item.Image = image;
            Assert.AreEqual(image, item.Image);
        }

        [Test]
        public void Comments_GetShouldReturnTheSetValue()
        {
            List<Comment> comments = new List<Comment>();
            NewsItem item = new NewsItem();

            item.Comments = comments;
            Assert.AreEqual(comments, item.Comments);
        }
    }
}
