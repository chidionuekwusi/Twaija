 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common;
namespace TwaijaComposite.RequestAdapterModule
{
   public class HomeTimelineImp:TwitterMethodBase,IHomeTimeline
   {
       #region fields
       private decimal _newestId = 0;
       private decimal _lastId;
       bool firstRequest=true;
       #endregion
       public HomeTimelineImp()
       {
          
       }
       public static IList<Modules.Common.ITweet> Convert(TwitterStatusCollection collection)
       {
#if !SILVERLIGHT

           var tweets = new List<TwaijaComposite.Modules.Common.ITweet>();
           foreach (TwitterStatus status in collection)
           {
               try
               {
                   tweets.Add(new TweetAdapter(status));
               }
               catch
               {
               }
           }
           return tweets;
#else
            List<Modules.Common.ITweet> tweets = new List<Modules.Common.ITweet>();
            foreach (TwitterStatus status in collection)
            {
                tweets.Add(new TweetAdapter(status));
            }
            return tweets;

#endif
       }
      public static IList<Modules.Common.ITweet> ConvertAndSort(TwitterStatusCollection collection)
       {
#if !SILVERLIGHT
          
           var sortedtweets = new SortedList<TwaijaComposite.Modules.Common.ITweet, object>();
           foreach (TwitterStatus status in collection)
           {
               sortedtweets.Add(new TweetAdapter(status), null);
           }
           return sortedtweets.Keys.ToList();
#else
            List<Modules.Common.ITweet> tweets = new List<Modules.Common.ITweet>();
            foreach (TwitterStatus status in collection)
            {
                tweets.Add(new TweetAdapter(status));
            }
            var array = tweets.ToArray();
            Array.Sort(array);
            return array.ToList();

#endif
       }
       public IList<Modules.Common.ITweet> Refresh(Navigation direction )
        {
            IList<Modules.Common.ITweet> tweets = null;
            TimelineOptions options = new TimelineOptions() { IncludeRetweets = true, Count = this.User.NOOTimeline, UseSSL = true };
            TwitterStatusCollection response = null;
            switch (direction)
            {
                case Modules.Common.Navigation.None:
                    if (_newestId != default(decimal))
                    {
                        options.SinceStatusId = _newestId;
                    }
                    
                    var result = TwitterTimeline.HomeTimeline(Token, options);
                     response =result.ResponseObject;

                    tweets = (firstRequest)? Convert(response):ConvertAndSort(response);
                    if (response.Count != 0)
                    {
                        _newestId = response.First().Id;
                    }
                    break;
                case Modules.Common.Navigation.Forward:
                    options.MaxStatusId = _lastId;
                    options.Count = 10;
                    response = TwitterTimeline.HomeTimeline(Token, options).ResponseObject;
                    tweets =Convert(response);
                    if (response.Count != 0)
                    {
                        _lastId = response.Last().Id;
                    }
                    break;
            }
            if (firstRequest)
            {
                _lastId = tweets.Last().Id;
                _newestId = tweets.First().Id;
                firstRequest = false;
            }
            return tweets;
        }

       public IList<Modules.Common.ITweet> Create(Modules.Common.Navigation direction)
       {
           IList<ITweet> tweets = null;
           try
           {
               tweets = Refresh(direction);
               if (tweets.Count == 0)
               {
                   tweets = null;
               }
           }
           catch (Exception)
           {
           }
           return tweets;
       }

   }
}
