using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
   public class TweetSearchRequest:NavigationEnabledTimerBasedRequestTemplate<AggregateMessage<TweetViewmodel>,IList<ITweet>>
    {
       ITweetSearchMethod _method;
       public string Query { set { _method.Query= value; } }
       public string Geo { set { _method.GeoLocation = value; } }
       public ITwitterUser User { set { _method.User = value; } }
       public TweetSearchRequest(ITweetSearchMethod method):base(method)
       {
           this._method = method;
           RefreshTime = 300000;
           
       }
       public override void Response_Foward(IMessage message)
       {
           OnNewMessage(message, ColumnDirective.Add);
       }
       public override void Response_Backwards(IMessage message)
       {
           //throw new NotImplementedException();
       }
    }
}
