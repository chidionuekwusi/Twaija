using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Commands
{
   public interface ICreateColumnCommandHelper
    {
       CreateColumnEventArgs SetupArguments();
    }
   public interface IProfileCommandHelper
   {
       OpenProfileEventArgs SetupArguments();
   }
   public interface IHelper<T>
   {
       T SetupArguments();
   }
}
