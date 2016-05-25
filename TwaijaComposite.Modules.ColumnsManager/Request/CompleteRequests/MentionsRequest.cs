using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class MentionsRequest : NavgiationEnabledTimerBasedRequestTemplate_Twitter<AggregateMessage<TweetViewmodel>, IList<ITweet>>
    {
       
       IMentionsMethod _method { get; set; }
       PropertyChangedManager p_Manager;
       bool FirstRequest = true;

       public MentionsRequest(IMentionsMethod method)
           : base(method)
       {
           this._method = method;
           //Initialize Property Manager to handle changes..
           p_Manager = new PropertyChangedManager(this);
           p_Manager["ColumnRefreshTime"] = new PropertyChangeReaction("RefreshTime", new Action(() => { _method.Reset(); Restart(); }));
       }
       public override void ConfigureTwitterUser(ITwitterUser user)
       {
           base.ConfigureTwitterUser(user);
           _user.PropertyChanged += p_Manager.PropertyChangedHandler;
           _method.User = user;

       }
       protected override IList<ITweet> Request()
       {
           return _method.Create(Navigation.None);
       } 

       protected override bool Condition(object state)
       {
           return FirstRequest;
       }

       protected override void Action(IMessage o)
       {
           FirstRequest = false;
           base.Action(o);
       }
    }
}
