using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Resources;

namespace TwaijaComposite.Modules.ColumnsManager.Column.Factories
{
    /// <summary>
    /// TwitterFactoryHelper implemented as a Singleton
    /// </summary>
    public  class TwitterFactoryHelper
    {
        TwitterFactoryHelper()
        {

        }
       static TwitterFactoryHelper()
        {
            _soleInstance = new TwitterFactoryHelper();
        }
        public static TwitterFactoryHelper Create()
        {
            return _soleInstance;
        }
        static TwitterFactoryHelper _soleInstance;
        /// <summary>
        /// This method is to be called by TwitterFactories to initialize the request objects by calling Configuring them with the given TwitterUser
        /// </summary>
        /// <typeparam name="M">The Type that the model factory will produce</typeparam>
        /// <typeparam name="R">The Actual Request type that implements ITwitterRequest</typeparam>
        /// <param name="user">The User</param>
        /// <param name="parameters">Parameters of the entire Request</param>
        /// <param name="m_Container">The UnityContainer</param>
        /// <param name="selectedModelFactory"></param>
        /// <returns></returns>
        public  R CreateAndConfigureRequest<M, R>(IUser user, Dictionary<string, object> parameters,IUnityContainer m_Container,out IModelFactory<M> selectedModelFactory) where R:ITwitterRequest where M:new()
        {            
            selectedModelFactory = (!parameters.ContainsKey(CreateColumnEventParameters.ModelFactoryKey)) ? null : m_Container.Resolve<IModelFactory<M>>(parameters[CreateColumnEventParameters.ModelFactoryKey].ToString());
            var request = m_Container.Resolve<R>();
            request.ConfigureTwitterUser(user as ITwitterUser);
            return request;
        }
    }
}
