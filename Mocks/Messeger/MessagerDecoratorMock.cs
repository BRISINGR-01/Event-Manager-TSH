using Logic.Messages;
using Logic.Utilities;

namespace Mocks.Messeger
{
    public class MessagerDecoratorMock : MessagerDecorator
    {
        private Result res = Result.Success;
        public MessagerDecoratorMock(IMessager messager) : base(messager) { }
        public bool WasCalled = false;
        public void SetResult(Result result)
        {
            res = result;
        }
        public override Result Send(MessageData data)
        {
            WasCalled = true;
            return HandleWrappee(data, res);
        }
    }
}
