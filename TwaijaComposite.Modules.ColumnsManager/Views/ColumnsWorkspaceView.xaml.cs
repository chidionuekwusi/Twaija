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
using TwaijaComposite.Modules.Controls;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    public class AutoScrollIntoViewListBox : ListBox
    {
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    this.ScrollIntoView(e.NewItems[0]);
                    break;
            }
        }
    }
    /// <summary>
    /// Interaction logic for ColumnsWorkspaceView.xaml
    /// </summary>
    public partial class ColumnsWorkspaceView : UserControl
    {
        private IColumnsWorkspaceViewmodel model
        {
            set
            {
                DataContext = value;
            }
        }
        public ColumnsWorkspaceView(IColumnsWorkspaceViewmodel model)
        {
            InitializeComponent();
            this.model = model;
        }
    }
}
