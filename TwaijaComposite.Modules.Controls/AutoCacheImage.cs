using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TwaijaComposite.Modules.Controls
{

    public class AutoCacheImage:Image
    {
        private static object asynclock;
            static AutoCacheImage()
            {
                asynclock = new object();
            }
        public struct LocationAndCount
        {
            public string location;
            public int count;
        }
        private static Dictionary<string, LocationAndCount> UserImageCount = new Dictionary<string, LocationAndCount>();
        static string DefaultCacheLocation=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"/Twaija/imgcache/";
        public Uri ImageLocation
        {
            get { return (Uri)GetValue(ImageLocationProperty); }
            set { SetValue(ImageLocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageLocation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageLocationProperty =
            DependencyProperty.Register("ImageLocation", typeof(Uri), typeof(AutoCacheImage), new PropertyMetadata(null,new PropertyChangedCallback(ImageLocationChanged)));

private static void ImageLocationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
{
    if (d != null)
    {
       //check if its a new image location
        //if its a new image location copy the image to a different location then set the Source to that location on the machine
        if (e.NewValue != null)
        {
            var location = e.NewValue as Uri;
            bool cancache = false;
            if (UserImageCount.Keys.Contains(location.OriginalString))
            {
                if (UserImageCount[location.OriginalString].count> 20)
                {
                    cancache = true;
                    var entry = UserImageCount[location.OriginalString];
                    UserImageCount[location.OriginalString] = new LocationAndCount() { location = entry.location, count = 0 };
                    return;

                }
               var entry0= UserImageCount[location.OriginalString];
               UserImageCount[location.OriginalString] = new LocationAndCount() { location = entry0.location, count = entry0.count++ };
                Application.Current.Dispatcher.Invoke(new Func<AutoCacheImage,string,bool>((i,o)=>
                {
                    try
                    {
                        i.Source = new BitmapImage(new Uri(o));
                    }
                    catch
                    {
                    }
                    return true;
                }
                    ), System.Windows.Threading.DispatcherPriority.ApplicationIdle,d as AutoCacheImage, entry0.location);

            }
            else
            {
                cancache = true;
    
            }
            if (cancache)
            {
                copyToImageCache(d as AutoCacheImage,location);
            }
        }
    }
}
        
private static void copyToImageCache(AutoCacheImage sender,Uri location)
{
    ThreadPool.QueueUserWorkItem(new WaitCallback((d) =>
    {
        lock (asynclock)
        {
            bool failed = true;
            var simplename = Path.GetFileName(location.AbsolutePath);
            var fileextension = Path.GetExtension(simplename);
            var newlocation = DefaultCacheLocation + simplename;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(location.OriginalString);
            request.Method = "GET";
            byte[] data;
            try
            {
                if (!Directory.Exists(DefaultCacheLocation))
                {
                    Directory.CreateDirectory(DefaultCacheLocation);
                }
                using (var response = request.GetResponse())
                {
                    data = new byte[response.ContentLength];
                    using (var responsestream = response.GetResponseStream())
                    {
                        //Clipboard.GetImage();
//                        var encoder = new PngBitmapEncoder();
//encoder.Frames.Add(BitmapFrame.Create(image));
//using (var stream = dialog.OpenFile())
//    encoder.Save(stream);
                        //BitmapEncoder e = new PngBitmapEncoder();
                        //e.Frames.Add(BitmapFrame.Create(responsestream));
                        //e.Save(
                        //BitmapEncoder encoder = null;
                        //switch (fileextension.ToLower())
                        //{
                        //    case ".png":
                        //        encoder = new PngBitmapEncoder();
                        //        break;
                        //    case ".jpeg":
                        //        encoder = new JpegBitmapEncoder();
                        //        break;
                        //    case ".gif":
                        //        encoder=new GifBitmapEncoder();
                        //        break;
                              
                        //}
                        //var frame = BitmapFrame.Create(new Uri(location.OriginalString,UriKind.Absolute), BitmapCreateOptions.None, BitmapCacheOption.Default);
                        //frame.DownloadCompleted += frame_DownloadCompleted;
                        //frame.DownloadFailed += frame_DownloadFailed;
                        //throw new ArgumentNullException();
                      //  encoder.Frames.Add(frame);

                        BinaryReader reader = new BinaryReader(responsestream);
                        reader.Read(data, 0, data.Length);
                        using (var filestream = new FileStream(newlocation,FileMode.Create,FileAccess.Write))
                        {

                        //    encoder.Save(filestream);
                        BinaryWriter writer = new BinaryWriter(filestream);
                        writer.Write(data, 0, data.Length);
                        }
                    }
                }
                failed = false;
            }
            catch
            {

            }
            finally
            {
                Uri currentlocation = null;
                currentlocation = (!failed) ? new Uri(newlocation) : location;
                if(!UserImageCount.Keys.Contains(location.OriginalString))
                UserImageCount.Add(location.OriginalString, new LocationAndCount() { count = 0, location = currentlocation.OriginalString });
                Application.Current.Dispatcher.Invoke(new Func<AutoCacheImage, string, bool>(
                    (i, o) =>
                    {
                        try
                        {
                            i.Source = new BitmapImage(new Uri(o));
                        }
                        catch
                        {

                        }
                        return true;
                    }),System.Windows.Threading.DispatcherPriority.ApplicationIdle,sender,currentlocation.OriginalString);
            }
        }
    }));
}

static void frame_DownloadFailed(object sender, System.Windows.Media.ExceptionEventArgs e)
{
  
}

static void frame_DownloadCompleted(object sender, EventArgs e)
{
    
}

         
       public AutoCacheImage()
        {
           
        }
    }
}
