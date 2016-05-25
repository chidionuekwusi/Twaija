//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Modularity;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using TwaijaComposite.RequestAdapterModule.Commands;
using Microsoft.Practices.Prism.Modularity;

namespace TwaijaComposite.RequestAdapterModule
{
#if !SILVERLIGHT
    [ModuleDependency("CommonModule")]
    [Module(ModuleName = "RequestAdapterMethodsModule")]
#endif
    public class RequestAdapterMethodsModule:IModule
    {
        #region fields

        private readonly IUnityContainer m_Container;

        #endregion
        #region Contsructor

        public RequestAdapterMethodsModule(IUnityContainer container)
        {
            m_Container = container;
        }

        #endregion
        public void Initialize()
        {
            RegisterTypes();
        }

        private void RegisterTypes()
        {
            m_Container.RegisterType<ITwitterrizerMethodConsole, TwitterizerMethodConsole>();
            m_Container.RegisterType<ITwitterAccountMethodConsole, TwitterAccountMethodConsole>();
            m_Container.RegisterType<IRetweetCommand, RetweetCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IFollowCommand, FollowCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IUnFollowCommand, UnFollowCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IFavouriteCommand, FavouriteCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IUnFavouriteCommand, UnFavouriteCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IBlockCommand, BlockCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IUnBlockCommand, UnBlockCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IDeleteTweetCommand, DeleteCommandImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IPostMessageService, TwitterDirectMessagePostalService>(ServiceKeys.TwitterDirectMessagePostalServiceKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IRelationshipChecker, RelationshipCheckerImp>();
            m_Container.RegisterType<IDirectMessageMethod, DirectMessagesMethodImp>();
            m_Container.RegisterType<IMentionsMethod, MentionsMethodImp>();
            m_Container.RegisterType<IRetrieveConversationMethod, RetrieveConversationMethodImp>();
            m_Container.RegisterType<IRetrieveFavouritesMethod, RetrieveFavouritesMethodImp>();
            m_Container.RegisterType<IRetrieveTrendsMethod, RetrieveTrendsMethodImp>();
            m_Container.RegisterType<IRetrieveTwitterProfile_LargeMethod, RetrieveTwitterProfile_LargeMethodImp>();
            m_Container.RegisterType<ITweetSearchMethod, TweetSearchMethodImp>();
            m_Container.RegisterType<IUserSearchMethod, UserSearchMethodImp>();
            m_Container.RegisterType<IUserTimlineMethod, UserTimelineMethodImp>();
            m_Container.RegisterType<IHomeTimeline, HomeTimelineImp>();
            m_Container.RegisterType<IRetrieveFollowers_Or_Friends_Method, RetrieveFollowers_Or_Friends_Method>();
            m_Container.RegisterType<IPictureService, DefaultTwitterPictureService>(ServiceKeys.DefaultTwitterPictureServiceKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IPostMessageService, DefaultTwitterPostalService>(ServiceKeys.DefaultTwitterPostalServiceKey, new ContainerControlledLifetimeManager());
         //   m_Container.RegisterType<IPinAuthenticateMethod, PinAuthenticateMethod>(new ContainerControlledLifetimeManager());         
            m_Container.RegisterType<IPinBuildMethod, AuthenticationAddressBuilderImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IRequestTokenBuilder, RequestTokenBuilderImp>(new ContainerControlledLifetimeManager());
            //m_Container.RegisterType<IUserTimlineMethod, Mock.MockMethods>();
            //m_Container.RegisterType<IHomeTimeline, Mock.MockMethods>();
            //m_Container.RegisterType<IMentionsMethod, Mock.MockMethods>();
            //m_Container.RegisterType<IRetrieveTwitterProfile_LargeMethod, Mock.MockMethods>();
            //m_Container.RegisterType<IRetrieveFollowers_Or_Friends_Method, Mock.MockMethods>();
        }
    }
}
