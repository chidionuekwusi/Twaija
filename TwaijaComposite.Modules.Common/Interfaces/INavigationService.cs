using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Interfaces
{
   public interface INavigationService
    {
       string HomeView { get; }
       string HomeRegion { get; }
       void NavigateTo(ViewRequestedEventArgs args);
       ViewRequestedEvent RequestView { get; }
      
    }
}
