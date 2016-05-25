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
using System.Windows.Shapes;

namespace TwaijaComposite
{
    /// <summary>
    /// Interaction logic for DraggableWindow.xaml
    /// </summary>
    public partial class DraggableWindow : Window
    {
        public DraggableWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(DraggableWindow_Loaded);
        }

        void DraggableWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
        }

        public void overlay_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
