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
                UserRole.Student
            );
            Credentials credentials = new(Guid.Empty, "email", "", "");
            User user2 = new(
                Guid.Empty,
                Guid.Empty,
                "User 2",
                UserRole.Administrator
            );
            Manager.User.Create(user1, credentials);
            var res = Manager.User.Create(user2, credentials);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual("A user with this email already exists", res.Exception.Message);
        }
        [TestMethod]
        public void ValidateUserName()
        {
            User user = new(
                Guid.Empty,
                Guid.Empty,
                "-",
                UserRole.Student
            );
            var res = Manager.User.Create(user);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Exception.Message, "The username is too short");
        }
    }
}