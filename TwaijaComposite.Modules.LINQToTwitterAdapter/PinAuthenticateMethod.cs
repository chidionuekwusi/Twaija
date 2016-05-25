using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using LinqToTwitter;
using System.Threading.Tasks;
namespace TwaijaComposite.Modules.LINQToTwitterAdapter
{
    public class PinAuthenticateMethod:IPinAuthenticateMethod
    {
        public bool Authenticate(string consumerkey, string consumerSecret, string requestToken, string oop, out object token)
        {
            var result= AuthenticateAsync(consumerkey,consumerSecret, requestToken,oop).Result;
            token = result.Item2;
            return result.Item1;
   
        }
        public async Task<Tuple<bool,object>> AuthenticateAsync(string consumerkey, string consumerSecret, string requestToken, string oop)
        {
           object token = null;
            try
            {
                var auth = new PinAuthorizer()
                {
                    GetPin = () => { return oop; },
                    CredentialStore = new InMemoryCredentialStore() { ConsumerKey = consumerkey, ConsumerSecret = consumerSecret },
                    GoToTwitterAuthorization = (s) => { }
                };
                await  auth.AuthorizeAsync();
                token = new TwitterToken() { ConsumerKey = consumerkey, ConsumerSecret = consumerSecret, ScreenName = auth.CredentialStore.ScreenName, TokenKey = auth.CredentialStore.OAuthToken, TokenSecret = auth.CredentialStore.OAuthTokenSecret };
                return new Tuple<bool, object>(!string.IsNullOrEmpty(auth.CredentialStore.OAuthToken) ? true : false, token);
            }
            catch (Exception j)
            {
                return new Tuple<bool,object>(false,null);
                //throw;
            }
        }
    }
}
