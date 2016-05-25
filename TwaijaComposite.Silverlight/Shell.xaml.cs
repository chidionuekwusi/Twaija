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

namespace TwaijaComposite
{
    public partial class Shell : UserControl
    {
        public Shell()
        {
            InitializeComponent();
        }
        public void HandleRequestClose(object sender, EventArgs args)
        {
            Application.Current.MainWindow.Close();

        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Application.Current.MainWindow.DragMove();
            }
            catch
            {
            }
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            AdjustWindowState();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                switch (element.Tag as string)
                {
                    case "RightCorner":
                        Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomRight);
                        break;
                    case "LeftCorner":
                        Application.Current.MainWindow.DragResize(WindowResizeEdge.BottomLeft);
                        break;
                    case "Left":
                        Application.Current.MainWindow.DragResize(WindowResizeEdge.Left);
                        break;
                    case "Right":
                        Application.Current.MainWindow.DragResize(WindowResizeEdge.Right);
                        break;
                    case "Bottom":
                        Application.Current.MainWindow.DragResize(WindowResizeEdge.Bottom);
                        break;
                }
            }
        }

        void AdjustWindowState()
        {
            switch (Application.Current.MainWindow.WindowState)
            {
                case WindowState.Maximized:
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    break;
            }
        }
        private void Border_DoubleTap(object sender, GestureEventArgs e)
        {
            AdjustWindowState();
        }
    }
}
                                      