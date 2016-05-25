using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Composite.Presentation.Commands;
using TwaijaComposite.Modules.ColumnsManager.Commands;
using Microsoft.Practices.Unity;
using System.Windows.Input;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Commands.Factories;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;


namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    public enum SearchType
    {
        User,Tweets
    }
    public class SearchConsole : INotifyPropertyChanged
    {
        Dictionary<string, Type> TypeMappings = new Dictionary<string, Type>();
        private IUser CurrentUser
        {
            get;
            set;
        }
        public SearchConsole(IUser user)
        {
            CurrentUser = user;
            CurrentUserChanged = this.Userrepository_CurrentUserChanged;
            searchCommand = new DelegateCommand<object>(Search);
            SearchTypes = new List<string>();
            SearchTypes.Add("User");
            SearchTypes.Add("Tweets");
            TypeMappings.Add("User", typeof(ITwitterUser));
            TypeMappings.Add("Tweets", typeof(ITwitterUser));
            RefreshSearchCommand(CurrentUser);
            SelectedType = SearchTypes[0];
        }
        private bool canSearch;
        public bool CanSearch
        {
            get { return canSearch; }
            private set
            {
                canSearch = value;
                OnPropertyChanged("CanSearch");
            }
        }
        public bool SearchPredicate(object state)
        {
            return canSearch;
        }
        private string selectedType;
        public string SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {                  
                    selectedType = value;
                    OnPropertyChanged("SelectedType");
                    RefreshSearchCommand(CurrentUser);
                }
            }
        }
        [Dependency]
        public ICommandFactory Factory
        {
            get;
            set;
        }
        public IList<string> SearchTypes
        {
           get;
           private set;
        }
        private DelegateCommand<object> searchCommand;

        public DelegateCommand<object> SearchCommand
        {
            get
            {
                return searchCommand;
            }
        }
        public void Search(object state)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                switch (SelectedType)
                {
                    case "User":
                        Factory.CreateUserSearchCommand(Text).Execute(null);
                        break;
                    case "Tweets":
                        Factory.CreateTweetSearchCommand(Text, Geo).Execute(null);
                        break;
                }
            }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _geo;
        public string Geo
        {
            get { return _geo; }
            set
            {
                _geo = value; OnPropertyChanged("Geo");
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public SelectedUserChanged CurrentUserChanged { get; private set; }
        void Userrepository_CurrentUserChanged(SelectedUserChangedEventArgs args)
        {
            RefreshSearchCommand(args.NewUser);
        }
        void RefreshSearchCommand(IUser user)
        {
            if (user != null&&SelectedType!=null)
            {
                CanSearch = (TypeMappings[SelectedType].IsAssignableFrom(user.GetType())) ? true : false;
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public class ToolBarViewmodel:IToolbarViewmodel
    {
        IconifiedCommand homecommand;
        IconifiedCommand mentionscommand;
        IconifiedCommand dmscommand;
        public SearchConsole SearchConsole
        {
            get;
            private set;
        }
        [InjectionConstructor]
        public ToolBarViewmodel(Preferences pref,ICommandFactory factry)
        {
            _factory = factry;
            SearchConsole = new Viewmodels.SearchConsole(pref.TransparentUsersFacade.Userrepository.SelectedUser);
            SearchConsole.Factory = factry;
            _canExecuteTwitterCommand = (pref.TransparentUsersFacade.Userrepository.SelectedUser is ITwitterUser) ? true : false;
            _canExecuteFacebookCommand = (pref.TransparentUsersFacade.Userrepository.SelectedUser is IFacebookUser) ? true : false;
            this.Repos = pref.TransparentUsersFacade.Userrepository;
            pref.TransparentUsersFacade.Userrepository.CurrentUserChanged += new SelectedUserChanged(Userrepository_CurrentUserChanged);
            pref.TransparentUsersFacade.Userrepository.CurrentUserChanged += SearchConsole.CurrentUserChanged;
            homecommand=new IconifiedCommand((model) => { _factory.CreateHomeTimelineCommand(Repos.SelectedUser.ScreenName).Execute(model); },CanExecuteTwitterCommand) { Hint = "Home" };
            mentionscommand=new IconifiedCommand((model) => { _factory.CreateMentionsCommand(Repos.SelectedUser.ScreenName).Execute(model); },CanExecuteTwitterCommand) { Hint = "Mentions" };
            dmscommand=new IconifiedCommand((model) => { _factory.CreateDirectMessagesCommand(Repos.SelectedUser.ScreenName).Execute(model); },CanExecuteTwitterCommand) { Hint = "Direct Messages" };
        }

        void Userrepository_CurrentUserChanged(SelectedUserChangedEventArgs args)
        {
            _canExecuteTwitterCommand = (args.NewUser is ITwitterUser) ? true : false;
            _canExecuteFacebookCommand = (args.NewUser is IFacebookUser) ? true : false;
            foreach (IconifiedCommand command in Commands)
            {
                command.RaiseCanExecuteChanged();
            }
            SearchConsole.SearchCommand.RaiseCanExecuteChanged();
        }
        private bool _canExecuteTwitterCommand;
        private bool _canExecuteFacebookCommand;

        public bool CanExecuteFacebookCommand(object sender)
        {
            return _canExecuteFacebookCommand;
        }
        public bool CanExecuteTwitterCommand(object sender)
        {
            return _canExecuteTwitterCommand;
        }
       
        public ICommandFactory _factory { get; set; }
        public UserRepository Repos { get; set; }
        [Dependency(ServiceKeys.MainRegionNavigationService)]
        public INavigationService nr { get; set; }
        [Dependency]
        public IEventAggregator aggr { get; set; }
        public ToolBarViewmodel()
        {
           
        }

        public ICommand go
        {
            get
            {
                return new DelegateCommand<object>((d) =>
                {
                    nr.NavigateTo(new Common.Events.ViewRequestedEventArgs() { ViewRequested = ViewNames.AuthenticationView });
                });
            }
        }
        private List<IconifiedCommand> commands;
        public IList<IconifiedCommand> Commands
        {
            get
            {
                if (commands == null)
                {
                    commands = new List<IconifiedCommand>();
                    commands.Add(homecommand);
                    commands.Add(mentionscommand);
                    commands.Add(dmscommand);
                }
                return commands;
            }
        }
    }
}
