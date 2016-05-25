using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Request
{
    public class ConversationRequest:LoopBasedRequestTemplate_Twitter<SingleMessage<TweetViewmodel>,ITweet>
    {
        /// <summary>
        /// The Id of the first Tweet to obtain.
        /// </summary>
        public decimal TweetId { set { method.TweetId = value; } }
        /// <summary>
        /// The First Tweet To Display.
        /// </summary>
        public ITweet FirstTweet { set { method.Tweet = value; } }

        private IRetrieveConversationMethod method;
        protected override void Action(IMessage message)
        {
            OnNewMessage(message, ColumnDirective.Add);
        }
        public ConversationRequest(IRetrieveConversationMethod method)
        {
            this.IsContinousLoop = true;
            this.method = method;
            this.method.EndOfConversation += new EventHandler(method_EndOfConversation);
        }
        protected override ITweet Request()
        {
            return method.Create(Navigation.None);
        }
        void method_EndOfConversation(object sender, EventArgs e)
        {
            this.RequestAbortedFlag = true;
        }
        public override void ConfigureTwitterUser(Common.DataInterfaces.ITwitterUser user)
        {
            base.ConfigureTwitterUser(user);
            method.User = user;
        }
        public override void Dispose()
        {
            base.Dispose();
            this.method.EndOfConversation -= method_EndOfConversation;
        }
    }
}
