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

        public static Image global_sender;
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
            var images = Canvas.Children.OfType<Image>().ToList(); //Все элементы типа Image в canvas1
            foreach (var image in images)
            {
                if (image.Name == "dialog") //Соответствие на имя.
                    Canvas.Children.Remove(image); //Удаляем
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

        //перемещение картинки 
        private void Image_Drop(object sender, DragEventArgs e)
        {
            ((Image)sender).Source = global_sender.Source;
        }

        //Перемещение катинки
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // sender – объект, на котором произошло данное событие.
            Image lbl = sender as Image;
            global_sender = lbl;
            // Создаем источник.
            // Копируем содержимое метки Drop.
            // 1 параметр: Элемент управления, который будет источником.
            // 2 параметр: Данные, которые будут перемещаться.
            // 3 параметр: Эффект при переносе.
            DragDrop.DoDragDrop(lbl, lbl.Source, DragDropEffects.Copy);
        }

        //создает элемент Image в InkCanvas
        void InkCanvas_Drop(object sender, DragEventArgs e)
        {
            var s = sender as InkCanvas;
            Image img = new Image();
            img.Name = "dialog";
            img.Source = global_sender.Source;
            img.Width = global_sender.Width;
            img.Height = global_sender.Height;
            var poz = e.GetPosition((InkCanvas)sender);
            Thickness p = new Thickness(poz.X, poz.Y, 0, 0);
            img.Margin = p;
            Canvas.Children.Add(img);

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Cleaning_Click(object sender, RoutedEventArgs e)
        {
            this.Canvas.Strokes.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
