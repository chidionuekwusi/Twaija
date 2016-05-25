using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.AttributeTypes;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Twitterizer;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.Common.Exceptions;

namespace TwaijaComposite.RequestAdapterModule
{
    [InterfaceMethodImplemented(MethodName = "PostMessageAndPicture")]
    public class DefaultTwitterPictureService : IClipboardOperationAware,IPictureService
    {
              
            public bool PostPicture(Uri Source, out string outputpicturelocation)
            {
                throw new NotImplementedException();
            }

            public string Name
            {
                get { return ServiceKeys.DefaultTwitterPictureServiceKey; }
            }

            public Uri ThumbnailImage
            {
                get { return null; }
            }



            public bool PostMessageAndPicture(IUser user, Uri PictureLocation, string message)
            {
                if ((user as ITwitterUser) == null)
                {
                    throw new WrongUserTypeException("Wrong user type supplied to post method, a TwitterUser is required, what was supplied:" + user.GetType().FullName);
                }
                decimal id = default(decimal);
                if (Operation != null)
                {
                    if (!Operation.Processed)
                    {
                        if (Operation.OperationKey == TwaijaComposite.Modules.Common.Resources.ClipboardOperationKeys.ReplyToKey || Operation.OperationKey == ClipboardOperationKeys.TwitterDirectMessageKey)
                        {
                            id = Convert.ToDecimal(Operation.Parameter);
                        }
                    }
                }
                try
                 {
                    var token = (user as ITwitterUser).Token;
                    if (Operation != null)
                    {
                        if (!Operation.Processed)
                        {
                            if (Operation.OperationKey == TwaijaComposite.Modules.Common.Resources.ClipboardOperationKeys.TwitterDirectMessageKey)
                            {
                                var response0 = TwitterDirectMessage.Send(new OAuthTokens() { AccessToken = token.TokenKey, AccessTokenSecret = token.TokenSecret, ConsumerKey = token.ConsumerKey, ConsumerSecret = token.ConsumerSecret }, id, message);
                                if (response0 != null)
                                {
                                    Operation.Processed = true;
                                    return true;
                                }
                                return false;
                            }
                        }
                    }
                     var response = (TwitterStatus.UpdateWithMedia(new OAuthTokens() { AccessToken = token.TokenKey, AccessTokenSecret = token.TokenSecret, ConsumerKey = token.ConsumerKey, ConsumerSecret = token.ConsumerSecret }, message, PictureLocation.OriginalString, new StatusUpdateOptions {   }));
                     if (response.ResponseObject != null)
                     {
                         return true;
                     }
                 }
                 catch (Exception e)
                 {
                     
                 }
                 return false;
            }

        

            public Modules.Clipboard.IClipboardOperation Operation
            {
                get;
                set;
            }

            public event MessageSentHandler MessageSent;

            public event MessageFailedHandler MessageFailed;

    }
}
