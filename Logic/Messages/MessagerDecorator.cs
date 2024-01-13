using Logic.Utilities;

namespace Logic.Messages
{
    public abstract class MessagerDecorator : IMessager
    {
        protected readonly IMessager wrappee;
        public MessagerDecorator(IMessager messager)
        {
            wrappee = messager;
        }
        public abstract Result Send(MessageData data);
        protected Result HandleWrappee(MessageData data, Result res)
        {
            if (res.IsUnSuccessful) return res;

            return wrappee.Send(data);
        }
    }
}
