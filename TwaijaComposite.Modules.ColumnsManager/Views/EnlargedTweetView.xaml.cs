using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TwaijaComposite.Modules.ColumnsManager.Viewmodels;

namespace TwaijaComposite.Modules.ColumnsManager.Views
{
    /// <summary>
    /// Interaction logic for EnlargedTweetView.xaml
    /// </summary>
    public partial class EnlargedTweetView : UserControl
    {
        Popup popup;
        public EnlargedTweetView()
        {
            InitializeComponent();
            DataContextChanged += TweetViewmodelTemplate_DataContextChanged;
        }
        void TweetViewmodelTemplate_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                TweetViewmodel model = e.NewValue as TweetViewmodel;
                if (model != null)
                {
                    if (model.Tweet != null)
                    {
                        if (model.Tweet.EmbeddedPictureThumbnailLocations != null)
                        {
                            if (model.Tweet.EmbeddedPictureThumbnailLocations.Count > 0)
                            {

                                try
                                {
                                    Popup pop = new Popup();
                                    pop.StaysOpen = false;
                                    pop.Placement = PlacementMode.Center;
                                    pop.AllowsTransparency = true;
                                    pop.PopupAnimation = PopupAnimation.Fade;
                                    pop.IsOpen = false;
                                    popup = pop;
                                    Border border1 = new Border() { Background = new SolidColorBrush(Color.FromArgb(255, 25, 25, 25)), BorderBrush = new SolidColorBrush(Colors.WhiteSmoke), BorderThickness = new Thickness(3) };
                                    Image popupimage = new Image();
                                    popupimage.Source = new BitmapImage(new Uri(model.Tweet.EmbeddedPictureThumbnailLocations.First().Value));
                                    popupimage.BeginInit();
                                    //popupimage.EndInit();
                                    popupimage.MaxHeight = 450;
                                    popupimage.MaxWidth = 450;
                                    popupimage.VerticalAlignment = VerticalAlignment.Stretch;
                                    popupimage.HorizontalAlignment = HorizontalAlignment.Stretch;
                                    StackPanel panel = new StackPanel() { Orientation = Orientation.Vertical };
                                    panel.Children.Add(popupimage);
                                    panel.Children.Add(new TweetNotification() { DataContext = DataContext, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch });
                                    border1.Child = panel;
                                    pop.Child = border1;
                                    //grid.Children.Add(pop);
                                    Image image = new Image();
                                    image.BeginInit();
                                    image.Source = new BitmapImage(new Uri(model.Tweet.EmbeddedPictureThumbnailLocations.First().Key));
                                    image.EndInit();
                                    image.MaxWidth = 285;
                                    image.MaxHeight = 285;
                                    image.Stretch = Stretch.Uniform;
                                    Button button = new Button();
                                    button.Click += button_Click;
                                    button.Template = Resources["clearbutton"] as ControlTemplate;
                                    Border border = new Border() { Background=new SolidColorBrush(Color.FromArgb(255,25,25,25)) };
                                    border.Child = image;
                                    button.Content = border;
                                    button.SetValue(Grid.ColumnSpanProperty, 2);
                                    button.SetValue(Grid.RowProperty, 2);
                                    tweetgrid.Children.Add(button);
                                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                                    button.VerticalAlignment = VerticalAlignment.Top;
                                    button.MaxHeight =285;
                                    button.MaxWidth = 285;
                                    //button.SetValue(Grid.RowProperty, 2);
                                    //button.SetValue(Grid.ColumnProperty, 1);
                                    //tweettext.Width = 200;
                                    //tweettext.FontSize = 10;
                                }
                                catch (Exception)
                                {


                                }
                            }
                        }
                    }

                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}
