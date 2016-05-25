using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ProfileViewer.ProfileManager;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.DataInterfaces;
using TwaijaComposite.Modules.ProfileViewer.Viewmodels;
using TwaijaComposite.Modules.ProfileViewer.Views;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common.Resources;

namespace TwaijaComposite.Modules.ProfileViewer.ProfileEventHandlers
{
   public  class TwitterProfileHandler:IProfileEventHandler
    {  
       public readonly IColumnResolutionService cRService;

       public TwitterProfileHandler(IColumnResolutionService service)
       {
           cRService = service;
       }
        public string Name
        {
            get { return ProfileHandlerTypeKeys.TwitterProfileKey; }
        }

        public object HandleEvent(Common.Events.OpenProfileEventArgs args)
        {
            var ScreenName = args.Parameters[CreateColumnEventParameters.TargetScreenNameKey].ToString();
            var model = new ProfileViewmodel();
            model.MainContent = cRService.HandleEvent(new CreateTwitterProfileCommandHelper() {  ScreenName=ScreenName}.SetupArguments());
            var timeline = cRService.HandleEvent(new UserTimelineCommandHelper() { ScreenName = ScreenName, CustomModelFactoryKey = ModelFactoryKeys.TweetViewmodelCustomFactoryKey, ColumnImpType = "ProfileColumn" }.SetupArguments());
            var mentions = cRService.HandleEvent(new TweetSearchCommandHelper() { Query = ScreenName, ColumnImpType = "ProfileColumn" }.SetupArguments());
            var followers = cRService.HandleEvent(new CreateFollowersCommandHelper() { ScreenName = ScreenName, ColumnImpType = "ProfileColumn" }.SetupArguments());
            var friends = cRService.HandleEvent(new CreateFriendsCommandHelper() { ScreenName = ScreenName, ColumnImpType = "ProfileColumn" }.SetupArguments());
            var fav = cRService.HandleEvent(new CreateFavouritesCommandHelper() { ScreenName = ScreenName, ColumnImpType = "ProfileColumn" }.SetupArguments());
            timeline.Header = "timeline";
            mentions.Header = "mentions";
            followers.Header = "followers";
            friends.Header = "friends";
            fav.Header = "favourites";
            model.SecondaryContent.Add(timeline);
            model.SecondaryContent.Add(mentions);
            model.SecondaryContent.Add(followers);
            model.SecondaryContent.Add(friends);
            model.SecondaryContent.Add(fav);
            model.Selected = timeline;
            var view = new ProfileView();
            view.DataContext = model;
            return view;
        }
    }
}
