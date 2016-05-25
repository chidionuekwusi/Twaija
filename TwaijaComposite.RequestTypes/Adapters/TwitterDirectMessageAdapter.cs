using System;
using Twitterizer;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.RequestAdapterModule
{
    public class TwitterDirectMessageAdapter:ITwitterDirectMessage
    {
        TwitterDirectMessage message;

        #region ITwitterDirectMessage
        public decimal Id { get { return message.Id; } }

        public decimal SenderId { get { return message.SenderId; } }

        public string Text { get { return message.Text; } }

        public decimal RecipientId { get { return message.RecipientId; } }

        public DateTime CreatedDate { get { return message.CreatedDate; } }

        public string SenderScreenName { get { return message.SenderScreenName; } }

        public string RecipientScreenName { get { return message.RecipientScreenName; } }

        public Uri SenderProfileImage { get { return new Uri(message.Sender.ProfileImageLocation); } }
     
        public TwitterDirectMessageAdapter(TwitterDirectMessage message)
        {
            this.message = message;
        }

        public int CompareTo(ITwitterDirectMessage other)
        {
            if (this.CreatedDate.Ticks < other.CreatedDate.Ticks)
            {
                return 1;
            }
            if (this.CreatedDate.Ticks > other.CreatedDate.Ticks)
            {
                return -1;
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
        #endregion

        public string Name
        {
            get { return message.Sender.Name; }
        }
    }
}
