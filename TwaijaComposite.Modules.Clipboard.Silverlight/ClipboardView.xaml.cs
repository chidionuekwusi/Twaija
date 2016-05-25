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
using TwaijaComposite.Modules.Clipboard.Viewmodels;

namespace TwaijaComposite.Modules.Clipboard
{
    public partial class ClipboardView : UserControl
    {
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
        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            counter.Text = Convert.ToString(140 - messageboard.Text.Length);
        }

        private void messageboard_TextChanged(object sender, TextChangedEventArgs e)
        {
            counter.Text = Convert.ToString(140 - messageboard.Text.Length);
        }
    }
}
