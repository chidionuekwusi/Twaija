using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Events;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class FollowCommandHelper:TwitterCommandBaseHelper
    {
       public  string TargetScreenName { get; set; }
       IEventAggregator aggr;
       IFollowCommand command;
        public FollowCommandHelper(IEventAggregator aggr,IFollowCommand command)
        {
            this.aggr = aggr;
            this.command = command;
        }
        protected override bool Condition(ITwitterUser user)
        {
            return command.Follow(TargetScreenName,user);
        }
        public override void Execute(object parameter)
        {
            var eve = aggr.GetEvent<UserResolutionDialogEvent>();
            eve.Publish(new UserRDEventPayLoad() { action = base.Template, IsExecutedOnUIThread = false });
        }
    }
  
    public delegate void CommandStateChange(CommandStateChangeEventArgs NewState);
    public class CommandStateChangeEventArgs : EventArgs
    {
        public string StateName { get; set; }
        public object State { get; set; }
    }
    public interface ICommandState
    {
        string StateName { get; set; }
        bool Execute(TwoStateBasedCommandHelper helper,ITwitterUser user);
    }
    public abstract class TwoStateBasedCommandHelper : TwitterCommandBaseHelper
    {
        public ICommandState State { get; set; }
        public virtual string StateAName { get { return ""; } }
        public  virtual string StateBName { get { return ""; } }
        public event CommandStateChange StateChange;
        public abstract bool ConditionStateA(ITwitterUser user);
        public abstract bool ConditionStateB(ITwitterUser user);
        public void SwitchToStateA()
        {
            State = new CommandStateA();
        }
        public void SwitchToStateB()
        {
            State = new CommandStateB();
        }
        protected override bool Condition(ITwitterUser parameter)
        {
            return State.Execute(this,parameter); 
        }
        public virtual void OnStateChange(CommandStateChangeEventArgs args)
        {
            if (StateChange != null)
            {
                StateChange(args);
            }
        }

    }
    public class CommandStateA : ICommandState
    {
        public bool Execute(TwoStateBasedCommandHelper helper, ITwitterUser user)
        {
            if (helper.ConditionStateA(user))
            {
                helper.State = new CommandStateB();
                helper.State.StateName = helper.StateBName;
                helper.OnStateChange(new CommandStateChangeEventArgs() { State = helper.State, StateName = helper.StateBName });
                return true;
            }
            return false;
        }

        public string StateName
        {
            get;
            set;
        }
    }
    public class CommandStateB : ICommandState
    {
        public bool Execute(TwoStateBasedCommandHelper helper,ITwitterUser user)
        {
            if (helper.ConditionStateB(user))
            {
                helper.State = new CommandStateA();
                helper.State.StateName = helper.StateAName;
                helper.OnStateChange(new CommandStateChangeEventArgs() { State = helper.State, StateName = helper.StateAName });
                return true;
            }
            return false;
        }

        public string StateName
        {
            get;
            set;
        }
    }
    public class Follow_UnfollowTwoStateCommand : TwoStateBasedCommandHelper
    {
       IEventAggregator aggr;
       IFollowCommand commandA;
       IUnFollowCommand commandB;
       public override string StateAName { get { return "Follow"; } }
       public override string StateBName { get { return "UnFollow"; } }
       public string TargetScreenName { get; set; }
       public Follow_UnfollowTwoStateCommand(IEventAggregator aggr, IFollowCommand commandA,IUnFollowCommand commandB)
        {
            this.aggr = aggr;
            this.commandA = commandA;
            this.commandB = commandB;
        }
       
        public override bool ConditionStateA(ITwitterUser user)
        {
           return commandA.Follow(TargetScreenName, user);
        }

        public override bool ConditionStateB(ITwitterUser user)
        {
            return commandB.UnFollow(TargetScreenName, user);
        }
        public override void Execute(object parameter)
        {
            var eve = aggr.GetEvent<UserResolutionDialogEvent>();
            eve.Publish(new UserRDEventPayLoad() { action = base.Template, IsExecutedOnUIThread = false });
        }
    }
    public class Block_UnBlockTwoStateCommand : TwoStateBasedCommandHelper
    {
       IEventAggregator aggr;
       IBlockCommand commandA;
       IUnBlockCommand commandB;
       public string StateAName { get { return "Block"; } }
       public string StateBName { get { return "UnBlock"; } }
       public string TargetScreenName { get; set; }
       public Block_UnBlockTwoStateCommand(IEventAggregator aggr, IBlockCommand commandA, IUnBlockCommand commandB)
        {
            this.aggr = aggr;
            this.commandA = commandA;
            this.commandB = commandB;
        }
        public override bool ConditionStateA(ITwitterUser user)
        {
            return commandA.Block(TargetScreenName, user);
        }

        public override bool ConditionStateB(ITwitterUser user)
        {
            return commandB.UnBlock(TargetScreenName, user);
        }
        public override void Execute(object parameter)
        {
            var eve = aggr.GetEvent<UserResolutionDialogEvent>();
            eve.Publish(new UserRDEventPayLoad() { action = base.Template, IsExecutedOnUIThread = false });
        }
    }
}
