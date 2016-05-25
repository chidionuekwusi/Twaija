using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TwaijaComposite.Modules.ColumnsManager.Notifications;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    public partial class NotificationsViewImp : UserControl,INotificationsView
    {
        public NotificationsViewImp()
        {
            InitializeComponent();
        }

        private INotificationViewmodel _model;
        [Dependency]
        public INotificationViewmodel model
        {
            get { return _model; }
            set
            {
                DataContext = value;
                _model = value;
            }
        }

        public bool IsAlive
        {
            get;
            set;
        }

        public Common.Position SuggestedPosition
        {
            get;
            set;
        }

   
    }
}
