using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateDirectMessagesCommandHelper:SetTargetScreenNameCommandHelperBase
    {
        public CreateDirectMessagesCommandHelper()
        {
            ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterDirectMessagesKey;
        }
    }
}
