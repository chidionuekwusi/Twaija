using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.ProfileViewer.ProfileManager
{
   public interface IProfileEventHandler
    {
       string Name { get; }
       /// <summary>
       /// Returns a view that can be added to an IRegion
       /// </summary>
       /// <param name="args">arguments used in handler resolution and paramter passing</param>
       /// <returns></returns>
       object HandleEvent(OpenProfileEventArgs args);
    }
}
