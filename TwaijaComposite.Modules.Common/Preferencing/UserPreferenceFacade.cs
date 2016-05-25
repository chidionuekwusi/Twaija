using System;
using System.Collections.Generic;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.Common.Interfaces;
using System.Runtime.Serialization;

/*This is a Transparent Facade ,Where Users are visible to the 
 client either directly or through the Facade....*/
namespace TwaijaComposite.Modules.Common.Preferencing
{
#if !SILVERLIGHT
    [Serializable]
#else
    [DataContract]
#endif
    public class UserPreferenceFacade
    {
        #region Properties
#if SILVERLIGHT
        [DataMember]
#endif
        public UserRepository Userrepository { get;  set; }
        #endregion

    
        public UserPreferenceFacade()
        {
            Userrepository = new UserRepository();
        }

        #region Methods
        public void Load(UserPreferenceFacade mem)
        {
            foreach (IUser user in mem.Userrepository.Users)
            {
                this.Userrepository.Add(user);
            }
            if (mem.Userrepository.SelectedUser != null)
            {
                this.Userrepository.SelectedUser = this.Userrepository.Find<IUser>(mem.Userrepository.SelectedUser.ScreenName);
            }
        }
        
        #endregion
    } 
}
