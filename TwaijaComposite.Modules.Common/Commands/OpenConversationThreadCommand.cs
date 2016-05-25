using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class OpenConversationThreadCommand:ProfileCommandHelperBase
    {
        public ITweet Tweet { get; set; }
        public decimal TweetId { get; set; }
        public OpenConversationThreadCommand()
        {
            ProfileType = TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterConversationThreadKey;
        }
        protected override Dictionary<string, object> PrepareParamters()
        {
            var dictionary= base.PrepareParamters();
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TweetKey, Tweet);
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TweetIdKey, TweetId);
            return dictionary;
        }
    }
    public class OpenUserSearchCommand : ProfileCommandHelperBase
    {
        public OpenUserSearchCommand()
        {
            ProfileType = TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterUserSearchKey;
        }
        public string ScreenName { get; set; }
        protected override Dictionary<string, object> PrepareParamters()
        {
            var dictionary= base.PrepareParamters();
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TargetScreenNameKey, ScreenName);
            return dictionary;
        }
    }
}
