using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.ApplicationEngine
{
    /*this is used by the System to Start initialize the application */
   public interface IEngine
    {
        void ActivateMainView(object view);
        void DeactivateMainView(object view);
        void ActivateComponents();
        void StartEngine();
    }
}
