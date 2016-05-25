using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Commands
{
   public interface ICommandExecutor<T>
    {
        IHelper<T> Helper { get; set; }
        Action<object> Execute { get; }
    }
   
}
