using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class GetFollowersRequest : NavigationEnabledLoopBasedRequest_Twitter<AggregateMessage<TwitterProfile_SmallViewmodel>, IList<ITwitterProfile_Small>>
    {
        IRetrieveFollowers_Or_Friends_Method method;
        /// <summary>
        /// This is the Target of the main method call sent to twitter,either set this or id but not both
        /// </summary>
        /// <param name="name"></param>
        public void SetTargetScreenName(string name)
        {
            if (method == null) 
            {
                throw new ArgumentNullException("RetrieveFollowersMethod has not been created");
            }
            method.ScreenName = name;
        }
        public override void ConfigureTwitterUser(ITwitterUser user)
        {
            base.ConfigureTwitterUser(user);
            method.User = user;
        }
        public void SetUserId(decimal id)
        {
            if (method == null)
            {
                throw new ArgumentNullException("RetrieveFollowersMethod has not been created");
            }
            method.UserId = id;
        }
        public GetFollowersRequest(IRetrieveFollowers_Or_Friends_Method method):base(method)
        {
            this.method = method;
            method.Relationship = TwitterRelationship.Followers;
        }
        protected override void Action(IMessage o)
        {
            OnNewMessage(o, ColumnDirective.AddAndClear);
        }
        protected override IList<ITwitterProfile_Small> Request()
        {
            return method.Create(Navigation.None);
        }
        public override void Response_Foward(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
        public override void Response_Backwards(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.AddAndClear);
        }
    }
}
