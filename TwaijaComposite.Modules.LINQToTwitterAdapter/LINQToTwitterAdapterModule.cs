using LinqToTwitter;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.LINQToTwitterAdapter
{
    public class LINQToTwitterAdapterModule : IModule
    {
        private IUnityContainer m_Container;
        public LINQToTwitterAdapterModule(IUnityContainer container)
        {
            m_Container = container;
        }
        public void Initialize()
        {
            m_Container.RegisterType<IPinAuthenticateMethod, PinAuthenticateMethod>(new ContainerControlledLifetimeManager());        
           
        }
    }
}
