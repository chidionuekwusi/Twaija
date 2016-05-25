//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace TwaijaComposite.Modules.Common
{
   public interface IRequestTokenBuilder
    {
       string BuildRequestTokenString(string ConsumerKey, string ConsumerSecret, string callbackaddress);
    }
}
