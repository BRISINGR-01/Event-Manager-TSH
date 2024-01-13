using Logic.Utilities;

namespace Logic.Interfaces
{
    public interface IWebPushHandler
    {
        public void Send(PushSubscription subscription);
    }
}
