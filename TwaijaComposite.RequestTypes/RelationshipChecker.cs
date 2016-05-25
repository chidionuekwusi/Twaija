using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Commands;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using TwaijaComposite.Modules.Common.Preferencing;
using Microsoft.Practices.Unity;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Threading;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.RequestAdapterModule
{
  
   public class RelationshipCheckerImp:IRelationshipChecker,INotifyPropertyChanged
    {
       [Dependency]
       public IStateBasedNetworkCommandFactory factory { get; set; }
       [Dependency]
       public IDispatcher dispatcher { get; set; }
       object asynchlock = new object();
       public ISwitchableCommands Commandable { get; set; }
       public string FirstScreenName { get; set; }
       public string SecondScreenName { get; set; }
       private bool _isFollowing;
       public bool IsFollowing
       {
           get
           {
               return _isFollowing;
           }
           set
           {
               _isFollowing = value;
               OnPropertyChanged("IsFollowing");
               StringBuilder build = new StringBuilder(FirstScreenName);
               if (_isFollowing)
               {
                   build.Append(" is following ");
               }
               else { build.Append("is not following"); }
               build.Append(SecondScreenName);
               IsFollowingString = build.ToString();

           }
       }
      bool _isFollowed;
      public bool IsFollowed
       {
           get
           {
               return _isFollowed;
           }
           set
           {
               _isFollowed = value;
               OnPropertyChanged("IsFollowed");
               
               StringBuilder build = new StringBuilder(FirstScreenName);
               if (_isFollowed)
               {
                   build.Append(" is followed by");
               }
               else
               {
                   build.Append(" is not followed by");
               }
               build.Append(SecondScreenName);
               IsFollowedString = build.ToString();
           }
       }
        #region IRelationshipChecker Members
        bool canCheck = true;
        private DelegateCommand<object> _command;
        public ICommand CheckRelationshipCommand
        {
            get
            {
                if (_command == null)
                {
                    _command = new DelegateCommand<object>(Check,CanExecute);
                }
                return _command;
            }
        }
        private bool CanExecute(object state)
        {
            return canCheck;
        }
        protected virtual void Check(object state)
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                lock (asynchlock)
                {
                    canCheck = false;
                    _command.RaiseCanExecuteChanged();
                    int attempts = 0;
                    while (true)
                    {
                        if (attempts >= 3)
                        {
                            break;
                        }
                        Twitterizer.TwitterRelationship result = null;
                        try
                        {
                            //SetCommands("Test Pilot", false, true);
                            //IsFollowing = false;
                            //IsFollowed = false;
                            if (User == null)
                            {
                                result = TwitterFriendship.Show(FirstScreenName, SecondScreenName).ResponseObject;
                            }
                            else
                            {
                                //result = new Twitterizer.TwitterRelationship() { Source = new TwitterRelationshipUser() {ScreenName="slowdog", Following = true, FollowedBy = true, Blocking = false }, Target = new TwitterRelationshipUser() {ScreenName="Dirty Dog", Blocking=false  } }; 
                                result = TwitterFriendship.Show(new OAuthTokens() { ConsumerKey = User.Token.ConsumerKey, AccessToken = User.Token.TokenKey, AccessTokenSecret = User.Token.TokenSecret, ConsumerSecret = User.Token.ConsumerSecret }, FirstScreenName, SecondScreenName).ResponseObject;
                            }
                            IsFollowing = result.Source.Following;
                            IsFollowed = result.Source.FollowedBy;
                            if (result.Target.Blocking.HasValue)
                            {
                                SetCommands(result.Target.ScreenName, result.Source.Following, result.Target.Blocking.Value);
                            }
                            else
                            {
                                SetCommands(result.Target.ScreenName, result.Source.Following, false);
                            }
                            break;
                        }
                        catch
                        {
                        }
                        attempts++;
                    }
                    canCheck = true;
                    _command.RaiseCanExecuteChanged();
                }
            }, state);
            
        }
        #endregion
       void SetCommands(string ScreenName,bool Following,bool Blocked)
       {
           List<ICommand> commands = new List<ICommand>();
           if (Commandable != null)
           {
               InitialState ini = InitialState.StateA;
               if (Following)
               {
                   ini = InitialState.StateB;
               }
               else
               {
                   
               }
               commands.Add(factory.CreateFollowCommand(ScreenName, ini));
               if (Blocked)
               {
                   ini = InitialState.StateB;
               }
               else
               {
                   ini = InitialState.StateA;
               }
               commands.Add(factory.CreateBlockCommand(ScreenName, ini));
              foreach (ICommand command in Commandable.Commands)
               {
                   commands.Add(command);
               }
              Commandable.Commands = commands;
           }
       }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                dispatcher.Invoke((o) =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }
        #endregion

        #region IRelationshipChecker Members


        public ITwitterUser User
        {
            get;
            set;
        }

        #endregion
        private string _isfollowingString;
        private string _isfollowedString;
        public string IsFollowingString
        {
            get { return _isfollowingString; }
            set
            {
                _isfollowingString = value;
                OnPropertyChanged("IsFollowingString");
            }
        }

        public string IsFollowedString
        {
            get { return _isfollowedString; }
            set
            {
                _isfollowedString = value;
                OnPropertyChanged("IsFollowedString");
            }
        }
    }
}
