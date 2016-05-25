using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace TwaijaComposite.Modules.ColumnsManager
{

    public class ColumnHeaderColorConverter : IValueConverter
    {
        static Random gen;
        static List<Color> Brushes;
        static List<Color> mcolors = new List<Color>();
        static ColumnHeaderColorConverter()
        {

#if !SILVERLIGHT
            mcolors.Add(Colors.Aqua);
            mcolors.Add(Colors.Black);
            mcolors.Add(Colors.DeepPink);
            mcolors.Add(Colors.DodgerBlue);
            mcolors.Add(Colors.LightBlue);
            mcolors.Add(Colors.LawnGreen);
            mcolors.Add(Colors.LightSeaGreen);
            mcolors.Add(Colors.Lime);
            mcolors.Add(Colors.LimeGreen);
            mcolors.Add(Colors.MidnightBlue);
            mcolors.Add(Colors.OrangeRed);
            mcolors.Add(Colors.Red);
            mcolors.Add(Colors.RoyalBlue);
            mcolors.Add(Colors.SeaGreen);
            mcolors.Add(Colors.Blue);
#else
              mcolors.Add(Colors.Black);
              mcolors.Add(Colors.Blue);
              mcolors.Add(Colors.Red);
             mcolors.Add(Colors.LightGray);
#endif

            Brushes = new List<Color>();
           gen = new Random();
           var colors = typeof(Colors).GetProperties();
           for (int x = 0; x < mcolors.Count; x++)
           {

               var color = mcolors[x];
               color.A = 100;
               Brushes.Add(color);
           }
           //for (int x = 0; x < colors.Length; x++)
           //{

           //    var color = ((Color)(colors[x].GetValue(colors[x], null)));
           //    color.A = 90;
           //    Brushes.Add(color);
           //}

           //Brushes.Add(Colors.Red);
           //Brushes.Add(Colors.Purple);
#if !SILVERLIGHT
           //var color = Colors.OrangeRed;
           //var color1 = Colors.GreenYellow;
           //var color2 = Colors.Navy;
           //var color3 = Colors.MidnightBlue;
           //color.A = 90;
           //color1.A = 90;
           //color2.A = 90;
           //color3.A = 90;
           //Brushes.Add(color);
           //Brushes.Add(color1);
           //Brushes.Add(color2);
           //Brushes.Add(color3);
           //Brushes.Add(Colors.LightBlue);
          // Brushes.Add(Colors.LightYellow);
          // Brushes.Add(Colors.YellowGreen);
          //// Brushes.Add(Colors.Violet);
          // Brushes.Add(Colors.Navy);
           //Brushes.Add(Colors.Aqua);
#endif
            //for (int y=0;y<Brushes.Count;y++)
           //{
           //    Brushes[y].A = 90;
           //}
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush(Brushes[gen.Next(Brushes.Count)]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
