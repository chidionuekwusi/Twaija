using System;
using System.Collections.Generic;
//using Microsoft.Practices.Composite.Presentation.Events;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class CreateColumnEvent : CompositePresentationEvent<CreateColumnEventArgs>
     {

     }
     public class CreateColumnEventArgs
     {
         public string ColumnImpType { get; set; }
         public string ColumnType { get; set; }
         public string ColumnBuildType { get; set; }
         public IUser User { get; set; }
         public Dictionary<string, object> Parameters { get; set; }
     }
}
