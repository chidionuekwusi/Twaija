using System;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Preferencing;
using System.Collections.Generic;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ApplicationEngine
{
    //This class is responsible for Managing the main application view and alerting its siblings
    public class Engine:IEngine
    {
        #region fields
        private IEnumerable<IController> _controllers;
        private IRegionManager m_RegionManager;
        bool isMainWindowActivated = false;
        #endregion
        public Engine(IEnumerable<IController> controllers, IRegionManager manager)
       {
           if (manager == null)
           {
               throw new ArgumentNullException("a region manager must be passed to the Engine");
           }
           if (controllers == null)
           {
               throw new ArgumentNullException("the system cannot work without controllers, no controllers are passed to the Engine");
           }
           _controllers = controllers;   
           m_RegionManager = manager;
       }
        public void ActivateMainView(object view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("u must pass a view to activate");
            }
            if (!m_RegionManager.Regions[RegionNames.MainSpace].Views.Contains(view))
            {
                m_RegionManager.Regions[RegionNames.MainSpace].Add(view);
            }
            m_RegionManager.Regions[RegionNames.MainSpace].Activate(view);
        }

        public void DeactivateMainView(object view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("u must pass a view to activate");
            }
            m_RegionManager.Regions[RegionNames.MainSpace].Deactivate(view);
        }
        void InitializeManagers()
        {
            foreach (IController manager in _controllers)
            {
                manager.Initialize();
            }
        }
        public void ActivateComponents()
        {
            if (!isMainWindowActivated)
            {
                InitializeManagers();
                isMainWindowActivated = true;
            }
        }
        public void StartEngine()
        {
            //The Application is started from the Proxy
        }

        
    }

    public class EngineProxy : IEngine
    {
        #region fields
        private readonly IRegionManager m_RegionManager;
        private object _mainPageLayout;
        private IEngine Engine;
        IOptionFileWriterService writer;
        INavigationService navigationService;
        #endregion
        
        [Dependency]
        public IUnityContainer UnityContainer { get; set; } 

        public EngineProxy(object mainPageLayout, IEngine engine,IOptionFileWriterService writer,INavigationService navigationService,IEventAggregator aggregator)
       {
           if (engine == null)
           {
               throw new ArgumentNullException("No Engine was passed to Engine proxy");
           }
           if (mainPageLayout == null)
           {
               throw new ArgumentNullException("No layout view was passed to engine proxy");
           }
           if (writer == null)
           {
               throw new ArgumentNullException("No IOptionFileWriterService was passed to the Engine Proxy");
           }
           if (navigationService == null)
           {
               throw new ArgumentNullException("No NavigationService was passed to the Engine proxy");
           }
           if (aggregator == null)
           {
               throw new ArgumentNullException("No EventAggregator was passed to the Engine proxy");
           }
           _mainPageLayout = mainPageLayout;
           this.Engine = engine;
           this.navigationService = navigationService;
           this.writer = writer;
           aggregator.GetEvent<ApplicationCloseEvent>().Subscribe(HandleApplicationShutdown);
       }
        void HandleApplicationShutdown(ApplicationStateProxy state)
        {
            var pref = UnityContainer.Resolve<Preferences>();
            if (pref.TransparentUsersFacade.Userrepository.NumberOfUsers > 0)
            {
                writer.CreateFile(FolderNames.TOKENFOLDER, pref.CreateSaveData());
            }
        }
        public void ActivateMainView(object view)
        {
            Engine.ActivateMainView(view);
        }

        public void DeactivateMainView(object view)
        {
            Engine.DeactivateMainView(view);
        }

        public void ActivateComponents()
        {
            Engine.ActivateComponents();
        }
        /// <summary>
        /// This Method Starts the Main application by checking for an existing Option file and if none is found hands over control to
        /// Authentication Module to handle authentication .
        /// </summary>
        public virtual void StartEngine()
        {
            
            object data;
            if (writer.ReadFile(FolderNames.TOKENFOLDER, typeof(PreferencesSaveData), out data) && data is PreferencesSaveData)
            {
                //At this point the Preferences have been initialized with their proper value.. 
                //m_Container.RegisterInstance<Preferences>(pref as Preferences);
                PreferencesSaveData savedata = (data as PreferencesSaveData);
                UnityContainer.Resolve<Preferences>().Load(savedata);
                if ((savedata).userspecific.Users.Count == 0)
                {
                    NavigateToLogin();
                    return;
                }
                ActivateMainView(_mainPageLayout);
                Engine.ActivateComponents();
            }
            else
            {
                NavigateToLogin();
            }
        }
        void NavigateToLogin()
        {
            this.navigationService.NavigateTo(new ViewRequestedEventArgs() { context = Engine, RegionRequested = RegionNames.MainSpace, ViewRequested = ViewNames.AuthenticationView, NavigationCallback = Activate, IsOneTimeCallbackInvocation = true });

        }
         void Activate(NavigatingEventArgs args)
        {
            if (args != null)
            {
                switch (args.Direction)
                {
                    case NavigationDirection.From:
                        Engine.ActivateComponents();
                        break;
                }
            }
        }
    }
}
