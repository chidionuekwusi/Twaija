using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace TwaijaComposite.Modules.Common
{
#if !SILVERLIGHT
    [ValueConversion(typeof(string),typeof(string))]
#endif
    public class UIStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                var param = parameter.ToString();
                var text = value.ToString();
                var result = text;
                switch (param)
                {
                    case UIStringConverterType.ColumnHeader:
                        if (text.Length > 35)
                        {
                            result = text.Substring(0, 30) + "...";
                        }
                        break;
                    case UIStringConverterType.ShortBio:

                        break;
                }

                return result;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class UIStringConverterType
    {
       public const string ColumnHeader="ColumnHeader";
       public const string ShortBio = "ShortBio";
    }
}
