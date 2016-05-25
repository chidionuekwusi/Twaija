using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;

namespace TwaijaComposite
{
    public class ShellViewmodel
    {
        public ShellViewmodel(IEventAggregator evnt)
        {
            evt = evnt;
            evt.GetEvent<RequestApplicationCloseEvent>().Subscribe(CloseApp);
        }
        
        public IEventAggregator evt { get; set; }
        public event EventHandler RequestClose;
        private DelegateCommand<object> _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand<object>((u) => {
                        if (RequestClose != null)
                        {
                            evt.GetEvent<ApplicationCloseEvent>().Publish(new ApplicationStateProxy() { ExitCode = ApplicationExitCode.safeExit});
#if SILVERLIGHT     
                            RequestClose(this, null);
#endif
                        }
                    
                    });
                }
                return _closeCommand;
            }
        }

        public void CloseApp(object arg) 
        {
            OnRequestClose(null);
        }
        void OnRequestClose(EventArgs e)
        {
            if (RequestClose != null)
            {
                RequestClose(this, e);
            }
        }
    }
}
