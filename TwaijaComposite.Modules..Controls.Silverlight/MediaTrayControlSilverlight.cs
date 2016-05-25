using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Windows.Controls.Primitives;
using System.Text;

namespace TwaijaComposite.Modules.Controls
{
    [TemplatePart(Name = "PART_Button")]
    [TemplatePart(Name = "PART_Image")]
    [TemplatePart(Name = "PART_CancelButton")]
    [TemplatePart(Name = "PART_Popup")]
    [TemplateVisualState(GroupName="CommonStates",Name="Normal")]
    [TemplateVisualState(GroupName = "CommonStates", Name = "MouseOver")]
    [TemplateVisualState(GroupName="PopupStates",Name="PopupIsClosed")]
    [TemplateVisualState(GroupName = "PopupStates", Name = "PopupIsOpen")]
    public partial class MediaTray : ContentControl
    {

        private bool LazyLoad = false;
        private bool Initialized = false;
        public bool IsDialogInsertedPicture = false;
        private string CurrentFileLocation = "";
        private static string BufferLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Button trayButton;
        Button cancelButton;
        Image trayImage;
        Popup trayPopup;
        public Style FullTrayButtonStyle
        {
            get { return (Style)GetValue(FullTrayButtonStyleProperty); }
            set { SetValue(FullTrayButtonStyleProperty, value); }
        }


        public Uri DirectImageSource
        {
            get { return (Uri)GetValue(DirectImageSourceProperty); }
            set { SetValue(DirectImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectImageSourceProperty =
            DependencyProperty.Register("DirectImageSource", typeof(Uri), typeof(MediaTray), new PropertyMetadata(new PropertyChangedCallback(DirectSourcePropertyChanged)));

        
        // Using a DependencyProperty as the backing store for FullTrayButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FullTrayButtonStyleProperty =
            DependencyProperty.Register("FullTrayButtonStyle", typeof(Style), typeof(MediaTray), new PropertyMetadata(null));


        public Style EmptyTrayButtonStyle
        {
            get { return (Style)GetValue(EmptyTrayButtonStyleProperty); }
            set { SetValue(EmptyTrayButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EmptyTrayButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmptyTrayButtonStyleProperty =
            DependencyProperty.Register("EmptyTrayButtonStyle", typeof(Style), typeof(MediaTray), new PropertyMetadata(null));


        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(MediaTray), new PropertyMetadata(0.0));


        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(MediaTray), new PropertyMetadata(0.0));


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
                EmptyTrayButtonStyle = this.Resources["DefaultEmptyStyle"] as Style;
            }
            if (FullTrayButtonStyle == null)
            {
                FullTrayButtonStyle = this.Resources["DefaultFullStyle"] as Style;
            }
            trayImage.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(trayImage_ImageFailed);
            trayImage.ImageOpened += new EventHandler<RoutedEventArgs>(trayImage_ImageOpened);
            trayButton.Style = EmptyTrayButtonStyle;
            trayButton.Click += new RoutedEventHandler(trayButton_Click);
            cancelButton.Click += new RoutedEventHandler(cancelButton_Click);
            Binding binding = new Binding();
            binding.Source = this;
            binding.Path = new PropertyPath(MediaTray.ImageWidthProperty);
            trayImage.SetBinding(FrameworkElement.WidthProperty, binding);
            Binding binding0 = new Binding();
            binding0.Source = this;
            binding0.Path = new PropertyPath(MediaTray.ImageHeightProperty);
            trayImage.SetBinding(FrameworkElement.HeightProperty, binding0);
            Initialized = true;
        }

        void trayImage_ImageOpened(object sender, RoutedEventArgs e)
        {
         
        }

        void trayImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            
        }

        void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Cancel();
        }
        void Cancel()
        {

            trayButton.Style = this.EmptyTrayButtonStyle;
            this.trayImage.Source = null;
            VisualStateManager.GoToState(this, "PopupIsClosed", true);
            trayPopup.IsOpen = false;
            ImageSource = null;
            DirectImageSource = null;
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
            if (diag.File != null)
                SetupPic(new Uri(diag.File.FullName));
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
        BitmapImage CreateImage(string location)
        {
            BitmapImage image = null;
            using (var file = File.Open(location, FileMode.Open))
            {
                image = new BitmapImage();
                image.SetSource(file);
            }
            return image;
        }
        ~MediaTray()
        {
            DeleteCurrentFile();
        }
        public void SetupPic(Uri location)
        {
            if (File.Exists(location.OriginalString))
            {
                Cancel();
                var loc = MediaTray.BufferLocation + System.IO.Path.GetFileName(location.OriginalString);
                CacheImage(location.OriginalString, loc);
                var image = CreateImage(location.OriginalString); 
                trayImage.Source = image;
                CurrentFileLocation = loc;
                this.ImageSource = new Uri(location.OriginalString, UriKind.RelativeOrAbsolute);
                trayButton.Style = FullTrayButtonStyle;
                trayPopup.IsOpen = true;
                VisualStateManager.GoToState(this, "PopupIsOpen", true);
            }
        }
        static void ImageSourcePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
   

        }
       static void DirectSourcePropertyChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue != null)
            {
                var o = owner as MediaTray;
                if (o != null)
                {
                    o.SetupPic(args.NewValue as Uri);
                    return;
                }
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
        public int rand = 0;
        public MediaTray()
        {
            rand = new Random().Next(40000);
            this.MouseEnter += new MouseEventHandler(MediaTray_MouseEnter);
            this.MouseMove += new MouseEventHandler(MediaTray_MouseMove);
            this.MouseLeave += new MouseEventHandler(MediaTray_MouseLeave);
            VisualStateManager.GoToState(this, "Normal", false);
            VisualStateManager.GoToState(this, "PopupIsClosed", false);
        }

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
    }
}
