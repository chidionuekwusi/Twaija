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
    public class FavouriteCommandHelper:TwitterCommandBaseHelper
    {
        public decimal Id { get; set; }
        string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    SuccessMessage = "Tweet Favourited:" + value;
                }
            }
        }
        [Dependency]
        public IEventAggregator aggr { get; set; }
        [Dependency]
        public IFavouriteCommand command { get; set; }
        public override void Execute(object parameter)
        {
            aggr.GetEvent<UserResolutionDialogEvent>().Publish(new UserRDEventPayLoad() {Text="Favourite? :"+this.Text, action = this.Template, IsExecutedOnUIThread = false });
        }
        protected override bool Condition(Common.DataInterfaces.ITwitterUser user)
        {
            return command.Favourite(Id, user);
        }
    }
}
