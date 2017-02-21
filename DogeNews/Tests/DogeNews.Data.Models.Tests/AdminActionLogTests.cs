using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;

namespace DogeNews.Data.Models.Tests
{
    [TestFixture]
    public class AdminActionLogTests
    {
        [Test]
        public void Id_Get_ShouldReturnSetValue()
        {
            int expectedId = 1;
            AdminActionLog log = new AdminActionLog();

            log.Id = expectedId;

            Assert.AreEqual(expectedId,log.Id);
            Assert.AreEqual(typeof(int), log.Id.GetType());
        }

        [Test]
        public void User_ShouldHaveRequiredAttrubute()
        {
            Type type = typeof(AdminActionLog);
            PropertyInfo propertyInfo = type.GetProperty("User");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.AreEqual(propertyInfo.PropertyType, typeof(User));
            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void User_Get_ShouldReturnSetValue()
        {
            User user = new User();
            AdminActionLog log = new AdminActionLog();

            log.User = user;

            Assert.NotNull(log.User);
            Assert.AreSame(log.User, user);
            Assert.AreEqual(typeof(User), log.User.GetType());
        }

        [Test]
        public void InvokedMethodName_ShouldHaveRequiredAttrubute()
        {
            Type type = typeof(AdminActionLog);
            PropertyInfo propertyInfo = type.GetProperty("InvokedMethodName");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.AreEqual(propertyInfo.PropertyType, typeof(string));
            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void InvokedMethodName_Get_ShouldReturnSetValue()
        {
            AdminActionLog log = new AdminActionLog();

            log.InvokedMethodName = "method";

            Assert.NotNull(log.InvokedMethodName);
            Assert.AreEqual(typeof(string), log.InvokedMethodName.GetType());
        }

        [Test]
        public void InvokedMethodArguments_ShouldHaveRequiredAttrubute()
        {
            Type type = typeof(AdminActionLog);
            PropertyInfo propertyInfo = type.GetProperty("InvokedMethodArguments");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.AreEqual(propertyInfo.PropertyType, typeof(string));
            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void InvokedMethodArguments_Get_ShouldReturnSetValue()
        {
            AdminActionLog log = new AdminActionLog();

            log.InvokedMethodArguments = "method";

            Assert.NotNull(log.InvokedMethodArguments);
            Assert.AreEqual(typeof(string), log.InvokedMethodArguments.GetType());
        }

        [Test]
        public void Date_ShouldHaveRequiredAttrubute()
        {
            Type type = typeof(AdminActionLog);
            PropertyInfo propertyInfo = type.GetProperty("Date");
            bool doesRequiredExist = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            Assert.AreEqual(propertyInfo.PropertyType, typeof(DateTime));
            Assert.IsTrue(doesRequiredExist);
        }

        [Test]
        public void Date_Get_ShouldReturnSetValue()
        {
            AdminActionLog log = new AdminActionLog();

            log.Date = DateTime.UtcNow;

            Assert.NotNull(log.Date);
            Assert.AreEqual(typeof(DateTime), log.Date.GetType());
            Assert.AreEqual(log.Date.Year, DateTime.UtcNow.Year);
            Assert.AreEqual(log.Date.Month, DateTime.UtcNow.Month);
            Assert.AreEqual(log.Date.Day, DateTime.UtcNow.Day);
            Assert.AreEqual(log.Date.Hour, DateTime.UtcNow.Hour);
            Assert.AreEqual(log.Date.Minute, DateTime.UtcNow.Minute);
            Assert.AreEqual(log.Date.Second, DateTime.UtcNow.Second);
        }

        [Test]
        public void Date_Get_ShouldReturnSetValueByConstructor()
        {
            AdminActionLog log = new AdminActionLog();

            Assert.NotNull(log.Date);
            Assert.AreEqual(typeof(DateTime), log.Date.GetType());
            Assert.AreEqual(log.Date, DateTime.UtcNow);
        }
    }
}