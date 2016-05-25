using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public delegate void MessageSentHandler(string message);
    public delegate void MessageFailedHandler(object sender,MessageFailedEventArgs error);
    public class MessageFailedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
    public interface IPostMessageService
    {
        event MessageSentHandler MessageSent;
        event MessageFailedHandler MessageFailed;
        string Name { get; }
        bool PostMessage(IUser user, string message,params object[] parameter);
    }
}
