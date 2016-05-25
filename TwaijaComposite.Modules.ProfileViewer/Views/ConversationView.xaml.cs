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

namespace TwaijaComposite.Modules.ProfileViewer.Views
{
    /// <summary>
    /// Interaction logic for ConversationView.xaml
    /// </summary>
    public partial class ConversationView : UserControl,IDisposable
    {
        public ConversationView()
        {
            InitializeComponent();
        }
        public void Dispose()
        {
            var disposable = DataContext as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
