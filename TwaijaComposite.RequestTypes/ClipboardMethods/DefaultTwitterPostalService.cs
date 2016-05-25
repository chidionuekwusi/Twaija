using System;
using Twitterizer;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Exceptions;

namespace TwaijaComposite.RequestAdapterModule
{
    public class DefaultTwitterPostalService:IPostMessageService
    {
        public string Name
        {
            get { return ServiceKeys.DefaultTwitterPostalServiceKey; }
        }

        public bool PostMessage(Modules.Common.DataInterfaces.IUser user, string message,params object[] paramter)
        {
            if (!(user is ITwitterUser))
            {
                throw new WrongUserTypeException("Wrong user type supplied to post method, a TwitterUser is required, what was supplied:" + user.GetType().FullName);
            }
            var token=(user as ITwitterUser).Token;
            try
            {
                if (paramter != null)
                {
                    if (paramter.Length > 0)
                    {
                        var response=TwitterStatus.Update(new OAuthTokens() { AccessToken = token.TokenKey, AccessTokenSecret = token.TokenSecret, ConsumerKey = token.ConsumerKey, ConsumerSecret = token.ConsumerSecret }, message, new StatusUpdateOptions() { InReplyToStatusId = Convert.ToDecimal(paramter[0]) }).ResponseObject;
                        if (response != null)
                        {
                            OnMessageSent(message);
                            return true;
                        }
                        else
                        {
                            OnMessageFailed(new MessageFailedEventArgs() { Message = message });
                            return false;
                        }
                    }
                }
               var response0=TwitterStatus.Update(new OAuthTokens() { AccessToken = token.TokenKey, AccessTokenSecret = token.TokenSecret, ConsumerKey = token.ConsumerKey, ConsumerSecret = token.ConsumerSecret }, message, new StatusUpdateOptions() {  }).ResponseObject;
               if (response0 != null)
               {
                   OnMessageSent(message);
                   return true;
               }
               else
               {
                   OnMessageFailed(new MessageFailedEventArgs() { Message = message});
               }
               return false;
            }
            catch (TwitterizerException e)
            {
                OnMessageFailed(new MessageFailedEventArgs() { Message = message, Exception = e });
              
            }
            return false;
        }



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
                MessageFailed(this,args);
            }
        }
    }
}
