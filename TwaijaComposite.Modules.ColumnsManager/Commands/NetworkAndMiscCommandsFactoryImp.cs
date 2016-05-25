                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Presentation.Commands;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class SimpleStateBasedNetworkCommandFactory : IStateBasedNetworkCommandFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }
        public ICommand CreateFollowCommand(string TargetScreenName,InitialState state)
        {
            var command = m_Container.Resolve<Follow_UnfollowTwoStateCommand>();
            command.TargetScreenName = TargetScreenName;
            StateBasedCommand maincommand = null;
            switch (state)
            {
                case InitialState.StateA:
                    maincommand = new StateBasedCommand(command.Execute) { Hint = command.StateAName };
                    command.SwitchToStateA();
                    break;
                case InitialState.StateB:
                    maincommand = new StateBasedCommand(command.Execute) { Hint = command.StateBName };
                    command.SwitchToStateB();
                    break;
            }
            command.StateChange += maincommand.CommandStateChangeHandler;
            return maincommand;
        }

        public ICommand CreateBlockCommand(string TargetScreenName,InitialState state)
        {
            var command = m_Container.Resolve<Block_UnBlockTwoStateCommand>();
            command.TargetScreenName = TargetScreenName;
            StateBasedCommand maincommand = null;
            switch (state)
            {
                case InitialState.StateA:
                    maincommand = new StateBasedCommand(command.Execute) { Hint = command.StateAName };
                    command.SwitchToStateA();
                    break;
                case InitialState.StateB:
                    maincommand = new StateBasedCommand(command.Execute) { Hint = command.StateBName };
                    command.SwitchToStateB();
                    break;
            }
            command.StateChange += maincommand.CommandStateChangeHandler;
            return maincommand;
        }
    }
    public class IconifiedNetworkAndMiscCommandFactory : INetworkAndMiscCommandsFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }

        #region INetworkAndMiscCommandsFactory Members

        public ICommand CreateFollowCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<FollowCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return new IconifiedCommand(command.Execute) {Hint="Follow" };
        }

        public ICommand CreateUnFollowCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<UnFollowCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return new IconifiedCommand(command.Execute) { Hint = "Unfollow" };
        }

        public ICommand CreateBlockCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<BlockCommandHepler>();
            command.TargetScreenName = TargetScreenName;
            return new IconifiedCommand(command.Execute) { Hint = "Block"};
        }

        public ICommand CreateUnBlockCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<UnBlockCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return new IconifiedCommand(command.Execute) {Hint="Unblock" };
        }

        public ICommand CreateFavouriteCommand(decimal Id, string Text = null)
        {
            var command = m_Container.Resolve<FavouriteCommandHelper>();
            command.Id = Id;
            command.Text = Text;
            return new IconifiedCommand(command.Execute) { Hint = "Favourite" };
        }

        public ICommand CreateUnFavouriteCommand(decimal Id, string Text = null)
        {
            var command = m_Container.Resolve<DeleteFavouriteCommandHelper>();
            command.Id = Id;
            return new IconifiedCommand(command.Execute) { Hint = "Delete Favourite"};
        }

        public ICommand CreateDeleteTweetCommand(decimal Id)
        {
            var command = m_Container.Resolve<DeleteTweetCommandHelper>();
            command.TweetId = Id;
            return new IconifiedCommand(command.Execute) {Hint="Delete Tweet" };
        }

        public ICommand CreateReplyCommand(string ScreenName, decimal Id = 0)
        {
            var command = m_Container.Resolve<ReplyCommandHelper>();
            command.UserId = Id;
            command.ScreenName = ScreenName;
            return new IconifiedCommand(command.Execute) { Hint = "Reply"};
        }

        public ICommand CreateRetweetCommand(decimal Id, string Text)
        {
            var command = m_Container.Resolve<RetweetCommandHelper>();
            command.Id = Id;
            command.Text = Text;
            return new IconifiedCommand(command.Execute) { Hint = "Retweet"};
        }

        #endregion

        #region INetworkAndMiscCommandsFactory Members


        public ICommand CreateRelationshipCheckerCommand(string SecondScreenName, Common.DataInterfaces.ITwitterUser User = null, string FirstScreenName = null)
        {
            return new IconifiedCommand(CreateRelationshipChecker(SecondScreenName,User,FirstScreenName).CheckRelationshipCommand.Execute) { Hint = "Relationship" };

        }
       

        public IRelationshipChecker CreateRelationshipChecker(string SecondScreenName, Common.DataInterfaces.ITwitterUser User = null, string FirstScreenName = null)
        {
            IRelationshipChecker checker = null;
            Preferences pref = null;
            try
            {
                checker = m_Container.Resolve<IRelationshipChecker>();
                if (!string.IsNullOrEmpty(FirstScreenName))
                {
                    checker.FirstScreenName = FirstScreenName;
                }
                else
                {
                    checker.FirstScreenName = m_Container.Resolve<Preferences>().TransparentUsersFacade.Userrepository.SelectedUser.ScreenName;
                }
                if (User == null)
                {
                    if (pref == null)
                    {
                        pref = m_Container.Resolve<Preferences>();
                    }
                    checker.User = pref.TransparentUsersFacade.Userrepository.SelectedUser as ITwitterUser;
                    if (checker.User == null)
                    {
                        foreach (IUser user in pref.TransparentUsersFacade.Userrepository.Users)
                        {
                            if (user is ITwitterUser)
                            {
                                checker.User = user as ITwitterUser;
                            }
                        }
                        if (checker.User == null)
                        {
                            throw new ArgumentException(" No suitable user exists to check status of friendship ");
                        }
                    }
                }
                checker.SecondScreenName = SecondScreenName;
                if (User != null)
                {
                    checker.User = User;
                }
                return checker;

            }
            catch
            {

            }
            return null;
        }
        #endregion

        #region INetworkAndMiscCommandsFactory Members

       
        public ICommand CreateDirectMessageReplyCommand(decimal Id, string ScreenName)
        {
            var helper = m_Container.Resolve<DirectMessageReplyCommandHelper>();
            helper.UserId=Id;
            helper.ScreenName=ScreenName;
            return new IconifiedCommand(helper.Execute) { Hint = "Reply DM" };
        }

        #endregion
    }
    public class SimpleNetworkAndMiscCommandFactory : INetworkAndMiscCommandsFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }
        public ICommand CreateFollowCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<FollowCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return  new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateUnFollowCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<UnFollowCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateBlockCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<BlockCommandHepler>();
            command.TargetScreenName = TargetScreenName;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateUnBlockCommand(string TargetScreenName)
        {
            var command = m_Container.Resolve<UnBlockCommandHelper>();
            command.TargetScreenName = TargetScreenName;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateFavouriteCommand(decimal Id, string Text = null)
        {
            var command = m_Container.Resolve<FavouriteCommandHelper>();
            command.Id = Id;
            command.Text = Text;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateUnFavouriteCommand(decimal Id, string Text = null)
        {
            var command = m_Container.Resolve<DeleteFavouriteCommandHelper>();
            command.Id = Id;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateDeleteTweetCommand(decimal Id)
        {
            var command = m_Container.Resolve<DeleteTweetCommandHelper>();
            command.TweetId = Id;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateReplyCommand(string ScreenName,decimal Id=0)
        {
            var command = m_Container.Resolve<ReplyCommandHelper>();
            command.UserId = Id;
            command.ScreenName=ScreenName;
            return new DelegateCommand<object>(command.Execute);
        }

        public ICommand CreateRetweetCommand(decimal Id, string Text)
        {
            var command = m_Container.Resolve<RetweetCommandHelper>();
            command.Id = Id;
            command.Text = Text;
            return new DelegateCommand<object>(command.Execute);
        }

        #region INetworkAndMiscCommandsFactory Members


        public IRelationshipChecker CreateRelationshipChecker(string SecondScreenName, Common.DataInterfaces.ITwitterUser User = null, string FirstScreenName = null)
        {
            IRelationshipChecker checker = null;
            Preferences pref=null;
            try
            {
                checker = m_Container.Resolve<IRelationshipChecker>();
                if (!string.IsNullOrEmpty(FirstScreenName))
                {
                    checker.FirstScreenName = FirstScreenName;
                }
                else
                {
                    pref=m_Container.Resolve<Preferences>();
                    checker.FirstScreenName = pref.TransparentUsersFacade.Userrepository.SelectedUser.ScreenName;
                }
                if (User == null)
                {
                    if (pref == null)
                    {
                        pref = m_Container.Resolve<Preferences>();
                    }
                    checker.User = pref.TransparentUsersFacade.Userrepository.SelectedUser as ITwitterUser;
                    if (checker.User == null)
                    {
                        foreach (IUser user in pref.TransparentUsersFacade.Userrepository.Users)
                        {
                            if (user is ITwitterUser)
                            {
                                checker.User = user as ITwitterUser;
                            }
                        }
                        if (checker.User == null)
                        {
                            throw new ArgumentException(" No suitable user exists to check status of friendship ");
                        }
                    }
                }
                checker.SecondScreenName = SecondScreenName;
                checker.User = User;
                return checker;

            }
            catch
            {

            }
            return null;
        }

        public ICommand CreateRelationshipCheckerCommand(string SecondScreenName, Common.DataInterfaces.ITwitterUser User = null, string FirstScreenName = null)
        {
           return CreateRelationshipChecker(SecondScreenName, User, FirstScreenName).CheckRelationshipCommand;
        }

        #endregion

        #region INetworkAndMiscCommandsFactory Members


        public ICommand CreateDirectMessageReplyCommand(decimal Id, string ScreenName)
        {
            var helper = m_Container.Resolve<DirectMessageReplyCommandHelper>();
            helper.UserId=Id;
            helper.ScreenName=ScreenName;
            return new DelegateCommand<object>(helper.Execute);
        }

        #endregion
    }
}
