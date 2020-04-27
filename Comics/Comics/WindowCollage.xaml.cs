using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Comics
{
    /// <summary>
    /// Логика взаимодействия для WindowCollage.xaml
    /// </summary>
    public partial class WindowCollage : Window
    {
        enum Mods
        {
            /// <summary>
            /// Режим ожидания действий пользователя
            /// </summary>
            Wait,
            /// <summary>
            /// Режим добавления изображения из имеющихся (выбрано, но не добавлено)
            /// </summary>
            Add,
            /// <summary>
            /// Режим изменения 
            /// </summary>
            Edit
        }

        /// <summary>
        /// НЕ ИСПОЛЬЗОВАТЬ (текущий коллаж, использовать через SelectedCollage)
        /// </summary>
        int selectedCollage = -1;

        /// <summary>
        /// Изображение, выбранное для добавления в коллаж
        /// </summary>
        Image SelectedNewImage;
        /// <summary>
        /// Текущий режим
        /// </summary>
        Mods Mode = Mods.Wait;
        /// <summary>
        /// Положение мыши на момент начала перемещения изображения
        /// </summary>
        Point MousePoinOnStartEdit;
        /// <summary>
        /// Положение изображения на момент начала перемещения
        /// </summary>
        Point ImagePoinOnStartEdit;

        /// <summary>
        /// Словарь канвасов. Ключ — номер канваса, значение — сам канвас
        /// </summary>
        Dictionary<int, Canvas> Canvases = new Dictionary<int, Canvas>();
        /// <summary>
        /// Список коллажей, что были созданы
        /// </summary>
        List<Collage> Collages = new List<Collage>();

        /// <summary>
        /// Текущий коллаж
        /// </summary>
        int SelectedCollage
        {
            get { return selectedCollage; }
            set // при установке значения совершаем много действий
            {
                // сохраняем текущий, чтобы не потерять внесённые изменения (ну или хотя бы просто превьюху обновим)
                SaveCurrentCollage();
                // принимаем новое значение, проверяя его на соответствие границам
                selectedCollage = value;
                if (selectedCollage < 0)
                    selectedCollage = Collages.Count > 0 ? 0 : -1;
                if (selectedCollage > Collages.Count - 1)
                    selectedCollage = Collages.Count - 1;

                // очищаем редактор коллажа
                gridEditor.Children.Clear();
                // если что-то выбрано (-1 — ничего не выбрано), то начинаем заполнение грида с редактором
                if (selectedCollage != -1)
                {
                    // очищаем словарь канвасов, ибо сейчас они все будут пересозданы
                    Canvases.Clear();
                    // получаем текущий объект коллажа
                    var curClg = Collages[selectedCollage];
                    // загружаем соответствующий шаблон
                    LoadTempelate(curClg.TempelateName);
                    // определяем грид контента шаблона
                    var gc = (Grid)gridEditor.Children[0];
                    // в зависимости от типа шаблона, загружаем его параметры, а так же заполняем словарь канвасов
                    switch (curClg.TempelateName)
                    {
                        case "T1": // крест
                            gc.RowDefinitions[0].Height = (GridLength)curClg.Params["TopLen"];
                            gc.RowDefinitions[2].Height = (GridLength)curClg.Params["DownLen"];
                            gc.ColumnDefinitions[0].Width = (GridLength)curClg.Params["LeftLen"];
                            gc.ColumnDefinitions[2].Width = (GridLength)curClg.Params["RightLen"];

                            goto case "GetCanvaces"; // переход к загрузке канвасов
                           

                        case "T2": // косой
                            gc.RowDefinitions[0].Height = (GridLength)curClg.Params["TopLen"];
                            gc.RowDefinitions[2].Height = (GridLength)curClg.Params["DownLen"];
                            var gcr1 = (Grid)gc.Children[1]; // строка 1
                            var gcr2 = (Grid)gc.Children[2]; // строка 2
                            gcr1.ColumnDefinitions[0].Width = (GridLength)curClg.Params["TopLeftLen"];
                            gcr1.ColumnDefinitions[2].Width = (GridLength)curClg.Params["TopRightLen"];
                            gcr2.ColumnDefinitions[0].Width = (GridLength)curClg.Params["DownLeftLen"];
                            gcr2.ColumnDefinitions[2].Width = (GridLength)curClg.Params["DownRightLen"];
                            // заполняем список канвасов из обеих строк
                            foreach (UIElement el in gcr1.Children)
                            {
                                var canvas = el as Canvas;
                                if (canvas != null)
                                {
                                    int number = int.Parse(canvas.Name.Substring(6));
                                    Canvases.Add(number, canvas);
                                }
                            }
                            foreach (UIElement el in gcr2.Children)
                            {
                                var canvas = el as Canvas;
                                if (canvas != null)
                                {
                                    int number = int.Parse(canvas.Name.Substring(6));
                                    Canvases.Add(number, canvas);
                                }
                            }
                            break;

                        case "T3":
                            gc.ColumnDefinitions[0].Width = (GridLength)curClg.Params["LeftLen"];
                            gc.ColumnDefinitions[2].Width = (GridLength)curClg.Params["RightLen"];

                            goto case "GetCanvaces"; // переход к загрузке канвасов
                            

                        case "T4":
                            gc.RowDefinitions[0].Height = (GridLength)curClg.Params["TopLen"];
                            gc.RowDefinitions[2].Height = (GridLength)curClg.Params["DownLen"];

                            goto case "GetCanvaces"; // переход к загрузке канвасов
                            

                        case "GetCanvaces": // это костыльная ветка, в которую попадают все, кроме T2, он отличается, поэтому для него всё в нём делается
                            //заполняем список канвасов
                            foreach (UIElement el in gc.Children)
                            {
                                var canvas = el as Canvas;
                                if (canvas != null)
                                {
                                    int number = int.Parse(canvas.Name.Substring(6));
                                    Canvases.Add(number, canvas);
                                }
                            }
                            break;
                    }

                    // заполняем канвасы
                    foreach (int number in Canvases.Keys)
                    {
                        var canvas = Canvases[number];
                        // если установлено изображение, то ставим его
                        if (curClg.Images.ContainsKey(number))
                            SetImageToCanvas(curClg.Images[number], canvas);
                        else
                        {
                            // если нет изображения, то ставим затычку, ибо иначе канвас не обрабатывает события
                            // создаём битмап и закрашиваем его белым, преобразовываем в впфный битмап
                            using (var bm = new System.Drawing.Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight))
                            {
                                using (var g = System.Drawing.Graphics.FromImage(bm))
                                    g.Clear(System.Drawing.Color.White);

                                SetImageToCanvas(new Image()
                                {
                                    Source = ConvertBtoBS(bm),
                                }, canvas);
                            }
                        }
                        // событие клика по битмапу
                        canvas.MouseDown += Canvas_MouseDown;
                    }
                }

                // обновляем панельку внизу формы
                UpdateCollagesPanel();
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // если мы пребываем в режиме добавления изображения, то начинаем добавлять или заменять изображение в канвасе
            if (Mode == Mods.Add)
            {
                // установа режима ожидания сразу и нормального курсора обратно
                Mode = Mods.Wait;
                Cursor = Cursors.Arrow;
                // определяем канвас, который вызвал событие и принимет изображение
                var canvas = sender as Canvas;
                // определяем номер канваса из его имени (canvas1, canvas2, canvas3, canvas4), отсекая первые 6 символов и оставляя лишь цифру
                int number = int.Parse(canvas.Name.Substring(6));
                // получаем текущий коллаж, с которым работаем
                var clg = Collages[SelectedCollage];
                Collage.Image img = null;
                // если есть изображение, соответствующее этому канвасу, то заменяем его просто
                if (clg.Images.ContainsKey(number))
                {
                    img = clg.Images[number];
                    img.Img = SelectedNewImage.Source;
                }
                else // иначе создаём новое Collage.Image
                {
                    img = new Collage.Image()
                    {
                        Img = SelectedNewImage.Source,
                        Left = 0,
                        Top = 0,
                        Width = SelectedNewImage.Source.Width,
                        Height = SelectedNewImage.Source.Height
                    };
                    clg.Images.Add(number, img);
                }
                // обновляем данные в самом канвасе
                SetImageToCanvas(img, canvas);
            }
        }

        /// <summary>
        /// Сохраение текущего коллажа
        /// </summary>
        void SaveCurrentCollage()
        {
            if (SelectedCollage > -1)
            {
                // определяем текущий коллаж
                var clg = Collages[SelectedCollage];

                // сохраняем превьюху (по сути скриним грид с редактором)
                int w = (int)Math.Ceiling(gridEditor.RenderSize.Width);
                int h = (int)Math.Ceiling(gridEditor.RenderSize.Height);
                RenderTargetBitmap rtb = new RenderTargetBitmap(w, h, 96d, 96d, PixelFormats.Default);
                rtb.Render(gridEditor);
                var pos = gridEditor.TranslatePoint(new System.Windows.Point(0, 0), gridEditor.Parent as UIElement);
                var cb = new CroppedBitmap(rtb, new Int32Rect(0, 0, (int)Math.Ceiling(gridEditor.RenderSize.Width), (int)gridEditor.RenderSize.Height));
                clg.Preview = cb;

                // сохраняем параметры изображений в канвасах для коллажа
                foreach(int number in Canvases.Keys)
                {
                    if(clg.Images.ContainsKey(number))
                    {
                        var image = (Image)Canvases[number].Children[0];
                        clg.Images[number].Left = Canvas.GetLeft(image);
                        clg.Images[number].Top = Canvas.GetTop(image);
                        clg.Images[number].Width = image.Width;
                        clg.Images[number].Height = image.Height;
                    }
                }

                //Сохраняем текущие параметры границ для коллажа
                var gc = gridEditor.Children[0] as Grid;
                switch(clg.TempelateName)
                {
                    case "T1":
                        clg.Params["TopLen"] = gc.RowDefinitions[0].Height;
                        clg.Params["DownLen"] = gc.RowDefinitions[2].Height;
                        clg.Params["LeftLen"] = gc.ColumnDefinitions[0].Width;
                        clg.Params["RightLen"] = gc.ColumnDefinitions[2].Width;
                        break;

                    case "T2":
                        clg.Params["TopLen"] = gc.RowDefinitions[0].Height;
                        clg.Params["DownLen"] = gc.RowDefinitions[2].Height;
                        var gcr1 = (Grid)gc.Children[1];
                        var gcr2 = (Grid)gc.Children[2];
                        clg.Params["TopLeftLen"] = gcr1.ColumnDefinitions[0].Width;
                        clg.Params["TopRightLen"] = gcr1.ColumnDefinitions[2].Width;
                        clg.Params["DownLeftLen"] = gcr2.ColumnDefinitions[0].Width;
                        clg.Params["DownRightLen"] = gcr2.ColumnDefinitions[2].Width;
                        break;

                    case "T3":
                        clg.Params["LeftLen"] = gc.ColumnDefinitions[0].Width;
                        clg.Params["RightLen"] = gc.ColumnDefinitions[2].Width;
                        break;

                    case "T4":
                        clg.Params["TopLen"] = gc.RowDefinitions[0].Height;
                        clg.Params["DownLen"] = gc.RowDefinitions[2].Height;
                        break;
                }
            }
        }

        public WindowCollage()
        {
            InitializeComponent(); 
        }

        /// <summary>
        /// Показать форму как дилог с набором подотовленных изображений
        /// </summary>
        /// <param name="imagesSource">набор подотовленных изображений из Window2 (редактор скринов)</param>
        public void ShowDialog(StackPanel imagesSource)
        {
            // очищаем имеющиеся, если были с прошлого открытия формы, чтоб не продублировать
            stackPanelImages.Children.Clear();
            // проходим каждую картинку, копируем её на эту форму и устанавливаем обработчик клика
            foreach (Image image in imagesSource.Children)
            {
                var img = new Image()
                {
                    Source = image.Source,
                    Margin = new Thickness(3, 3, 3, 3),
                    Width = 120,
                    Height = 90
                }; 
                img.MouseDown += Image_MouseDown;
                stackPanelImages.Children.Add(img);
            }
            // Открываем форму как даилог
            base.ShowDialog();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // если мы ничего не перемещаем, то при нажатии на картинку из списка сохранённых и обработанных, входим в режим добавления 
            if (Mode != Mods.Edit)
            {
                Mode = Mods.Add; // устанавливаем режим
                SelectedNewImage = sender as Image; // запоминаем что мы собираемся добавить
                Cursor = Cursors.Cross; // курсор крестиком, чтоб было понятно, что что-то сейчас будет добавляться
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // при кнопке закрытия не закрываем, а скрываем форму, чтобы потом снова можно было её открыть, ничего не потеряв
            e.Cancel = true;
            Hide();
        }

        private void btnV1_Click(object sender, RoutedEventArgs e)
        {
            //крестообразный разделитель
            NewCollage("T1");
        }

        private void btnV2_Click(object sender, RoutedEventArgs e)
        {
            //косой разделитель
            NewCollage("T2");
        }

        private void btnV3_Click(object sender, RoutedEventArgs e)
        {
            //вертикальный разделитель
            NewCollage("T3");
        }

        private void btnV4_Click(object sender, RoutedEventArgs e)
        {
            //горизонтальный разделитель
            NewCollage("T4");
        }

        /// <summary>
        /// Обновление панели со списком коллажей
        /// </summary>
        void UpdateCollagesPanel()
         {
            // очистка
            stackPanelCollages.Children.Clear();

            // проходим все коллажи, что есть уже
            int i = 0;
            foreach (var clg in Collages)
            {
                var preview = new CollagePreview(clg, i++ == SelectedCollage, collage_MouseDown);
                stackPanelCollages.Children.Add(preview.GetVisualElement());
            }
        }

        private void collage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // если кликнули на коллаж, то отмечаем его как выбранный
            SelectedCollage = stackPanelCollages.Children.IndexOf((UIElement)((Image)sender).Parent);
        }

        /// <summary>
        /// Создаёт новый пустой коллаж по шаблону
        /// </summary>
        /// <param name="tempelateName">имя шаблона</param>
        void NewCollage(string tempelateName)
        {
            Collages.Add(new Collage(tempelateName)); // создаём коллаж
            SelectedCollage = Collages.Count - 1; // и устанавливаем его как выбранный
        }

        /// <summary>
        /// Загружает шаблон в область редактора
        /// </summary>
        /// <param name="name">имя шаблона</param>
        void LoadTempelate(string name)
        {
         
             // очистка области редактора и загрузка шаблона из файла
             gridEditor.Children.Clear();
             var loader = new TempelateLoader(name);
             loader.SetEventHandlerOnSplitters(GridSplitter_DragCompleted);
             gridEditor.Children.Add(loader.Content);
            
        }

        private void GridSplitter_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            // когда сместили границу в редакторе коллажа, на всякий случай корректируем все изображения внутри канвасов
            foreach(Canvas canvas in Canvases.Values)
            {
                CorrectFormatImageInCanvas(canvas.Children[0] as Image);
            }
        }

        /// <summary>
        /// Устанавливаем в канвас изображение типа Collage.Image
        /// </summary>
        /// <param name="img">Изображение типа Collage.Image</param>
        /// <param name="canvas">Канвас</param>
        void SetImageToCanvas(Collage.Image image, Canvas canvas)
        {
            canvas.Children.Clear();//удаляем всё, что было внутри            
            var img = new Image();// создаём визуальный объект изображения
            img.Source = image.Img; // устанавливаем источник из полученного изображеия
            canvas.Children.Add(img); // и добавляем в канвас
            img.Stretch = Stretch.Uniform;//режим растягивания
            // устанавливаем параметры смещения и размеры в соответствии с полученный Collage.Image
            img.Width = image.Width;
            img.Height = image.Height;
            Canvas.SetLeft(img, image.Left);
            Canvas.SetTop(img, image.Top);
            // установка обработчиков событий
            img.MouseDown += Img_MouseDown;
            img.MouseMove += Img_MouseMove;
            img.MouseUp += Img_MouseUp;
            img.MouseLeave += Img_MouseLeave;
            img.MouseWheel += Img_MouseWheel;
        }

        /// <summary>
        /// Устанавливаем в канвас изображение
        /// </summary>
        /// <param name="img">Изображение</param>
        /// <param name="canvas">Канвас</param>
        void SetImageToCanvas(Image img, Canvas canvas)
        {
            canvas.Children.Clear();//удаляем всё, что было внутри
            canvas.Children.Add(img);//добавляем изображение
            img.Stretch = Stretch.Uniform;//режим растягивания
            // устанавливаем начальные параметры смещения и размеры
            img.Width = img.Source.Width;
            img.Height = img.Source.Height;
            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
            // установка обработчиков событий
            img.MouseDown += Img_MouseDown;
            img.MouseMove += Img_MouseMove;
            img.MouseUp += Img_MouseUp;
            img.MouseLeave += Img_MouseLeave;
            img.MouseWheel += Img_MouseWheel;
        }

        /// <summary>
        /// Корректировка положения и размера изображения в канвасе.
        /// </summary>
        /// <param name="img">изображение</param>
        void CorrectFormatImageInCanvas(Image img)
        {
            var canvas = img.Parent as Canvas;

            // сначала выравниваем размеры. если высота или ширина изображения меньше, чем у канваса, то увеличиваем пропорционально
            if (img.Width < canvas.ActualWidth)
            {
                double ratio = img.Source.Width / img.Source.Height; // соотношение сторон оринигала
                img.Width = canvas.ActualWidth;
                img.Height = img.Width / ratio;
            }
            if (img.Height < canvas.ActualHeight)
            {
                double ratio = img.Source.Height / img.Source.Width; // соотношение сторон оринигала
                img.Height = canvas.ActualHeight;
                img.Width = img.Height / ratio;
            }

            // если где-то вышли за границу, то возвращаем на место
            double top = Canvas.GetTop(img);
            double left = Canvas.GetLeft(img);
            if (Canvas.GetLeft(img) > 0)
                Canvas.SetLeft(img, 0);
            if (Canvas.GetTop(img) > 0)
                Canvas.SetTop(img, 0);
            if (Canvas.GetLeft(img) + img.Width < canvas.ActualWidth)
                Canvas.SetLeft(img, canvas.ActualWidth - img.Width);
            if (Canvas.GetTop(img) + img.Height < canvas.ActualHeight)
                Canvas.SetTop(img, canvas.ActualHeight - img.Height);
        }

        private void Img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Mode == Mods.Wait)
            {
                Image img = sender as Image;
                // устанавливаем на сколько меняется размер изображения за одно прокручивание, по умаолчанию на 5%
                // а так же получаем из дельты направление изменения размера
                // деление дельты на свой модуль нужно потому, что дельта имеет большое значение
                double xm = (e.Delta / Math.Abs(e.Delta)) * img.Source.Width * 0.05;
                double ym = (e.Delta / Math.Abs(e.Delta)) * img.Source.Height * 0.05;
                // устанавливаем новые размеры изображения
                img.Width += xm;
                img.Height += ym;

                CorrectFormatImageInCanvas(img);
            }
        }

        private void Img_MouseLeave(object sender, MouseEventArgs e)
        {
            // если двигали изображение в канвасе и мышка вышла за пределы, то прерываем движение
            if (Mode == Mods.Edit) 
                Mode = Mods.Wait;
        }

        private void Img_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // если двигали изображение в канвасе и отпустили мышь, то прерываем движение
            if (Mode == Mods.Edit)
                Mode = Mods.Wait;
        }

        private void Img_MouseMove(object sender, MouseEventArgs e)
        {
            // если двигаем изображение в канвасе, то ставим новые параметры его координат
            if (Mode == Mods.Edit)
            {
                Image img = sender as Image;
                Canvas.SetLeft(img, ImagePoinOnStartEdit.X + e.GetPosition(this).X - MousePoinOnStartEdit.X);
                Canvas.SetTop(img, ImagePoinOnStartEdit.Y + e.GetPosition(this).Y - MousePoinOnStartEdit.Y);
                CorrectFormatImageInCanvas(img); 
            }
        }

        private void Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Если режим ожидания действий пользователя и нажали на изображение в канвасе, то запоминаем его начальное положение и начальный клик мыши
            if (Mode == Mods.Wait)
            {
                Image img = sender as Image;
                Mode = Mods.Edit;
                MousePoinOnStartEdit = e.GetPosition(this);
                ImagePoinOnStartEdit = new Point(Canvas.GetLeft(img), Canvas.GetTop(img));
            }
        }

        /// <summary>
        /// Конвертация обычного Bitmap в волшебный BitmapSource для впф
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource ConvertBtoBS(System.Drawing.Bitmap bitmap)
        {
            var bitmapData = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            var bitmapSource = BitmapSource.Create(
                bitmapData.Width, bitmapData.Height,
                bitmap.HorizontalResolution, bitmap.VerticalResolution,
                PixelFormats.Bgr24, null,
                bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

            bitmap.UnlockBits(bitmapData);
            return bitmapSource;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentCollage();

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Выберите папку для сохранения";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int i = 1;
                foreach (var collage in Collages)
                {
                    using (var fileStream = new FileStream(fbd.SelectedPath + "\\"+(i++).ToString("00")+".png", FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(collage.Preview as BitmapSource));
                        encoder.Save(fileStream);
                    }
                }
            }
        }

       
    }
}
