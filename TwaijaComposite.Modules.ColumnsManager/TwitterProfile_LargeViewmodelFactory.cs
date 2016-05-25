using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Commands;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class TwitterProfile_LargeViewmodelFactory:IModelFactory<TwitterProfile_LargeViewmodel>
    {
        [Dependency("IconifiedNetworkCommandFactory")]
        public INetworkAndMiscCommandsFactory netCommandsFactory { get; set; }
        [Dependency]
        public IStateBasedNetworkCommandFactory stateCommandFactory { get; set; }
        [Dependency]
        public IDispatcher dispatcher { get; set; }
        #region IModelFactory<TwitterProfile_LargeViewmodel> Members

        public TwitterProfile_LargeViewmodel Create(object optionalparamter)
        {
            TwitterProfile_LargeViewmodel profile = null;
            try
            {
                var options=optionalparamter as ITwitterProfile_Large;
                profile = new TwitterProfile_LargeViewmodel();
                profile.dispatcher = dispatcher;
                var checker=netCommandsFactory.CreateRelationshipChecker(options.ScreenName, null, null);
                 profile.RelationshipResolver = checker;
                //Configure
                 checker.Commandable = profile;

            }
            catch
            {

            }
            return profile;
        }

        #endregion
    }
}
