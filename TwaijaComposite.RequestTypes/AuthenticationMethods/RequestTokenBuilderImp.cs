using System;
using System.Threading;
using TwaijaComposite.Modules.Common;
using Twitterizer;
namespace TwaijaComposite.RequestAdapterModule
{
    public class RequestTokenBuilderImp : IRequestTokenBuilder
    {
        private string Token = null;
        private bool CanLeave = false;
        public string BuildRequestTokenString(string ConsumerKey, string ConsumerSecret, string callbackaddress)
        {
#if SILVERLIGHT
            TweetSharp.TwitterService service = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
            service.GetRequestToken(callbackaddress,(token0,response) => 
            {                
                Token=token0.Token;
             
            });
            AutoResetEvent eve = new AutoResetEvent(false);
            while (Token == null)
            {
                eve.WaitOne(200);
            }
            return Token;
#else
            var token = OAuthUtility.GetRequestToken(ConsumerKey, ConsumerSecret, callbackaddress);
            return token.Token;
#endif
        }
    }
}
