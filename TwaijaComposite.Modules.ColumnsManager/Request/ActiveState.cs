using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Request;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ActiveState:IRequestState<ITimerBasedRequest>,IRequestState<ILoopRequest>
    {
        public void Start(ITimerBasedRequest context)
        {
            if (context.RTimer != null)
            {
                Restart(context);
            }
        }

        public void Restart(ITimerBasedRequest context)
        {
            if (context.RTimer != null)
            {
                context.RTimer.Change(context.InitialDelayTime, context.RefreshTime);
            }
        }

        public void Stop(ITimerBasedRequest context)
        {

            if (context.RTimer != null)
            {
                context.RTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            }
        }

        public void Dispose(ITimerBasedRequest context)
        {
            if (context.RTimer != null)
            {
                context.RTimer.Dispose();
            }
        }

        public void Start(ILoopRequest context)
        {
            
        }

        public void Restart(ILoopRequest context)
        {
            
        }

        public void Stop(ILoopRequest context)
        {
            context.RequestAbortedFlag = true;
            context.State = new IdleState();
        }

        public void Dispose(ILoopRequest context)
        {
            Stop(context);
        }
    }
}
