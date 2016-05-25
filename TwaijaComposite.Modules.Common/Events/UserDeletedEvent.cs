using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwaijaComposite.Modules.Common.DataInterfaces;

namespace TwaijaComposite.Modules.Common.Events
{
    public class UserDeletedEvent : CompositePresentationEvent<IUser>
    {
        public UserDeletedEvent()
        {

        }
    }
}
