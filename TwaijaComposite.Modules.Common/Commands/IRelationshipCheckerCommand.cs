using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Commands
{
   public interface IRelationshipChecker
   {
       string IsFollowingString { get; }
       string IsFollowedString { get; }
       bool IsFollowing { get; }
       bool IsFollowed { get; }
       string FirstScreenName { get; set; }
       string SecondScreenName { get; set; }
       ITwitterUser User { get; set; }
       ICommand CheckRelationshipCommand { get; }
       ISwitchableCommands Commandable { get; set; }
    }
}
