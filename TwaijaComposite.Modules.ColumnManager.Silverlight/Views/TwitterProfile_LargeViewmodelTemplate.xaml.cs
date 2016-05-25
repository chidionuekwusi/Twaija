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
using System.Windows.Data;
using System.Text;

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
    public partial class TwitterProfile_LargeViewmodelTemplate : UserControl
    {
        public TwitterProfile_LargeViewmodelTemplate()
        {
            InitializeComponent();
        }
    }
}
