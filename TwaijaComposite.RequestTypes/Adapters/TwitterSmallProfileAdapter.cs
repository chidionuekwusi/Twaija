using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public class TwitterSmallProfileAdapter:ITwitterProfile_Small
    {
        public TwitterSmallProfileAdapter(TwitterUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }
            this.Name = user.Name;
            ScreenName = user.ScreenName;
            Bio = user.Description;
            Location = user.Location;
            Uri loc=null;
            Uri.TryCreate(user.ProfileImageLocation,UriKind.RelativeOrAbsolute,out loc);
            ProfilePictureLocation = loc;
            if(user.Verified.HasValue)
            IsVerified = user.Verified.Value;
            IsProtected = user.IsProtected;
            if(user.NumberOfFollowers.HasValue)
            Followers = user.NumberOfFollowers.Value;
            Friends = user.NumberOfFriends;
            Tweets = user.NumberOfStatuses;
        }
        public string ScreenName
        {
            get;set;
        }

        public string Bio
        {
            get;set;
        }

        public string Location
        {
            get;
            set;
        }

        public Uri ProfilePictureLocation
        {
            get;
            set;
        }

        public bool IsVerified
        {
            get;
            set;
        }

        public bool IsProtected
        {
            get;
            set;
        }


        public long Followers
        {
            get;
            set;
        }

        public long Friends
        {
            get;
            set;
        }

        public long Tweets
        {
            get;
            set;
        }


        public string Name
        {
            get;
            set;
        }
    }
}
