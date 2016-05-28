using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;
using System.Globalization;
namespace TwaijaComposite.RequestAdapterModule
{
    public class TweetSearchMethodImp:ITweetSearchMethod
    {
        decimal topId;
        bool first=true;
        int page = 0;
        public string Query
        {
            get;
            set;
        }

        public string GeoLocation
        {
            get;
            set;
        }

        public Modules.Common.DataInterfaces.ITwitterUser User
        {
            get;
            set;
        }

        static IList<ITweet> Convert(TwitterSearchResultCollection collection)
        {
            IList<ITweet> tweets = new List<ITweet>();
            foreach (var result in collection)
            {
                tweets.Add(new TweetAdapter(result));
            }
            return tweets;
        }
        IList<ITweet> Refresh(Navigation direction)
        {
            int requestcount = 50;
            if (User != null)
            {
                requestcount = User.NOOSearch;
            }
            IList<ITweet> tweets = null;
            int currentPage = page++;
            SearchOptions options=new SearchOptions() { Language=CultureInfo.CurrentCulture.TwoLetterISOLanguageName, Count =requestcount, GeoCode=GeoLocation, IncludeEntities=true,Locale="ja"};
            if (!first)
            {
                options.UntilDate = DateTime.Now;
            }
            switch (direction)
            {
                case Navigation.Forward:
                    tweets = Convert(TwitterSearch.Search(Query, new SearchOptions() { Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName, Count = 50, GeoCode = GeoLocation, IncludeEntities = true, Locale="ja" }).ResponseObject);
                    break;
                case Navigation.None:
                    tweets= Convert(TwitterSearch.Search(Query, options).ResponseObject); 
                    break;
            }
           
            if (tweets != null)
            {
                if (first)
                {
                    first = false;
                }
                page++;
            }
            return tweets;
        }
        public IList<Modules.Common.ITweet> Create(Modules.Common.Navigation direction)
        {
            IList<ITweet> tweets = null;
            try
            {
               tweets= Refresh(direction);
            }
            catch (Exception e)
            {

            }
            return tweets;
        }
    }
}
