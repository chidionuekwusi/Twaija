using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.ViewModels;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.Modules.Authentication
{
    public class MockAuthenticationStrategy:ViewModelBase,IAuthenticateStrategy
    {
        [Dependency]
        public IDispatcher dispatcher{get;set;}
        [Dependency]
        public  Preferences _pref { get; set; }
        [Dependency]
        public  IUnityContainer m_Container { get; set; }
        [Dependency]
        public  IOptionFileWriterService OptionsFileWriter { get; set; }
        public string Name
        {
            get { return "Mock"; }
        }

        public Uri Address
        {
            get;
            set;
        }

        public System.Windows.Input.ICommand BrowserNavigationReaction
        {
            get { return null; }
        }

        public event EventHandler Close;

        public event Action RequestBrowserTransition;

        public string Hint
        {
            get { return "Mock Auth Strategy"; }
        }

        public string ThumbnailImage
        {
            get {return IconLocator.Follow; }
        }
        private string _screenNameToCreate;

        void OnClose(EventArgs args)
        {
            if (Close != null)
            {
                Close(this, args);
            }
        }
        public ICommand CreateUser
        {
            get
            {
                return new DelegateCommand<object>(
       (s) =>
       {
           if (s != null)
           {
               var text = s.ToString();
               if (!string.IsNullOrEmpty(text))
               {
                   var pref = m_Container.Resolve<Preferences>();
                   var user0 = m_Container.Resolve<ITwitterUser>();
                   user0.ScreenName = text;
                   user0.Token = new TwitterToken();
                   _pref.TransparentUsersFacade.Userrepository.Add(user0);
                   dispatcher.Invoke((h) =>
                   {
                       _pref.TransparentUsersFacade.Userrepository.SelectedUser = user0;
                   }, null);
                   OptionsFileWriter.CreateFile(FolderNames.TOKENFOLDER, _pref.CreateSaveData());
               }

           }



       }, (d) => { return true; });


            }
        }
    }
}
