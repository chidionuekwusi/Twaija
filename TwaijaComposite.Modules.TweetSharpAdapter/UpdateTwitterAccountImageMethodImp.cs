using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.TweetSharpAdapter
{
    public class UpdateTwitterAccountImageMethodImp : ITwitterUpdateAccountImageMethod
    {
        public void UpdateProfileImage(string imageLocation, Modules.Common.DataInterfaces.ITwitterUser mainuser)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                try
                {
                    var parameters = s as object[];
                    var user = parameters[1] as Modules.Common.DataInterfaces.ITwitterUser;
                    TweetSharp.TwitterService service = new TweetSharp.TwitterService(consumerKey: user.Token.ConsumerKey, consumerSecret: user.Token.ConsumerSecret, token: user.Token.TokenKey, tokenSecret: user.Token.TokenSecret);
                    var response = service.UpdateProfileImage(new TweetSharp.UpdateProfileImageOptions() { ImagePath = parameters[0] as string, IncludeEntities = true });
                    if (!string.IsNullOrEmpty(response.ProfileImageUrl))
                    {
                        ProfileImageUpdated(this, new UpdateProfileEventArguments() { Successful = true, parameter = new Uri(response.ProfileImageUrl) });
                        return;
                    }
                  
                }
                catch (ArithmeticException e)
                {
                    ProfileImageUpdated(this, new UpdateProfileEventArguments() { Aborted = true });
                    return;
                }
                catch
                {

                }
                ProfileImageUpdated(this, new UpdateProfileEventArguments() { Successful = false });
            }), new object[] { imageLocation, mainuser });
        }

        public event EventHandler<UpdateProfileEventArguments> ProfileImageUpdated;
    }
}
