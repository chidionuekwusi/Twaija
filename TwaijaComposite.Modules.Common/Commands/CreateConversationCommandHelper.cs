using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class CreateConversationCommandHelper:CreateColumnCommandHelperBase
    {
        public ITweet Tweet { get; set; }
        public decimal TweetId { get; set; }
        public CreateConversationCommandHelper()
        {
            ColumnType = TwaijaComposite.Modules.Common.Resources.ColumnTypeKeys.TwitterConversationKey;
        }
        protected override Dictionary<string, object> PrepareOptions()
        {            
            var dic= base.PrepareOptions();
            if (Tweet == null)
            {
                dic.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TweetIdKey, TweetId);
            }
            else
            {
                dic.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TweetKey, Tweet);
            }
            return dic;
        }
    }
}
