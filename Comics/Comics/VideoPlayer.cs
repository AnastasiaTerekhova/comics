using Microsoft.Win32;
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
using System.Windows.Threading;

namespace Comics
{
    public class VideoPlayer
    {
        public MediaElement Media { get; private set; }
        private DispatcherTimer Timer { get; set; }
        public VideoPlayer(MediaElement media, DispatcherTimer timer)
        {
            Media = media;
            Timer = timer;
        }
        public void DownloadVideo()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Video files (*.MP4, *.AVI, *.MKW, *.WMV)|*.mp4;*.avi;*.mkv;*.wmv";
            if (openDialog.ShowDialog() == true)
            {
                Media.Source = new Uri(openDialog.FileName);
            }
        }
        public RenderTargetBitmap MakeScreenShot()
        {
            int width = Media.NaturalVideoWidth;
            int height = Media.NaturalVideoHeight;
            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            var vb = new VisualBrush(Media);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(vb, null, new Rect(new System.Windows.Size(width, height)));
            }
            bitmap.Render(dv);
            return bitmap;
        }

        public void Play()
        {
            Media.Play();
            Timer.Start();
        }
        public void Pause()
        {
            Media.Pause();
            Timer.Stop();
        }
        public void Stop()
        {
            Media.Stop();
            Timer.Stop();
        }
    }
}
