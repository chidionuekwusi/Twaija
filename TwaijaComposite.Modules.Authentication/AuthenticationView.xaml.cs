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
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.Authentication
{
    /// <summary>
    /// Interaction logic for AuthenticationView.xaml
    /// </summary>
    public partial class AuthenticationView : UserControl
    {

        public AuthenticationView(Authentication.ViewModels.AuthenticationViewModel context)
        {
            InitializeComponent();
            DataContext =context;
        }

    

    }
}
