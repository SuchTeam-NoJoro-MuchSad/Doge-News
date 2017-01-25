using DogeNews.Web.Common.Enums;
using NUnit.Framework;

namespace DogeNews.Web.Models.Tests
{
    [TestFixture]
    public class UserWebModelTests
    {
        [Test]
        public void Id_ShouldReturnSetValue()
        {
            int id = 1;
            var user = new UserWebModel();

            user.Id = id;
            Assert.AreEqual(id, user.Id);
        }

        [Test]
        public void Username_ShouldReturnSetValue()
        {
            string username = "username";
            var user = new UserWebModel();

            user.Username = username;
            Assert.AreEqual(username, user.Username);
        }

        [Test]
        public void Firstname_ShouldReturnSetValue()
        {
            string firstname = "firstname";
            var user = new UserWebModel();

            user.FirstName = firstname;
            Assert.AreEqual(firstname, user.FirstName);
        }

        [Test]
        public void Lastname_ShouldReturnSetValue()
        {
            string lastname = "lastname";
            var user = new UserWebModel();

            user.LastName = lastname;
            Assert.AreEqual(lastname, user.LastName);
        }

        [Test]
        public void Email_ShouldReturnSetValue()
        {
            string email = "email";
            var user = new UserWebModel();

            user.Email = email;
            Assert.AreEqual(email, user.Email);
        }

        [Test]
        public void Salt_ShouldReturnSetValue()
        {
            string salt = "salt";
            var user = new UserWebModel();

            user.Salt = salt;
            Assert.AreEqual(salt, user.Salt);
        }

        [Test]
        public void Password_ShouldReturnSetValue()
        {
            string password = "password";
            var user = new UserWebModel();

            user.Password = password;
            Assert.AreEqual(password, user.Password);
        }

        [Test]
        public void UserRole_ShouldReturnSetValue()
        {
            var role = UserRoleType.Admin;
            var user = new UserWebModel();

            user.UserRole = role;
            Assert.AreEqual(role, user.UserRole);
        }
    }
}
