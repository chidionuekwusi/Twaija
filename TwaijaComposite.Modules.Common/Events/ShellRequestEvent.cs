using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class ShellRequestEvent : CompositePresentationEvent<ShellRequestArguments>
    {
        public ShellRequestEvent()
        {
        }
    }
    public class ShellRequestArguments
    {
        public ShellRequestArguments()
        {
            Parameters = new Dictionary<string, object>();
        }
        public string SuggestedHandler { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
