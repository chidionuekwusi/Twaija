using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Request;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class IdleState:IRequestState<ITimerBasedRequest>,IRequestState<ILoopRequest>
    {
        
        public void Restart(ITimerBasedRequest context)
        {
            if (!context.RTimer.Initialised)
            {
                context.RTimer.Initialize(context.TemplateMethod, context.InitialDelayTime, context.RefreshTime);
                context.State = new ActiveState();
            }
        }

        public void Stop(ITimerBasedRequest context)
        {
            
        }

        public void Dispose(ITimerBasedRequest context)
        {
            context.RTimer.Dispose();
        }

        public void Start(ITimerBasedRequest context)
        {
            Restart(context);
        }

        public void Start(ILoopRequest context)
        {
            context.EnterRequestLoop();
            context.State = new ActiveState();
        }

        public void Restart(ILoopRequest context)
        {
            context.RequestAbortedFlag = false;
            Start(context);
        }

        public void Stop(ILoopRequest context)
        {
          
        }

        public void Dispose(ILoopRequest context)
        {
            
        }
    }
}
