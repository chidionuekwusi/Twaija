using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Commands
{
   public class CreateHomeTimelineCommandHelper:SetTargetScreenNameCommandHelperBase
    {
       public CreateHomeTimelineCommandHelper()
       {
           ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterHometimelineKey;
       }
    }
}
