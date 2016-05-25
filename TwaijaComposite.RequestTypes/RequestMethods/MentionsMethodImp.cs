using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common;
namespace TwaijaComposite.RequestAdapterModule
{
   public class MentionsMethodImp:TwitterMethodBase,IMentionsMethod
    {

       #region fields
       private decimal _newestId = 0;
       private decimal _lastId;
       bool firstRequest = true;
       #endregion
       public MentionsMethodImp()
       {
          
       }
       public IList<Modules.Common.ITweet> Refresh(Navigation direction)
        {
            IList<Modules.Common.ITweet> tweets = null;
            TimelineOptions options = new TimelineOptions() { IncludeRetweets = true, Count = this.User.NOOTimeline };
            switch (direction)
            {
                case Modules.Common.Navigation.None:
                    options.SinceStatusId = _newestId;
                    var response = TwitterTimeline.Mentions(Token, options).ResponseObject;   
                    tweets =(firstRequest)? HomeTimelineImp.Convert(response):HomeTimelineImp.ConvertAndSort(response);
                    if(response.Count>0)
                     _newestId = response.First().Id;
                    break;
                case Modules.Common.Navigation.Forward:
                    options.MaxStatusId = _lastId;
                    options.Count = 10;
                    var response0 = TwitterTimeline.Mentions(Token, options).ResponseObject;
                    tweets =(firstRequest)? HomeTimelineImp.Convert(response0):HomeTimelineImp.ConvertAndSort(response0);
                    if (response0.Count > 0)
                    _lastId = response0.Last().Id;
                    break;
            }
            if (firstRequest)
            {
                if (tweets.Count > 0)
                {
                    _lastId = tweets.Last().Id;
                    _newestId = tweets.First().Id;
                    firstRequest = false;
                }
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

       public void Reset()
       {
           
       }
    }
}
