using Logic.Utilities;

namespace Logic.Messages
{
    public class Messager : IMessager
    {
        public Result Send(MessageData data) => Result.Success;
    }
}
