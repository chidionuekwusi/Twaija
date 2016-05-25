using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class StateBasedCommand : ICommand, INotifyPropertyChanged
    {
        public StateBasedCommand(Action<object> action, Func<object, bool> canexec = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException("Action to invoke cannot be null:StateBasedCommand");
            }
            canexec = (canexec != null) ? canexec : (o) => { return true; };
            _command = new DelegateCommand<object>(action, canexec);
            this.CommandStateChangeHandler = HandleChange;
        }
        protected void HandleChange(CommandStateChangeEventArgs args)
        {
            if (args != null)
            {
                Hint = args.StateName;
            }
        }
        DelegateCommand<object> _command;
        private string name;
        public string Hint
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Hint"));
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return _command.CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { _command.CanExecuteChanged += value; }
            remove { _command.CanExecuteChanged -= value; }
        }

        public void Execute(object parameter)
        {
            _command.Execute(parameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public CommandStateChange CommandStateChangeHandler;
    }
}
