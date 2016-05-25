using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Clipboard;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Resources;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    internal class ReplyOperation:IClipboardOperation
    {
        public ReplyOperation(string screenName)
        {
            OperationDescription = screenName;
        }
        public bool Processed
        {
            get;
            set; 
        }

        public string OperationKey
        {
            get { return ClipboardOperationKeys.ReplyToKey; }
        }
        private string descrip="In reply to ";
        public string OperationDescription
        {
            get { return descrip; }
            private set
            {
                if (descrip != value)
                {
                    descrip += value;
                }
            }
        }

        public string PostMessageServiceKey
        {
            get { return ServiceKeys.DefaultTwitterPostalServiceKey; }
        }

        public object Parameter
        {
            get;
            set;
        }
    }
    internal class DirectMessageOperation : IClipboardOperation
    {
        #region IClipboardOperation Members

        public bool Processed
        {
            get;
            set;
        }

        public string OperationKey
        {
            get { return ClipboardOperationKeys.TwitterDirectMessageKey; }
        }

        public string OperationDescription
        {
            get { return "Direct Message"; }
        }

        public string PostMessageServiceKey
        {
            get { return ServiceKeys.TwitterDirectMessagePostalServiceKey; }
        }

        public object Parameter
        {
            get;
            set;
        }

        #endregion
    }

    public class DirectMessageReplyCommandHelper
    {
        public decimal UserId { get; set; }
        public string ScreenName { get; set; }
        [Dependency]
        public IEventAggregator aggr { get; set; }
        public  void Execute(object parameter)
        {
            aggr.GetEvent<CopyToClipboardEvent>().Publish(ScreenName);
            //if (UserId != default(decimal))
            //{
                aggr.GetEvent<SetClipboardOperationEvent>().Publish(new DirectMessageOperation() {  Parameter = ScreenName });
            //}
        }
    }
    public class ReplyCommandHelper
    {
       public decimal UserId { get; set; }
       public string ScreenName { get; set; }
        [Dependency]
       public IEventAggregator aggr { get; set; }
        public  void Execute(object parameter)
        {
            string text = '@' + ScreenName;
            aggr.GetEvent<CopyToClipboardEvent>().Publish(text);
            if (UserId != default(decimal))
            {
                aggr.GetEvent<SetClipboardOperationEvent>().Publish(new ReplyOperation(text) { Parameter = UserId });
            }
        }
    }
}
