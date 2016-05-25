using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Events;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
   public class DeleteFavouriteCommandHelper  
        : TwitterCommandBaseHelper
   {
           public decimal Id { get; set; }
           public DeleteFavouriteCommandHelper()
           {
               SuccessMessage = "Favourite Deleted";
           }
           [Dependency]
           public IEventAggregator aggr { get; set; }
           [Dependency]
           public IUnFavouriteCommand command { get; set; }
           public override void Execute(object parameter)
           {
               aggr.GetEvent<UserResolutionDialogEvent>().Publish(new UserRDEventPayLoad() { action = this.Template, IsExecutedOnUIThread = false });
           }
           protected override bool Condition(Common.DataInterfaces.ITwitterUser user)
           {
               return command.UnFavourite(Id, user);
           }
       }
    
}
