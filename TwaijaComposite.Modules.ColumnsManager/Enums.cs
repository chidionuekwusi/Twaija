using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public delegate void NewMessageHandler(IMessage message,ColumnDirective directive);


    public enum ColumnDirective
    {
        Add,AddAndClear,Insert
    }
}
