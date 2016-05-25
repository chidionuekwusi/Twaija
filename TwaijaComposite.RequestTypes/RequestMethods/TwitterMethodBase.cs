using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.RequestAdapterModule
{
    public abstract class TwitterMethodBase:ITwitterMethod
    {
        public Twitterizer.OAuthTokens Token { get; set; }
        private ITwitterUser user;
        public Modules.Common.DataInterfaces.ITwitterUser User
        {
            get
            {
                return user;
            }
            set
            {
                if (this.user != value)
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException("User passed to HomeTimelineAdapter Create Method is null");
                    }
                    if (value.Token == null)
                    {
                        throw new ArgumentNullException("User.Token passed to HomeTimelineAdapter Create Method is null");
                    }
                    this.user = value;
                    Token = new Twitterizer.OAuthTokens();
                    Token.AccessToken = user.Token.TokenKey;
                    Token.AccessTokenSecret = user.Token.TokenSecret;
                    Token.ConsumerKey = user.Token.ConsumerKey;
                    Token.ConsumerSecret = user.Token.ConsumerSecret;
                }
            }
           
        }
    }
}
