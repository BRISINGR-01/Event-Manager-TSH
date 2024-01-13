using Logic.Configuration;
using Logic.Interfaces;
using WebPush;

namespace Logic.Messages
{
    public class WebPushHandler : IWebPushHandler
    {
        private VapidDetails config;
        public WebPushHandler(VapidConfig vapid)
        {
            this.config = new VapidDetails(vapid.Subject, vapid.PublicKey, vapid.PrivateKey);
        }
        public void Send(Logic.Utilities.PushSubscription subscription)
        {
            new WebPushClient().SendNotification(new PushSubscription(subscription.Endpoint, subscription.P256DH, subscription.Auth), subscription.Payload, config);
        }

    }
}
