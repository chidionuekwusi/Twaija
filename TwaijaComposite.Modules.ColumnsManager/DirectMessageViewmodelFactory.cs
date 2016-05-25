using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Commands.Factories;
using TwaijaComposite.Modules.Common.Commands;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class DirectMessageViewmodelFactory:IModelFactory<DirectMessageViewmodel>
    {
        [Dependency("IconifiedCommandFactory")]
        ICommandFactory columnCommandFactory { get; set; }
        [Dependency("IconifiedNetworkCommandFactory")]
        INetworkAndMiscCommandsFactory netCommandFactory { get; set; }

        #region IModelFactory<DirectMessageViewmodel> Members

        public DirectMessageViewmodel Create(object optionalparamter)
        {
            DirectMessageViewmodel message = null;
            var innermessage = optionalparamter as ITwitterDirectMessage;
            try
            {
                message = new DirectMessageViewmodel();
                message.Commands.Add(columnCommandFactory.CreateOpenTwitterProfileCommand(innermessage.SenderScreenName));
                message.Commands.Add(netCommandFactory.CreateDirectMessageReplyCommand(innermessage.SenderId, innermessage.SenderScreenName));
                message.Commands.Add(netCommandFactory.CreateBlockCommand(innermessage.SenderScreenName));
                message.Commands.Add(netCommandFactory.CreateUnBlockCommand(innermessage.SenderScreenName));
            }
            catch
            {
            }
            return message;
        }

        #endregion
    }
}
