using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Presentation.Events;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class ViewRequestedEvent : CompositePresentationEvent<ViewRequestedEventArgs>
    {
        public ViewRequestedEvent()
            : base()
        {

        }
    }

   
}
