using System;
using System.Collections.Generic;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Commands
{
    public interface INetworkAndMiscCommandsFactory
    {
        ICommand CreateFollowCommand(string TargetScreenName);
        ICommand CreateUnFollowCommand(string TargetScreenName);
        ICommand CreateBlockCommand(string TargetScreenName);
        ICommand CreateUnBlockCommand(string TargetScreenName);
        ICommand CreateFavouriteCommand(decimal Id, string Text = null);
        ICommand CreateUnFavouriteCommand(decimal Id, string Text = null);
        ICommand CreateDeleteTweetCommand(decimal Id);
        ICommand CreateReplyCommand(string ScreenName, decimal Id = 0);
        ICommand CreateRetweetCommand(decimal Id, string Text);
        ICommand CreateDirectMessageReplyCommand(decimal Id, string ScreenName);
        IRelationshipChecker CreateRelationshipChecker(string SecondScreenName,ITwitterUser User=null,string FirstScreenName=null);
        ICommand CreateRelationshipCheckerCommand(string SecondScreenName, ITwitterUser User = null, string FirstScreenName = null);
  
    }
}
