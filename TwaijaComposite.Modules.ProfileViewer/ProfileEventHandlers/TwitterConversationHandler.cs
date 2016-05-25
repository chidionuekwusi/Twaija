using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ProfileViewer.ProfileManager;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.Common;
using TwaijaComposite.Modules.ProfileViewer.Views;

namespace TwaijaComposite.Modules.ProfileViewer.ProfileEventHandlers
{
    public class TwitterConversationHandler : IProfileEventHandler
    {
        public readonly IColumnResolutionService cRService;

        public TwitterConversationHandler(IColumnResolutionService service)
       {
           cRService = service;
       }

        public string Name
        {
            get { return ProfileHandlerTypeKeys.TwitterConversationThreadKey; }
        }

        public object HandleEvent(Common.Events.OpenProfileEventArgs args)
        {
            var viewmodel=cRService.HandleEvent(new CreateConversationCommandHelper() { Tweet = args.Parameters[CreateColumnEventParameters.TweetKey] as ITweet, TweetId = (decimal)args.Parameters[CreateColumnEventParameters.TweetIdKey] }.SetupArguments());
            var view = new ConversationView();
            view.DataContext = viewmodel;
            viewmodel.Initialize();
            return view;
        }
    }
}
