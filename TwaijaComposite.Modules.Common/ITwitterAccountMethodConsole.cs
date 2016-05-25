using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common
{
    public class UpdateProfileEventArguments:EventArgs
    {
        public  object parameter { get; set; }
        public bool Successful { get; set; }
        public bool Aborted { get; set; }
    }
    public interface ITwitterAccountMethodConsole
    {
        event EventHandler<UpdateProfileEventArguments> ProfileUpdated;
        event EventHandler<UpdateProfileEventArguments> Refreshed;
        void UpdateProfile(string location, string description, string web, string name,ITwitterUser user);
        void RefreshProfile(ITwitterUser user);
    }
    public interface ITwitterUpdateAccountImageMethod
    {   
        event EventHandler<UpdateProfileEventArguments> ProfileImageUpdated;
        void UpdateProfileImage(string imageLocation, ITwitterUser user);
    }
}
