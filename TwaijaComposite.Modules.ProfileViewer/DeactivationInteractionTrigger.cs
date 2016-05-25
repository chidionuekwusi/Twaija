using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ProfileViewer
{
    public interface DeactivationInteractionTrigger
    {
        void Deactivate();
        event EventHandler Deactivated;
    }
}
