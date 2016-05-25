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
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    /// <summary>
    /// Interaction logic for ToolBarView.xaml
    /// </summary>
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
