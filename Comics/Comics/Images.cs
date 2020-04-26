using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Comics
{
    class Images
    {
        public System.Windows.Controls.Image currentImg = null;
        private System.Windows.Controls.Grid GridWithCanvas;
        public System.Windows.Controls.Image ImageInCanvas { get; set; }
        public InkCanvas Canvas { get; private set; }

        public Images(InkCanvas canvas, System.Windows.Controls.Image imgInCanvas, Grid gridWithCanvas)
        {
            Canvas = canvas;
            ImageInCanvas = imgInCanvas;
            GridWithCanvas = gridWithCanvas;
            Canvas.EditingMode = InkCanvasEditingMode.Ink;
            Canvas.DefaultDrawingAttributes.Color = Colors.Black;
        }

      
        public void SetCurrentImg(System.Windows.Controls.Image img)
        {
            currentImg = img;
            ImageInCanvas.Source = img.Source;
        }

        public void SaveCurrentImage()
        {
            if (currentImg == null)
                return;
            int w = (int)Math.Max(ImageInCanvas.Source.Width, Math.Ceiling(GridWithCanvas.RenderSize.Width));
            int h = (int)Math.Max(ImageInCanvas.Source.Height, Math.Ceiling(GridWithCanvas.RenderSize.Height));
            RenderTargetBitmap rtb = new RenderTargetBitmap(w, h, 96d, 96d, PixelFormats.Default);
            rtb.Render(Canvas);
            var pos = Canvas.TranslatePoint(new System.Windows.Point(0, 0), Canvas.Parent as UIElement);
            var cb = new CroppedBitmap(rtb, new Int32Rect((int)Math.Ceiling(pos.X), (int)Math.Ceiling(pos.Y), (int)Math.Ceiling(ImageInCanvas.Source.Width), (int)ImageInCanvas.Source.Height));
            currentImg.Source = cb;
            Canvas.Strokes.Clear();
        }

      
    }
}
