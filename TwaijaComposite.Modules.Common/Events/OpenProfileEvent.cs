using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class OpenProfileEvent : CompositePresentationEvent<OpenProfileEventArgs>
    {

    }
    public class OpenProfileEventArgs
    {
        public string ProfileType { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
