using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.ColumnsManager.Converter;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public abstract class TimerBasedRequestTemplate<T,B>:IRequest,ITimerBasedRequest where T:IMessage
    {
        #region field
        [Dependency]
        public IConverter<T, B> Converter { get; set; }
        private object synchlock = new object();

        #endregion
        public TimerBasedRequestTemplate() { }
        /// <summary>
        /// Default ColumnDirective used to post message is Add
        /// </summary>
        /// <param name="o"></param>
        protected virtual void Action(IMessage o)
        {         
            OnNewMessage(o, ColumnDirective.Add);
        }
        /// <summary>
        /// returns true always if not overriden by subclass 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        protected virtual bool Condition(object state)
        {
            return true;
        }
        /// <summary>
        /// Default ColumnDirective used to post message is Insert
        /// </summary>
        /// <param name="o"></param>
        protected virtual void CounterAction(IMessage o)
        {           
            OnNewMessage(o, ColumnDirective.Insert);
        }
        protected virtual B Request()
        {
            return default(B);
        }
        public  void TemplateMethodImp(object o)
        {
            lock (synchlock)
            {
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
                        if (Condition(message))
                        {
                            Action(message);
                        }
                        else
                        {
                            CounterAction(message);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
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

        public virtual void Dispose()
        {
            State.Dispose(this);
        }
        [Dependency]
        public ITimer RTimer
        {
            get;
            set;
        }

        private int _refreshTime=60000;
        public int RefreshTime
        {
            get
            {
                return _refreshTime;
            }
            set
            {
                _refreshTime = value;
            }
        }
        private int _initialdelay=0;
        public int InitialDelayTime
        {
            get
            {
                return _initialdelay;
            }
            set
            {
                _initialdelay = value;
            }
        }
        private Action<object> _template;
        public Action<object> TemplateMethod
        {
            get
            {
                if (_template == null)
                {
                    _template = TemplateMethodImp;
                }
                return _template;
            }
        }
        [Dependency]
        public IRequestState<ITimerBasedRequest> State
        {
            get;
            set;
        }
        protected void OnNewMessage(IMessage message, ColumnDirective dir)
        {
            if (NewMessage != null)
            {
                NewMessage(message, dir);
            }
        }
    }
}
