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
using TwaijaComposite.Modules.Clipboard.Viewmodels;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Windows.Threading;

namespace TwaijaComposite.Modules.Clipboard
{
    /// <summary>
    /// Interaction logic for ClipboardView.xaml
    /// </summary>
    public partial class ClipboardView : UserControl
    {
       
        DispatcherTimer timer;
        [Dependency]
        public IClipboardViewmodel model
        {
            set
            {
                DataContext = value;
            }
        }
        public ClipboardView()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick+=timer_Tick;
            deliverystatus.TextChanged += deliverystatus_TextChanged;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            deliverystatus.Text = string.Empty;
        }

        void RestartTimer()
        {
            timer.Stop();
            timer.Start();
        }
        void deliverystatus_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = sender as TextBox;
            if (text.Text.Contains("message"))
            {
                RestartTimer();
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
         
            counter.Text = Convert.ToString(140-messageboard.Text.Length);
        }

        private void messageboard_TextChanged(object sender, TextChangedEventArgs e)
        { 
            if (messageboard.Text.Equals("What's happening ?"))
            {
                counter.Text = Convert.ToString(140);
                return;
            }
            counter.Text = Convert.ToString(140 - messageboard.Text.Length);
           var rect= messageboard.GetRectFromCharacterIndex(messageboard.CaretIndex);
        }
    }
}
