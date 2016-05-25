using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.RequestAdapterModule
{
    public class UserTimelineMethodImp : TwitterMethodBase,IUserTimlineMethod
    {
        int page = 1;

        UserTimelineOptions options = new UserTimelineOptions() { IncludeRetweets=true };
        public string ScreenName
        {
            get { return options.ScreenName; }
            set { options.ScreenName = value; }
        }
        
        public int NumberOfObjectsToRetrieve
        {
            get { return options.Count; }
            set { options.Count = value; }
        }
        IList<Modules.Common.ITweet> Refresh(Navigation direction)
        {
            options.Page = page;
            options.Count = User.NOOUserTimeline;
            switch (direction)
            {
                case Modules.Common.Navigation.Forward:
                    options.Page++;
                    break;
                case Modules.Common.Navigation.Back:
                    options.Page--;
                    break;
            }
            var response = TwitterTimeline.UserTimeline(Token, options).ResponseObject;
#if !SILVERLIGHT
            var sortedtweets = new SortedList<TwaijaComposite.Modules.Common.ITweet, object>();
            foreach (TwitterStatus status in response)
            {
                sortedtweets.Add(new TweetAdapter(status), null);
            }
            page = options.Page;
            var list= sortedtweets.Keys.ToList();
            list.Reverse();
            return list;
#else
            List<Modules.Common.ITweet> tweets = new List<Modules.Common.ITweet>();
            foreach (TwitterStatus status in response)
            {
                tweets.Add(new TweetAdapter(status));
            }
            var array = tweets.ToArray();
            Array.Sort(array);
            page=options.Page;
            return array.ToList();

#endif
        }
        public IList<Modules.Common.ITweet> Create(Modules.Common.Navigation direction)
        {
            IList<ITweet> tweets = null;
            try
            {
                tweets= Refresh(direction);
            }
            catch (Exception)
            {

            }
            return tweets;
        }
    }
}
