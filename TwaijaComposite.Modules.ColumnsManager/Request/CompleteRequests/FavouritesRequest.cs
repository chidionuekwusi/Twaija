using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
   public  class FavouritesRequest:NavigationEnabledLoopBasedRequest_Twitter<AggregateMessage<TweetViewmodel>,IList<ITweet>>
    {
       IRetrieveFavouritesMethod method;
       public string TargetScreenName { set { method.ScreenName = value; } }
       public FavouritesRequest(IRetrieveFavouritesMethod method):base(method)
       {
           this.method = method;
       }
       public override void ConfigureTwitterUser(Common.DataInterfaces.ITwitterUser user)
       {
           base.ConfigureTwitterUser(user);
           method.User = user;
       }

       protected override void Action(IMessage message)
       {
           OnNewMessage(message, ColumnDirective.Add);
       }
    }
}
