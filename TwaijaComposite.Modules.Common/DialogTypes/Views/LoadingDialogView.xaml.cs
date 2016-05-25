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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    /// <summary>
    /// Interaction logic for LoadingDialogView.xaml
    /// </summary>
    public partial class LoadingDialogView : UserControl
    {
        public LoadingDialogView()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoadingDialogView_Loaded);

        }

        void LoadingDialogView_Loaded(object sender, RoutedEventArgs e)
        {
            (Resources["Loadingboard"] as Storyboard).Begin();
            Loaded -= LoadingDialogView_Loaded;
        }
    }
}
