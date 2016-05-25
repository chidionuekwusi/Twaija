using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.Modules.Options
{
    public class LayoutViewmodel
    {
        [Dependency(ServiceKeys.MainRegionNavigationService)]
        public INavigationService m_Service { get; set; }
        private DelegateCommand<object> _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new DelegateCommand<object>(GoBack);
                }
                return _goBackCommand; }
        }
        protected void GoBack(object state)
        {
            m_Service.NavigateTo(null);
        }
    }
}
