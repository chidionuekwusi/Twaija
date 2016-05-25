using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DialogTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public interface INotificationViewmodel : IClosableContent
    {
        IEnumerable<Notice> Content { get; }
        void Add(Notice notice);
        void Clear();
    }
}
