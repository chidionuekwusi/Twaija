using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Interfaces
{
   public interface INavigationAware
    {
       Action<NavigatingEventArgs> Navigating { get; }
       event NavigatingEventHandler NavigatingEvent;
    }
}
