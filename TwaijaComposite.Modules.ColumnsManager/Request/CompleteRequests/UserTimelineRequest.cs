using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
   public class UserTimelineRequest:NavigationEnabledLoopBasedRequest_Twitter<AggregateMessage<TweetViewmodel>,IList<ITweet>>
    {
       IUserTimlineMethod method;
       PropertyChangedManager manager;
       public int NumberToRetrieve { set { method.NumberOfObjectsToRetrieve = value; } }
       public string ScreenName { set { method.ScreenName = value; } }
       public UserTimelineRequest(IUserTimlineMethod method):base(method)
       {
           manager = new PropertyChangedManager(this);
           manager["NOOUserTimeline"] = new PropertyChangeReaction("NumberToRetrieve");
           this.method = method;
       }
       //Overridden in this class to setup up property manager.
       public override void ConfigureTwitterUser(Common.DataInterfaces.ITwitterUser user)
       {
           base.ConfigureTwitterUser(user);
           method.User = user;
           user.PropertyChanged += manager.PropertyChangedHandler;      
       }
    }
}
