using Microsoft.Practices.Prism.Events;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
