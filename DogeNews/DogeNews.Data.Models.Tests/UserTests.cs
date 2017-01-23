using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

using NUnit.Framework;
using DogeNews.Web.Common.Enums;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Username_ShouldHaveRequiredAttribute()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Username");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Username_ShouldHaveMinLengthAttributeWithValue3()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Username");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Username_ShouldHaveMaxLengthAttributeWithValue20()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Username");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 20;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Username_ShouldHaveIndexAttributeWithValueSetIsUniqueToTrue()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Username");
            var indexAttribute = (IndexAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as IndexAttribute != null);

            Assert.IsTrue(indexAttribute.IsUnique);
        }

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
        public void Email_ShouldHaveMinLengthAttributeWithValue3()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Email");
            var minLengthAttribute = (MinLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MinLengthAttribute != null);
            var expectedLength = 3;

            Assert.AreEqual(expectedLength, minLengthAttribute.Length);
        }

        [Test]
        public void Email_ShouldHaveMaxLengthAttributeWithValue100()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Email");
            var maxLengthAttribute = (MaxLengthAttribute)propertyInfo
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x as MaxLengthAttribute != null);
            var expectedLength = 100;

            Assert.AreEqual(expectedLength, maxLengthAttribute.Length);
        }

        [Test]
        public void Salt_ShouldHaveRequiredAttribute()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("Salt");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void UserRole_ShouldHaveRequiredAttribute()
        {
            var userType = typeof(User);
            var propertyInfo = userType.GetProperty("UserRole");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Id_GetShouldReturnSetValue()
        {
            int id = 1;
            var user = new User();

            user.Id = id;
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void Username_GetShouldReturnSetValue()
        {
            string username = "username";
            var user = new User();

            user.Username = username;
            Assert.AreEqual(username, user.Username);
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
        public void Salt_GetShouldReturnSetValue()
        {
            string salt = "salt";
            var user = new User();

            user.Salt = salt;
            Assert.AreEqual(salt, user.Salt);
        }

        [Test]
        public void PassHash_GetShouldReturnSetValue()
        {
            string passhash = "passhash";
            var user = new User();

            user.PassHash = passhash;
            Assert.AreEqual(passhash, user.PassHash);
        }

        [Test]
        public void UserRole_GetShouldReturnSetValue()
        {
            var userrole = UserRoleType.Admin;
            var user = new User();

            user.UserRole = userrole;
            Assert.AreEqual(userrole, user.UserRole);
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
