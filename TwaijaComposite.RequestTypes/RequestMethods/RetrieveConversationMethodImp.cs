using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;
using Twitterizer;
namespace TwaijaComposite.RequestAdapterModule
{
    public class RetrieveConversationMethodImp:TwitterMethodBase,IRetrieveConversationMethod
    {
        public decimal TweetId
        {
            get;
            set;
        }
        private ITweet _tweet;
        bool returnFirstTweet=false;
        public Modules.Common.ITweet Tweet
        {
            get { return _tweet; }
            set
            {
                _tweet = value;
                returnFirstTweet = true;
            }
        }

        ITweet Refresh()
        {
            if (returnFirstTweet)
            {
                TweetId = Tweet.InReplyToStatusId.Value;
                returnFirstTweet = false;
                return Tweet;
            }
            var response = TwitterStatus.Show(TweetId).ResponseObject;
            if (response != null)
            {
                if (response.InReplyToStatusId.HasValue)
                {
                    TweetId = response.InReplyToStatusId.Value;
                }
                else
                {
                    EndOfConversation(this, null);
                }
            }
            return new TweetAdapter(response);
        }
        public Modules.Common.ITweet Create(Modules.Common.Navigation direction)
        {

            if (Tweet == null && TweetId == default(decimal))
            {
                throw new ArgumentNullException("Both TweetId and Tweet cannot be null at the same time");
            }
            ITweet tweet = null;
            try
            {
                tweet = Refresh();
            }
            catch (Exception)
            {
            }
            return tweet;
        }


        public event EventHandler EndOfConversation;
    }
}
