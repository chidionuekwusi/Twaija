using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.ViewModels;
using TwaijaComposite.Modules.Common.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common;
using Microsoft.Practices.Prism.Commands;
using GalaSoft.MvvmLight.Command;
using TwaijaComposite.Modules.Common.DialogTypes;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Text;
using Microsoft.Practices.Prism.Events;
using TwaijaComposite.Modules.Common.Events;


namespace TwaijaComposite.Modules.Options.Viewmodels
{
    public class EditableUserFactoryRepository
    {
        public EditableUserFactoryRepository()
        {
            Repository = new Dictionary<Type, IEditableUserFactory>();
        }
        public Dictionary<Type, IEditableUserFactory> Repository { get; set; }
    }
    public interface IEditableUser
    {
        string ScreenName { get; set; }
        void Initialize();
        IUser User { get; set; }
        bool IsInitialized { get; }
        ICommand SaveCommand { get; }
        ICommand RefreshCommand { get; }
        bool IsInEditMode { get; set; }
    }
    public class TwitterEditableUser : ViewModelBase, IEditableUser, IClosableContent, IProgressReportable
    {
        [Dependency]
        public IDialogFacade DialogFacade { get; set; }
        [Dependency]
        public IDispatcher dispatcher { get; set; }
        ITwitterAccountMethodConsole console;
        [Dependency]
        public ITwitterAccountMethodConsole MethodConsole
        {
            get { return console; }
            set
            {
                if (console != value)
                {
                    console = value;
                    value.ProfileUpdated += new EventHandler<UpdateProfileEventArguments>(value_ProfileUpdated);
                    value.Refreshed += value_ProfileUpdated;
                }
            }
        }
        ITwitterUpdateAccountImageMethod updateImageMethodConsole;
        [Dependency]
        public ITwitterUpdateAccountImageMethod UpdateImageMethodConsole
        {
            get { return updateImageMethodConsole; }
            set
            {
                if (updateImageMethodConsole != value)
                {
                    updateImageMethodConsole = value;
                    value.ProfileImageUpdated += new EventHandler<UpdateProfileEventArguments>(value_ProfileImageUpdated);
                }
            }
        }

        void value_ProfileImageUpdated(object sender, UpdateProfileEventArguments e)
        {
            if (e.Successful)
            {
                dispatcher.Invoke((i) =>
                {
                    if (e.parameter != null)
                    {
                        if (e.parameter is Uri)
                        {
                            Image = e.parameter as Uri;
                            ImageTray.EmptyTray();
                        }
                    }

                });
                return;
            }
            if (e.Aborted)
            {
                DialogFacade.PushMessageDialog("An Error occured while trying to upload the image");

            }
            OnProgressReport("Aborted", 100);
            this.OnCloseLoadingDialog();
        }

        void value_ProfileUpdated(object sender, UpdateProfileEventArguments e)
        {
            if (e.Aborted)        
            {
                OnProgressReport("Aborted", 100);
                this.OnCloseLoadingDialog();
            }
            if (e.Successful)
            {
                
                var profile = e.parameter as ITwitterProfile_Large;
                if (profile != null)
                {
                    if (!IsInitialized)
                    {
                        IsInitialized = true;
                    }
                    dispatcher.Invoke((s) =>
                    {
                        this.Image = new Uri(profile.ProfileImageLocation);
                        this.Description = profile.Description;
                        this.Location = profile.Location;
                        this.Web = profile.Website;
                        this.Name = profile.Name;
                    });
                    OnProgressReport("Aborted", 100);
                    this.OnCloseLoadingDialog();
                    return;

                }
                
               
            }
            if (e.parameter != null)
            {
                OnProgressReport(e.parameter.ToString(), 0);
                return;
            }
        }
        private Uri imageLoc;

        public Uri Image
        {
            get { return imageLoc; }
            set
            {
                if (imageLoc != value)
                {
                    imageLoc = value;
                    OnPropertyChanged("Image");
                }
                if (User != null)
                {
                    if (User.DisplayImage != value)
                    {
                        User.DisplayImage = value;
                    }
                }
            }
        }
        private IPictureTray _picTray;

        [Dependency]
        public IPictureTray ImageTray
        {
            get { return _picTray; }
            set { _picTray = value; }
        }
        
        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }
        string _web;
        public string Web
        {
            get { return _web; }
            set
            {
                _web = value;
                OnPropertyChanged("Web");
            }
        }

