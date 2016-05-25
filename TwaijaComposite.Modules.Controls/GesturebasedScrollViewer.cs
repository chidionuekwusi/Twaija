using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace TwaijaComposite.Modules.Controls
{
#if !SILVERLIGHT
    public class GesturebasedScrollViewer:ScrollViewer
    {
         private Point  mouseDragStartPoint;
         private Point scrollStartOffset;
         protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
         {
             mouseDragStartPoint = e.GetPosition(this);
             scrollStartOffset.X = HorizontalOffset;
             scrollStartOffset.Y = VerticalOffset;

             // Update the cursor if scrolling is possible 
             this.Cursor = (ExtentWidth > ViewportWidth) ||
                 (ExtentHeight > ViewportHeight) ?
                 Cursors.ScrollAll : Cursors.Arrow;

             this.CaptureMouse();
             base.OnMouseDoubleClick(e);
         }
    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e) 
    { 
        //mouseDragStartPoint = e.GetPosition(this); 
        //scrollStartOffset.X = HorizontalOffset; 
        //scrollStartOffset.Y = VerticalOffset; 

        //// Update the cursor if scrolling is possible 
        //this.Cursor = (ExtentWidth > ViewportWidth) || 
        //    (ExtentHeight > ViewportHeight) ? 
        //    Cursors.ScrollAll : Cursors.Arrow; 

        //this.CaptureMouse();
        base.OnPreviewMouseLeftButtonDown(e); 
    } 

    protected override void OnPreviewMouseMove(MouseEventArgs e) 
    { 
        if (this.IsMouseCaptured) 
        { 
            // Get the new mouse position. 
            Point mouseDragCurrentPoint = e.GetPosition(this); 

            // Determine the new amount to scroll. 
            Point delta = new Point( 
                (mouseDragCurrentPoint.X > this.mouseDragStartPoint.X) ? 
                -(mouseDragCurrentPoint.X - this.mouseDragStartPoint.X) : 
                (this.mouseDragStartPoint.X - mouseDragCurrentPoint.X), 
                (mouseDragCurrentPoint.Y > this.mouseDragStartPoint.Y) ? 
                -(mouseDragCurrentPoint.Y - this.mouseDragStartPoint.Y) : 
                (this.mouseDragStartPoint.Y - mouseDragCurrentPoint.Y)); 

            // Scroll to the new position. 
            ScrollToHorizontalOffset(this.scrollStartOffset.X + delta.X); 
            ScrollToVerticalOffset(this.scrollStartOffset.Y + delta.Y); 
        } 
        base.OnPreviewMouseMove(e); 
    } 
        
    protected override  void OnPreviewMouseUp(MouseButtonEventArgs e) 
    { 
        if (this.IsMouseCaptured) 
        { 
            this.Cursor = Cursors.Arrow; 
            this.ReleaseMouseCapture(); 
        } 
        base.OnPreviewMouseUp(e); 
    } 
    }
#endif
}
