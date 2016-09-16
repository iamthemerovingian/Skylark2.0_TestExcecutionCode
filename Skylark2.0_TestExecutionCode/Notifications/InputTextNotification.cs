using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skylark2_TestExecutionCode.Notifications
{
    class InputTextNotification : Confirmation
    {
        public string ErrorCodeText { get; set; }

        public string RootCauseText { get; set; }

    }
}
