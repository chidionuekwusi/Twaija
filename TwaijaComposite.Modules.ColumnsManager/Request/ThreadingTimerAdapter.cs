using System;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class ThreadingTimerAdapter:ITimer
    {
        private System.Threading.Timer timer;
        public void Initialize(Action<object> callbackMethodToInvoke, object state, int interval)
        {
            if (!Initialised)
            {
                timer = new System.Threading.Timer(new System.Threading.TimerCallback(callbackMethodToInvoke), null, 0, interval);
                Initialised = true;
            }
        }

        public void Change(int delay, int newInterval)
        {
            timer.Change(delay, newInterval);
        }

        public bool Initialised
        {
            get;
            set;
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }
    }
}
