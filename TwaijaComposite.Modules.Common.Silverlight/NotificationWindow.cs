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
using TwaijaComposite.Modules.Common.Behaviors;

namespace TwaijaComposite.Modules.Common
{
    public class NotificationWindow : Window, INotificationWindow
    {

        public void Show(Position popupposition)
        {
            this.Show();
        }

        public event EventHandler Closed;

        public new object Content
        {
            get
            {
                return base.Content;
            }
            set
            {
                base.Content = value as FrameworkElement;
            }
        }

        public object Owner
        {
            get
            {
                return this.Owner;
            }
            set
            {
                this.Owner = value;
            }
        }

        public Style Style
        {
            get
            {
                return this.Style;
            }
            set
            {
                this.Style = value;
            }
        }
    }
}
