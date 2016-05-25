using System;
using TwaijaComposite.Modules.Common;
using Twitterizer;
namespace TwaijaComposite.RequestAdapterModule
{
   public class AuthenticationAddressBuilderImp:IPinBuildMethod
    {

        public string Build(string requestToken)
        {
            return OAuthUtility.BuildAuthorizationUri(requestToken).AbsoluteUri;
        }
    }
}
