using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Runtime.Serialization;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Preferencing
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Preferences
    {
        
        public UserPreferenceFacade TransparentUsersFacade { get; private set; }
        public GeneralPreferences GeneralOptions { get; private set; }
        public Preferences()
        {
           TransparentUsersFacade = new UserPreferenceFacade();
           GeneralOptions = new GeneralPreferences();
        }
        public void Load(PreferencesSaveData data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Data passed is null");
            }
            var facade = new UserPreferenceFacade();           
            foreach (IUserMemento memento in data.userspecific.Users)
            {
                facade.Userrepository.Add(memento.CreateUser());
            }
            if (data.userspecific.SelectedUser != null)
            {
                facade.Userrepository.SelectedUser = data.userspecific.SelectedUser.CreateUser();
            }
            TransparentUsersFacade.Load(facade);
            GeneralOptions = data.generalOptions;
        }
        public PreferencesSaveData CreateSaveData()
        {
            var data = new PreferencesSaveData();
            foreach (IUser user in TransparentUsersFacade.Userrepository.Users)
            {
                data.userspecific.Users.Add(user.CreateUserMemento());
            }
            if (TransparentUsersFacade.Userrepository.SelectedUser != null)
            {
                data.userspecific.SelectedUser = TransparentUsersFacade.Userrepository.SelectedUser.CreateUserMemento();
            }
            data.generalOptions = this.GeneralOptions;
            return data;
        }
    }
#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
    public class PreferencesSaveData
    {
        public PreferencesSaveData()
        {
            userspecific = new UserPreferencesMemento();
        }
#if SILVERLIGHT
        [DataMember]
#endif
        public GeneralPreferences generalOptions { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public UserPreferencesMemento userspecific { get; set; }

    }
    public class GeneralPreferencesMemento
    {
        private bool prompt = true;
        public List<string> ColumnsCreated { get; set; }
        public bool PromptUserSelectionDialog
        {
            get
            { return prompt; }
            set { prompt = value; }
        }
        public bool ShowMessageSentDialog { get; set; }
        public int MaximumNumberOfItemsInASingleColumn { get; set; }
        public bool IsOldRetweetStyle { get; set; }
        private Position selectedNotificationWindowPostion = Position.BottomRight;

        public Position SelectedNotificationWindowPosition
        {
            get { return selectedNotificationWindowPostion; }
            set { selectedNotificationWindowPostion = value; }
        }
    }


#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
    public class UserPreferencesMemento
    {
        public UserPreferencesMemento()
        {
            Users = new List<IUserMemento>();
        }
#if SILVERLIGHT
        [DataMember]
#endif
        public IUserMemento SelectedUser { get; set; }
#if SILVERLIGHT
        [DataMember]
#endif
        public IList<IUserMemento> Users { get; set; }
    }
}
