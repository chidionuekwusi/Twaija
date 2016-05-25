using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public interface ITwitterrizerMethodConsole
    {
        TwitterResponse<UserIdCollection> GetFollowersIds(OAuthTokens token, UsersIdsOptions options);
        TwitterResponse<TwitterUserCollection> GetUsers(OAuthTokens token, LookupUsersOptions options);
        TwitterResponse<UserIdCollection> GetFriendsIds(OAuthTokens token, UsersIdsOptions options);
    }
    public class TwitterizerMethodConsole:ITwitterrizerMethodConsole
    {

        public TwitterResponse<UserIdCollection> GetFollowersIds(OAuthTokens token, UsersIdsOptions options)
        {
          return  TwitterFriendship.FollowersIds(token, options);
        }

        public TwitterResponse<TwitterUserCollection> GetUsers(OAuthTokens token, LookupUsersOptions options)
        {
            return TwitterUser.Lookup(token, options);
        }


        public TwitterResponse<UserIdCollection> GetFriendsIds(OAuthTokens token, UsersIdsOptions options)
        {
            return  TwitterFriendship.FriendsIds(token, options);
        }
    }
}
