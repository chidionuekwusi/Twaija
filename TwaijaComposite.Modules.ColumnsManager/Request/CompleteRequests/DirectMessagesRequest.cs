using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
   public class DirectMessagesRequest:TimerBasedRequestTemplate_Twitter<AggregateMessage<DirectMessageViewmodel>,IList<ITwitterDirectMessage>>
    {
       bool FirstRequest=true;
       PropertyChangedManager p_Manager;
       IDirectMessageMethod method;
       public DirectMessagesRequest(IDirectMessageMethod method)
       {
           this.method = method;
       }
       public override void ConfigureTwitterUser(ITwitterUser user)
       {
           base.ConfigureTwitterUser(user);
           p_Manager = new PropertyChangedManager(this);
           p_Manager["ColumnRefreshTime"] = new PropertyChangeReaction("RefreshTime", new Action(Restart));
           user.PropertyChanged += p_Manager.PropertyChangedHandler;
           method.User = user;
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
        protected override IList<ITwitterDirectMessage> Request()
        {
            return method.Create();
        }
        public override void Dispose()
        {
            this._user.PropertyChanged -= p_Manager.PropertyChangedHandler;
            this._user = null;
            p_Manager = null;
            
            base.Dispose();

        }
    }
}
