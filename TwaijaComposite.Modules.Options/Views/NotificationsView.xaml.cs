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
using TwaijaComposite.Modules.Options.Viewmodels;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.Options.Views
{
    /// <summary>
    /// Interaction logic for NotificationsView.xaml
    /// </summary>
    public partial class NotificationsView : UserControl,IHeader
    {
        [Dependency]
        public INotificationsViewmodel model
        {
            set { DataContext = value; }
        }
        public NotificationsView()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return "notifications"; }
        }
    }
}
