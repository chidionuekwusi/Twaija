using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.AttributeTypes;
using TwaijaComposite.Modules.Common.Services;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;
namespace TwaijaComposite.RequestAdapterModule
{
    public class TwitterDirectMessagePostalService:IPostMessageService
    {

        #region IPostMessageService Members

        public string Name
        {
            get { return ServiceKeys.TwitterDirectMessagePostalServiceKey; }
        }

        public bool PostMessage(Modules.Common.DataInterfaces.IUser user, string message, params object[] parameter)
        {
            if (!(user is ITwitterUser))
            {
                throw new WrongUserTypeException("Wrong user type supplied to post method, a TwitterUser is required, what was supplied:" + user.GetType().FullName);
            }
            try
            {
                var id = Convert.ToString(parameter[0]);
                var token = (user as ITwitterUser).Token;
                var response = TwitterDirectMessage.Send(new OAuthTokens() { AccessToken = token.TokenKey, AccessTokenSecret = token.TokenSecret, ConsumerKey = token.ConsumerKey, ConsumerSecret = token.ConsumerSecret },id, message);
                var resp = response.ResponseObject;
                if (resp!= null)
                {
                    OnMessageSent(message);
                    return true;
                }
            }
            catch(Exception t)
            {
                OnMessageFailed(new MessageFailedEventArgs() { Message=message,Exception=t});
            }
            return false;
        }

        #endregion

        public event MessageSentHandler MessageSent;

        public event MessageFailedHandler MessageFailed;
        public void OnMessageSent(string message)
        {
            if (MessageSent != null)
            {
                MessageSent(message);
            }
        }
        public void OnMessageFailed(MessageFailedEventArgs args)
        {
            if (MessageFailed != null)
            {
                MessageFailed(this, args);
            }
        }
    }
}
