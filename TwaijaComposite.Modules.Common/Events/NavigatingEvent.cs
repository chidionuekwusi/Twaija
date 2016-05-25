using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Events
{
   public delegate void NavigatingEventHandler(object view,NavigatingEventArgs Name);
   public class NavigatingEventArgs : EventArgs
   {
       public string NameOfRequestedView { get; set; }
       public NavigationDirection Direction { get; set; }
       public string NameOfRequestedRegion { get; set; }
   }
   public enum NavigationDirection
   {
       To,From
   }
}
