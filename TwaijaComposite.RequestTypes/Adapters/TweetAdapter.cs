using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;
using Twitterizer.Entities;
using System.Xml.Serialization;
using System.IO;
namespace TwaijaComposite.RequestAdapterModule
{
    public class TweetAdapter:ITweet
    {
        #region field

        #endregion
        public TweetAdapter()
        {

        }
        string ParseSource(string source)
        {
            var rest = source.Split('<', '>');
            if (rest != null)
                if (rest.Length >= 3)
                {
                    var src = rest[2];
                    return src;
                }
            return source;
        }
        
        public TweetAdapter(TwitterStatus stat)
        {
            Name = stat.User.Name;
            ScreenName = stat.User.ScreenName;
            Id = stat.Id;
            StringId = stat.StringId;
            IsTruncated = stat.IsTruncated;
            CreatedDate = stat.CreatedDate;
            Source = ParseSource(stat.Source);
            InReplyToScreenName = stat.InReplyToScreenName;
            InReplyToStatusId = stat.InReplyToStatusId;
            InReplyToUserId = stat.InReplyToUserId;
            Text = stat.Text;
            IsFavorited = stat.IsFavorited;
            if (stat.RetweetedStatus != null)
            {
                RetweetedStatus = new TweetAdapter(stat.RetweetedStatus);
            }
            RetweetCount = stat.RetweetCount;
            Retweeted = stat.Retweeted;
            RetweetCountString = stat.RetweetCountString;
            Thumbnail = new Uri(stat.User.ProfileImageLocation);
            if (stat.Entities != null)
            {
                CreateEntities(stat.Entities);
            }
        }
        //public TweetAdapter(TwitterSearchResult result)
        //{
            
        //    Id = result.Id;
        //    StringId = Convert.ToString(result.Id);
        //    CreatedDate = result.CreatedDate;
        //    Source = ParseSource(result.Source.Replace("&gt;", ">").Replace("&lt;", "<"));
        //    InReplyToStatusId = result.InReplyToStatusId;
        //    Name = result.FromUserScreenName;
        //    ScreenName = result.FromUserScreenName;
        //    //InReplyToStatusId = result.ToUserId;
        //    InReplyToUserId = result.ToUserId;
        //    InReplyToScreenName = result.ToUserScreenName;
        //    Text = result.Text;
        //    //ScreenName = result.FromUserScreenName;
        //    Thumbnail = new Uri(result.ProfileImageLocation);
        //    if (result.Entities != null)
        //    {
        //        CreateEntities(result.Entities);
        //    }
          
        //}
        public decimal Id
        {
            get;
            set;
        }

        public string StringId
        {
            get;
            set;
        }

        public bool? IsTruncated
        {
            get;
            set;
        }

        public DateTime CreatedDate
        {
            get;
            set;
        }

        public string Source
        {
            get;
            set;
        }

        public string InReplyToScreenName
        {
            get;
            set;
        }

        public decimal? InReplyToUserId
        {
            get;
            set;
        }

        public decimal? InReplyToStatusId
        {
            get;
            set;
        }

        public bool? IsFavorited
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }
        [XmlIgnore]
        public ITweet RetweetedStatus
        {
            get;
            set;
        }

        public string RetweetCountString
        {
            get;
            set;
        }

        public int? RetweetCount
        {
            get;
            set;
        }

        public bool Retweeted
        {
            get;
            set;
        }
        public Dictionary<string,string> EmbeddedPictureThumbnailLocations
        {
            get;
            set;
        }
       [XmlIgnore]
        public Uri Thumbnail
        {
            get;
            set;
        }

        void CreateEntities(TwitterEntityCollection collection)
        {
            EmbeddedPictureThumbnailLocations=new Dictionary<string,string>();
            _resolved = new Dictionary<string, string>();
            _mentionedhandles = new List<MentionEntity>();
            foreach (TwitterEntity entity in collection)
            {
                if (entity is TwitterMediaEntity)
                {
                    var ent = (entity as TwitterMediaEntity);
                    switch (ent.MediaType)
                    {
                        case TwitterMediaEntity.MediaTypes.Photo:
                            _resolved.Add(ent.Url, ent.MediaUrl);
                           StringBuilder smallurl = new StringBuilder(ent.MediaUrl);
                           StringBuilder largeurl=new StringBuilder(ent.MediaUrl);
                            if (ent.Sizes != null)
                            {
                                foreach (TwitterMediaEntity.MediaSize size in ent.Sizes)
                                {
                                    if (size.Size == TwitterMediaEntity.MediaSize.MediaSizes.Small)
                                    {
                                        smallurl.Append("?size=small");
                                    }
                                    if (size.Size == TwitterMediaEntity.MediaSize.MediaSizes.Large)
                                    {
                                        largeurl.Append("?size=Large");
                                    }
                                }
                            }
                            EmbeddedPictureThumbnailLocations.Add(smallurl.ToString(),largeurl.ToString());
                            break;
                        case TwitterMediaEntity.MediaTypes.Unknown:
                            _resolved.Add(ent.Url, ent.ExpandedUrl);
                            break;
                    }
                   
                }
                if (entity is TwitterMentionEntity)
                {
                    _mentionedhandles.Add(new MentionEntity((entity as TwitterMentionEntity).ScreenName, (entity as TwitterMentionEntity).UserId));
                }
            }
        }
        private List<MentionEntity> _mentionedhandles;
        public List<Modules.Common.DataInterfaces.MentionEntity> MentionedHandles
        {
            get
            {
                return _mentionedhandles;
            }
            set
            {
                _mentionedhandles = value;
            }
        }
        
        private Dictionary<string, string> _resolved;
        public Dictionary<string, string> ResolvedUrls
        {
            get
            {
                return _resolved;
            }
            set
            {
                _resolved = value;
            }
        }

        public int CompareTo(ITweet other)
        {
            //Original
            //if (this.CreatedDate.Ticks < other.CreatedDate.Ticks)
            //{
            //    return 1;
            //}
            //if (this.CreatedDate.Ticks > other.CreatedDate.Ticks)
            //{
            //    return -1;
            //}
            if (this.CreatedDate.Ticks < other.CreatedDate.Ticks)
            {
                return -1;
            }
            if (this.CreatedDate.Ticks > other.CreatedDate.Ticks)
            {
                return 1;
            }

            if (CreatedDate.Ticks == other.CreatedDate.Ticks)
            {
                if (this.Equals(other))
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            return -1;
        }

        public string ScreenName
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;

        }
    }
}
