using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Comics
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        MainWindow mainWindow = new MainWindow();
        //Brush brush = new SolidColorBrush(Color.FromArgb(120, 128, 128, 128));
        //Brush notBrush = Brushes.Transparent;        
        Button oldButton;
        //Создание экземпляров классов
        InstrumentsMenu instruments;
        Palette palette;
        Images draw;


        public Window2()
        {
            InitializeComponent();
            draw = new Images(Canvas, ImageInCanvas, GridWithCanvas);
            oldButton = btnColor1;
            palette = new Palette(oldButton, Canvas);
            instruments = new InstrumentsMenu(Canvas, 5);
            TextBoxSize.Text = instruments.sizePen.ToString();
            mainWindow.AddEvent += (img) => {
                var newImg = new Image()
                {
                    Source = img.Clone(),
                    Margin = new Thickness(3, 3, 3, 3),
                    Width = 120,
                    Height = 90
                };
                newImg.MouseDown += image_MouseDown;
                stackPanel.Children.Add(newImg);
            };

            AddButton_Click(null, new RoutedEventArgs());
        }

        void image_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                draw.SaveCurrentImage();
                draw.SetCurrentImg(sender as Image);
            }
        }

        private void BtnPencil_Click(object sender, RoutedEventArgs e)
        {
            instruments.TakeBrush(oldButton);
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            //if (oldButton == sender) return;
            palette.ChangeColor(sender as Button);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.ShowDialog();
            Show();
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            instruments.Plus();
            TextBoxSize.Text = instruments.sizePen.ToString();
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            instruments.Sub();
            TextBoxSize.Text = instruments.sizePen.ToString();
        }

        private void btn_AddText_Click(object sender, RoutedEventArgs e)
        {
            instruments.AddText();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Close();
        }

        private void btn_AddFigure_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
