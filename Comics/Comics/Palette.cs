using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Comics
{
    class Palette
    {
        private System.Windows.Media.Brush OldBrush { get; set; }
        private Button Butt { get; set; }
        private InkCanvas Canvas { get; set; }
        private Thickness oldThickness = new Thickness(1);
        private Thickness newThickness = new Thickness(2);
        public Palette(Button button, InkCanvas canvas)
        {
            Butt = button;
            OldBrush = Butt.BorderBrush.Clone();
            Canvas = canvas;
        }
        public void ChangeColor(Button button)
        {
            Butt.BorderBrush = OldBrush;
            Butt.BorderThickness = oldThickness;
            OldBrush = button.BorderBrush.Clone();
            button.BorderBrush = System.Windows.Media.Brushes.White;
            button.BorderThickness = newThickness;
            Canvas.DefaultDrawingAttributes.Color = (button.Background as SolidColorBrush).Color;
            Butt = button;
        }
    }
}
