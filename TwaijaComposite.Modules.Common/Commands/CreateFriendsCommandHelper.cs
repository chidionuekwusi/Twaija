using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateFriendsCommandHelper:SetTargetScreenNameCommandHelperBase
    {
        public CreateFriendsCommandHelper()
        {
            ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterFriendsKey;
        }

    }
}
