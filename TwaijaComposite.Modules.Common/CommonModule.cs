using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.DialogTypes;
using TwaijaComposite.Modules.Common.Commands.Factories;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Behaviors;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
//using TwaijaComposite.Modules.Data;

namespace TwaijaComposite.Modules.Common
{
    #if !SILVERLIGHT
    [Module(ModuleName="CommonModule")]
   #endif
    public class CommonModule:IModule
    {  
       #region fields
       protected  IUnityContainer m_Container;
       protected readonly IRegionManager m_RegionManager;
       #endregion


       #region Contructor
       public CommonModule(IUnityContainer container,IRegionManager manager)
       {
           this.m_Container = container;
           m_RegionManager = manager;
       }
       #endregion

        #region IModule Members

        public void Initialize()
        {
            RegisterViewsAndViewModels();
        }

        private void RegisterViewsAndViewModels()
        {           
            m_Container.RegisterType<IController, CommonInfrastructureController>("CommonManager",new ContainerControlledLifetimeManager());
            m_Container.RegisterType<UserResolutionService>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ICommandExecutor<CreateColumnEventArgs>, CreateColumnCommandExecutor>();
            m_Container.RegisterType<ICommandExecutor<OpenProfileEventArgs>, ProfileCommandExecutor>();
            m_Container.RegisterType<ICommandFactory, SimpleCommandFactory>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ICommandFactory, IconifiedCommandFactory>("IconifiedCommandFactory", new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IDecisionDialogViewmodel, DecisionDialogViewmodel>();
            m_Container.RegisterType<IYesNoDialogViewmodel, YesNoDialogViewmodel>();
            m_Container.RegisterType<IOkDialogViewmodel, OkDialogViewmodel>();
            m_Container.RegisterType<IDialogFacade, DialogFacadeImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ITwitterUser, TwitterUser>();
            m_Container.RegisterType<Preferencing.Preferences>(new ContainerControlledLifetimeManager());
            var p=m_Container.Resolve<Preferencing.Preferences>();
            m_Container.RegisterInstance<Preferencing.GeneralPreferences>(p.GeneralOptions);
            m_Container.RegisterInstance<Preferencing.UserPreferenceFacade>(p.TransparentUsersFacade);
            m_Container.RegisterInstance<Preferencing.UserRepository>(p.TransparentUsersFacade.Userrepository);
            m_Container.RegisterInstance<TwitterAppCredentials>("Twaija",new TwitterAppCredentials() { ConsumerKey = "TWfvHOpH5mxVQ4kIqNaidw", ConsumerSecret = "2T4MizMnoQgoqYT334tk3iRM2rGccBja6jTX25EWc" });
            var nav = new NavigationServiceImp(m_Container,m_RegionManager);
            nav.SetHome(ViewNames.MainApplicationTemplateView, RegionNames.MainSpace);
            m_Container.RegisterInstance<INavigationService>(ServiceKeys.MainRegionNavigationService, nav);
            m_Container.RegisterType<IOptionFileWriterService,OptionFileWriterImp>();
            //m_Container.RegisterType<NotificationActivationBehavior>();
        }
       
        #endregion
    }
}
