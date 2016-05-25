using System;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TwaijaComposite.Modules.Common.Preferencing
{
    public delegate void SelectedUserChanged(SelectedUserChangedEventArgs args);
    public class SelectedUserChangedEventArgs : EventArgs
    {
        public IUser NewUser { get; set; }
        public IUser OldUser { get; set; }
    }
#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
   public class UserRepository
   {
       #region fields
  
       private NotifiableList<IUser> _users;
#if !SILVERLIGHT
        [field:NonSerialized]
#endif
       public event SelectedUserChanged CurrentUserChanged;

#if SILVERLIGHT
        [DataMember]
#endif
       public IEnumerable<IUser> Users
       {
           get { return _users; }
           set {
               if (value != null)
               {
                   var people=new NotifiableList<IUser>();
                   var enumerator=value.GetEnumerator();
                   while (enumerator.MoveNext())
                   {
                       people.Add(enumerator.Current);
                   }
                   _users = people;
               }
             
           }   
       }

       private IUser _currentUser;
#if SILVERLIGHT
        [DataMember]     
#endif
       public IUser SelectedUser
       {
           get { return _currentUser; }
           set
           {
               if (_currentUser != value)
               {
                   var old = _currentUser;
                   _currentUser = value;
                   OnCurrentUserChanged(new SelectedUserChangedEventArgs() { NewUser = value, OldUser = old });

               }
           }
       }
      // public int Count { get { return _users.Count; } }
       public event CollectionChangedHandler CollectionChanged
       {
           add
           {
               _users.CollectionChanged += value;
           }
           remove
           {
               _users.CollectionChanged -= value;
           }
       }
       #endregion
       protected void OnCurrentUserChanged(SelectedUserChangedEventArgs args)
       {
           if (CurrentUserChanged != null)
           {
               CurrentUserChanged(args);
           }
       }
       public int NumberOfUsers
       {
           get { return _users.Count; }
       }
       public UserRepository()
       {
           _users = new NotifiableList<IUser>();
       }
       #region Indexer

       public IUser this[string name]
       {
           get
           {
               return Find<IUser>(name);
           }
       }

       #endregion

       public T Find<T>(string Username) where T : class,IUser
       {
           T found = null;
           foreach (IUser user in Users)
           {
               if (user.ScreenName == Username)
               {
                   if (user is T)
                   {
                       found = user as T;
                   }
               }
           }
           return found;
       }
       public bool Remove(IUser user)
       {
           if (user != null)
           {
               return _users.Remove(user);
           }
           return false;
       }
       public bool Remove<T>(string Username) where T : class,IUser
       {
           var user = Find<T>(Username);
           return Remove(user);

       }
       public bool Remove(string Username)
       {
           var user = Find<IUser>(Username);
           return Remove(user);
       }
       public void Add(IUser user)
       {
           InnerAdd(user);
       }
       private void InnerAdd(IUser user)
       {
           _users.Add(user);
       }
    }
}
