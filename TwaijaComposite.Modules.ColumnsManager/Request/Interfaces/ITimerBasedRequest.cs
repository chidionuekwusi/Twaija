using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public interface ITimerBasedRequest
    {
         ITimer RTimer { get; set; }
         int RefreshTime { get; set; }
         int InitialDelayTime { get; set; }
         Action<object> TemplateMethod { get; }
         IRequestState<ITimerBasedRequest> State { get; set; }
    }
}
