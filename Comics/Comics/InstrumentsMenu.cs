using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Comics
{
    class InstrumentsMenu
    {
        private bool isPencil = true;
        public int sizePen { get; set; }
        private InkCanvas Canvas { get; set; }
        public InstrumentsMenu(InkCanvas canvas, int size)
        {
            Canvas = canvas;
            sizePen = size;
        }
        public void TakeBrush(Button oldButton)
        {
            if (!isPencil)
            {
                Canvas.DefaultDrawingAttributes.Color = (oldButton.Background as SolidColorBrush).Color;
                getSizePen(sizePen);
            }
            isPencil = !isPencil;
        }
        private void getSizePen(int size)
        {
            Canvas.DefaultDrawingAttributes.Height = size;
            Canvas.DefaultDrawingAttributes.Width = size;
        }
        public void Plus()
        {
            if (sizePen <= 34)
            {
                sizePen++;
                getSizePen(sizePen);
            }
        }
        public void Sub()
        {
            if (sizePen >= 2)
            {
                sizePen--;
                getSizePen(sizePen);
            }
        }
        public void AddText()
        {
            TextBox tb = new TextBox
            {
                Width = 100,
                Height = 50,
                BorderThickness = new Thickness(0),
                BorderBrush = new SolidColorBrush(Color.FromRgb(5, 5, 5)),
                Margin = new Thickness(20, 20, 0, 0)
            };
            Canvas.Children.Add(tb);

            tb.Focus();
            Canvas.EditingMode = InkCanvasEditingMode.Select;
        }
    }
}
