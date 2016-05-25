using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.AttributeTypes;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.Commands;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using TwaijaComposite.Modules.Common.Services;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    [ScreenNamePropertyLocator(TargetPropertyName="ScreenName")]
    [TextPropertyLocator(TargetPropertyName="Bio")]
    [FollowerPropertyLocator(TargetPropertyName="Followers")]
    [FriendPropertyLocator(TargetPropertyName="Friends")]
   public class TwitterProfile_LargeViewmodel:ISetable<ITwitterProfile_Large>,ISwitchableCommands,INotifyPropertyChanged,IDisposable
   {
        public IDispatcher dispatcher { get; set; }
        public TwitterProfile_LargeViewmodel()
        {
        }
        #region Filter Properties
       public string ScreenName
        {
            get { return Profile.ScreenName; }
        }
        public long Friends
        {
            get { return Profile.NumberOfFriends; }
        }
        public long Followers
        {
            get { return Profile.NumberOfFollowers.Value; }
        }
        public string Bio
        {
            get { return Profile.Description; }
        }
       #endregion

        public ITwitterProfile_Large Profile
        {
            get;
            set;
        }
        public void Set(ITwitterProfile_Large param)
        {
            Profile = param;
            RelationshipResolver.CheckRelationshipCommand.Execute(null);
        }
        private IList<ICommand> _commands;
        public IList<System.Windows.Input.ICommand> Commands
        {
            get
            {
                if (_commands == null)
                {
                    _commands = new List<ICommand>();
                }
                return _commands;
            }
            set
            {
                if (_commands != value)
                {
                    _commands = value;
                    if (dispatcher != null)
                    {
                        dispatcher.Invoke((s) => { OnPropertyChanged("Commands"); });
                    }
                }
            }
                
        }
        
        public IRelationshipChecker RelationshipResolver { get; set; }


        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            RelationshipResolver = null;
            Commands.Clear();

        }
   }
}
