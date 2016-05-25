using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Exceptions
{
   public  class WrongUserTypeException:Exception
    {
       public Type ExpectedType { get; set; }
       public Type ActualType { get; set; }
       public WrongUserTypeException(string message)
           : base(message)
       {
       }
       public WrongUserTypeException()
           : base()
       {
       }
       public WrongUserTypeException(string message, Exception e)
           : base(message, e)
       {
       }
    }
}
