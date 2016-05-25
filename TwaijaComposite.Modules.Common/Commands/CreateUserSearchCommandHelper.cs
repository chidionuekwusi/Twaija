using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateUserSearchCommandHelper:SetTargetScreenNameCommandHelperBase
    {
        public CreateUserSearchCommandHelper()
        {
            ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterUserSearchKey;
        }
    }
}
