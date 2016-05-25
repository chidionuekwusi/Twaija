using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.DataInterfaces;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.AttributeTypes;

namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    [FollowerPropertyLocator(TargetPropertyName="Followers")]
    [FriendPropertyLocator(TargetPropertyName="Friends")]
    [TextPropertyLocator(TargetPropertyName="Bio")]
    [ScreenNamePropertyLocator(TargetPropertyName="ScreenName")]
   public class TwitterProfile_SmallViewmodel:ISetable<ITwitterProfile_Small>,ICommandable
    {
       public ITwitterProfile_Small Profile
       {
           get { return profile; }
       }
       public TwitterProfile_SmallViewmodel()
       {
           Commands = new List<ICommand>();
       }
       private ITwitterProfile_Small profile;
       public void Set(ITwitterProfile_Small param)
       {
           profile = param;
       }
       public long Followers
       {
           get { return profile.Followers; }
       }

       public long Friends
       {
           get { return profile.Friends; }
       }
       public string Bio
       {
           get { return profile.Bio; }
       }
       public string ScreenName
       {
           get { return profile.ScreenName; }
       }

        #region ICommandable Members

        public IList<System.Windows.Input.ICommand> Commands
        {
            get;
            private set;
        }

        #endregion
    }
}
