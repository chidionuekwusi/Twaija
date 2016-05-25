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

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    public class ProfileStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                StringBuilder builder = new StringBuilder(parameter.ToString());
                builder.Append(value);
                return builder.ToString();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Interaction logic for TwitterProfile_LargeViewmodelTemplate.xaml
    /// </summary>
    public partial class TwitterProfile_LargeViewmodelTemplate : UserControl
    {
        public TwitterProfile_LargeViewmodelTemplate()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

    }
}
