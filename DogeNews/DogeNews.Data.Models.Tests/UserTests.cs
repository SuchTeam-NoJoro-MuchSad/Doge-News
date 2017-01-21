using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using NUnit.Framework;

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
    }
}
