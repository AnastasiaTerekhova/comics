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

        Brush brush = new SolidColorBrush(Color.FromArgb(120, 128, 128, 128));
        Brush notBrush = Brushes.Transparent;
        bool isPencil = true;
        Button oldButton;
        Brush oldBrush;
        Thickness oldThickness = new Thickness(1);
        Thickness newThickness = new Thickness(2);
        int sizePen = 5;
        Image currentImg = null;
       

        public Window2()
        {
            InitializeComponent();

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

          
            oldBrush = btnColor1.BorderBrush.Clone();
         
            oldButton = btnColor1;

            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
            getSizePen(sizePen);
        }

        void image_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SaveCurrentImage();
                SetCurrentImg(sender as Image);
            }
        }

        void SetCurrentImg(Image img)
        {
            currentImg = img;
            imageInCanvas.Source = img.Source;
        }

        void SaveCurrentImage()
        {
            if (currentImg == null)
                return;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)imageInCanvas.Source.Width, (int)imageInCanvas.Source.Height, 96d, 96d, PixelFormats.Default);
            rtb.Render(canvas);
            currentImg.Source = rtb;
            canvas.Strokes.Clear();
        }

        private void BtnPencil_Click(object sender, RoutedEventArgs e)
        {
            if (!isPencil)
            {
                canvas.DefaultDrawingAttributes.Color = (oldButton.Background as SolidColorBrush).Color;
                getSizePen(sizePen);
            }
            isPencil = !isPencil;
        }

        private void BtnColor_Click(object sender, RoutedEventArgs e)
        {
            if (oldButton == sender) return;

            oldButton.BorderBrush = oldBrush;
            oldButton.BorderThickness = oldThickness;
           
            oldBrush = (sender as Button).BorderBrush.Clone();
            (sender as Button).BorderBrush = Brushes.White;
            (sender as Button).BorderThickness = newThickness;
          
            canvas.DefaultDrawingAttributes.Color = ((sender as Button).Background as SolidColorBrush).Color;
            oldButton = sender as Button;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.ShowDialog();
            Show();           
        }

        private void getSizePen(int size)
        {
            canvas.DefaultDrawingAttributes.Height = size;
            canvas.DefaultDrawingAttributes.Width = size;
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            if (sizePen <= 34)
            {
                sizePen++;
                TextBoxSize.Text = sizePen.ToString();
                getSizePen(sizePen);
            }
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            if (sizePen >= 2)
            {
                sizePen--;
                TextBoxSize.Text = sizePen.ToString();
                getSizePen(sizePen);
            }
        }

        private void btn_AddText_Click(object sender, RoutedEventArgs e)
        {            
            TextBox tb = new TextBox
            {
                Width = 100,
                Height = 50,
                BorderThickness = new Thickness(0),
                BorderBrush = new SolidColorBrush(Color.FromRgb(5, 5, 5)),
                Margin = new Thickness(20, 20, 0, 0)
            };
            
            this.canvas.Children.Add(tb);
            
            tb.Focus();
            this.canvas.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.Close();
        }

        private void btn_AddFigure_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
