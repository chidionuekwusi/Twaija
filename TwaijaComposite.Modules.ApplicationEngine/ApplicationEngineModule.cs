using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Practices.Composite.Modularity;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ApplicationEngine
{
#if !SILVERLIGHT
    [ModuleDependency("AuthenticationModule")]
    [ModuleDependency("CommonModule")]
    [Module(ModuleName="ApplicationEngineModule")]
#endif
    public class ApplicationEngineModule:IModule
    {
          #region fields
       private  IUnityContainer m_Container;
       #endregion

       #region Contructor
       public ApplicationEngineModule(IUnityContainer container)
       {
           this.m_Container = container;
       }
       #endregion
        private void RegisterViewsAndTypes()
        {
            //All Managers should be loaded before this Module begins initializing.
            //register all picture services        
            m_Container.RegisterType<IPictureServicesRepository, PictureServicesRepository>(new ContainerControlledLifetimeManager(), new InjectionConstructor(m_Container.ResolveAll<IPictureService>()));
            //register all postal services
            m_Container.RegisterType<IPostMessageServiceRepository, PostMessageServiceRepositoryImp>(new ContainerControlledLifetimeManager(), new InjectionConstructor(m_Container.ResolveAll<IPostMessageService>()));
            m_Container.RegisterType<ApplicationEngine.ViewModels.ApplicationEngineViewModel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<object, MainApplicationTemplateView>(ViewNames.MainApplicationTemplateView,new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IEngine, Engine>(new ContainerControlledLifetimeManager(),new InjectionConstructor(m_Container.ResolveAll<IController>(),new ResolvedParameter<IRegionManager>()));
            m_Container.RegisterType<IEngine,EngineProxy>("EngineProxy",new ContainerControlledLifetimeManager(),new InjectionConstructor(new ResolvedParameter<object>(ViewNames.MainApplicationTemplateView),new ResolvedParameter<IEngine>(),new ResolvedParameter<IOptionFileWriterService>(),new ResolvedParameter<INavigationService>(ServiceKeys.MainRegionNavigationService),m_Container.Resolve<IEventAggregator>()));
          //  m_RegionManager.RegisterViewWithRegion(RegionNames.MainSpace, typeof(MainApplicationTemplateView));
           
        }

        #region IModule Members

        public void Initialize()
        {
           RegisterViewsAndTypes();
           m_Container.Resolve<IEngine>("EngineProxy").StartEngine();
        }

        #endregion

    }
}
