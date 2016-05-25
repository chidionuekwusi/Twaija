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
using Microsoft.Practices.Prism.Regions;
using System.Windows.Data;
using Microsoft.Practices.Unity;

namespace TwaijaComposite.Modules.Options.Views
{
    public class HeaderConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class Layout : UserControl
    {
        [Dependency]
        public LayoutViewmodel model
        {
            set { DataContext = value; }
        }
        public Layout()
        {
            InitializeComponent();
            TabControlRegionAdapter.SetItemContainerStyle(tabber, this.Resources["TabItemStyle"] as Style);
        }
    }
}
