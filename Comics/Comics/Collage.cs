using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comics
{
    class Collage
    {
        public class Image
        {
            /// <summary>
            /// Само изображение 
            /// </summary>
            public System.Windows.Media.ImageSource Img;
            /// <summary>
            /// Ширина изображения
            /// </summary>
            public double Width;
            /// <summary>
            /// Высота изображения
            /// </summary>
            public double Height;
            /// <summary>
            /// Отступ сверху, относительно канваса-родителя
            /// </summary>
            public double Top;
            /// <summary>
            /// Отступ слева, относительно канваса-родителя
            /// </summary>
            public double Left;
        }

        public Collage(string tempelateName)
        {
            TempelateName = tempelateName;
            // установка начальных значений границ для разных шаблонов
            switch (tempelateName)
            {
                case "T1":
                    Params.Add("LeftLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("RightLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("TopLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("DownLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    break;
                case "T2":
                    Params.Add("TopLeftLen", new System.Windows.GridLength(20, System.Windows.GridUnitType.Star));
                    Params.Add("TopRightLen", new System.Windows.GridLength(30, System.Windows.GridUnitType.Star));
                    Params.Add("DownLeftLen", new System.Windows.GridLength(30, System.Windows.GridUnitType.Star));
                    Params.Add("DownRightLen", new System.Windows.GridLength(20, System.Windows.GridUnitType.Star));
                    Params.Add("TopLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("DownLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    break;
                case "T3":
                    Params.Add("LeftLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("RightLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    break;
                case "T4":
                    Params.Add("TopLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    Params.Add("DownLen", new System.Windows.GridLength(50, System.Windows.GridUnitType.Star));
                    break;
            }
        }

        /// <summary>
        /// Название файла шаблона (без расширения)
        /// </summary>
        public string TempelateName;
        /// <summary>
        /// Список дополнительных параметров коллажа
        /// </summary>
        public Dictionary<string, object> Params = new Dictionary<string, object>();
        /// <summary>
        /// Список изображений в коллаже
        /// </summary>
        public Dictionary<int, Image> Images = new Dictionary<int, Image>();
        /// <summary>
        /// Изображение предпросмотра и оно же полное изображение, которое будет потом сохранено
        /// </summary>
        public System.Windows.Media.ImageSource Preview = null;
    }
}
