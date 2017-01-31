using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class UserTests
    {
      
        [Test]
        public void Firstname_ShouldHaveMinLengthAttributeWithValue3()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("FirstName");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Firstname_ShouldHaveMaxLengthAttributeWithValue20()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("FirstName");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 20;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Lastname_ShouldHaveMinLengthAttributeWithValue3()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("LastName");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Lastname_ShouldHaveMaxLengthAttributeWithValue20()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("LastName");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 20;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }
        
        [Test]
        public void Id_GetShouldReturnSetValue()
        {
            string id = "1";
            var user = new User();

            user.Id = id;
            Assert.AreEqual(id, user.Id);
        }
        
        [Test]
        public void Firstname_GetShouldReturnSetValue()
        {
            string firstname = "firstname";
            var user = new User();

            user.FirstName = firstname;
            Assert.AreEqual(firstname, user.FirstName);
        }

        [Test]
        public void Lastname_GetShouldReturnSetValue()
        {
            string lastname = "lastname";
            var user = new User();

            user.LastName = lastname;
            Assert.AreEqual(lastname, user.LastName);
        }

        [Test]
        public void Email_GetShouldReturnSetValue()
        {
            string email = "email";
            var user = new User();

            user.Email = email;
            Assert.AreEqual(email, user.Email);
        }
        
        [Test]
        public void NewsItems_GetShouldReturnSetValue()
        {
            var items = new List<NewsItem>();
            var user = new User();

            user.NewsItems = items;
            Assert.AreEqual(items, user.NewsItems);
        }

        [Test]
        public void Comments_GetShouldReturnSetValue()
        {
            var comemnts = new List<Comment>();
            var user = new User();

            user.Comments = comemnts;
            Assert.AreEqual(comemnts, user.Comments);
        }
    }
}
