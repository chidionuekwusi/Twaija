using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public delegate void SuccessfulMessageResponse(IMessage message);
    public abstract class NavigationEnabledTimerBasedRequestTemplate<T,B>:TimerBasedRequestTemplate<T,B>,INavigationEnabledRequest where T:IMessage
    {
        public NavigationEnabledTimerBasedRequestTemplate(IRequestMethod<B> method)
        {
            r_Method = method;
        }
        private object synchlock = new object();
        protected IRequestMethod<B> r_Method;
        protected override B Request()
        {
            return r_Method.Create(Navigation.None);
        }
        private void Template(Navigation direction, SuccessfulMessageResponse resp)
        {
            /*Attempt to navigate the request foward, if it fails three times abort the process*/
            lock (synchlock)
            {
                IMessage message = null;
                int attempts = 0;
                while (true)
                {
                    if (attempts >= 3)
                    {
                        break;
                    }
                    try
                    {
                        var response = r_Method.Create(direction);
                        if (response != null)
                        {
                            message = Converter.Convert(response);
                        }

                    }
                    catch (Exception)
                    {
                        
                    }
                    try
                    {
                        if (message != null)
                        {
                            resp.Invoke(message);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    attempts++;
                }
            }
        }
        public virtual void Foward()
        {

            Template(Navigation.Forward, Response_Foward);
        }

        public  void Backwards()
        {
            Template(Navigation.Back, Response_Backwards);
        }


        public virtual void Response_Foward(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.Add);
        }

        public virtual void Response_Backwards(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.Insert);
        }
    }
}
