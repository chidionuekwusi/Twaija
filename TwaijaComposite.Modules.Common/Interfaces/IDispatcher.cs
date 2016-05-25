using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading;

namespace TwaijaComposite.Modules.Common.Services
{
    public interface IDispatcher
    {
        void Invoke(SendOrPostCallback method, object args=null);
        void Invoke(SendOrPostCallback method, bool directInvocation, object args=null);
        void BeginInvoke(SendOrPostCallback method,object args=null);
        void BeginInvoke(Action action);
    }
    
    
}
