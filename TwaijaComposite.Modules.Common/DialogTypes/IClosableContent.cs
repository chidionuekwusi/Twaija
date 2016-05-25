using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public interface IClosableContent
    {
        event EventHandler CloseContent;
        void TriggerContentClosure();
    }
}
