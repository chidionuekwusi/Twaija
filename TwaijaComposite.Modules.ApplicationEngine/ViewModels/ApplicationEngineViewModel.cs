using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.ViewModels;
//using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Services;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Preferencing;
using System.Collections.ObjectModel;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Microsoft.Practices.Prism.Commands;

namespace TwaijaComposite.Modules.ApplicationEngine.ViewModels
{
   public class ApplicationEngineViewModel:ViewModelBase
   {
       #region fields
       #endregion

       [Dependency(ServiceKeys.MainRegionNavigationService)]
       public INavigationService NavigationService { get; set; }
       private UserRepository _repo;
       
       public UserRepository URepository
       {
           get { return _repo; }
         private  set
           {
               if (_repo != value)
               {
                   foreach (IUser user in value.Users)
                   {
                       AddUser(user);
                   }
                   _repo = value;
                   _repo.CollectionChanged += new CollectionChangedHandler(_repo_CollectionChanged);
                   _repo.CurrentUserChanged += new SelectedUserChanged(Userrepository_CurrentUserChanged);
                   SelectedUser = _repo.SelectedUser;
               }
           }
       }

       //void _repo_CurrentUserChanged(SelectedUserChangedEventArgs args)
       //{
       //    SelectedUser = args.NewUser;
       //}

       private IUser selectedUser;
       public IUser  SelectedUser
       {
           get { return selectedUser; }
           set
           {
  
                       if (selectedUser != value)
                       {
                           selectedUser = value;
                           OnPropertyChanged("SelectedUser");
                           if (_repo.SelectedUser != value)
                           {
                               _repo.SelectedUser = value;
                           }
                       
               }
           }

       }
       protected void AddUser(IUser user)
       {
           Dispatcher.Invoke((e) => { _users.Add(e as IUser); }, user);
       }
       protected void RemoveUser(IUser user)
       {
           Dispatcher.Invoke((e) => { _users.Remove(e as IUser); }, user);
       }
       void _repo_CollectionChanged(object changeditem, ListChangedEventType change)
       {
           var user = changeditem as IUser;
           if (user != null)
           {
               switch (change)
               {
                   case ListChangedEventType.Add:
                       AddUser(user);
                       break;
                   case ListChangedEventType.Remove:
                       RemoveUser(user);
                       break;
               }
           }
       }
       private ObservableCollection<IUser> _users = new ObservableCollection<IUser>();
       public IEnumerable<IUser> Users
       {
           get
           {
               return _users;
           }
       }
       [Dependency]
       public IDispatcher Dispatcher { get; set; }
       #region Constructor
       public ApplicationEngineViewModel(Preferences preferences)
       {
           if (preferences == null)
           {
               throw new ArgumentNullException("preferences object is null, ApplicationEngineViewModelConstructor");
           }
           URepository = preferences.TransparentUsersFacade.Userrepository;
       }

       void Userrepository_CurrentUserChanged(SelectedUserChangedEventArgs args)
       {
           if (selectedUser == null)
           {
               this.SelectedUser = args.NewUser;
               if (args.NewUser != null)
               {
                   URepository.CurrentUserChanged -= this.Userrepository_CurrentUserChanged;
               }
           }
       }
       #endregion
       public DelegateCommand<object> GoToOptionsViewCommand
       {
           get { return new DelegateCommand<object>((p) => GoToOptionsViewMethod(p)); }
       }
       public DelegateCommand<object> GoToLoginViewCommand
       {
           get { return new DelegateCommand<object>((p) => GoToLogOnViewMethod(p)); }
       }
       void GoToLogOnViewMethod(object s)
       {
           NavigationService.NavigateTo(new Common.Events.ViewRequestedEventArgs() { ViewRequested = ViewNames.AuthenticationView, RegionRequested = RegionNames.MainSpace, context = this });

       }
       private void GoToOptionsViewMethod(object p)
       {
          
           NavigationService.NavigateTo(new Common.Events.ViewRequestedEventArgs() { ViewRequested = ViewNames.OptionsView, RegionRequested = RegionNames.MainSpace, context = this });
       }
    }
}
