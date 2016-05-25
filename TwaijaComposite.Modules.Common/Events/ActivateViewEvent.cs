using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class ActivateViewEvent : CompositePresentationEvent<ActivateViewEventArgs>
    {
    
    }
     public class ActivateViewEventArgs : EventArgs
    {
        public string TargetModule { get; set; }
        public string ActivationRegion { get; set; }
        public ActivateViewEventArgs()
            : base()
        {
           
        }
    }
}
