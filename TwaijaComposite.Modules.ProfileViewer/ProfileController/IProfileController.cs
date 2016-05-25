using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ProfileViewer.ProfileManager
{
    public interface IProfileController:IController
    {
        bool CanNavigateFoward(object state);
        bool CanNavigateBackwards(object state);
        void NavigateFoward(object state);
        void NavigateBackwards(object state);
    }
}
