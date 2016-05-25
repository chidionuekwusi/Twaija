using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common
{
   public interface IPinAuthenticateMethod
    {
        bool Authenticate(string consumerkey,string consumerSecret,string requestToken,string oop,out object token);


    }
}
