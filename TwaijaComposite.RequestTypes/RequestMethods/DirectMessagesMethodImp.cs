using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
namespace TwaijaComposite.RequestAdapterModule
{
    public class DirectMessagesMethodImp : TwitterMethodBase,IDirectMessageMethod
    {      
        decimal newestId;
        public IList<Modules.Common.DataInterfaces.ITwitterDirectMessage> Create()
        {
            try
            {
                var response = TwitterDirectMessage.DirectMessages(Token, new DirectMessagesOptions() { SinceStatusId = newestId, Count = User.NOODirectMessages, IncludeEntites = true });
                if (response.ResponseObject.Count == 0)
                {
                    return null;
                }
                newestId = response.ResponseObject[0].Id;
                return Convert(response.ResponseObject);
            }
            catch (Exception)
            {
                return null;
            }
        }
        IList<TwaijaComposite.Modules.Common.DataInterfaces.ITwitterDirectMessage> Convert(TwitterDirectMessageCollection collection)
        {
            IList<TwaijaComposite.Modules.Common.DataInterfaces.ITwitterDirectMessage> list = new List<TwaijaComposite.Modules.Common.DataInterfaces.ITwitterDirectMessage>();
            foreach (TwitterDirectMessage message in collection)
            {
                list.Add(new TwitterDirectMessageAdapter(message));
            }
            return list;
        }
    }
}
