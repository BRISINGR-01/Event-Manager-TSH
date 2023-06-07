using Domain.Managers;
using Logic;
using Logic.Models;
using Shared.Enums;
using Shared.Errors;

namespace Unit_testing.Tests
{
    [TestClass]
    public class ResultTests : BaseTester
    {
        [TestMethod]
        public void AccessDeniedException()
        {
            var res = Result<bool>.From(() => throw new AccessDeniedException());
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsNotNull(res.Redirection);
            Assert.IsTrue(res.Redirection.EndsWith("AccessDenied"));
            Assert.AreEqual(res.Error, "You are not allowed to execute this action");
        }
        [TestMethod]
        public void ServerException()
        {
            var res = Result<bool>.From(() => throw new ServerException(""));
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsNotNull(res.Redirection);
            Assert.IsTrue(res.Redirection.EndsWith("ServerError"));
            Assert.IsNull(res.Error);
        }
        [TestMethod]
        public void ClientException()
        {
            string message = "Error to show to user";
            var res = Result<bool>.From(() => throw new ClientException(message));
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsNull(res.Redirection);
            Assert.AreEqual(res.Error, message);
        }
        [TestMethod]
        public void DeveloperException()
        {
            var res = Result<User>.From(() => throw new DeveloperException("Some hidden error"), CRUD.DELETE, "user");
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsTrue(res.ErrorIsDefault);
            Assert.IsNull(res.Redirection);
            Assert.AreEqual(res.Error, "A problem occurred while deleting the user");
        }
        [TestMethod]
        public void ExceptionWithArea()
        {
            var res = Result<User>.From(() => throw new AccessViolationException("Some hidden error"), area: "user");
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsTrue(res.ErrorIsDefault);
            Assert.IsNull(res.Redirection);
            Assert.AreEqual(res.Error, "A problem occurred while getting the user");
        }
        [TestMethod]
        public void ExceptionWithAction()
        {
            var res = Result<User>.From(() => throw new AccessViolationException("Some hidden error"), action: CRUD.UPDATE);
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsTrue(res.ErrorIsDefault);
            Assert.IsNull(res.Redirection);
            Assert.AreEqual(res.Error, "A problem occurred while updating the user");
        }
    }
}