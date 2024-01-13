using Logic.Utilities;

namespace Logic.Messages
{
    public interface IMessager
    {
        public Result Send(MessageData data);
    }
}
