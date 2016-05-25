using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Twitterizer;

namespace TwaijaComposite.RequestAdapterModule
{
    public class TwitterProfile_LargeAdapter:ITwitterProfile_Large
    {
        public TwitterProfile_LargeAdapter()
        {
           
        }
        public TwitterProfile_LargeAdapter(TwitterUser user)
        {
            Id = user.Id;
            StringId = user.StringId;
            Name = user.Name;
            Location = user.Location;
            Description = user.Description;
            CreatedDate = user.CreatedDate;
            TimeZone = user.TimeZone;
            NumberOfFollowers = user.NumberOfFollowers;
            NumberOfStatuses = user.NumberOfStatuses;
            NumberOfFriends = user.NumberOfFriends;
            IsContributorsEnabled = user.IsContributorsEnabled;
            Language = user.Language;
            DoesReceiveNotifications = user.DoesReceiveNotifications;
            ScreenName = user.ScreenName;
            IsFollowing = user.IsFollowing;
            IsFollowedBy = user.IsFollowedBy;
            NumberOfFavorites = user.NumberOfFavorites;
            IsProtected = user.IsProtected;
            IsGeoEnabled = user.IsGeoEnabled;
            TimeZoneOffset = user.TimeZoneOffset;
            Website = user.Website;
            ListedCount = user.ListedCount;
            FollowRequestSent = user.FollowRequestSent;
            Verified = user.Verified;
            ProfileBackgroundColorString = user.ProfileBackgroundColorString;
            IsProfileBackgroundTiled = user.IsProfileBackgroundTiled;
            ProfileLinkColorString = user.ProfileLinkColorString;
            if (user.ProfileImageLocation != null)
            {
                ProfileImageLocation = user.ProfileImageLocation.Replace("_normal", string.Empty).Replace("_small", string.Empty); 
       
            }
            ProfileBackgroundImageLocation = user.ProfileBackgroundImageLocation;
            ProfileTextColorString = user.ProfileTextColorString;
            ProfileImageSecureLocation = user.ProfileImageSecureLocation;
            ProfileSidebarBorderColorString = user.ProfileSidebarBorderColorString;
            //ProfileImageLocation = user.ProfileImageLocation;
        }
        #region Properties
        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
       public decimal Id { get; set; }

        /// <summary>
        /// Gets or sets the string id.
        /// </summary>
        /// <value>The string id.</value>
       public string StringId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
       public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
       public string Location { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
       public string Description { get; set; }


        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
       public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
       public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the number of followers.
        /// </summary>
        /// <value>The number of followers.</value>
       public long? NumberOfFollowers { get; set; }

        /// <summary>
        /// Gets or sets the number of statuses.
        /// </summary>
        /// <value>The number of statuses.</value>
       public long NumberOfStatuses { get; set; }

        /// <summary>
        /// Gets or sets the number of friends.
        /// </summary>
        /// <value>The number of friends.</value>
       public long NumberOfFriends { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has enabled contributors access to his or her account.
        /// </summary>
        /// <value>The is contributors enabled value.</value>
       public bool IsContributorsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
       public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user receives notifications.
        /// </summary>
        /// <value>
        /// <c>true</c> if the user receives notifications; otherwise, <c>false</c>.
        /// </value>
       public bool? DoesReceiveNotifications { get; set; }

        /// <summary>
        /// Gets or sets the screenname.
        /// </summary>
        /// <value>The screenname.</value>
       public string ScreenName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authenticated user is following this user.
        /// </summary>
        /// <value>
        /// <c>true</c> if the authenticated user is following this user; otherwise, <c>false</c>.
        /// </value>
       public bool? IsFollowing { get; set; }

        /// <summary>
        /// Gets or sets the a value indicating whether the authenticated user is followed by this user.
        /// </summary>
        /// <value>The is followed by.</value>
       public bool? IsFollowedBy { get; set; }

        /// <summary>
        /// Gets or sets the number of favorites.
        /// </summary>
        /// <value>The number of favorites.</value>
       public long NumberOfFavorites { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is protected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is protected; otherwise, <c>false</c>.
        /// </value>
       public bool IsProtected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this user is geo enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user is geo enabled; otherwise, <c>false</c>.
        /// </value>
       public bool? IsGeoEnabled { get; set; }

        /// <summary>
        /// Gets or sets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        /// <remarks>Also called the Coordinated Universal Time (UTC) offset.</remarks>
       public double? TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the user's website.
        /// </summary>
        /// <value>The website address.</value>
       public string Website { get; set; }

        /// <summary>
        /// Gets or sets the listed count.
        /// </summary>
        /// <value>The listed count.</value>
       public int ListedCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [follow request sent].
        /// </summary>
        /// <value><c>true</c> if [follow request sent]; otherwise, <c>false</c>.</value>
       public bool? FollowRequestSent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is verified.
        /// </summary>
        /// <value><c>true</c> if the user is verified; otherwise, <c>false</c>.</value>
       public bool? Verified { get; set; }

        #region Profile Layout Properties
        /// <summary>
        /// Gets or sets the color of the profile background.
        /// </summary>
        /// <value>The color of the profile background.</value>
       public string ProfileBackgroundColorString { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this user's profile background image is tiled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this user's profile background image is tiled; otherwise, <c>false</c>.
        /// </value>
       public bool? IsProfileBackgroundTiled { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile link.
        /// </summary>
        /// <value>The color of the profile link.</value>
       public string ProfileLinkColorString { get; set; }


        /// <summary>
        /// Gets or sets the profile background image location.
        /// </summary>
        /// <value>The profile background image location.</value>
       public string ProfileBackgroundImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile text.
        /// </summary>
        /// <value>The color of the profile text.</value>
       public string ProfileTextColorString { get; set; }



        /// <summary>
        /// Gets or sets the profile image location.
        /// </summary>
        /// <value>The profile image location.</value>
       public string ProfileImageLocation { get; set; }

        /// <summary>
        /// Gets or sets the secure profile image location (https).
        /// </summary>
        /// <value>The profile image location.</value>
       public string ProfileImageSecureLocation { get; set; }

        /// <summary>
        /// Gets or sets the color of the profile sidebar border.
        /// </summary>
        /// <value>The color of the profile sidebar border.</value>
       public string ProfileSidebarBorderColorString { get; set; }

        #endregion

        #endregion
       
    }
}
