using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Events
{
    public class ViewRequestedEventArgs : EventArgs
    {
        public Action<NavigatingEventArgs> NavigationCallback { get; set; }
        public object context { get; set; }
        public string ViewRequested { get; set; }
        public string RegionRequested { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsOneTimeCallbackInvocation { get; set; }
        public ViewRequestedEventArgs()
            : base()
        {

        }
    }
}
