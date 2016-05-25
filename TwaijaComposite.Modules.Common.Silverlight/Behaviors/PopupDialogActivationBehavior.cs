using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Behaviors;


namespace TwaijaComposite.Modules.Common.Behaviors
{
    public class PopupDialogActivationBehavior : DialogActivationBehavior
    {
        /// <summary>
        /// Creates a wrapper for the Silverlight <see cref="System.Windows.Controls.Primitives.Popup"/>.
        /// </summary>
        /// <returns>Instance of the <see cref="System.Windows.Controls.Primitives.Popup"/> wrapper.</returns>
        protected override IWindow CreateWindow()
        {
            return new PopupWrapper();
        }
    }
}
