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
using System.Windows.Threading;

namespace TwaijaComposite.Modules.Controls
{
    [TemplateVisualState(GroupName = "CommonStates", Name = "Normal")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "MouseOver")]
    [TemplateVisualState(GroupName="CommonStates",Name="Pressed")]
    public partial class PopupCommands : UserControl
    {
        DispatcherTimer timer;
        bool canClosePopup = false;
        public PopupCommands()
        {
            InitializeComponent();
            Unloaded += new RoutedEventHandler(PopupCommands_Unloaded);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            listbox.MouseEnter += new MouseEventHandler(popup_MouseEnter);
            listbox.MouseLeave += new MouseEventHandler(popup_MouseLeave);
            this.MouseEnter += new MouseEventHandler(TweetViewmodelTemplate_MouseEnter);
            this.MouseLeave += new MouseEventHandler(TweetViewmodelTemplate_MouseLeave);
            VisualStateManager.GoToState(this, "Normal", false);
           
        }

        void PopupCommands_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.popup != null)
            {
                this.popup.IsOpen = false;
            }
        }

        void popup_MouseLeave(object sender, MouseEventArgs e)
        {
            canClosePopup = true;
        }

        void popup_MouseEnter(object sender, MouseEventArgs e)
        {
            canClosePopup = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
                if (canClosePopup)
                {
                    popup.IsOpen = false;
                    timer.Stop();
                }
            
        }
        void TweetViewmodelTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!popup.IsOpen)
            {
                VisualStateManager.GoToState(this, "Normal", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Pressed", true);
            }
        }

        void TweetViewmodelTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            popup.IsOpen =(!popup.IsOpen)? true:false;
            if (popup.IsOpen)
            {
                VisualStateManager.GoToState(this, "Pressed", true);
                canClosePopup = true;
                timer.Start();
            }
            else
            {
                VisualStateManager.GoToState(this, "MouseOver", true);
                canClosePopup = false;
                timer.Stop();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
            VisualStateManager.GoToState(this, "Normal", true);
            timer.Stop();
            canClosePopup = false;
        }
    }
}
