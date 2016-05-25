using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Clipboard;
using Microsoft.Practices.Prism.Events;
//using Microsoft.Practices.Composite.Presentation.Events;

namespace TwaijaComposite.Modules.Common.Events
{
    public class SetClipboardOperationEvent : CompositePresentationEvent<IClipboardOperation>
    {

    }
}
