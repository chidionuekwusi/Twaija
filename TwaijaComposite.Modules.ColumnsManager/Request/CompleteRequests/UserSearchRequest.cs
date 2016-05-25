using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class UserSearchRequest:NavigationEnabledLoopBasedRequest_Twitter<AggregateMessage<TwitterProfile_SmallViewmodel>,IList<ITwitterProfile_Small>>
    {
        IUserSearchMethod _method;
        public string TargerScreenName { set { _method.TargetScreenName = value; } }
        public UserSearchRequest(IUserSearchMethod method):base(method)
        {
            this._method = method;
        }
        public override void ConfigureTwitterUser(ITwitterUser user)
        {
            base.ConfigureTwitterUser(user);
            _method.User = user;
        }
        public override void Response_Foward(Common.IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
        public override void Response_Backwards(Common.IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
    }
}
