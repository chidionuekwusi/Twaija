using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class ApplicationCloseEvent : CompositePresentationEvent<ApplicationStateProxy>
    {
        public ApplicationCloseEvent()
        {
            
        }
        public virtual SubscriptionToken Subscribe(Action<ApplicationStateProxy> action)
        {
            return base.Subscribe(action);
        }
    }
    public class ApplicationStateProxy
    {
        public ApplicationExitCode ExitCode { get { return ApplicationState.ExitCode; } set { ApplicationState.ExitCode = value; } }
    }
    public static class ApplicationState
    {
        public static ApplicationExitCode ExitCode {get;set;}
    }
    public enum ApplicationExitCode
    {
        safeExit=0,failedAuthentication=1
    }
}
