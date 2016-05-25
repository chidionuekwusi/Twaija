using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using TwaijaComposite.Modules.Common;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public class PinAuthenticateMethod:IPinAuthenticateMethod
    {
        public bool Authenticate(string consumerkey, string consumerSecret, string requestToken, string oop,out object token)
        {
            token = null;
            try
            {
               
               var response= OAuthUtility.GetAccessToken(consumerkey,consumerSecret,requestToken,oop.ToString());
               token = new TwitterToken() {  ScreenName=response.ScreenName, TokenKey= response.Token, TokenSecret = response.TokenSecret, ConsumerKey = consumerkey, ConsumerSecret = consumerSecret };
               return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
