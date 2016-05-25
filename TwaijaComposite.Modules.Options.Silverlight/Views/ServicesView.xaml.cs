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
using TwaijaComposite.Modules.Options.Viewmodels;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.Options.Views
{
    public partial class ServicesView : UserControl
    {
        [Dependency]
        public IServicesViewmodel model
        {
            set { DataContext = value; }
        }
        public ServicesView()
        {
            InitializeComponent();
        }
    }
}
