using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common
{
    public interface ITweet:IComparable<ITweet>
    {
        string Name { get; set; }
        string ScreenName { get; set; }

        decimal Id { get; set; }

        string StringId { get; set; }

        bool? IsTruncated { get; set; }

        DateTime CreatedDate { get; set; }

        string Source { get; set; }

        string InReplyToScreenName { get; set; }

        decimal? InReplyToUserId { get; set; }

        decimal? InReplyToStatusId { get; set; }

        bool? IsFavorited { get; set; }

        string Text { get; set; }

        Uri  Thumbnail { get; set; }

        ITweet RetweetedStatus { get; }

        string RetweetCountString { get; set; }

        int? RetweetCount { get; }

        bool Retweeted { get; set; }

        List<MentionEntity> MentionedHandles { get; set; }

        Dictionary<string, string> ResolvedUrls { get; set; }

        Dictionary<string,string> EmbeddedPictureThumbnailLocations { get; set; }
    }
}
