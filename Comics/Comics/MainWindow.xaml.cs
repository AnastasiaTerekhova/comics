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
using System.Windows.Media.Composition;
using System.Windows.Media.Effects;


namespace Comics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public delegate void OnAdd(ImageSource img);
        public event OnAdd AddEvent;
        DispatcherTimer timer = new DispatcherTimer();
        VideoPlayer videoPlayer;

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            videoPlayer = new VideoPlayer(media1, timer);

            TextBox1.Text = "0";

        }
        private void timerTick(object sender, object e)
        {
            time.Text = media1.Position.ToString(@"mm\:ss");
            sliderback2.Value = media1.Position.TotalSeconds;
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            // media1.Source = new Uri(textBox1.Text);
            videoPlayer.DownloadVideo();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play();
            if (videoPlayer.Media.Source != null)
            {
                Fix.IsEnabled = true;
            }
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Pause();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
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
            image.Source = videoPlayer.MakeScreenShot();
            AddEvent.Invoke(image.Source);

            int n = Convert.ToInt32(TextBox1.Text);
            n++;
            TextBox1.Text = n.ToString();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }


    }
}
