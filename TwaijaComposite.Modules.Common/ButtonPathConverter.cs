using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace TwaijaComposite.Modules.Common
{
#if !SILVERLIGHT
    [ValueConversion(typeof(string),typeof(Geometry))]
#endif
    public class ButtonPathConverter:IValueConverter
    {
        const string TwitterBird = "m 3.5 23.039848 c -1.8483002 -0.757416 -1.7801857 -0.906176 0.8980167 -1.96124 2.7622055 -1.088156 2.8090689 -1.206698 1 -2.529522 C 4.3541075 17.785761 3.4840541 16.787448 3.4645646 16.330611 3.4450751 15.873775 2.8667424 14.4875 2.1793808 13.25 1.4211249 11.884864 1.3283111 11 1.9433775 11 2.5944929 11 2.5489715 10.104206 1.8161206 8.4957739 0.20475819 4.9592185 1.3474 4.4335521 5.3154349 6.8859325 9.9455607 9.7475076 12.626398 9.6246864 13.406822 6.5152296 c 0.70714 -2.8174706 4.392671 -4.2079957 7.250037 -2.7353883 1.013728 0.522447 2.369562 0.7602373 3.012965 0.528423 1.218815 -0.4391315 1.23861 2.1166355 0.02579 3.329458 C 23.313025 8.0203084 23 9.5417859 23 11.018783 23 19.228673 11.499848 26.318112 3.5 23.039848 z";
        const string Twitter_T = "F1 M 7.16692,9.03937C 6.69601,9.20844 5.98749,9.29303 5.04136,9.29303C 2.79646,9.29303 1.674,8.50293 1.674,6.92273L 1.674,3.71722L 0,3.71722L 0,2.12537L 1.674,2.12537L 1.674,0.614441L 4.84783,0L 4.84783,2.12537L 7.16692,2.12537L 7.16692,3.71722L 4.84783,3.71722L 4.84783,6.54663C 4.84783,7.27551 5.27467,7.63995 6.12833,7.63995C 6.46378,7.63995 6.80997,7.57361 7.16692,7.44098L 7.16692,9.03937 Z ";
        const string Facebook_F = "m 1.5714286 24.428571 c -2.2679508 -2.26795 -2.2679508 -20.5891916 0 -22.8571424 2.2679508 -2.26795082 20.5891924 -2.26795082 22.8571424 0 2.267951 2.2679508 2.267951 20.5891924 0 22.8571424 -2.26795 2.267951 -20.5891916 2.267951 -22.8571424 0 z M 18 18.5 C 18 15.166667 18.388889 14 19.5 14 20.325 14 21 13.325 21 12.5 21 11.675 20.325 11 19.5 11 18.675 11 18 10.325 18 9.5 18 8.675 18.675 8 19.5 8 20.325 8 21 7.325 21 6.5 21 4.6855075 16.925255 4.4747448 15.2 6.2 14.54 6.86 14 8.21 14 9.2 c 0 0.99 -0.45 1.8 -1 1.8 -0.55 0 -1 0.675 -1 1.5 0 0.825 0.45 1.5 1 1.5 0.55 0 1 2.025 1 4.5 0 3.833333 0.296296 4.5 2 4.5 1.703704 0 2 -0.666667 2 -4.5 z";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object convertedValue = null;
            if (value != null)
            {
                var result = value.ToString();
                
#if !SILVERLIGHT
                switch (result)
                {
                    case "TwitterBird":
                        convertedValue = Geometry.Parse(TwitterBird);
                        break;
                    case "TwitterT":
                        convertedValue = Geometry.Parse(Twitter_T);
                        break;
                    case "FacebookF":
                        convertedValue = Geometry.Parse(Facebook_F);
                        break;
                }
#endif
            }
            return convertedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
