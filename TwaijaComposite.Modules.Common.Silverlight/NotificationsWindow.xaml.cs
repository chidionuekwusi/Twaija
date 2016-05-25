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
using System.Windows.Threading;

namespace TwaijaComposite.Modules.Common
{
    public partial class NotificationsWindow : ChildWindow
    {

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        DispatcherTimer timer;
        bool CanExit = true;
        public NotificationsWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(60000);
            timer.Tick += Tick;
            this.MouseEnter += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseEnter);
            this.MouseLeave += new System.Windows.Input.MouseEventHandler(NotificationsWindow_MouseLeave);
        }
        void NotificationsWindow_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = true;
        }

        void NotificationsWindow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CanExit = false;
        }
        protected override void OnOpened()
        {
            base.OnOpened();
            timer.Start();
        }
        public void Tick(object sender, EventArgs args)
        {
            if (CanExit)
            {
                timer.Tick -= Tick;
                timer.Stop();
                this.Close();
            }
        }
    }
}

