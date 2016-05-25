using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Services;
using System.ComponentModel;
//using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows.Input;
using System.Threading;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.Authentication
{
    public class PinAuthenticationStrategy:IAuthenticateStrategy,INotifyPropertyChanged
    {
        #region fields
        private object asynchlock = new object();
        private DelegateCommand<object> _completeauth;
        private DelegateCommand<object> _builduri;
        private readonly IUnityContainer m_Container;
        private IPinAuthenticateMethod templatemethod;
        private IPinBuildMethod buildmethod;
        private IRequestTokenBuilder requestTokenBuilder;
        private readonly Preferences _pref;
        private string requestToken;
        TwaijaComposite.Modules.Common.Services.TwitterAppCredentials credentials;
        IDispatcher dispatcher;
        private readonly IOptionFileWriterService OptionsFileWriter;
        #endregion
        [Dependency]
        public IDialogFacade d { get; set; }
        public string Pin { get; set; }
        bool canSend;
        public bool CanSend
        {
            get { return canSend; }
            set
            {
                canSend = value;
                OnPropertyChanged("CanSend");
            }
        }
        public PinAuthenticationStrategy(IDispatcher disp,IPinAuthenticateMethod method,IRequestTokenBuilder requestStringBuilder,IPinBuildMethod addressbuilder, IOptionFileWriterService writer,Preferences pref, IUnityContainer container)
        {
            dispatcher = disp;
            requestTokenBuilder = requestStringBuilder;
            buildmethod = addressbuilder;
            m_Container = container;
            OptionsFileWriter = writer;
            templatemethod = method;
            credentials = container.Resolve<TwaijaComposite.Modules.Common.Services.TwitterAppCredentials>("Twaija");
            _pref = pref;
            _bnr = new DelegateCommand<object>(HandleBrowserNavigation);
            _completeauth = new DelegateCommand<object>((i) => CompleteAuthenticationMethod(Pin));
        }
        public bool Authenticate(object pin)
        {
            object token = null;
            try
            {
                if (Convert.ToInt32(pin) < 7)
                {
                    return false;
                }
                if (templatemethod.Authenticate(credentials.ConsumerKey, credentials.ConsumerSecret, requestToken, Convert.ToString(pin), out token))
                {
                    if (!(token is TwitterToken))
                    {
                        throw new Exception("Expected TwitterToken got " + token.GetType());
                    }

                    var user = m_Container.Resolve<ITwitterUser>();
                    user.Token = token as TwitterToken;
                    user.ScreenName = (token as TwitterToken).ScreenName;
                    if (_pref.TransparentUsersFacade.Userrepository.NumberOfUsers == 0 && _pref.TransparentUsersFacade.Userrepository.SelectedUser == null)
                    {
                        dispatcher.Invoke((s) =>
                        {
                            _pref.TransparentUsersFacade.Userrepository.SelectedUser = user;
                        });
                    }
                    if (_pref.TransparentUsersFacade.Userrepository.Find<TwitterUser>(user.ScreenName) == null)
                    {
                        _pref.TransparentUsersFacade.Userrepository.Add(user);
                        OptionsFileWriter.CreateFile(FolderNames.TOKENFOLDER, _pref.CreateSaveData());
                    }
                    else
                    {
                        d.PushMessageDialog("The Account Already Exists");
                    }
                    return true;
                }


            }
            catch (InvalidCastException)
            {

            }
            catch
            {
            }
            return false;
            
        }

        public string BuildAddress()
        {

            try
            {
                requestToken = requestTokenBuilder.BuildRequestTokenString(credentials.ConsumerKey, credentials.ConsumerSecret, "oob");
                if (string.IsNullOrEmpty(requestToken))
                {
                    throw new ArgumentNullException();
                }
                return buildmethod.Build(requestToken);
            }
            catch
            {
                return null;
            }
        }

        public void HandleBrowserNavigation(object state)
        {
           
        }
        public ICommand BuildAuthenticationUri
        {
            get
            {
                if (_builduri == null)
                {
                    _builduri = new DelegateCommand<object>((p) => BuildAuthUriMethod(p));
                }
                return _builduri;
            }
        }

        private void BuildAuthUriMethod(object p)
        {
            lock (asynchlock)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    
                    Uri addy = null;
                    Uri.TryCreate(this.BuildAddress(), UriKind.RelativeOrAbsolute, out addy);
                    if (addy != null)
                    {
                        dispatcher.Invoke((s) =>
                        {
                            Address = s as Uri;
                            //Tell the authentication Viewmodel that the browser should popup now...
                            RequestBrowserTransition();
                            CanSend = true;
                            _completeauth.RaiseCanExecuteChanged();
                        }, addy);

                    }
                }, null);
            }
        }
       
        //protected bool CanSend(object o)
        //{
        //    return canSend;
        //}
        
        public event PropertyChangedEventHandler PropertyChanged;
        private Uri _address;
        public Uri Address
        {
            get { return _address; }
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged("Address");
                }
            }
        }
        private DelegateCommand<object> _bnr;
        public System.Windows.Input.ICommand BrowserNavigationReaction
        {
            get
            {
                return _bnr;
            }
        }
        public ICommand CompleteAuthenticationCommand
        {
            get
            {
                return _completeauth;
            }
        }
        void OnClose(EventArgs args)
        {
            if (Close != null)
            {
                Close(this, args);
            }
        }
        private void CompleteAuthenticationMethod(object i)
        {
            lock (asynchlock)
            {
                if (canSend)
                {
                    canSend = false;
                    _completeauth.RaiseCanExecuteChanged();
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        if (Authenticate(state))
                        {
                            dispatcher.Invoke((u)=>
                            OnClose(null));
                        }
                        canSend = true;
                        _completeauth.RaiseCanExecuteChanged();
                    }, i);
                }
            }
        }
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event EventHandler Close;

        public string Name
        {
            get { return "Twitter"; }
        }


        public event Action RequestBrowserTransition;


        public string Hint
        {
            get { return "Click GO, to start the process, you will be transferred to twitter.com where a pin will be displayed after you authorize Twaija. Input this pin in the box above and click SEND to complete the process."; }
        }


        public string ThumbnailImage
        {
            get { return IconLocator.TwitterBird_Large_26x26; }
        }
    }
}
