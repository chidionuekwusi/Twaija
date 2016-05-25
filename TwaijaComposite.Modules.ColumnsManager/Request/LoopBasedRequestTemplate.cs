using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
using System.Threading;
using TwaijaComposite.Modules.ColumnsManager.Converter;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public abstract class LoopBasedRequestTemplate<T,B>:ILoopRequest ,IRequest where T:IMessage
    {
        protected bool IsContinousLoop = false;
        private object synchlock = new object();
        [Dependency]
        public IConverter<T, B> Converter { get; set; }      
        [Dependency]
        public IRequestState<ILoopRequest> State { get; set; }
        public virtual void EnterRequestLoop()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((d) =>
            {
                lock (synchlock)
                {
                    int attempts = 0;
                    AutoResetEvent rst = new AutoResetEvent(false);
                    while (true)
                    {
                        if (RequestAbortedFlag)
                        {
                            break;
                        }
                        if (attempts >= 2)
                        {
                            attempts = 0;
                            rst.WaitOne(30000);
                        }
                        IMessage message = null;
                        try
                        {
                            var data = Request();
                            if (data != null)
                            {
                                message = Converter.Convert(data);
                            }
                        }
                        catch (Exception e)
                        {
                           
                        }
                        try
                        {
                            if (message != null)
                            {
                                Action(message);
                                if (!IsContinousLoop)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }
                        attempts++;
                    }
                    this.Stop();
                }
            }));
        }
        /// <summary>
        /// Default behaviour sent is AddAndClear
        /// </summary>
        /// <param name="message"></param>
        protected virtual void Action(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
        protected virtual B Request()
        {
            return default(B);
        }
        public void Start()
        {
            State.Start(this);
        }

        public void Restart()
        {
            State.Restart(this);
        }

        public void Stop()
        {
            State.Stop(this);
        }

        public event NewMessageHandler NewMessage;
        protected void OnNewMessage(IMessage message, ColumnDirective directive)
        {
            if (NewMessage != null)
            {
                NewMessage(message, directive);
            }
        }
        public virtual void Dispose()
        {
            State.Dispose(this);
        }

        public bool RequestAbortedFlag
        {
            get;
            set;
        }
    }
}
