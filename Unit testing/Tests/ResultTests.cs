using Logic.Models;
using Logic.Utilities;
using Shared.Errors;

namespace Unit_testing.Tests
{
    [TestClass]
    public class ResultTests : BaseTester
    {
        [TestMethod]
        public void AccessDeniedException()
        {
            var res = Result.From(() => throw new AccessDeniedException());
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsNotNull(res.ErrorMessage);
            Assert.IsTrue(res.Exception is AccessDeniedException);
            Assert.IsTrue(res.ErrorMessage.StartsWith("Access Denied to an unauthenticated user"));
        }
        [TestMethod]
        public void ServerException()
        {
            var res = Result.From(() => throw new ServerException("Db is down"));
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsTrue(res.Exception is ServerException);
            Assert.IsNotNull(res.ErrorMessage);
        }
        [TestMethod]
        public void ClientException()
        {
            string message = "Error to show to user";
            var res = Result.From(() => throw new ClientException(message));
            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.ErrorMessage, message);
        }
        [TestMethod]
        public void DeveloperException()
        {
            var res = Result<User>.From(() => throw new DeveloperException("Some hidden error"));
            Assert.IsFalse(res.IsSuccessful);
            Assert.IsFalse(res.Exception is DeveloperException);
            Assert.AreEqual("An error occurred", res.ErrorMessage);
        }
        [TestMethod]
        public void NotCustomException()
        {
            var res = Result<User>.From(() => throw new AccessViolationException("Some hidden error"));
            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual("An error occurred", res.ErrorMessage);
        }
    }
}