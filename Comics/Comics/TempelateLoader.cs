using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Comics
{
    /// <summary>
    /// Загрузчик шаблонов
    /// </summary>
    class TempelateLoader
    {
        /// <summary>
        /// Grid, загруженный из шаблона
        /// </summary>
        public Grid Content
        {
            get;
            private set;
        }

        /// <summary>
        /// Имя шаблона
        /// </summary>
        public string TempelateName
        {
            get;
            private set;
        }

        /// <summary>
        /// Создание шаблона по имени
        /// </summary>
        /// <param name="tempelateName"></param>
        public TempelateLoader(string tempelateName)
        {
            TempelateName = tempelateName;
            var reader = new System.IO.StreamReader(Environment.CurrentDirectory + "\\Tempelates\\" + tempelateName + ".xaml");
            Content = (Grid)System.Windows.Markup.XamlReader.Load(reader.BaseStream);
        }

        /// <summary>
        /// Установка обработчиков события смещения для элементов типа GridSplitter
        /// </summary>
        /// <param name="eventHandler"></param>
        public void SetEventHandlerOnSplitters(DragCompletedEventHandler eventHandler)
        {
            // Ищем разделители и вешаем им обработчик события смещения
            foreach (var o in Content.Children)
            {
                var splitter = o as GridSplitter;
                if (splitter != null)
                {
                    splitter.DragCompleted += eventHandler;
                }
            }
        }
    }
}
