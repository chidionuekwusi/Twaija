using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    [ScreenNamePropertyLocator(TargetPropertyName = "ScreenName")]
    [TextPropertyLocator(TargetPropertyName = "Text")]
   public class DirectMessageViewmodel:ISetable<ITwitterDirectMessage>,ICommandable
    {
       public string DateStringFormat
       {
           get
           {
               return Message.CreatedDate.ToLongDateString()+ ", {0}";
           }
          
       }
       public DirectMessageViewmodel()
       {
           Commands = new List<ICommand>();
       }
       public ITwitterDirectMessage Message
       {
           get;
           set;
       }
        public void Set(ITwitterDirectMessage param)
        {
            Message=param;
        }
        public string Text
        {
            get{return Message.Text;}
        }
         public string ScreenName
        {
            get{return Message.SenderScreenName;}
        }
        #region ICommandable Members

        public IList<System.Windows.Input.ICommand> Commands
        {
            get;
            private set;
        }

        #endregion
    }
}
