using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using Microsoft.Practices.Prism.Events;
namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class UnFollowCommandHelper : TwitterCommandBaseHelper
    {
        public string TargetScreenName { get; set; }
        UserResolutionDialogEvent eve;
        IUnFollowCommand command;
        public UnFollowCommandHelper(IEventAggregator aggr, IUnFollowCommand command)
        {
            eve = aggr.GetEvent<UserResolutionDialogEvent>();
            this.command = command;
        }
        public override void Execute(object parameter)
        {
            eve.Publish(new UserRDEventPayLoad() { action = base.Template, IsExecutedOnUIThread = false } );
        }
        protected override bool Condition(ITwitterUser user)
        {
            return command.UnFollow(TargetScreenName, user); 
        }
    }
}
