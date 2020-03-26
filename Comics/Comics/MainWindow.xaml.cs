using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Size = System.Windows.Size;
using Microsoft.Win32;

namespace Comics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
   

    public partial class MainWindow : Window
    {
        public delegate void OnAdd(ImageSource img);
        public event OnAdd AddEvent;

     
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
          
        }

            private void timerTick(object sender, object e)
        {
            time.Text = media1.Position.ToString(@"mm\:ss");
            sliderback2.Value = media1.Position.TotalSeconds;
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
           // media1.Source = new Uri(textBox1.Text);
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Video files (*.MP4, *.AVI, *.MKW, *.WMV)|*.mp4;*.avi;*.mkv; *.wmv"; 
            if (openDialog.ShowDialog() == true)
            {
                media1.Source = new Uri(openDialog.FileName);

            }
            
        }
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            media1.Play();
            timer.Start();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            media1.Pause();
            timer.Stop();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            media1.Stop();
            timer.Stop();
        }

        private void media1_MediaOpened(object sender, RoutedEventArgs e)
        {
            slider2.Maximum = media1.NaturalDuration.TimeSpan.TotalSeconds;
            sliderback2.Maximum = media1.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (media1 != null)
            {
                media1.Volume = slider1.Value;
            }
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media1.Pause();
            media1.Position = TimeSpan.FromSeconds(slider2.Value);
            media1.Play();
        }

        private void FixButton_Click(object sender, RoutedEventArgs e)
        {
            int width = media1.NaturalVideoWidth;
            int height = media1.NaturalVideoHeight;
            var bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            var vb = new VisualBrush(media1);
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                dc.DrawRectangle(vb, null, new Rect(new Size(width, height)));
            }
            bitmap.Render(dv);
            image.Source = bitmap;

            AddEvent.Invoke(image.Source);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            
        }


    }
}
