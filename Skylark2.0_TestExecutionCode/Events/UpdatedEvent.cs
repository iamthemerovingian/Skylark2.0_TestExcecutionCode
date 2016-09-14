using Prism.Events;
using Skylark2_TestExecutionCode.Models;


namespace Skylark2_TestExecutionCode.Events
{
    /// <summary>
    /// This updated event is used to communicate between two different ViewModels.
    /// You can notify the rest of your viewmodels that you have made changes in another viewmodel.
    /// 
    /// The class name is the name of the Event, it implements the PubSubEvent and sends a object of type string.
    /// 
    /// This Object of type string is what goes around to be recieved by other viewmodels.
    /// </summary>
    public class UpdatedEvent : PubSubEvent<string>
    {
    }
    public class ErrorCodeUpdated : PubSubEvent<string>
    {
    }
    public class RootCauseUpdated : PubSubEvent<string>
    {
    }
}
