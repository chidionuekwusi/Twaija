using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Presentation.Commands;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Prism.Commands;
//using Microsoft.Practices.Composite.Events;

namespace TwaijaComposite.Modules.Common.Commands.Factories
{
    public interface ICommandFactory
    {
        /// <summary>
        /// This function builds the specific search command from the parameters passed
        /// </summary>
        /// <param name="Text">This parameter cannot be null , if it is null an ArgumentNullException will be thrown</param>
        /// <param name="CustomColumnImplementation">Supply a Custom Column Implementation Name if needed ,if null is supplied default value is used</param>
        /// <param name="CustomBuilderImplementation">Supply a Custom Column Builder Implementation Name if needed ,if null is supplied default value is used</param>
        /// <returns>SearchCommand</returns>
        ICommand CreateTweetSearchCommand(string Text,string GeoString=null, string CustomColumnImplementation=null, string CustomBuilderImplementation=null,string CustomModelFactory=null);
        ICommand CreateUserSearchCommand(string TargetScreenName, string CustomColumnImplementation=null, string CustomBuilderImplementation=null,string CustomModelFactory=null);
        ICommand CreateHomeTimelineCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null,string CustomModelFactory=null);
        ICommand CreateMentionsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null,string CustomModelFactory=null);
        ICommand CreateDirectMessagesCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null,string CustomModelFactory=null);
        ICommand CreateFollowersCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null,string CustomModelFactory=null);
        ICommand CreateFriendsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null,string CustomModelFactory=null);
        ICommand CreateConversationCommand(ITweet Tweet, decimal TweetId = 0, string CustomColumnImplementation=null, string CustomBuilderImplementation=null,string CustomModelFactory=null);
        ICommand CreateOpenTwitterProfileCommand(string TargetScreenName);
    }
   /// <summary>
   /// This class returns DelegateCommands using the parameters supplied and creates them by composition
   /// </summary>
    public class SimpleCommandFactory : ICommandFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }

        public virtual ICommand CreateTweetSearchCommand(string Text, string GeoString = null, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var helper = new TweetSearchCommandHelper() { Query = Text, Geo = GeoString, CustomModelFactoryKey = CustomModelFactory, BuilderType = CustomBuilderImplementation, ColumnImpType = CustomColumnImplementation };
            ICommandExecutor<CreateColumnEventArgs> executor=m_Container.Resolve<ICommandExecutor<CreateColumnEventArgs>>();
            executor.Helper=helper;
            return new DelegateCommand<object>(executor.Execute);
        }

        public virtual ICommand CreateUserSearchCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var helper = m_Container.Resolve<OpenUserSearchCommand>();
            helper.ScreenName = TargetScreenName;
            var executor = m_Container.Resolve<ICommandExecutor<OpenProfileEventArgs>>();
            executor.Helper = helper;
            return new DelegateCommand<object>(executor.Execute);
           // return Create<CreateUserSearchCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
        }

        public virtual ICommand CreateHomeTimelineCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            return Create<CreateHomeTimelineCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
     
        }

        public virtual ICommand CreateMentionsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            return Create<CreateMentionsCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
     
        }

        public virtual ICommand CreateDirectMessagesCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            return Create<CreateDirectMessagesCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
     
        }

        public virtual ICommand CreateFollowersCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            return Create<CreateFollowersCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
     
        }

        public virtual ICommand CreateFriendsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            return Create<CreateFriendsCommandHelper>(TargetScreenName,m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
     
        }

        public ICommand CreateConversationCommand(ITweet Tweet, decimal TweetId = 0, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var helper = new CreateConversationCommandHelper() { Tweet = Tweet, TweetId = TweetId, CustomModelFactoryKey = CustomModelFactory, ColumnImpType = CustomColumnImplementation, BuilderType = CustomBuilderImplementation };
            return new DelegateCommand<object>(new CreateColumnCommandExecutor() { Helper = helper }.Execute); 
        }

        static ICommand Create<R>(string ScreenName, IUnityContainer container,string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null) where R:SetTargetScreenNameCommandHelperBase,new()
        {
            var helper = new R() {ScreenName=ScreenName,CustomModelFactoryKey=CustomModelFactory, BuilderType=CustomBuilderImplementation, ColumnImpType=CustomColumnImplementation };
            ICommandExecutor<CreateColumnEventArgs> executor = container.Resolve<ICommandExecutor<CreateColumnEventArgs>>();
            executor.Helper = helper;
            return new DelegateCommand<object>(executor.Execute);
        }


        #region ICommandFactory Members


        public ICommand CreateOpenTwitterProfileCommand(string TargetScreenName)
        {
            var helper = m_Container.Resolve<OpenTwitterProfileCommandHelper>();
            helper.TargetScreenName = TargetScreenName;
            var executor=m_Container.Resolve<ICommandExecutor<OpenProfileEventArgs>>();
            executor.Helper=helper;
            return new DelegateCommand<object>(executor.Execute);
        }

        #endregion
    }

    /// <summary>
    /// This class returns DelegateCommands using the parameters supplied and creates them by composition
    /// </summary>
    public class IconifiedCommandFactory : ICommandFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }

        static IconifiedCommand Create<R>(string ScreenName, IUnityContainer container, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null) where R : SetTargetScreenNameCommandHelperBase, new()
        {
            var helper = new R() { ScreenName = ScreenName, CustomModelFactoryKey = CustomModelFactory, BuilderType = CustomBuilderImplementation, ColumnImpType = CustomColumnImplementation };
            ICommandExecutor<CreateColumnEventArgs> executor = container.Resolve<ICommandExecutor<CreateColumnEventArgs>>();
            executor.Helper = helper;
            return new IconifiedCommand(executor.Execute);
        }

        public ICommand CreateOpenTwitterProfileCommand(string TargetScreenName)
        {
            var helper = m_Container.Resolve<OpenTwitterProfileCommandHelper>();
            helper.TargetScreenName = TargetScreenName;
            var executor = m_Container.Resolve<ICommandExecutor<OpenProfileEventArgs>>();
            executor.Helper = helper;
            return new IconifiedCommand(executor.Execute) { Hint="Open Profile"};
        }

        #region ICommandFactory Members

        public virtual ICommand CreateTweetSearchCommand(string Text, string GeoString = null, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var helper = new TweetSearchCommandHelper() { Query = Text, Geo = GeoString, CustomModelFactoryKey = CustomModelFactory, BuilderType = CustomBuilderImplementation, ColumnImpType = CustomColumnImplementation };
            ICommandExecutor<CreateColumnEventArgs> executor = m_Container.Resolve<ICommandExecutor<CreateColumnEventArgs>>();
            executor.Helper = helper;
            return new IconifiedCommand(executor.Execute) { Hint="Find:"+Text};
        }

        public virtual ICommand CreateUserSearchCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var helper = m_Container.Resolve<OpenUserSearchCommand>();
            helper.ScreenName = TargetScreenName;
            var executor = m_Container.Resolve<ICommandExecutor<OpenProfileEventArgs>>();
            executor.Helper = helper;
            return new IconifiedCommand(executor.Execute) { Hint = "Find:"+TargetScreenName };
            //var command= Create<CreateUserSearchCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            //command.Hint = "Find:"+TargetScreenName;
            //return command;
        }

        public virtual ICommand CreateHomeTimelineCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var command= Create<CreateHomeTimelineCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            command.Hint = TargetScreenName+"'s timeline";
            return command;
        }

        public virtual ICommand CreateMentionsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var command= Create<CreateMentionsCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            command.Hint = TargetScreenName+"'s mentions";
            return command;
        }

        public virtual ICommand CreateDirectMessagesCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var command= Create<CreateDirectMessagesCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            command.Hint = TargetScreenName+"'s Direct Messages";
            return command;
        }

        public virtual ICommand CreateFollowersCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var command= Create<CreateFollowersCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            command.Hint = TargetScreenName + "'s Followers";
            return command;
        }

        public virtual ICommand CreateFriendsCommand(string TargetScreenName, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            var command= Create<CreateFriendsCommandHelper>(TargetScreenName, m_Container, CustomColumnImplementation, CustomBuilderImplementation, CustomModelFactory);
            command.Hint = TargetScreenName + "'s Followers";
            return command;
        }

        public ICommand CreateConversationCommand(ITweet Tweet, decimal TweetId = 0, string CustomColumnImplementation = null, string CustomBuilderImplementation = null, string CustomModelFactory = null)
        {
            //var helper = new CreateConversationCommandHelper() { Tweet = Tweet, TweetId = TweetId, CustomModelFactoryKey = CustomModelFactory, ColumnImpType = CustomColumnImplementation, BuilderType = CustomBuilderImplementation };
            //return new IconifiedCommand(new CreateColumnCommandExecutor() { Helper = helper, aggr=m_Container.Resolve<IEventAggregator>() }.Execute) { Hint="Conversation" };

            var helper = m_Container.Resolve<OpenConversationThreadCommand>();
            helper.Tweet = Tweet;
            helper.TweetId = TweetId;
            var executor = m_Container.Resolve<ICommandExecutor<OpenProfileEventArgs>>();
            executor.Helper = helper;
            return new IconifiedCommand(executor.Execute) { Hint = "Conversation" };
        }


        #endregion
    }
}
