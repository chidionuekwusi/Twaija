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
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    public partial class ToolBarView : UserControl
    {
        [Dependency]
        public IToolbarViewmodel model
        {
            set
            {
                DataContext = value;
            }
        }
        public ToolBarView()
        {
            InitializeComponent();
        }
    }
}
