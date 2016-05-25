using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Commands
{
   public class CreateFavouritesCommandHelper:SetTargetScreenNameCommandHelperBase
    {
       public CreateFavouritesCommandHelper()
       {
           ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterFavouritesKey;
       }

    }
}
