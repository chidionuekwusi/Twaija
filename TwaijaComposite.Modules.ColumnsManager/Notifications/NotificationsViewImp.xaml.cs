using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using TwaijaComposite.Modules.Common;

namespace TwaijaComposite.Modules.ColumnsManager.Notifications
{
    /// <summary>
    /// Interaction logic for NotificationsViewImp.xaml
    /// </summary>
    public partial class NotificationsViewImp : UserControl, INotificationsView
    {
        public NotificationsViewImp()
        {
            InitializeComponent();
           
        }
        [Dependency]
        public INotificationViewmodel model
        {
            set
            {
                DataContext = value;
                
            }
        }

        public bool IsAlive
        {
            get;
            set;
        }

        public Position SuggestedPosition
        {
            get;
            set;
        }



        public event EventHandler RequestResetTimer;


        public void TriggerTimerReset()
        {
            if (RequestResetTimer != null)
            {
                RequestResetTimer(this, null);
            }
        }
    }
}
