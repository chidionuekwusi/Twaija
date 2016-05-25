using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class ProfileCommandHelperBase:IHelper<OpenProfileEventArgs>
    {
        protected string ProfileType { get; set; }
        protected virtual Dictionary<string, object> PrepareParamters()
        {
            return new Dictionary<string, object>();
        }
        protected virtual OpenProfileEventArgs SetUpOptions(Dictionary<string, object> dictionary)
        {
            return new OpenProfileEventArgs() { ProfileType = ProfileType, Parameters = dictionary };
        }
        public Events.OpenProfileEventArgs SetupArguments()
        {
            return SetUpOptions(PrepareParamters());
        }
    }
}
