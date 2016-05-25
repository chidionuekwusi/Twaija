using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class OpenTwitterProfileCommandHelper:ProfileCommandHelperBase
    {
        public string TargetScreenName { get; set; }
        public OpenTwitterProfileCommandHelper()
        {
            ProfileType = TwaijaComposite.Modules.Common.Resources.ProfileHandlerTypeKeys.TwitterProfileKey;
        }
        protected override Dictionary<string, object> PrepareParamters()
        {
            var dictionary= base.PrepareParamters();
            dictionary.Add(TwaijaComposite.Modules.Common.Resources.CreateColumnEventParameters.TargetScreenNameKey, TargetScreenName);
            return dictionary;
        }
    }
}
