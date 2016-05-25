using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Commands
{
   public class TweetSearchCommandHelper:CreateColumnCommandHelperBase
   {
       public string Query { get; set; }
       public string Geo { get; set; }
       public TweetSearchCommandHelper()
       {
           ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterTweetSearchKey;
       }
        protected override Dictionary<string, object> PrepareOptions()
        {
            var dictionary= base.PrepareOptions();
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TextKey, Query);
            if(!string.IsNullOrEmpty(Geo))
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.GeoStringKey, Geo);
            return dictionary;
        }
    }
}
