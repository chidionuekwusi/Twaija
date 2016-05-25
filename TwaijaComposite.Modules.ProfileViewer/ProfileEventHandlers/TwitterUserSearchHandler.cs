using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.ProfileViewer.ProfileManager;
using TwaijaComposite.Modules.Common.Resources;
using TwaijaComposite.Modules.Common.Interfaces;
using TwaijaComposite.Modules.Common.Commands;
using TwaijaComposite.Modules.ProfileViewer.Views;

namespace TwaijaComposite.Modules.ProfileViewer.ProfileEventHandlers
{
    public  class TwitterUserSearchHandler:IProfileEventHandler
    {
        public TwitterUserSearchHandler(IColumnResolutionService service)
        {
            cRService = service;
        }
        public readonly IColumnResolutionService cRService;
        public string Name
        {
            get { return ProfileHandlerTypeKeys.TwitterUserSearchKey; }
        }

        public object HandleEvent(Common.Events.OpenProfileEventArgs args)
        {
            var viewmodel = cRService.HandleEvent(new CreateUserSearchCommandHelper() { ScreenName = args.Parameters[CreateColumnEventParameters.TargetScreenNameKey].ToString() }.SetupArguments());
            var view = new UserSearchView();
            view.DataContext = viewmodel;
            viewmodel.Initialize();
            return view;
        }
    }
}
