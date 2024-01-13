using Logic.Models;

namespace Logic.Utilities
{
    public class PushSubscription
    {
        public string Endpoint = string.Empty;
        public string P256DH = string.Empty;
        public string Auth = string.Empty;
        public string Payload = string.Empty;
        public PushSubscription(string payLoad)
        {
            Payload = payLoad;
        }

        public PushSubscription WithData(PushNotificationSubscription data)
        {
            Endpoint = data.Endpoint;
            P256DH = data.P256DH;
            Auth = data.Auth;

            return this;
        }
    }
}
