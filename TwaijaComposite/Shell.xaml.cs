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

namespace TwaijaComposite
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private ShellViewmodel model;
        public Shell()
        {         
            InitializeComponent();
            DataContextChanged += Shell_DataContextChanged;
        }

        void Shell_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            model = e.NewValue as ShellViewmodel;
            if(model!=null)
            model.RequestClose += model_RequestClose;
        }

        void model_RequestClose(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            if (model !=null)
            {
                model.CloseCommand.Execute(null);
            }
            base.OnClosed(e);
        }
    }
}
