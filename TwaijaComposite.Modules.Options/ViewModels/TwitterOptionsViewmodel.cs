using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.ViewModels;

namespace TwaijaComposite.Modules.Options.Viewmodels
{
   public class TwitterOptionsViewmodel:ViewModelBase,ITwitterOptionsViewmodel
    {
       public UserPreferenceFacade UserPreferences { get; set; }
       public List<int> PossibleNumberOfObjectsToRetrieve { get; set; }
       ITwitterUser _selectedTwitterUser;
       public ITwitterUser SelectedTwitterUser
       {
           get { return _selectedTwitterUser; }
           set
           {
               if (_selectedTwitterUser != value)
               {
                   _selectedTwitterUser = value;
                   OnPropertyChanged("SelectedTwitterUser");
               }
           }
       }
       List<ITwitterUser> _users;
       public List<ITwitterUser> TwitterUserList
       {
           get { return _users; }
           set
           {
               if (_users != value)
               {
                   _users = value;
                   OnPropertyChanged("TwitterUserList");
               }
           }
       }
       public TwitterOptionsViewmodel(Preferences pref)
       {
           if (pref == null)
           {
               throw new ArgumentNullException("Preference passed to TwitterOptionsViewmodel is Null");
           }
           this.UserPreferences = pref.TransparentUsersFacade;
           Initialize();
       }
       private void Initialize()
       {
           this.UserPreferences.Userrepository.CollectionChanged += Userrepository_CollectionChanged;
           this.TwitterUserList = new List<ITwitterUser>(this.UserPreferences.Userrepository.Users.OfType<ITwitterUser>());
           PossibleNumberOfObjectsToRetrieve = new List<int>();
           PossibleNumberOfObjectsToRetrieve.Add(20);
           PossibleNumberOfObjectsToRetrieve.Add(50);
           PossibleNumberOfObjectsToRetrieve.Add(80);
           PossibleNumberOfObjectsToRetrieve.Add(100);
       }

       void Userrepository_CollectionChanged(object changeditem, Common.ListChangedEventType change)
       {
           this.TwitterUserList = new List<ITwitterUser>(this.UserPreferences.Userrepository.Users.OfType<ITwitterUser>());
       }
        public string Header
        {
            get { return "twitter"; }
        }
    }
}
