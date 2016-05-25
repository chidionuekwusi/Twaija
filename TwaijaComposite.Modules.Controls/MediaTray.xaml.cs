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
using System.Windows.Controls.Primitives;
using System.IO;

namespace TwaijaComposite.Modules.Controls
{
    /// <summary>
    /// Interaction logic for MediaTray.xaml
    /// </summary>
    [TemplatePart(Name="PART_Button")]
    [TemplatePart(Name = "PART_Image")]
    [TemplatePart(Name = "PART_CancelButton")]
    [TemplatePart(Name = "PART_Popup")]
    public partial class MediaTray : UserControl
    {
        private bool LazyLoad = false;
        private bool Initialized = false;
        public bool IsDialogInsertedPicture = false;
        private string CurrentFileLocation = "";
        private static string BufferLocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+"xbuffer";
       // private Style fullTrayButtonStyle;
       // private Style emptyTrayButtonStyle;
        Button trayButton;
        Button cancelButton;
        Image trayImage;
        Popup trayPopup;
        public Style FullTrayButtonStyle
        {
            get { return (Style)GetValue(FullTrayButtonStyleProperty); }
            set { SetValue(FullTrayButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FullTrayButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullTrayButtonStyleProperty =
            DependencyProperty.Register("FullTrayButtonStyle", typeof(Style), typeof(MediaTray), new UIPropertyMetadata(null));

        
        public Style EmptyTrayButtonStyle
        {
            get { return (Style)GetValue(EmptyTrayButtonStyleProperty); }
            set { SetValue(EmptyTrayButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmptyTrayButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmptyTrayButtonStyleProperty =
            DependencyProperty.Register("EmptyTrayButtonStyle", typeof(Style), typeof(MediaTray), new UIPropertyMetadata(null));

        
        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(MediaTray), new UIPropertyMetadata(0.0));

        
        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(MediaTray), new UIPropertyMetadata(0.0));

        
        public Uri ImageSource
        {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Uri), typeof(MediaTray), new PropertyMetadata(ImageSourcePropertyChanged));

        public override void OnApplyTemplate()
        {
            try
            {
                trayButton = this.GetTemplateChild("PART_Button") as Button;
                trayPopup = this.GetTemplateChild("PART_Popup") as Popup;
                trayImage = this.GetTemplateChild("PART_Image") as Image;
                cancelButton = this.GetTemplateChild("PART_CancelButton") as Button;
                Initialize();
            }
            catch
            {
            }
           base.OnApplyTemplate();
        }
        protected void Initialize()
        {
            if (EmptyTrayButtonStyle == null)
            {
                EmptyTrayButtonStyle = this.FindResource("DefaultEmptyStyle") as Style;
            }
            if (FullTrayButtonStyle == null)
            {
                FullTrayButtonStyle = this.FindResource("DefaultFullStyle") as Style;
            }
            trayButton.Style = EmptyTrayButtonStyle;
            trayButton.Click += new RoutedEventHandler(trayButton_Click);
            cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath(MediaTray.ImageHeightProperty);
            trayImage.SetBinding(FrameworkElement.WidthProperty, binding);
            Binding binding0 = new Binding();
            binding0.Source = this;
            binding0.Path = new PropertyPath(MediaTray.ImageHeightProperty);
            trayImage.SetBinding(FrameworkElement.HeightProperty, binding0);
            trayButton.InvalidateVisual();
            Initialized = true;
            if (LazyLoad)
            {
                SetupPic(ImageSource);
            }
        }

        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }
        void Cancel()
        {
 
            trayButton.Style = this.EmptyTrayButtonStyle;
            this.trayImage.Source = null;
            trayButton.InvalidateVisual();
            trayImage.InvalidateVisual();
            trayPopup.IsOpen = false;
            ImageSource = null;
            DeleteCurrentFile();
        }
        void trayButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog diag = new Microsoft.Win32.OpenFileDialog();
            diag.Filter = "Image files (*.bmp, *.jpg, *.JPEG, *.png)|*.bmp;*.jpg;*.JPEG;*.png";
            diag.Multiselect = false;
            diag.FileOk += new System.ComponentModel.CancelEventHandler(diag_FileOk);
            diag.ShowDialog();
           
        }

        void diag_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog diag=sender as Microsoft.Win32.OpenFileDialog;
            if (File.Exists(diag.FileName))
            {
                trayButton.Style = this.FullTrayButtonStyle;
                var location = MediaTray.BufferLocation +System.IO.Path.GetFileName(diag.FileName);
                CacheImage(diag.FileName, location);
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(location);
                image.EndInit();
                trayImage.Source = image;
                IsDialogInsertedPicture = true;
                this.ImageSource = new Uri(diag.FileName);
                trayButton.InvalidateVisual();
                trayImage.InvalidateVisual();
                CurrentFileLocation = location;
                trayPopup.IsOpen = true;
                diag.FileOk -= diag_FileOk;
                IsDialogInsertedPicture = false;
            }
        }
        void CacheImage(string location,string copiedlocation)
        {
            try
            {
                File.Copy(location, copiedlocation);
            }
            catch
            {

            }
        }
        ~MediaTray()
        {
            DeleteCurrentFile();
        }
        public void SetupPic(Uri location)
        {
            if (!Initialized)
            {
                LazyLoad = true;
                return;
            }
            if (File.Exists(location.OriginalString))         
            {
                Cancel();
                var loc = MediaTray.BufferLocation + System.IO.Path.GetFileName(location.OriginalString);
                CacheImage(location.OriginalString, loc);
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(loc);
                image.EndInit();
                trayImage.Source = image;
                trayButton.InvalidateVisual();
                trayImage.InvalidateVisual();
                CurrentFileLocation = loc;
                trayButton.Style = FullTrayButtonStyle;
                trayPopup.IsOpen = true;
            }
        }
        static void ImageSourcePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue == null)
            {
                var o = owner as MediaTray;
                if (o != null)
                {
                    o.Cancel();
                    return;
                }
            }
            var main = owner as MediaTray;
            if (!main.IsDialogInsertedPicture)
            {
                main.SetupPic(args.NewValue as Uri);
            }
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
        public MediaTray()
        {
            InitializeComponent();
        }
    }
}
