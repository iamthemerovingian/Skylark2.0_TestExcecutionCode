using Prism.Events;

namespace Skylark2_TestExecutionCode.Events
{
    public class UpdatedEvent : PubSubEvent<string>
    {
    }

    public class CloseFormEvent: PubSubEvent<bool>
    {
    }
}
