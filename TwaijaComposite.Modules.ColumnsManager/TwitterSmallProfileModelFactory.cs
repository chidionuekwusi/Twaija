using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Commands.Factories;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager
{
  public  class TwitterProfile_SmallViewmodelModelFactory:IModelFactory<TwitterProfile_SmallViewmodel>
    {
        [Dependency("IconifiedNetworkCommandFactory")]
        public INetworkAndMiscCommandsFactory netCommandsFactory { get; set; }
        [Dependency("IconifiedCommandFactory")]
        public ICommandFactory columnCommandFactory { get; set; }


        #region IModelFactory<TwitterProfile_SmallViewmodel> Members

        TwitterProfile_SmallViewmodel IModelFactory<TwitterProfile_SmallViewmodel>.Create(object optionalparamter)
        {
            var profile = new TwitterProfile_SmallViewmodel();
            var options=optionalparamter as ITwitterProfile_Small;
            try
            {
                profile.Commands.Add(columnCommandFactory.CreateOpenTwitterProfileCommand(options.ScreenName));
                profile.Commands.Add(netCommandsFactory.CreateFollowCommand(options.ScreenName));
                profile.Commands.Add(netCommandsFactory.CreateUnFollowCommand(options.ScreenName));
                profile.Commands.Add(netCommandsFactory.CreateBlockCommand(options.ScreenName));
                profile.Commands.Add(netCommandsFactory.CreateUnBlockCommand(options.ScreenName));
            }
            catch { }
            return profile;
        }

        #endregion
    }
}
