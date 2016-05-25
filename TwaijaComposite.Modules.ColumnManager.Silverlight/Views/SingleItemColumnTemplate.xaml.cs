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
using System.Collections;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    public class SingleItemContentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var content = value as IEnumerable;
            if (content != null)
            {
               var enumerator =content.GetEnumerator();
               if(enumerator.MoveNext())
               return enumerator.Current;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public partial class SingleItemColumnTemplate : UserControl
    {
        public SingleItemColumnTemplate()
        {
            InitializeComponent();
        }
    }
}
