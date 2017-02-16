using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class UserTests
    {
      
        [Test]
        public void Firstname_ShouldHaveMinLengthAttributeWithValue3()
        {
            Type userType = typeof(User);
            PropertyInfo propertyInfo = userType.GetProperty("FirstName");
            MinLengthAttribute minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            int expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Firstname_ShouldHaveMaxLengthAttributeWithValue20()
        {
            Type userType = typeof(User);
            PropertyInfo propertyInfo = userType.GetProperty("FirstName");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 20;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Lastname_ShouldHaveMinLengthAttributeWithValue3()
        {
            Type userType = typeof(User);
            PropertyInfo propertyInfo = userType.GetProperty("LastName");
            MinLengthAttribute minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            int expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Lastname_ShouldHaveMaxLengthAttributeWithValue20()
        {
            Type userType = typeof(User);
            PropertyInfo propertyInfo = userType.GetProperty("LastName");
            MaxLengthAttribute maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            int expectedLength = 20;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }
        
        [Test]
        public void Id_GetShouldReturnSetValue()
        {
            string id = "1";
            User user = new User();

            user.Id = id;
            Assert.AreEqual(id, user.Id);
        }
        
        [Test]
        public void Firstname_GetShouldReturnSetValue()
        {
            string firstname = "firstname";
            User user = new User();

            user.FirstName = firstname;
            Assert.AreEqual(firstname, user.FirstName);
        }

        [Test]
        public void Lastname_GetShouldReturnSetValue()
        {
            string lastname = "lastname";
            User user = new User();

            user.LastName = lastname;
            Assert.AreEqual(lastname, user.LastName);
        }

        [Test]
        public void Email_GetShouldReturnSetValue()
        {
            string email = "email";
            User user = new User();

            user.Email = email;
            Assert.AreEqual(email, user.Email);
        }
        
        [Test]
        public void NewsItems_GetShouldReturnSetValue()
        {
            List<NewsItem> items = new List<NewsItem>();
            User user = new User();

            user.NewsItems = items;
            Assert.AreEqual(items, user.NewsItems);
        }

        [Test]
        public void Comments_GetShouldReturnSetValue()
        {
            List<Comment> comemnts = new List<Comment>();
            User user = new User();

            user.Comments = comemnts;
            Assert.AreEqual(comemnts, user.Comments);
        }
    }
}
