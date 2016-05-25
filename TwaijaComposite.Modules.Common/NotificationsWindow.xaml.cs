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
using System.Windows.Shapes;
using System.Windows.Threading;
using TwaijaComposite.Modules.ColumnsManager.Notifications;
using TwaijaComposite.Modules.Common.DialogTypes;

namespace TwaijaComposite
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationsWindow : Window
    {
        DispatcherTimer timer;
        bool CanExit = false;
        public NotificationsWindow()
        {
            //this.MouseDown += border_MouseDown;
            //this.AllowsTransparency = true;
            //this.WindowStyle = WindowStyle.None;
            this.SizeToContent = SizeToContent.WidthAndHeight;      
            Loaded += new RoutedEventHandler(NotificationsWindow_Loaded);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Tick;
            this.MouseEnter += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseEnter);
            this.MouseLeave += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseLeave);
            ContentRendered += NotificationsWindow_ContentRendered;
        }
        bool NotificationsViewRendered = false;
        void NotificationsWindow_ContentRendered(object sender, EventArgs e)
        {
            if (this.Content is INotificationsView)
            {
                if (!NotificationsViewRendered)
                {
                    (this.Content as INotificationsView).RequestResetTimer += NotificationsWindow_RequestResetTimer;
                    NotificationsViewRendered = true;
                }
            }
        }

        void NotificationsWindow_RequestResetTimer(object sender, EventArgs e)
        {
            CanExit = false;
            timer.Stop();
            timer.Start();
        }

        void NotificationsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CanExit)
            {
                timer.Start();
            }
            CanExit = true;
        }
        void NotificationsWindow_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = true;
        }

        void NotificationsWindow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = false;
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
        }

        public void Tick(object sender, EventArgs args)
        {
            if (CanExit)
            {
                timer.Tick -= Tick;
                timer.Stop();
                var content = (Content as INotificationsView);
                if (content != null)
                {
                    content.RequestResetTimer -= NotificationsWindow_RequestResetTimer;
                }
                this.Close();
                
            }
        }
        void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }

    }
}