        public bool IsInitialized
        {
            get;
            private set;
        }
        private ICommand _saveProfileCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveProfileCommand == null)
                {
                    _saveProfileCommand = new RelayCommand(() =>
                {
                    if (IsInitialized)
                    {

                        DialogFacade.PushYesNoDecisionDialog("Are u sure u want to save?", new Action<object>((s) =>
                        {
                            DialogFacade.PushLoadingDialog(this);
                            StringBuilder builder = new StringBuilder("updating: profile information");
                            OnProgressReport(builder.ToString(), 1);
                            MethodConsole.UpdateProfile(Location, Description, Web, Name, User as ITwitterUser);

                        }), false);
                    }

                }, () => { return true; });
                } return _saveProfileCommand;
            }
        }
        private ICommand saveImageCommand;
        public ICommand SaveImageCommand
        {
            get
            {
                if (saveImageCommand == null)
                {
                    saveImageCommand = new RelayCommand(() =>
                    {
                        if (IsInitialized)
                        {
                            if (!ImageTray.IsEmpty)
                            {
                                DialogFacade.PushYesNoDecisionDialog("Are u sure u want to change your display picture?", new Action<object>((s) =>
                                {

                                    if (!ImageTray.IsEmpty)
                                    {
                                        DialogFacade.PushLoadingDialog(this);
                                        StringBuilder builder = new StringBuilder("updating: profile image");
                                        OnProgressReport(builder.ToString(), 0);
                                        updateImageMethodConsole.UpdateProfileImage(ImageTray.Picture.OriginalString, User as ITwitterUser);
                                    }

                                }), false);
                            }
                            else
                            {
                                DialogFacade.PushMessageDialog("Select a picture to upload");
                            }
                        }

                    }, () => { return true; });
                }
                return saveImageCommand;
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (IsInitialized)
                    {

                        MethodConsole.RefreshProfile(User as ITwitterUser);
                    }

                }, () => { return true; });
            }
        }

        private bool isInEditMode;

        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set
            {
                isInEditMode = value;
                OnPropertyChanged("IsInEditMode");
            }
        }
        public IUser User
        {
            get;
            set;
        }

        public void Initialize()
        {
            MethodConsole.RefreshProfile(this.User as ITwitterUser);
        }

        public event EventHandler CloseContent;
        private void OnCloseLoadingDialog()
        {
            if (CloseContent != null)
            {
                CloseContent(this, null);
            }
        }
        public void TriggerContentClosure()
        {
            throw new NotImplementedException();
        }

        public event ProgressReport Progress;
        protected void OnProgressReport(string Message, int PercentageCompleted)
        {
            if (Progress != null)
            {
                Progress(this, Message, PercentageCompleted);
            }
        }
        string _screenName;
        public string ScreenName
        {
            get
            {
                return _screenName;
            }
            set
            {
                _screenName = value;
                OnPropertyChanged("ScreenName");
            }
        }
    }
    public interface IEditableUserFactory
    {
        IEditableUser Create(IUser user);
    }
    public class TwitterEditableUserFactory : IEditableUserFactory
    {
        [Dependency]
        public IUnityContainer m_Container { get; set; }
        public IEditableUser Create(IUser user)
        {
            if (!(user is ITwitterUser))
            {
                throw new ArgumentException("Invalid User Type ,Expected a Twitter User");
            }
            var tuser = user as ITwitterUser;
            var editable = m_Container.Resolve<TwitterEditableUser>();
            editable.User = tuser;
            editable.ScreenName = tuser.ScreenName;
            return editable; 
        }
    }
   public class AccountViewmodel:ViewModelBase,IAccountViewmodel
    {
       [Dependency]
       public EditableUserFactoryRepository EditableUserFactories { get; set; }
       [Dependency]
       public IDialogFacade Dialog { get; set; }
       [Dependency]
       public IOptionFileWriterService writer { get; set; }
       [Dependency]
       public IEventAggregator agg { get; set; }
       private DelegateCommand<object> _deleteUserCommand;
       Preferences pref;
       public DelegateCommand<object> DeleteUserCommand
       {
           get
           {
               if (_deleteUserCommand == null)
               {
                   _deleteUserCommand = new DelegateCommand<object>((o) =>
                   {
                       Dialog.PushYesNoDecisionDialog("Are u sure u want to Delete " + SelectedUser.ScreenName, (s) => {
                           agg.GetEvent<UserDeletedEvent>().Publish(SelectedUser.User);
                           UserPreferences.Userrepository.Remove(SelectedUser.User);   
                           writer.CreateFile(FolderNames.TOKENFOLDER, pref.CreateSaveData());
                           if (pref.TransparentUsersFacade.Userrepository.NumberOfUsers == 0)
                           {
                               agg.GetEvent<RequestApplicationCloseEvent>().Publish(null);
                           }
                           else
                           {                    
                                   var enumerator= pref.TransparentUsersFacade.Userrepository.Users.GetEnumerator();
                                   enumerator.MoveNext();
                                   pref.TransparentUsersFacade.Userrepository.SelectedUser = enumerator.Current;                              
                           }
                       }, true);

                   }, (y) =>
                   {
                       if (SelectedUser != null)
                       {
                           return true;
                       }
                       return false;
                   });
                   
               }
               return _deleteUserCommand;
           }
       }
       private IDispatcher dispatcher;
       private ObservableCollection<IEditableUser> _users;
       public IEnumerable<IEditableUser> Users
       {
           get { return _users; }
       }
       private IEditableUser _selectedUser;
       public IEditableUser SelectedUser
       {
           get { return _selectedUser; }
           set
           {
               dispatcher.Invoke((u) =>
               {
                       _selectedUser = value;
                       if (value != null)
                       {
                           if (!_selectedUser.IsInitialized)
                               _selectedUser.Initialize();
                       }
                       OnPropertyChanged("SelectedUser");
               });
           }
       }
        public UserPreferenceFacade UserPreferences { get; set; }
        public string Header
        {
            get {return "account"; }
        }
        public void Initialize()
        {
            if (UserPreferences == null)
            {
                throw new ArgumentNullException("User preference facade cannot be null");
            }

            _users = new ObservableCollection<IEditableUser>();
            foreach (IUser user in UserPreferences.Userrepository.Users)
            {
                DispatcherInvokeAdd(Convert(user));
            }
            UserPreferences.Userrepository.CollectionChanged += new Common.CollectionChangedHandler(Userrepository_CollectionChanged);
            if (SelectedUser == null)
            {
                SelectedUser = _users[0];
            }
        }
        public IEditableUser Convert(IUser user)
        {
            if(EditableUserFactories.Repository.ContainsKey(user.GetType()))
           return EditableUserFactories.Repository[user.GetType()].Create(user);

           throw new KeyNotFoundException("No repository entry (EditableUserFactory) exists to create the desired editable user type,check the repository Dufus");
        }
        void Userrepository_CollectionChanged(object changeditem, Common.ListChangedEventType change)
        {
            switch (change)
            {
                case Common.ListChangedEventType.Add:

                    DispatcherInvokeAdd(Convert(changeditem as IUser));
                    break;
                case Common.ListChangedEventType.Remove:
                    DispatcherInvokeRemove(changeditem as IUser);
                    break;
            }
            
        }
        void DispatcherInvokeAdd(IEditableUser user)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) => Add(user)));
        }
        void DispatcherInvokeRemove(IUser user)
        {
            dispatcher.Invoke(new System.Threading.SendOrPostCallback((u) =>
            {
                IEditableUser selecteduser=null;
                foreach (IEditableUser editable in _users)
                {
                    if (editable.User.Equals(user))
                    {
                        selecteduser = editable;
                    }
                }
                if(selecteduser!=null)
                Remove(selecteduser);
            }));
        }
        void Add(IEditableUser user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }
        }
        void Remove(IEditableUser user)
        {
            _users.Remove(user);
        }

        public AccountViewmodel(Preferences pref,IDispatcher dispatcher,EditableUserFactoryRepository repo)
        {
            if (pref == null)
            {
                throw new ArgumentNullException("Preference passed to TwitterOptionsViewmodel is Null");
            }
            this.dispatcher = dispatcher;
            this.pref = pref;
            UserPreferences = pref.TransparentUsersFacade;
            this.EditableUserFactories=repo;
            Initialize();           
        }
    }
}
