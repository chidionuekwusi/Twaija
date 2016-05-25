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
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    public partial class ColumnsWorkspaceView : UserControl
    {
       [Dependency]
        public IColumnsWorkspaceViewmodel model
        {
            set
            {
                DataContext = value;
            }
        }
        public ColumnsWorkspaceView()
        {
            InitializeComponent();
        }
    }
}
