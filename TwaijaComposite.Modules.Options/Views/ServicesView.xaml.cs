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
    /// Interaction logic for ServicesView.xaml
    /// </summary>
    public partial class ServicesView : UserControl,IHeader
    {
        [Dependency]
        public IServicesViewmodel model
        {
            set
            {
                DataContext = value;
            }
        }
        public ServicesView()
        {
            InitializeComponent();
        }

        public string Header
        {
            get { return "services"; }
        }
    }
}
