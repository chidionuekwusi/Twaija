using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Reflection;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.Exceptions;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager
{
    /*Overrides Action to support the condition change*/
    public class HomeTimelineRequest : NavgiationEnabledTimerBasedRequestTemplate_Twitter<AggregateMessage<TweetViewmodel>, IList<ITweet>>
    {
        #region fields  
        bool FirstRequest = true;
        IHomeTimeline Timeline;
        PropertyChangedManager p_Manager;
        #endregion
        [InjectionConstructor]
        public  HomeTimelineRequest(IHomeTimeline tl):base(tl)
        {     
            Timeline = tl;
            //Initialize Property Manager
            p_Manager = new PropertyChangedManager(this);
            p_Manager["ColumnRefreshTime"]=new PropertyChangeReaction("RefreshTime",new Action(Restart));
        }
        public override void Dispose()
        {
            this._user.PropertyChanged -= p_Manager.PropertyChangedHandler;
            this._user = null;
            base.Dispose();
           
        }
        public override void ConfigureTwitterUser(ITwitterUser user)
        {
            base.ConfigureTwitterUser(user);
            _user.PropertyChanged += p_Manager.PropertyChangedHandler;
            Timeline.User = user;
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
        protected override IList<ITweet> Request()
        {
            return Timeline.Create(Navigation.None);
        }
        
    }
}
