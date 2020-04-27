using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Comics
{
    class CollagePreview
    {
        Border Border;

        public CollagePreview(Collage clg, bool selected, MouseButtonEventHandler collage_MouseDown)
        {
            //создаём границу для очередного, чтобы было красиво
            Border = new Border()
            {
                Margin = new Thickness(3, 3, 3, 3),
                BorderThickness = new Thickness(2)
            };
            //получаем превьюху, а если её нет, то используем из ресурсов картинку-заглушку
            ImageSource src = clg.Preview;
            if (src == null)
            {
                src = WindowCollage.ConvertBtoBS(Properties.Resources.EmptyCollagePreview);
            }
            // создаём визуальный объект под превьюху и устанавливаем его параметры
            var collage = new Image()
            {
                Source = src,
                Width = 120,
                Height = 90
            };
            // устанавливаем цвет (для текущего красный, для остальных серый)
            if (selected)
                Border.BorderBrush = Brushes.Red;
            else
                Border.BorderBrush = Brushes.Gray;
            // обработчик клика на превьюху коллажа
            collage.MouseDown += collage_MouseDown;
            //оборачиваем превьюху в подготовленную рамку
            Border.Child = collage;
        }

        public Border GetVisualElement()
        {
            return Border;
        }
    }
}
