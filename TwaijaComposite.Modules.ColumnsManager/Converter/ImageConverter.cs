using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TwaijaComposite.Modules.ColumnsManager
{
    public class ImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is Uri)
                {
                    if (value != null)
                    {
                        var uri = value as Uri;
#if SILVERLIGHT
                        var image = new BitmapImage(uri);
#else
                        var image = new BitmapImage() { };
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                        image.UriSource = uri;
                        image.EndInit();
#endif
                   
                        return image;
                    }
                }
                if (value is string)
                {
                    var source = (string)value;
                    var uri = new Uri(source, UriKind.RelativeOrAbsolute);
#if SILVERLIGHT
                    var image = new BitmapImage(uri);
#else
                        var image = new BitmapImage() { };
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                        image.UriSource = uri;
                        image.EndInit();
#endif
                    return image;
                }
            }
            catch
            {

            }
#if !SILVERLIGHT
            return new BitmapImage(new Uri("pack://application:,,,/TwaijaComposite.Modules.ColumnsManager;component/bird_48_blue.png"));
#else
            return null;
            //return new BitmapImage(new Uri("/TwaijaComposite.Modules.ColumnManager.Silverlight;component/bird_48_blue.png",UriKind.Relative));
#endif
        }
#if !SILVERLIGHT
        void image_DownloadCompleted(object sender, EventArgs e)
        {
            var image = sender as BitmapImage;
            image.Freeze();
        }
        void image_DecodeFailed(object sender, ExceptionEventArgs e)
        {
            (sender as BitmapImage).DecodeFailed -= image_DecodeFailed;
        }
#endif
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
