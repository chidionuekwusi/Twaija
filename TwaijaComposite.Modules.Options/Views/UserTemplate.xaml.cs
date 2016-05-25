using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TwaijaComposite.Modules.Options.Views
{
    public class ImageConverter : IValueConverter
    {
        BitmapImage CreateImage(string location)
        {
            BitmapImage image = null;
            image = new BitmapImage(new Uri(location));
            
            //using (var file = File.Open(location, FileMode.Open))
            //{
            //    image = new BitmapImage();
            //    image.BeginInit();
            //    image.StreamSource=file;
            //    image.EndInit();
            //}
            return image;
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var location = value as string;
            if (location != null)
                return CreateImage(location);

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    [TemplatePart(Name = "PART_Button")]
    [TemplatePart(Name = "PART_CancelButton")]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public class UserMediaTray : ContentControl
    {

        private string CurrentFileLocation = "";
        private static string BufferLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Button trayButton;
        Button cancelButton;
        TextBox textbox;

        public string ImageLocationString
        {
            get { return (string)GetValue(ImageLocationStringProperty); }
            set { SetValue(ImageLocationStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageLocationString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageLocationStringProperty =
            DependencyProperty.Register("ImageLocationString", typeof(string), typeof(UserMediaTray), new PropertyMetadata(null));



        public Uri ImageSource
        {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Uri), typeof(UserMediaTray), new PropertyMetadata(ImageSourcePropertyChanged));

        public override void OnApplyTemplate()
        {
            try
            {
                trayButton = this.GetTemplateChild("PART_Button") as Button;
                textbox = this.GetTemplateChild("PART_TextBox") as TextBox;
                cancelButton = this.GetTemplateChild("PART_CancelButton") as Button;
                Initialize();
                VisualStateManager.GoToState(this, "Normal", false);
            }
            catch
            {
            }
            base.OnApplyTemplate();
        }
        protected void Initialize()
        {
            trayButton.Click += new RoutedEventHandler(trayButton_Click);
            cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
        }
        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }
        void Cancel()
        {
            ImageSource = null;
            ImageLocationString = null;
            DeleteCurrentFile();
        }
        void trayButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "Image files (*.bmp, *.jpg, *.JPEG, *.png)|*.bmp;*.jpg;*.JPEG;*.png";
            diag.Multiselect = false;
            diag.ShowDialog();
            diag_FileOk(diag, null);

        }

        void diag_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog diag = sender as OpenFileDialog;
            if (!string.IsNullOrEmpty(diag.FileName))
            {
                SetupPic(new Uri(diag.FileName));
            }
        }
        void CacheImage(string location, string copiedlocation)
        {
            try
            {
                File.Copy(location, copiedlocation);
            }
            catch
            {

            }
        }

        ~UserMediaTray()
        {
            DeleteCurrentFile();
        }
        public void SetupPic(Uri location)
        {
            if (File.Exists(location.OriginalString))
            {
                //Cancel();
                var loc = UserMediaTray.BufferLocation + System.IO.Path.GetFileName(location.OriginalString);
                CacheImage(location.OriginalString, loc);
                CurrentFileLocation = loc;
                this.ImageSource = new Uri(loc, UriKind.RelativeOrAbsolute);
                ImageLocationString = location.OriginalString;

            }
        }
        static void ImageSourcePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {


        }
        void DeleteCurrentFile()
        {
            try
            {
                File.Delete(CurrentFileLocation);
            }
            catch
            {
            }
        }
        public int rand = 0;

        void MediaTray_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        void MediaTray_MouseMove(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        void MediaTray_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }

        public UserMediaTray()
        {
            this.MouseEnter += new MouseEventHandler(MediaTray_MouseEnter);
            this.MouseMove += new MouseEventHandler(MediaTray_MouseMove);
            this.MouseLeave += new MouseEventHandler(MediaTray_MouseLeave);
        }
    }


    /// <summary>
    /// Interaction logic for UserTemplate.xaml
    /// </summary>
    public partial class UserTemplate : UserControl
    {
        public UserTemplate()
        {
            InitializeComponent();
        }
    }
}
