using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.ViewModels;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
//using Microsoft.Practices.Composite.Presentation.Commands;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Preferencing;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
#if SILVERLIGHT
using GalaSoft.MvvmLight.Command;
#endif
namespace TwaijaComposite.Modules.Authentication.ViewModels
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class AuthenticationViewModel : ViewModelBase
    {
       
        #region fields
        Preferences pref;
        private IAuthenticateStrategy selectedAuthenticationStrategy;
        public IList<IAuthenticateStrategy> AuthStrategies { get; set; }
        private readonly INavigationService m_Navigator; 
        #endregion
        #region properties
        [Dependency]
        public IDispatcher dispatcher { get; set; }
        public IAuthenticateStrategy Selected
        {
            get { return selectedAuthenticationStrategy; }
            set
            {
                if (selectedAuthenticationStrategy != value)
                {
                    selectedAuthenticationStrategy = value;
                    OnPropertyChanged("Selected");
                }
            }
        }
        private bool canGoBack;

        public bool CanGoBack
        {
            get { return canGoBack; }
            set
            {
                canGoBack = value;
                if (dispatcher != null)
                {
                    dispatcher.Invoke(new System.Threading.SendOrPostCallback((d) =>
                    {
                        OnPropertyChanged("CanGoBack");
                    }));
                }
            }
        }
         

        public ICommand GoBackCommand
        {
            get
            {
#if !SILVERLIGHT
                return new DelegateCommand<object>((s) => { m_Navigator.NavigateTo(null); });
#else
                return new RelayCommand<object>((d)=>{m_Navigator.NavigateTo(null);});
#endif
            }
        }
        private bool _browserPopupIsOpen=false;
        public bool BrowserPopupIsOpen
        {
            get { return _browserPopupIsOpen; }
            set
            {
                if (_browserPopupIsOpen != value)
                {
                    _browserPopupIsOpen = value;
                    OnPropertyChanged("BrowserPopupIsOpen");
                }
            }
        }

        #endregion
        #region Constructors
        public AuthenticationViewModel(Preferences pref, INavigationService nav, IAuthenticateStrategy strat,
            IEnumerable<IAuthenticateStrategy> Strategies)
        {
            this.pref = pref;
            CanGoBack = (pref.TransparentUsersFacade.Userrepository.NumberOfUsers > 0) ? true : false;
            pref.TransparentUsersFacade.Userrepository.CollectionChanged += new CollectionChangedHandler(Userrepository_CollectionChanged);
            m_Navigator = nav;
            Selected = strat;
            strat.Close += this.selectedAuthenticationStrategy_Close;
            strat.RequestBrowserTransition += new Action(strategy_RequestBrowserTransition);
            AuthStrategies =new List<IAuthenticateStrategy>();
            AuthStrategies.Add(strat);
            foreach (IAuthenticateStrategy strategy in
                Strategies)
            {
                strategy.Close += this.selectedAuthenticationStrategy_Close;
                strategy.RequestBrowserTransition += new Action(strategy_RequestBrowserTransition);
                AuthStrategies.Add(strategy);
            }
            
        }

        void Userrepository_CollectionChanged(object changeditem, ListChangedEventType change)
        {
            CanGoBack = (pref.TransparentUsersFacade.Userrepository.NumberOfUsers > 0) ? true : false;
        }

        

        #endregion
        #region Methods
        void OpenBrowser()
        {
            BrowserPopupIsOpen = true;
        }
        void CloseBrowser()
        {
            BrowserPopupIsOpen = false;
        }
        void strategy_RequestBrowserTransition()
        {
            OpenBrowser();
        }

        void selectedAuthenticationStrategy_Close(object sender, EventArgs e)
        {
            m_Navigator.NavigateTo(null);
            CanGoBack = true;
        }

        #endregion
    }
}
