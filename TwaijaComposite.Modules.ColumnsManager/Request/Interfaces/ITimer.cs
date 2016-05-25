using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
   public interface ITimer:IDisposable
    {
       void Initialize(Action<object> callbackMethodToInvoke, object state, int interval);
       void Change(int delay, int newInterval);
       bool Initialised { get; set; }
    }
}
