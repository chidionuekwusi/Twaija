using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
#if SILVERLIGHT
using GalaSoft.MvvmLight.Command;
#endif

namespace TwaijaComposite.Modules.ProfileViewer.ProfileManager
{
    public class ProfileManagerViewmodel:INotifyPropertyChanged
    {
        #region fields
        IProfileController manager;
#if SILVERLIGHT
        RelayCommand<object> _goBackCommand;
        RelayCommand<object> _goFowardCommand;
#else
        private DelegateCommand<object> _goBackCommand;
        private DelegateCommand<object> _goFowardCommand;
#endif
        #endregion
        public ProfileManagerViewmodel(IProfileController manager)
        {
            this.manager = manager;
        }

        
        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
#if !SILVERLIGHT
                    _goBackCommand = new DelegateCommand<object>(manager.NavigateBackwards, manager.CanNavigateBackwards);
#else
                    _goBackCommand=new RelayCommand<object>(manager.NavigateBackwards, manager.CanNavigateBackwards);
#endif
                }
                return _goBackCommand;
            }
        }
        public ICommand GoFowardCommand
        {
            get
            {
                if (_goFowardCommand == null)
                {
#if !SILVERLIGHT
                    _goFowardCommand = new DelegateCommand<object>(manager.NavigateFoward, manager.CanNavigateFoward);
#else
                    _goFowardCommand = new RelayCommand<object>(manager.NavigateFoward, manager.CanNavigateFoward);
#endif
                }
                return _goFowardCommand;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
