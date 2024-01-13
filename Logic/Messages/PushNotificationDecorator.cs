using Logic.Interfaces;
using Logic.Utilities;
using Newtonsoft.Json;

namespace Logic.Messages
{
    public class PushNotificationDecorator : MessagerDecorator, IMessager
    {
        private readonly IManager manager;
        IWebPushHandler webPushHandler;
        public PushNotificationDecorator(IMessager wrappee, IWebPushHandler webPushHandler, IManager manager) : base(wrappee)
        {
            this.webPushHandler = webPushHandler;
            this.manager = manager;
        }
        public override Result Send(MessageData data)
        {
            Result res = Result.Fail;

            var getSubscriptions = manager.User.GetSubscriptions(data.Recipients, data.BranchId);

            if (getSubscriptions.IsUnSuccessful) return HandleWrappee(data, getSubscriptions.Fail);

            var payloadObj = new Dictionary<string, string>() {
                { "body", data.Message },
                {"title", data.Title },
            };
            string payload = JsonConvert.SerializeObject(payloadObj);
            PushSubscription pushSubscription = new(payload);

            foreach (var subscription in getSubscriptions.Value)
            {
                try
                {
                    webPushHandler.Send(pushSubscription.WithData(subscription));
                    res = Result.Success;
                }
                catch (Exception ex)
                {
                    if (res.IsUnSuccessful) res = Result.FailWith(ex.Message);
                }
            }

            return HandleWrappee(data, res);
        }
    }
}
