using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class DirectMessageViewmodelConverter:IModelFactory<DirectMessageViewmodel>
    {
        [Dependency]
        public Preferences pref { get; set; }
        [Dependency("IconifiedNetworkCommandFactory")]
        public INetworkAndMiscCommandsFactory netCommandsFactory { get; set; }
        public DirectMessageViewmodel Create(object optionalparamter)
        {
            try
            {
                //Configure
                var idirect=optionalparamter as ITwitterDirectMessage;
                var message = new DirectMessageViewmodel();
                message.Commands.Add(netCommandsFactory.CreateDirectMessageReplyCommand(idirect.Id, idirect.SenderScreenName));
                return message;
            }
            catch
            {
                return null;
            }
        }
    }
}
