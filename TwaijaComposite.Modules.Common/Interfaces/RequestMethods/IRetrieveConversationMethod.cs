using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.Interfaces
{
    public interface IRetrieveConversationMethod:ITwitterMethod,IRequestMethod<ITweet>
    {
        /// <summary>
        /// The Id of the first Tweet to obtain.
        /// </summary>
        decimal TweetId { get; set; }
        /// <summary>
        /// The First Tweet To Display.
        /// </summary>
        ITweet Tweet { get; set; }
        /// <summary>
        /// This is to inform the Request Object that the end of the conversation has been reached;
        /// </summary>
        event EventHandler EndOfConversation;
    }
}
