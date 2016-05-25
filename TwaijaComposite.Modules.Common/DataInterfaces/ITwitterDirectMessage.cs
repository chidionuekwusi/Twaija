using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.DataInterfaces
{
   public  interface ITwitterDirectMessage:IComparable<ITwitterDirectMessage>
    {
         string Name { get; }

         decimal Id { get;  }

        /// <summary>
        /// Gets  the sender id.
        /// </summary>
        /// <value>The sender id.</value>

        decimal SenderId { get;  }

        /// <summary>
        /// Gets the direct message text.
        /// </summary>
        /// <value>The direct message text.</value>

         string Text { get;  }

        /// <summary>
        /// Gets the recipient id.
        /// </summary>
        /// <value>The recipient id.</value>

         decimal RecipientId { get; }

        /// <summary>
        /// Gets the created date.
        /// </summary>
        /// <value>The created date.</value>

         DateTime CreatedDate { get;  }

        /// <summary>
        /// Gets the name of the sender screen.
        /// </summary>
        /// <value>The name of the sender screen.</value>

         string SenderScreenName { get;  }

        /// <summary>
        /// Gets the name of the recipient screen.
        /// </summary>
        /// <value>The name of the recipient screen.</value>
         string RecipientScreenName { get;  }

       /// <summary>
       /// Gets the Senders ProfileImageLocation
       /// </summary>
         Uri SenderProfileImage { get;  }
    }
}
