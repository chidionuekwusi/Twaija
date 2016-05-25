using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TwaijaComposite.Modules.Controls
{
    public static class Glyphs
    {
        public const string downarrow="F1 M 6,11.3333L 0.666672,0.666645L 11.3333,0.666645L 6,11.3333 Z"; 
        public const string leftarrow="F1 M 0.666672,5.99998L 11.3333,11.3333L 11.3333,0.666668L 0.666672,5.99998 Z"; 
        public const string rightarrow="F1 M 11.3333,5.99998L 0.666672,11.3333L 0.666672,0.666668L 11.3333,5.99998 Z";
        public const string uparrow="F1 M 6,0.666645L 0.666672,11.3333L 11.3333,11.3333L 6,0.666645 Z";


        public static string GetGlyph(DependencyObject obj)
        {
            return (string)obj.GetValue(GlyphProperty);
        }

        public static void SetGlyph(DependencyObject obj, string value)
        {
            obj.SetValue(GlyphProperty, value);
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.RegisterAttached("Glyph", typeof(string), typeof(Glyphs), new PropertyMetadata(string.Empty));

        
    }
    public class CustomListBox : ListBoxItem
    {
        public CustomListBox()
        {
            
        }
    }
}
