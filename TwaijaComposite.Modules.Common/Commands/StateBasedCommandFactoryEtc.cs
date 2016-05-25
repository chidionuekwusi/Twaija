using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TwaijaComposite.Modules.Common.Commands
{
    public enum InitialState
    {
        StateA, StateB
    }
    public interface IStateBasedNetworkCommandFactory
    {
        ICommand CreateFollowCommand(string TargetScreenName, InitialState state);
        ICommand CreateBlockCommand(string TargetScreenName, InitialState state);
    }
}
