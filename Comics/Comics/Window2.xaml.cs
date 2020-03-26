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
        Thickness newThickness = new Thickness(5);
        int sizePen = 25;
       

        public Window2()
        {
            InitializeComponent();

            mainWindow.AddEvent += (img) => {
                stackPanel.Children.Add(new Image()
                {
                    Source = img.Clone(),
                    Margin = new Thickness(3, 3, 3, 3),
                    Width = 120,
                    Height = 90
                });
            };

            AddButton_Click(null, new RoutedEventArgs());

            btnPencil.Background = brush;

            oldBrush = btnColor1.BorderBrush.Clone();
            btnColor1.BorderBrush = Brushes.DarkGray;
            btnColor1.BorderThickness = newThickness;
            
            oldButton = btnColor1;

            canvas.EditingMode = InkCanvasEditingMode.Ink;
            canvas.DefaultDrawingAttributes.Color = Colors.Black;
            getSizePen(sizePen);
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
            (sender as Button).BorderBrush = Brushes.DarkGray;
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

    }
}
