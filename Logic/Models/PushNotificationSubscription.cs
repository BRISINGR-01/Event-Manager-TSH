namespace Logic.Models
{
    public class PushNotificationSubscription
    {
        public Guid UserId { get; private set; }
        public string Endpoint { get; private set; }
        public string P256DH { get; private set; }
        public string Auth { get; private set; }
        public PushNotificationSubscription(Guid userId, string endpoint, string p256DH, string auth)
        {
            UserId = userId;
            Endpoint = endpoint;
            P256DH = p256DH;
            Auth = auth;
        }
    }
}
