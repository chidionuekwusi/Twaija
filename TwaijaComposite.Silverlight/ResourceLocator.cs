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

namespace TwaijaComposite
{
    public static class ResourceLocator
    {

        public static ResourceDictionary ApplicationBrushDictionary;
        static ResourceLocator()
         {
            try
            {
                ApplicationBrushDictionary = new ResourceDictionary();
                ApplicationBrushDictionary.Source = new Uri("/TwaijaComposite;component/Resources/Dictionaries/MainbrushesDictionary.xaml", UriKind.RelativeOrAbsolute);
            }
            catch
            {
            }
        }
    }
}
