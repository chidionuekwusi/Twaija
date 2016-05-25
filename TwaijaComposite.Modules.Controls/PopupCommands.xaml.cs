using System;
using System.Collections.Generic;
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
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace TwaijaComposite.Modules.Controls
{
	/// <summary>
	/// Interaction logic for PopupCommands.xaml
	/// </summary>
    [TemplatePart(Type = typeof(Button), Name = "PART_popuptrigger")]
    [TemplatePart(Type=typeof(Popup),Name="PART_popup")]
	public partial class PopupCommands : UserControl
	{

        private Button templatebutton;
        private Popup templatepopup;
        public FrameworkElement Host
        {
            get { return (FrameworkElement)GetValue(HostProperty); }
            set { SetValue(HostProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Host.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HostProperty =
            DependencyProperty.Register("Host", typeof(FrameworkElement), typeof(PopupCommands), new PropertyMetadata(OnHostPropertyChanged));

     
		public PopupCommands()
		{
            this.Loaded += new RoutedEventHandler(PopupCommands_Loaded);
			this.InitializeComponent();
		}

   

        void PopupCommands_Loaded(object sender, RoutedEventArgs e)
        {
            if (Host != null)
            {
                Host.MouseEnter += host_MouseEnter;
                Host.MouseLeave += host_MouseLeave;
            }
        }
		 private void popuptrigger_Click(object sender, RoutedEventArgs e)
        {
            templatepopup.IsOpen = true;
        }
         public static void OnHostPropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
         {

         }

          void host_MouseLeave(object sender, MouseEventArgs e)
         {
             DoubleAnimation timeline = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.3)));
             templatebutton.BeginAnimation(OpacityProperty, timeline);
         }

         public override void OnApplyTemplate()
         {
             templatebutton = GetTemplateChild("PART_popuptrigger") as Button;
             templatepopup = GetTemplateChild("PART_popup") as Popup;
             templatebutton.Opacity = 0;
             base.OnApplyTemplate();
         }
         void host_MouseEnter(object sender, MouseEventArgs e)
         {
             DoubleAnimation timeline = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.3)));
             templatebutton.BeginAnimation(OpacityProperty, timeline);
         }

         private void Button_Click(object sender, RoutedEventArgs e)
         {
             templatepopup.IsOpen = false;
         }
	}
}