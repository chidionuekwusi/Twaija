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
using System.Windows.Media.Animation;

namespace TwaijaComposite.Modules.ProfileViewer.Views
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl,DeactivationInteractionTrigger,IDisposable
    {
        Storyboard Deactivationboard;
        Storyboard Activationboard;
       
        public ProfileView()
        {
           
            InitializeComponent();
			this.Loaded+=new System.Windows.RoutedEventHandler(ProfileView_Loaded);
        }
        public override void OnApplyTemplate()
        {
            try
            {
                Deactivationboard = (FindResource("DeactivationStoryboard") as Storyboard);
                Deactivationboard.Completed += new EventHandler(board_Completed);

                Activationboard = this.FindResource("Storyboard1") as Storyboard;
                Activationboard.Completed += new System.EventHandler(Loadedboard_Completed);
            }
            catch
            {
            }
            base.OnApplyTemplate();
        }
       volatile bool keepLooping = false;
        public void Deactivate()
        {
            Deactivationboard.Begin();
            keepLooping = true;
     
        }

        void board_Completed(object sender, EventArgs e)
        {
            keepLooping = false;
            if (Deactivated != null)
            {
                Deactivated(this, null);
            }
        }
     void Loadedboard_Completed(object sender, EventArgs e)
        {

        }

        public event EventHandler Deactivated;

        private void ProfileView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
           // Activationboard.Begin();
			
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
