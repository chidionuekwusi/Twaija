using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DialogTypes;
using System.Collections.ObjectModel;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public interface INotificationsView : IRequestWindowPositionEntity
    {
        bool IsAlive { get; set; }
        event EventHandler RequestResetTimer;
        void TriggerTimerReset();
    }

    
}
