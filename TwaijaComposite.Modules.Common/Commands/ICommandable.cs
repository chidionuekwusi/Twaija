using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace TwaijaComposite.Modules.Common
{
    public interface ICommandable
    {
        IList<ICommand> Commands { get; }
    }
    public interface ISwitchableCommands
    {
        IList<ICommand> Commands { get; set; }
    }
}
