using System;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule.Commands
{
   public class FollowCommandImp:IFollowCommand
    {

       public bool Follow(string ScreenName, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterFriendship.Create(token, ScreenName).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }
    }
   public class UnFollowCommandImp:IUnFollowCommand
    {

       public bool UnFollow(string ScreenName, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterFriendship.Delete(token, ScreenName).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }
    }
   public class BlockCommandImp:IBlockCommand
    {

       public bool Block(string ScreenName, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterBlock.Create(token, ScreenName).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }
    }
   public class UnBlockCommandImp : IUnBlockCommand
   {

       public bool UnBlock(string ScreenName, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterBlock.Destroy(token, ScreenName).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }

   }
   public class FavouriteCommandImp : IFavouriteCommand
   {

       public bool Favourite(decimal Id, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterFavorite.Create(token,Id).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }
   }
   public class UnFavouriteCommandImp : IUnFavouriteCommand
   {

       public bool UnFavourite(decimal Id, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterFavorite.Delete(token, Id).ResponseObject != null);
           }
           catch
           {

           }
           return false;
       }
   }
   public class DeleteCommandImp : IDeleteTweetCommand
   {

       public bool DeleteTweet(decimal Id, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var token = new OAuthTokens() { AccessToken = user.Token.TokenKey, AccessTokenSecret = user.Token.TokenSecret, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret };
               return (TwitterStatus.Delete(token, Id).ResponseObject != null);
           }
           catch
           {
           }
           return false;
       }
       
   }
   public class RetweetCommandImp : IRetweetCommand
   {

       public bool Retweet(decimal Id, Modules.Common.DataInterfaces.ITwitterUser user)
       {
           try
           {
               var response=TwitterStatus.Retweet(new OAuthTokens() {AccessToken=user.Token.TokenKey, AccessTokenSecret=user.Token.TokenSecret,ConsumerKey=user.Token.ConsumerKey,ConsumerSecret=user.Token.ConsumerSecret }, Id);
               if (response != null)
               {
                   return true;
               }
           }
           catch
           {
           }
           return false;
       }
   }
}
