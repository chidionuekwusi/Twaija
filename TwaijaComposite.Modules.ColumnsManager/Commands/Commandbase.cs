using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Presentation.Commands;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public abstract class Commandbase:ICommand,IDisposable
    {
        #region fields
        protected Func<object, bool> _canexec;
        private DelegateCommand<object> command;
        #endregion
        public Commandbase(Func<object,bool> canexec = null)
        {
            _canexec = canexec;
            command =new DelegateCommand<object>(Execute,canexec);
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

        public abstract void Execute(object parameter);



        public virtual void Dispose()
        {
            command = null;
        }
    }
}
