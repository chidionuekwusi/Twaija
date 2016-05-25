using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Behaviors;

namespace TwaijaComposite.Modules.Common
{
    public interface INotifyOnHostEntry
    {
        AnimationTrigger OnHostEntry { get; set; }
    }
}
