using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public class RetrieveFavouritesMethodImp:TwitterMethodBase,IRetrieveFavouritesMethod
    {

        public string ScreenName
        {
            get;
            set;
        }
        int page = 1;
       IList<ITweet> Refresh(Navigation direction)
        {
            int currentpage=page;
           TwitterStatusCollection collection=null;
           switch (direction)
           {
               case Navigation.Forward:
                   collection = Twitterizer.TwitterFavorite.List(Token, new Twitterizer.ListFavoritesOptions() { UserNameOrId = ScreenName, Page = currentpage++ }).ResponseObject;
                   break;
               case Navigation.Back:
                   collection = TwitterFavorite.List(Token, new ListFavoritesOptions() { UserNameOrId = ScreenName, Page = currentpage-- }).ResponseObject;
                   break;
               case Navigation.None:
                   collection = TwitterFavorite.List(Token, new ListFavoritesOptions() { UserNameOrId = ScreenName, Page = currentpage }).ResponseObject;
                   break;
           }
           if (collection != null)
           {
               page = currentpage;
           }
           return Convert(collection);
        }

        private IList<ITweet> Convert(TwitterStatusCollection collection)
        {
            List<ITweet> tweets = new List<ITweet>();
            foreach (TwitterStatus status in collection)
            {
                tweets.Add(new TweetAdapter(status));
            }
            return tweets;
        }
        public IList<Modules.Common.ITweet> Create(Modules.Common.Navigation direction)
        {
            if (string.IsNullOrEmpty(ScreenName))
            {
                throw new ArgumentNullException("You have to set the Target screenname for favourites to be retrieved");
            }
            IList<ITweet> tweets = null;
            try
            {
                tweets = Refresh(direction);
            }
            catch (Exception)
            {

            }
            return tweets;
        }
    }
}
