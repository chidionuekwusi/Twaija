using System;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class UserResolutionDialogEvent : CompositePresentationEvent<UserRDEventPayLoad>
    {

    }
    public class UserRDEventPayLoad
    {
       public Action<object> action { get; set; }
       public  bool IsExecutedOnUIThread { get; set; }
       public string Text { get; set; }
    }
}
