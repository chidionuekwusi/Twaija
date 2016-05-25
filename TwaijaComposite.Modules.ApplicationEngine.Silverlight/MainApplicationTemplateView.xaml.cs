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
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ApplicationEngine
{
    public partial class MainApplicationTemplateView : UserControl
    {
        [Dependency]
        public ApplicationEngine.ViewModels.ApplicationEngineViewModel context
        {
            set
            {
                DataContext = value;
            }
        }
        public MainApplicationTemplateView()
        {
            InitializeComponent();
        }
    }
}
