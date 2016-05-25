using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public interface ILoopRequest
    {
        bool RequestAbortedFlag { get; set; }
        void EnterRequestLoop();
        IRequestState<ILoopRequest> State { get; set; }
    }
}
