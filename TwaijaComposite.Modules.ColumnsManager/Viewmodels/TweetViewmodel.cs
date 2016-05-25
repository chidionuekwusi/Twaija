using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using System.Windows.Input;
using TwaijaComposite.Modules.Common.AttributeTypes;


namespace TwaijaComposite.Modules.ColumnsManager.Viewmodels
{
    [ScreenNamePropertyLocator(TargetPropertyName="ScreenName")]
    [TextPropertyLocator(TargetPropertyName="Text")]
    public class TweetViewmodel:ISetable<ITweet>,ICommandable,INotifyOnHostEntry,IDisposable
    {
        public string DateStringFormat
        {
            get;
            private set;
        }
        public string SourceString
        {
            get;
            set;
        }
        public string EmbeddedPicture
        {
            get;
            set;
        }
        public ITweet Tweet
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
        public string ScreenName
        {
            get;
            set;
        }
        public IList<ICommand> Commands
        {
            get;
            set;
        }
        public TweetViewmodel()
        {
            Commands = new List<ICommand>();
        }
        public void Set(ITweet param)
        {
            Tweet = param;
            DateStringFormat = "{0}";
            this.ScreenName = param.ScreenName;
            StringBuilder build=new StringBuilder("via ");
            build.Append(param.Source);
            //build.Append(",");
            if(param.RetweetCount>0)
            {
                build.Append(", retweeted ");
                build.Append(param.RetweetCount);
                build.Append(" times");

            }
            if (!string.IsNullOrEmpty(param.InReplyToScreenName))
            {
                build.Append(", in reply to ");
                build.Append(param.InReplyToScreenName);
            }
            build.Append(".");
            this.SourceString = build.ToString();
            this.Text = param.Text;
            if (Tweet.EmbeddedPictureThumbnailLocations != null && Tweet.EmbeddedPictureThumbnailLocations.Count > 0)
            {
                object embed;
                if (( embed = Tweet.EmbeddedPictureThumbnailLocations.First()) != null)
                {
                    this.EmbeddedPicture =((KeyValuePair<string,string>) embed).Value;
                }
            }
           // this.EmbeddedPicture =(Tweet.EmbeddedPictureThumbnailLocations==null)? null: (Tweet.EmbeddedPictureThumbnailLocations.Count > 0) ? (Tweet.EmbeddedPictureThumbnailLocations.First()==null)?Value : null;
        }
        private Common.Behaviors.AnimationTrigger _onEntry=new Common.Behaviors.AnimationTrigger();
        public Common.Behaviors.AnimationTrigger OnHostEntry
        {
            get { return _onEntry; }
            set { _onEntry = value;}
        }

        public void Dispose()
        {
            if (Commands != null)
            {
                Commands.Clear();
            }
            _onEntry = null;
            Tweet = null;
        }
    }
}
