using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class TwitterProfile_LargeRequest:LoopBasedRequestTemplate_Twitter<SingleMessage<TwitterProfile_LargeViewmodel>,ITwitterProfile_Large>
    {
        IRetrieveTwitterProfile_LargeMethod _method;
        public string ScreenName { set { _method.TargetScreenName = value; } }
        public TwitterProfile_LargeRequest(IRetrieveTwitterProfile_LargeMethod method)
        {
            this._method = method;
        }
        protected override ITwitterProfile_Large Request()
        {
            return  _method.Create(Navigation.None);
        }
        public override void ConfigureTwitterUser(ITwitterUser user)
        {
            base.ConfigureTwitterUser(user);
            _method.User = user;
        }
    }
}
