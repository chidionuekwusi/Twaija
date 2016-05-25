using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Resources;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class UserTimelineCommandHelper:SetTargetScreenNameCommandHelperBase
    {
        public UserTimelineCommandHelper()
        {
            ColumnType = ColumnTypeKeys.TwitterUserTimelineKey;
        }
    }
}
