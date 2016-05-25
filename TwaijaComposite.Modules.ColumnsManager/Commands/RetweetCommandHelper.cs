using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Composite.Events;
using TwaijaComposite.Modules.Common.Interfaces.Commands;
using TwaijaComposite.Modules.Common.Preferencing;
using TwaijaComposite.Modules.Common.Events;
using Microsoft.Practices.Prism.Events;

namespace TwaijaComposite.Modules.ColumnsManager.Commands
{
    public class RetweetCommandHelper:TwitterCommandBaseHelper
    {
        public decimal Id { get; set; }
        string text;
        public string Text { get { return text; } set
            {
                if (text != value)
                {
                    text = "RT @" + value;
#if !SILVERLIGHT
                    FailureMessage="Failed to Retweet this:"+text;
                    SuccessMessage="Successfull Retweet:"+text;
#endif
                }
        } }
        [Dependency]
        public IEventAggregator aggr { get; set; }
        [Dependency]
        public IRetweetCommand command { get; set; }
        GeneralPreferences gen;
        public RetweetCommandHelper(Preferences pref)
        {
            gen = pref.GeneralOptions;
            this.FailureMessage = "Failed to Retweet , sorry";
        }
        protected override bool Condition(Common.DataInterfaces.ITwitterUser user)
        {
            return command.Retweet(Id, user);
        }
        public override void Execute(object parameter)
        {
            if (gen.IsOldRetweetStyle)
            {
                var eve = aggr.GetEvent<UserResolutionDialogEvent>();
                eve.Publish(new UserRDEventPayLoad() { action=Template, IsExecutedOnUIThread=false, Text="Select User To Retweet " });
            }
            else
            {
                aggr.GetEvent<CopyToClipboardEvent>().Publish(Text);
            } 
        }
    }
}
