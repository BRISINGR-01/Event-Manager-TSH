using Domain.Managers;
using Logic;
using Logic.Models;
using Shared.Enums;

namespace Unit_testing.Tests
{
    [TestClass]
    public class UserTests : BaseTester
    {
        [TestMethod]
        public void CreateUsersWithSameEmail()
        {
            User user1 = new(
                Guid.Empty,
                Guid.Empty,
                "User 1",
                "password",
                UserRole.Student,
                "email@email.com"
            );
            User user2 = new(
                Guid.Empty,
                Guid.Empty,
                "User 2",
                "password",
                UserRole.Administrator,
                "email@email.com"
            );
            Manager.User.Create(user1);
            var res = Manager.User.Create(user2);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "A user with this email already exists");
        }
        [TestMethod]
        public void ValidateEmail()
        {
            User user = new(
                Guid.Empty,
                Guid.Empty,
                "User 1",
                "password",
                UserRole.Student,
                "31"
            );
            var res = Manager.User.Create(user);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "The email is invalid");
        }
        [TestMethod]
        public void ValidateUserName()
        {
            User user = new(
                Guid.Empty,
                Guid.Empty,
                "-",
                "password",
                UserRole.Student,
                "email2@gmail.com"
            );
            var res = Manager.User.Create(user);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "The username is too short");
        }
        [TestMethod]
        public void ValidatePassword()
        {
            User user = new(
                Guid.Empty,
                Guid.Empty,
                "------",
                "-",
                UserRole.Student,
                "email3@gmail.com"
            );
            var res = Manager.User.Create(user);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "Please provide a stronger password");
        }
    }
}