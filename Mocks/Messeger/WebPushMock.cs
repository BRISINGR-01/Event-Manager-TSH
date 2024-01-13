using Logic.Interfaces;
using Logic.Utilities;

namespace Mocks.Messeger
{
    public class WebPushMock : IWebPushHandler
    {
        public PushSubscription? Subscription;
        public WebPushMock() { }

        public void Send(PushSubscription subscription)
        {
            Subscription = subscription;
        }
    }
}
