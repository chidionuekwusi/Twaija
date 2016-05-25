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

namespace TwaijaComposite.Modules.Common.DialogTypes
{
    public partial class LoadingDialogView : UserControl
    {
        public LoadingDialogView()
        {
            InitializeComponent();
            Loaded+=new RoutedEventHandler(LoadingDialogView_Loaded);
           
        }

        void LoadingDialogView_Loaded(object sender, RoutedEventArgs e)
        {
            (Resources["Loadingboard"] as Storyboard).Begin();
            Loaded -= LoadingDialogView_Loaded;
        }
      
    }
}
