using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateTwitterProfileCommandHelper:SetTargetScreenNameCommandHelperBase
    {
        public CreateTwitterProfileCommandHelper()
        {
            ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterProfileKey;
            ColumnImpType = "SingleItemColumn";
        }
    }
}
