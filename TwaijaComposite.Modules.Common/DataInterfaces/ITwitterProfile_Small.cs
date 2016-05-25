using System;
using System.Collections.Generic;


namespace TwaijaComposite.Modules.Common.DataInterfaces
{
    public interface ITwitterProfile_Small
    {
        string ScreenName { get; set; }
        string Bio { get; set; }
        string Location { get; set; }
        Uri ProfilePictureLocation { get; set; }
        bool IsVerified { get; set; }
        bool IsProtected { get; set; }
        long Followers { get; set; }
        long Friends { get; set; }
        long Tweets { get; set; }
        string Name { get; set; }
    }
}
