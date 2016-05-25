using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Data
{
    public class DataModule:IModule
    {
        #region fields
        private readonly IUnityContainer m_Container;
        #endregion
        public DataModule(IUnityContainer container)
        {
            m_Container = container;
        }
        public void Initialize()
        {

            RegisterTypes();
        }

        private void RegisterTypes()
        {
            m_Container.RegisterType<ITwitterUser, TwitterUser>();
        }
    }
}
