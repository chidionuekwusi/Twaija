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

namespace TwaijaComposite.Modules.Authentication
{
    public partial class AuthenticationView : UserControl
    {
        private Storyboard OnSocialNetworkChanged;
        private Storyboard loaded;
        [Dependency]
        public Authentication.ViewModels.AuthenticationViewModel context
        {
            set
            {
                DataContext = value;
            }
        }
        
        public AuthenticationView()
        {
            InitializeComponent();
            loaded = this.Resources["SlideAnimations"] as Storyboard;
            AvailableAuthCommands.SelectionChanged += new SelectionChangedEventHandler(AvailableAuthCommands_SelectionChanged);
            OnSocialNetworkChanged = this.Resources["OnSelectedSocialNetworkChanged"] as Storyboard;
            Loaded += new RoutedEventHandler(AuthenticationView_Loaded);
        }

        void AuthenticationView_Loaded(object sender, RoutedEventArgs e)
        {
            loaded.Begin();
        }

        void AvailableAuthCommands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OnSocialNetworkChanged != null)
            {
                OnSocialNetworkChanged.Begin();
            }
        }
    }
}
