using Logic.Messages;
using Logic.Utilities;
using Mocks.Messeger;
using Mocks.Repositories;

namespace Unit_testing.Tests
{
    [TestClass]
    public class MessagesTests : BaseTester
    {
        [TestMethod]
        public void Send()
        {
            var messeger = new Messager();
            var mock1 = new MessagerDecoratorMock(messeger);
            var mock2 = new MessagerDecoratorMock(mock1);
            var mock3 = new MessagerDecoratorMock(mock2);

            mock3.Send(new MessageData("", "", Guid.Empty));

            Assert.IsTrue(mock1.WasCalled);
            Assert.IsTrue(mock2.WasCalled);
            Assert.IsTrue(mock3.WasCalled);
        }
        [TestMethod]
        public void BreakChain()
        {
            var messeger = new Messager();
            var mock1 = new MessagerDecoratorMock(messeger);
            var mock2 = new MessagerDecoratorMock(mock1);
            var mock3 = new MessagerDecoratorMock(mock2);

            mock3.SetResult(Result.Fail);
            mock3.Send(new MessageData("", "", Guid.Empty));

            Assert.IsFalse(mock1.WasCalled);
            Assert.IsFalse(mock2.WasCalled);
            Assert.IsTrue(mock3.WasCalled);
        }
        [TestMethod]
        public void WebPush()
        {
            Manager.User.Subscribe(new Logic.Models.PushNotificationSubscription(MockRepositories.UserFirst.Id, "", "", ""));
            var mock = new WebPushMock();
            var messeger = new Messager();
            var push = new PushNotificationDecorator(messeger, mock, Manager);

            var data = new MessageData("some title", "a message", MockRepositories.BranchFirst.Id);
            data.AddRecipient(MockRepositories.UserFirst.Role);
            push.Send(data);

            Assert.IsNotNull(mock.Subscription);
            Assert.IsTrue(mock.Subscription.Payload.Contains("title\":\"" + data.Title));
            Assert.IsTrue(mock.Subscription.Payload.Contains("body\":\"" + data.Message));
        }
    }
}