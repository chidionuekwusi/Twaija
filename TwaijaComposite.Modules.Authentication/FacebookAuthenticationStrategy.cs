using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.Authentication
{
    public class FacebookAuthenticationStrategy:IAuthenticateStrategy,INotifyPropertyChanged
    {
        
        private DelegateCommand<object> _startCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_startCommand == null)
                {
                    _startCommand = new DelegateCommand<object>(Start);
                }
                return _startCommand;
            }
        }
        public string Name
        {
            get { return "Facebook"; }
        }
        private Uri _address;
        public Uri Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (_address != value)
                {
                    _address = value;
                }
            }
        }
        DelegateCommand<object> _browserNavigationReaction;
        public System.Windows.Input.ICommand BrowserNavigationReaction
        {
            get
            {
                if (_browserNavigationReaction == null)
                {
                    _browserNavigationReaction = new DelegateCommand<object>(Reaction);
                }
                return _browserNavigationReaction;
            }
        }

        public event EventHandler Close;
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public  void Reaction(object EditorBrowsableState){ }

        public void Start(object state) {  }
        protected void OnClose()
        {
            Close(this, null);
        }


        public event Action RequestBrowserTransition;


        public string Hint
        {
            get { return "Facebook authentication-coming soon like the new Batman movie"; }
        }

        public string ThumbnailImage
        {
            get { return TwaijaComposite.Modules.Common.IconLocator.Facebook_F_Large_26x26; }
        }
    }
}
