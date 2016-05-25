 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Regions;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using TwaijaComposite.Modules.ColumnsManager.Column;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ColumnsManager.Views;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.ColumnsManager.Request;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.ColumnsManager.Converter;
using TwaijaComposite.Modules.ColumnsManager.Column.Factories;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.ColumnsManager.Commands;
using TwaijaComposite.Modules.ColumnsManager.Notifications;
using TwaijaComposite.Modules.ColumnsManager.Behaviors;
using TwaijaComposite.Modules.Common.Resources;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ColumnsManagerModule:IModule
    {

        #region fields
        readonly IUnityContainer m_Container;
        readonly IRegionManager m_RegionManager;
        #endregion

        public ColumnsManagerModule(IUnityContainer container, IRegionManager manager)
        {
            m_Container = container;
            m_RegionManager = manager;
        }
        public void Initialize()
        {
            RegisterTypes();           
        }

        private void RegisterTypes()
        {
           
            #region Converters
            m_Container.RegisterType<IConverter<SingleMessage<TweetViewmodel>, ITweet>, SingleMessageConverter<TweetViewmodel, ITweet>>();
            m_Container.RegisterType<IConverter<AggregateMessage<TweetViewmodel>, IList<ITweet>>, AggregateMessageConverter<TweetViewmodel, ITweet>>();
            m_Container.RegisterType<IConverter<AggregateMessage<TwitterProfile_SmallViewmodel>, IList<ITwitterProfile_Small>>, AggregateMessageConverter<TwitterProfile_SmallViewmodel, ITwitterProfile_Small>>();
            m_Container.RegisterType<IConverter<AggregateMessage<DirectMessageViewmodel>, IList<ITwitterDirectMessage>>, AggregateMessageConverter<DirectMessageViewmodel, ITwitterDirectMessage>>();
            m_Container.RegisterType<IConverter<AggregateMessage<TrendsViewmodel>, IList<ITrend>>, AggregateMessageConverter<TrendsViewmodel, ITrend>>();
            m_Container.RegisterType<IConverter<SingleMessage<TwitterProfile_LargeViewmodel>, ITwitterProfile_Large>, SingleMessageConverter<TwitterProfile_LargeViewmodel, ITwitterProfile_Large>>();

            //m_Container.RegisterType(typeof(IConverter<SingleMessage<TweetViewmodel>, ITweet>), typeof(SingleMessageConverter<TweetViewmodel, ITweet>));
            //m_Container.RegisterType(typeof(IConverter<AggregateMessage<TweetViewmodel>, IList<ITweet>>), typeof(AggregateMessageConverter<TweetViewmodel, ITweet>));
            //m_Container.RegisterType(typeof(IConverter<AggregateMessage<TwitterProfile_SmallViewmodel>, IList<ITwitterProfile_Small>>), typeof(AggregateMessageConverter<TwitterProfile_SmallViewmodel, ITwitterProfile_Small>));
            //m_Container.RegisterType(typeof(IConverter<AggregateMessage<DirectMessageViewmodel>, IList<ITwitterDirectMessage>>),typeof( AggregateMessageConverter<DirectMessageViewmodel, ITwitterDirectMessage>));
            //m_Container.RegisterType(typeof(IConverter<AggregateMessage<TrendsViewmodel>, IList<ITrend>>),typeof( AggregateMessageConverter<TrendsViewmodel, ITrend>));
            //m_Container.RegisterType(typeof(IConverter<SingleMessage<TwitterProfile_LargeViewmodel>, ITwitterProfile_Large>),typeof( SingleMessageConverter<TwitterProfile_LargeViewmodel, ITwitterProfile_Large>));

            #endregion

            #region RequestTypes

            m_Container.RegisterType<HomeTimelineRequest>();
            m_Container.RegisterType<MentionsRequest>();
            m_Container.RegisterType<GetFollowersRequest>();
            m_Container.RegisterType<DirectMessagesRequest>();
            m_Container.RegisterType<GetFriendsRequest>();
            m_Container.RegisterType<TrendsRequest>();
            m_Container.RegisterType<TwitterProfile_LargeRequest>();
            m_Container.RegisterType<ConversationRequest>();
            m_Container.RegisterType<TweetSearchRequest>();
            m_Container.RegisterType<UserSearchRequest>();
            m_Container.RegisterType<FavouritesRequest>();

            #endregion

            #region ModelFactories
            //Register Model Factories.
            m_Container.RegisterType<IModelFactory<DirectMessageViewmodel>, DirectMessageViewmodelConverter>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IModelFactory<TweetViewmodel>, ProfileUserTimelineTweetViewmodelFactory>(ModelFactoryKeys.TweetViewmodelCustomFactoryKey,new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IModelFactory<TweetViewmodel>, TweetModelFactoryImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IModelFactory<TwitterProfile_LargeViewmodel>, TwitterProfile_LargeViewmodelFactory>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IModelFactory<TwitterProfile_SmallViewmodel>, TwitterProfile_SmallViewmodelModelFactory>(new ContainerControlledLifetimeManager());
            #endregion

            #region PartsFactories
            m_Container.RegisterType<IColumnPartsFactory, MentionsFactory>(ColumnTypeKeys.TwitterMentionsKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, DirectMessagesFactory>(ColumnTypeKeys.TwitterDirectMessagesKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, TweetSearchFactory>(ColumnTypeKeys.TwitterTweetSearchKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, UserSearchFactory>(ColumnTypeKeys.TwitterUserSearchKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, ConversationFactory>(ColumnTypeKeys.TwitterConversationKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, FriendsFactory>(ColumnTypeKeys.TwitterFriendsKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, FollowersFactory>(ColumnTypeKeys.TwitterFollowersKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, FavouritesFactory>(ColumnTypeKeys.TwitterFavouritesKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, HomeTimelineFactory>(ColumnTypeKeys.TwitterHometimelineKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, TwitterProfileFactory>(ColumnTypeKeys.TwitterProfileKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, TrendsFactory>(ColumnTypeKeys.TwitterTrendsKey, new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnPartsFactory, UserTimelineFactory>(ColumnTypeKeys.TwitterUserTimelineKey, new ContainerControlledLifetimeManager());
            #endregion

            #region Column Features
            m_Container.RegisterType<IColumnSkeletonFactory, ColumnSkeletonFactoryImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IFilter, Filter.FilterImp>();
            m_Container.RegisterType<ITimer, ThreadingTimerAdapter>();
            //All ColumnPartsFactories should be registered before this Service is registered...
            m_Container.RegisterType<IColumnResolutionService, ColumnResolutionServiceImp>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new ResolvedParameter<IDirector>(),new ResolvedParameter<IColumnSkeletonFactory>(), m_Container.ResolveAll<IColumnPartsFactory>(),new ResolvedParameter<Preferences>()));
            m_Container.RegisterType<IColumn, ColumnImp>();
            m_Container.RegisterType<IColumn, ProfileColumnImp>("ProfileColumn");
            m_Container.RegisterType<IColumn, SingleItemColumnImp>("SingleItemColumn");
            #endregion

            #region Others
            m_Container.RegisterType<IController, NotificationsControllerImp>("NotificationsManager",new ContainerControlledLifetimeManager());
            m_Container.RegisterType<INotificationViewmodel, NotificationViewmodelImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<INotificationsView, NotificationsViewImp>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IStateBasedNetworkCommandFactory, SimpleStateBasedNetworkCommandFactory>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<INetworkAndMiscCommandsFactory, SimpleNetworkAndMiscCommandFactory>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<INetworkAndMiscCommandsFactory, IconifiedNetworkAndMiscCommandFactory>("IconifiedNetworkCommandFactory", new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IRequestState<ITimerBasedRequest>, IdleState>();
            m_Container.RegisterType<IRequestState<ILoopRequest>, IdleState>();
            m_Container.RegisterType<IDirector, ColumnDirector>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IToolbarViewmodel, ToolBarViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IColumnsWorkspaceViewmodel, ColumnsWorkspaceViewmodel>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<IController,ColumnsController>("ColumnManager",new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ParseTweetBehavior>();
            m_Container.RegisterType<IinitializeUserHandler, TwitterUserInitializeHandler>("TwitterUserInitializeHandler",new ContainerControlledLifetimeManager());
            m_Container.RegisterType<InitializeUserHandlerRepository>(new ContainerControlledLifetimeManager(),new InjectionConstructor(m_Container.ResolveAll<IinitializeUserHandler>()));
            #endregion

            #region Views
          
            m_Container.RegisterType<Layout>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ColumnsWorkspaceView>(new ContainerControlledLifetimeManager());
            m_Container.RegisterType<ToolBarView>(new ContainerControlledLifetimeManager());
            m_RegionManager.RegisterViewWithRegion(RegionNames.Main_WorkareaSpace, () => m_Container.Resolve<ColumnsWorkspaceView>());
            m_RegionManager.RegisterViewWithRegion(RegionNames.ToolBar_WorkareaSpace, () => m_Container.Resolve<ToolBarView>());
            m_RegionManager.RegisterViewWithRegion(RegionNames.WorkareaSpace, typeof(Layout));

            #endregion
        }
    }
}
