using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public class NewNotificationsEvent : CompositePresentationEvent<Notice>
    {

    }
    public class Notice
    {
        public string Title { get; set; }
        public IList<object> Content { get; set; }
        public int NumberOfObjects { get; set; }
    }
}
