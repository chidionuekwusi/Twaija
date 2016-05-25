using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class BlockCommandHepler:TwitterCommandBaseHelper
    {
        string _targetScreenName;
        public string TargetScreenName
        {
            get { return _targetScreenName; }
            set
            {
                if (_targetScreenName != value)
                {
                    _targetScreenName = value;
                    FailureMessage = "Im sorry we couldnt Block " + value + "wait a few minutes and try again";
                    SuccessMessage = "We have successfully Blocked " + value;
                }
            }
        }
        [Dependency]
       public IEventAggregator aggr { get; set; }
        [Dependency]
       public  IBlockCommand command { get; set; }

        public override void Execute(object parameter)
        {
            aggr.GetEvent<UserResolutionDialogEvent>().Publish(new UserRDEventPayLoad() { action = this.Template, IsExecutedOnUIThread = false });
        }
        protected override bool Condition(Common.DataInterfaces.ITwitterUser user)
        {
            return command.Block(TargetScreenName, user);
        }
    }
}
