using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.Common.Commands
{
    public class IconifiedCommand:ICommand
    {
         #region Properties
        public Uri HighlightedIcon { get; set; }
        public Uri NormalIcon { get; set; }
        public Uri DisabledIcon { get; set; }
        public string PathIcon { get; set; }
        public string Hint { get; set; } 
#endregion
         #region fields
        protected Func<object, bool> _canexec;
        private DelegateCommand<object> command;
        #endregion
        public IconifiedCommand(Action<object> action,Func<object, bool> canexec = null)
        {
            _canexec = canexec;
            canexec = (canexec == null) ? (s) => { return true; } : canexec;
            command =new DelegateCommand<object>(action,canexec);
        }

        public bool CanExecute(object parameter)
        {
            return _canexec == null ? true : _canexec(parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add
            {
                command.CanExecuteChanged += value;
            }
            remove
            {
               command.CanExecuteChanged -= value;
            }
        }
        public virtual void Dispose()
        {
            command = null;
        }
        public void RaiseCanExecuteChanged()
        {
            command.RaiseCanExecuteChanged();
        }
        #region ICommand Members


        public void Execute(object parameter)
        {
            command.Execute(parameter);
        }

        #endregion
    }
}
