using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using Twitterizer;
using System.Threading;

namespace TwaijaComposite.RequestAdapterModule
{
    public class TwitterAccountMethodConsole : ITwitterAccountMethodConsole
    {
        public void UpdateProfile(string location, string description, string web, string name, Modules.Common.DataInterfaces.ITwitterUser user)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                int attempts = 0;
                while (true)
                {
                    if (attempts == 2)
                    {
                        break;
                    }
                    try
                    {
                        
                        var response = TwitterAccount.UpdateProfile(new OAuthTokens() { AccessTokenSecret = user.Token.TokenSecret, AccessToken = user.Token.TokenKey, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret }, new UpdateProfileOptions() { Name = name, Location = location, Url = web, Description = description }).ResponseObject;

                        if (response != null)
                        {
                            ProfileUpdated(this, new UpdateProfileEventArguments() { parameter = new TwitterProfile_LargeAdapter(response), Successful = true });
                            return;
                        }
                    }
                    catch
                    {
                        Thread.Sleep(6000);
                    }
                    attempts++;
                    ProfileUpdated(this, new UpdateProfileEventArguments() {  Successful=false, parameter="failed attempt "+attempts});
                }
                ProfileUpdated(this, new UpdateProfileEventArguments() { Successful = false, Aborted=true });
      
             
            }));
        }

       

        public event EventHandler<UpdateProfileEventArguments> ProfileUpdated;

        public event EventHandler<UpdateProfileEventArguments> Refreshed;

        public void RefreshProfile(Modules.Common.DataInterfaces.ITwitterUser user)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                try
                {
                    var response = Twitterizer.TwitterUser.Show(new OAuthTokens() { AccessTokenSecret = user.Token.TokenSecret, AccessToken = user.Token.TokenKey, ConsumerKey = user.Token.ConsumerKey, ConsumerSecret = user.Token.ConsumerSecret }, user.ScreenName).ResponseObject;

                    if (response != null)
                    {
                        Refreshed(this, new UpdateProfileEventArguments() { parameter = new TwitterProfile_LargeAdapter(response), Successful = true });
                        return;
                    }
                }
                catch
                {

                }
                Refreshed(this, new UpdateProfileEventArguments() { Successful = false ,  });
            }));
        }
    }
}
