using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace TwaijaComposite.Modules.Common
{

    public class RandomColorConverter : IValueConverter
    {
        static Random gen;
        static List<Color> Brushes;
        static List<Color> mcolors = new List<Color>();
        static RandomColorConverter()
        {

#if !SILVERLIGHT
           // mcolors.Add(Colors.Aqua);
           // mcolors.Add(Color.FromRgb(50,50,50));
            //mcolors.Add(Colors.DeepPink);
            mcolors.Add(Colors.DodgerBlue);
          //  mcolors.Add(Colors.LightBlue);
            //mcolors.Add(Colors.LawnGreen);
            //mcolors.Add(Colors.LightSeaGreen);
            //mcolors.Add(Colors.Lime);
          //  mcolors.Add(Colors.IndianRed);
          //  mcolors.Add(Colors.LimeGreen);
        //    mcolors.Add(Colors.MidnightBlue);
            mcolors.Add(Colors.OrangeRed);
            mcolors.Add(Colors.Red);
           // mcolors.Add(Colors.Pink);
           // mcolors.Add(Colors.SeaGreen);
           // mcolors.Add(Colors.Blue);
#else
              mcolors.Add(Colors.Black);
              mcolors.Add(Colors.Blue);
              mcolors.Add(Colors.Red);
             mcolors.Add(Colors.LightGray);
#endif


           Brushes = new List<Color>();
           gen = new Random();
           //var colors = typeof(Colors).GetProperties();
           for (int x = 0; x < mcolors.Count; x++)
           {

               var color=mcolors[x];
               //color.A = 240;
               Brushes.Add(color);
           }

        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var label = value.ToString().ToLower();
                if (label.Contains("timeline"))
                {
                    return new SolidColorBrush(Colors.DodgerBlue);
                }
                if (label.Contains("mentions"))
                {
                    return new SolidColorBrush(Colors.OrangeRed);
                }
                if (label.Contains("direct messages"))
                {
                    return new SolidColorBrush(Colors.Red);
                }
                if (label.Contains("find:"))
                {
                    return new SolidColorBrush(Color.FromArgb(255, 120, 120, 120));
                }
            }
            return new SolidColorBrush(Brushes[gen.Next(Brushes.Count)]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
