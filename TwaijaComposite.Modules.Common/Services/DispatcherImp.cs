using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
namespace TwaijaComposite.Modules.Common.Services
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class RefreshManager : IDispatcher
    {
        private static object synchlock = new object();
        private static Dispatcher dispatcher;
        private static DispatcherSynchronizationContext synchContext;
        private static Dictionary<SendOrPostCallback, object> InvocationQueue { get; set; }
        private static System.Threading.Timer InvocationTimer { get; set; }
        static RefreshManager()
        {        
            InvocationQueue = new Dictionary<SendOrPostCallback, object>();
        }
        public static void AssignDispatcher(Dispatcher dispatch)
        {
            lock (synchlock)
            {
                if (dispatcher == null)
                {
                    dispatcher = dispatch;
                    synchContext = new DispatcherSynchronizationContext(dispatcher);
                }
            }
        }
        public static void StartDispatcherTimer()
        {
            if (InvocationTimer == null)
            {
                InvocationTimer = new System.Threading.Timer(RefreshInvocationQueue, null, 60000, 60000);
            }
        }

        public static void RefreshInvocationQueue(object state)
        {
            lock (synchlock)
            {
                foreach (KeyValuePair<SendOrPostCallback, object> pair in InvocationQueue)
                {
                    try
                    {
#if SILVERLIGHT
                        
                        synchContext.Send(pair.Key,pair.Value);
#else
                        InnerInvoke(pair.Key, pair.Value);
#endif
                    }
                    catch
                    {

                    }
                }
                InvocationQueue.Clear();
            }
        }
#if !SILVERLIGHT
        private static void InnerInvoke(SendOrPostCallback method, object args)
        {
            if (!dispatcher.CheckAccess())
            {
                dispatcher.Invoke(method,DispatcherPriority.Background, args);
            }
            else
            {
                method.Invoke(args);
            }
        }
#endif
        public void Invoke(SendOrPostCallback method,object args=null)
        {
#if SILVERLIGHT
            synchContext.Send(method, args);
#else
            InnerInvoke(method, args);
#endif
        }


        public void Invoke(SendOrPostCallback method, bool directInvocation, object args=null)
        {
            lock (synchlock)
            {
                if (directInvocation)
                {
                    Invoke(method, args);
                    return;
                }
                else
                {

                    try
                    {
                        InvocationQueue.Add(method, args);
                    }
                    catch
                    {
                        RefreshInvocationQueue(null);
                        InvocationQueue.Add(method, args);
                    }
                }
                
            }
        }
        private void InnerBeginInvoke(SendOrPostCallback method,object args)
        {
            if (!dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(method, args);
            }
            else
            {
                dispatcher.BeginInvoke(method, args);
            }
        }
        public void BeginInvoke(SendOrPostCallback method, object args=null)
        {
            InnerBeginInvoke(method,args);    
        }

        public void BeginInvoke(Action action)
        {
            dispatcher.BeginInvoke(action);
        }
    }
}
