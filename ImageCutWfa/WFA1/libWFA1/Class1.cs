using System;
using System.Drawing;
using System.Drawing.Drawing2D;
//расчитывать по размеру картинки размер области, давать максимально допустимый размер
//перекинуть в либ 
//оси центра должны двигаться с квадратом
//починить объединение зумирования и поволота
namespace ImageCutLib
{
    public class ImageCut
    {
        private Rectangle selectionRectangle;
        private bool isDraggingRect = false;
        private bool isResizing = false;
        private Point dragStartPointForSelectionRectangle;
        private float aspectRatio;
        private Bitmap finalBitmap;
        private float zoomFactor = 1;
        public bool isShowCoordinateOfRectangleCenter = false;
        private ResizeHandle activeResizeHandle = ResizeHandle.None;

        public Rectangle SelectionRectangle
        {
            get => selectionRectangle;
            set => selectionRectangle = value;
        }
        public Point StartPointForSelectionRectangle
        {
            get => dragStartPointForSelectionRectangle;
            set => dragStartPointForSelectionRectangle = value;
        }

        public int SelectionWidth
        {
            get => selectionRectangle.Width;
            set => selectionRectangle.Width = value;

        }

        public int SelectionHeight
        {
            get => selectionRectangle.Height;
            set => selectionRectangle.Height = value;

        }
        public bool IsDraggingRect
        {
            get => isDraggingRect;
            set => isDraggingRect = value;
        }
        public bool IsResizing
        {
            get => isResizing;
            set => isResizing = value;
        }
        public float ZoomFactor => zoomFactor;  
        public Bitmap FinalBitmap => finalBitmap;

        
        public void InitializeSelectionRectangle(int imgWidth, int imgHeight, int containerWidth, int containerHeight,Point startPoint, float aspectRatio)
        {
            this.aspectRatio = aspectRatio;
            float newX, newY, rectWidth, rectHeight;

            // Задаём начальную ширину и высоту прямоугольника с учетом пропорции
            if (selectionRectangle.Width == 0) // задаём начальную ширину
                rectWidth = imgWidth;
            else if (selectionRectangle.Width > imgWidth)
                rectWidth = imgWidth;
            else
                rectWidth = selectionRectangle.Width;

            rectHeight = rectWidth / aspectRatio;
            if (imgHeight/ imgWidth < aspectRatio && imgWidth> imgHeight)
            {
                rectHeight = imgHeight;
                rectWidth = rectHeight * aspectRatio;
                if (rectWidth > containerWidth)
                {
                    rectWidth = containerWidth;
                    rectHeight = rectWidth / aspectRatio;
                }
            }

            // Если высота прямоугольника превышает размер контейнера, ограничиваем
            if (rectHeight > containerHeight)
            {
                rectHeight = imgHeight;
                rectWidth = rectHeight * aspectRatio;
            }

            // Расчёт начальных координат
            if (dragStartPointForSelectionRectangle == Point.Empty)
            {
                // Если прямоугольник ещё не перемещался (первоначальная инициализация), центрируем его
                newX = (containerWidth - rectWidth) / 2;
                newY = (containerHeight - rectHeight) / 2;
            }
            else
            {
                // Для последующих изменений пропорции, оставляем текущие координаты
                newX = startPoint.X - (rectWidth - selectionRectangle.Width) / 2;
                newY = startPoint.Y - (rectHeight - selectionRectangle.Height) / 2;
            }

            // Ограничения для координат
            newX = Math.Max(0, Math.Min(newX, containerWidth - rectWidth));
            newY = Math.Max(0, Math.Min(newY, containerHeight - rectHeight));

            // Применяем значения и сохраняем новый прямоугольник
            selectionRectangle = new Rectangle((int)newX, (int)newY, (int)rectWidth, (int)rectHeight);
            dragStartPointForSelectionRectangle = selectionRectangle.Location;
        }
        public void StartInteraction(Point location)
        {
            
            if (GetResizeHandle(location) == ResizeHandle.TopLeft ||
                GetResizeHandle(location) == ResizeHandle.BottomRight||
                GetResizeHandle(location) == ResizeHandle.TopRight ||
                GetResizeHandle(location) == ResizeHandle.BottomLeft)
            {
                activeResizeHandle = GetResizeHandle(location);
                if (activeResizeHandle != ResizeHandle.None) //проверяем что точно около одного из углов
                {
                    isResizing = true;
                    dragStartPointForSelectionRectangle = location;
                }
            }
            else if (selectionRectangle.Contains(location) && !isResizing)
            {
                isDraggingRect = true;
                dragStartPointForSelectionRectangle = location;
            }
        }

        public void Drag(Point location, int containerWidth, int containerHeight)
        {
            if (isDraggingRect && !isResizing)
            {
                int offsetX = location.X - dragStartPointForSelectionRectangle.X;
                int offsetY = location.Y - dragStartPointForSelectionRectangle.Y;

                selectionRectangle.Offset(offsetX, offsetY);
                if (selectionRectangle.Left < 0) //проверка и корректировка если вышли за границы
                    selectionRectangle.X = 0;
                if (selectionRectangle.Top < 0)
                    selectionRectangle.Y = 0;
                if (selectionRectangle.Right > containerWidth)
                    selectionRectangle.X = containerWidth - selectionRectangle.Width;
                if (selectionRectangle.Bottom > containerHeight)
                    selectionRectangle.Y = containerHeight - selectionRectangle.Height;
                dragStartPointForSelectionRectangle = location;

            }
        }
        public void Resize(Point location, int containerWidth, int containerHeight)
        {
            if (isResizing)
            {
                Rectangle newRectangle = selectionRectangle;
                int offsetX = location.X - dragStartPointForSelectionRectangle.X;
                int offsetY = location.Y - dragStartPointForSelectionRectangle.Y;

                // Перемещаем или изменяем размер прямоугольника в зависимости от активной точки изменения
                switch (activeResizeHandle)
                {
                    case ResizeHandle.TopLeft:
                        newRectangle.Width -= offsetX;
                        newRectangle.Height -= offsetY;
                        newRectangle.X += offsetX;
                        newRectangle.Y += offsetY;
                        break;

                    case ResizeHandle.TopRight:
                        newRectangle.Width += offsetX;
                        newRectangle.Height -= offsetY;
                        newRectangle.Y += offsetY;
                        break;

                    case ResizeHandle.BottomLeft:
                        newRectangle.Width -= offsetX;
                        newRectangle.Height += offsetY;
                        newRectangle.X += offsetX;
                        break;

                    case ResizeHandle.BottomRight:
                        newRectangle.Width += offsetX;
                        newRectangle.Height += offsetY;
                        break;
                }

                // Применяем пропорции, если они заданы
                if (aspectRatio > 0)
                {
                    // Сохраняем пропорции в зависимости от активной точки изменения
                    if (activeResizeHandle == ResizeHandle.TopLeft || activeResizeHandle == ResizeHandle.BottomRight)
                    {
                        newRectangle.Height = (int)(newRectangle.Width / aspectRatio);
                    }
                    else if (activeResizeHandle == ResizeHandle.TopRight || activeResizeHandle == ResizeHandle.BottomLeft)
                    {
                        newRectangle.Width = (int)(newRectangle.Height * aspectRatio);
                    }
                }

                // Ограничиваем размеры прямоугольника так, чтобы он не выходил за границы контейнера
                if (newRectangle.Width > 0 && newRectangle.Height > 0)
                {
                    // Ограничиваем ширину и высоту, чтобы прямоугольник не выходил за границы контейнера
                    if (newRectangle.Width > containerWidth - newRectangle.X)
                    {
                        newRectangle.Width = containerWidth - newRectangle.X;
                        newRectangle.Height = (int)(newRectangle.Width / aspectRatio); // Пропорции
                    }

                    if (newRectangle.Height > containerHeight - newRectangle.Y)
                    {
                        newRectangle.Height = containerHeight - newRectangle.Y;
                        newRectangle.Width = (int)(newRectangle.Height * aspectRatio); // Пропорции
                    }

                    // Ограничиваем положение прямоугольника, чтобы он не выходил за пределы контейнера
                    if (newRectangle.X < 0) newRectangle.X = 0;
                    if (newRectangle.Y < 0) newRectangle.Y = 0;

                    // Если изменился левый или верхний край, обновляем координаты
                    if (activeResizeHandle == ResizeHandle.TopLeft || activeResizeHandle == ResizeHandle.BottomLeft)
                        newRectangle.X = Math.Max(0, selectionRectangle.Right - newRectangle.Width);

                    if (activeResizeHandle == ResizeHandle.TopLeft || activeResizeHandle == ResizeHandle.TopRight)
                        newRectangle.Y = Math.Max(0, selectionRectangle.Bottom - newRectangle.Height);

                    // Проверка, что прямоугольник не выходит за границы контейнера
                    if (newRectangle.Right > containerWidth)
                        newRectangle.Width = containerWidth - newRectangle.X;

                    if (newRectangle.Bottom > containerHeight)
                        newRectangle.Height = containerHeight - newRectangle.Y;

                    selectionRectangle = newRectangle;
                }

                // Обновляем точку для следующего вычисления
                dragStartPointForSelectionRectangle = location;
            }
        }

        public ResizeHandle GetResizeHandle(Point location)
        {
            const int tolerance = 15;

            if (IsNearPoint(location, selectionRectangle.Left, selectionRectangle.Top, tolerance))
                return ResizeHandle.TopLeft;
            if (IsNearPoint(location, selectionRectangle.Right, selectionRectangle.Top, tolerance))
                return ResizeHandle.TopRight;
            if (IsNearPoint(location, selectionRectangle.Left, selectionRectangle.Bottom, tolerance))
                return ResizeHandle.BottomLeft;
            if (IsNearPoint(location, selectionRectangle.Right, selectionRectangle.Bottom, tolerance))
                return ResizeHandle.BottomRight;

            return ResizeHandle.None;
        }

        private bool IsNearPoint(Point p, int x, int y, int tolerance) //проверка находимся ли около одного из углов
        {
            return Math.Abs(p.X - x) <= tolerance && Math.Abs(p.Y - y) <= tolerance;
        }

        public void StopInteraction() //для прерывания действия после того как отпустили кнопку мыши
        {
            isDraggingRect = false;
            isResizing = false;
            activeResizeHandle = ResizeHandle.None;
        }
        public void AreaToCenter(int containerWidth, int containerHeight)
        {
            selectionRectangle.X = containerWidth / 2 - selectionRectangle.Width / 2;
            selectionRectangle.Y = containerHeight / 2 - selectionRectangle.Height / 2;
        }

        public void DrawSelectionRectangle(Graphics graphics)
        {
            if (selectionRectangle != Rectangle.Empty)
            {
                using Pen rectPen = new Pen(Color.Red, 2);
                rectPen.DashStyle = DashStyle.Dash;
                graphics.DrawRectangle(rectPen, selectionRectangle);
            }
        }
        public void CentralLines_Paint(Graphics graphics, int containerWidth, int containerHeight)
        {
            // Отрисовка центральных линий
            int centerX = selectionRectangle.X + selectionRectangle.Width / 2;
            int centerY = selectionRectangle.Y + selectionRectangle.Height / 2;

            // Отрисовка центральных линий, привязанных к центру прямоугольника
            using Pen centralPen = new Pen(Color.Black, 1);
            centralPen.DashStyle = DashStyle.Dash;
            graphics.DrawLine(centralPen, centerX, 0, centerX, containerHeight); // Вертикальная линия
            graphics.DrawLine(centralPen, 0, centerY, containerWidth, centerY); // Горизонтальная линия
        }
        public void GridLines_Paint(Graphics graphics, int containerWidth, int containerHeight, Point startPoint)
        {
            using Pen gridLinesPen = new Pen(Color.Gray, 2);
            gridLinesPen.DashStyle = DashStyle.Dash;
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y, selectionRectangle.X, 0); //отрисовка левых частей сетки
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y + selectionRectangle.Height, selectionRectangle.X, containerHeight);
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y, 0, selectionRectangle.Y);
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y + selectionRectangle.Height, 0, selectionRectangle.Y + selectionRectangle.Height);
            graphics.DrawLine(gridLinesPen, selectionRectangle.X+selectionRectangle.Width, selectionRectangle.Y, selectionRectangle.X + selectionRectangle.Width, 0);//отрисовка правых частей сетки
            graphics.DrawLine(gridLinesPen, selectionRectangle.X + selectionRectangle.Width, selectionRectangle.Y + selectionRectangle.Height, selectionRectangle.X + selectionRectangle.Width, containerHeight);
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y, containerWidth, selectionRectangle.Y);
            graphics.DrawLine(gridLinesPen, selectionRectangle.X, selectionRectangle.Y + selectionRectangle.Height, containerWidth, selectionRectangle.Y +selectionRectangle.Height);

        }
        public Bitmap CropImage(Bitmap originalImage, Rectangle selectionRectangle, int pictureBoxW, int pictureBoxH)
        {
            if (originalImage == null || selectionRectangle == Rectangle.Empty)
            {
                throw new InvalidOperationException("No image or selection rectangle available.");
            }

            // Вычисляем коэффициенты масштабирования по обеим осям
            float scaleX = (float)pictureBoxW / originalImage.Width;
            float scaleY = (float)pictureBoxH / originalImage.Height;

            // Масштабируем изображение так, чтобы одна из сторон заполнила PictureBox
            float scale = Math.Min(scaleX, scaleY);

            // Теперь, учитывая масштаб, мы вычисляем, как изменятся координаты прямоугольника
            // Учитываем сдвиг изображения внутри PictureBox и масштабирование
            int scaledX = (int)((selectionRectangle.X - (pictureBoxW - originalImage.Width * scale) / 2) / scale);
            int scaledY = (int)((selectionRectangle.Y - (pictureBoxH - originalImage.Height * scale) / 2) / scale);

            int scaledWidth = (int)(selectionRectangle.Width / scale);
            int scaledHeight = (int)(selectionRectangle.Height / scale);

            // Проверяем, чтобы вырезаемая область не выходила за пределы изображения
            scaledX = Math.Max(0, scaledX);
            scaledY = Math.Max(0, scaledY);
            scaledWidth = Math.Min(originalImage.Width - scaledX, scaledWidth);
            scaledHeight = Math.Min(originalImage.Height - scaledY, scaledHeight);

            // Создаем новый Bitmap для результата
            Bitmap croppedImage = new Bitmap(scaledWidth, scaledHeight);

            // Создаем Graphics объект для рисования на новом изображении
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawImage(
                    originalImage,
                    new Rectangle(0, 0, scaledWidth, scaledHeight), // Размер нового изображения
                    new Rectangle(scaledX, scaledY, scaledWidth, scaledHeight), // Исходная область для выреза
                    GraphicsUnit.Pixel
                );
            }

            return croppedImage;
        }

        
        public Bitmap ChangeImage(Bitmap loadedImage, float scaleFactor, float angle, ref float zoomFactor, bool isMouseWheelZoom, int containerWidth, int containerHeight)
        {
            if (loadedImage == null)
                return null;

            if (isMouseWheelZoom)
            {
                if (scaleFactor > 0)
                {
                    zoomFactor *= 1.1f;  // Увеличиваем масштаб на 10%
                }
                else if (scaleFactor < 0)
                {
                    zoomFactor /= 1.1f;  // Уменьшаем масштаб на 10%
                }
            }
            int offsetForImageX = 0, offsetForImageY = 0;
            int newWidth = (int)(loadedImage.Width * zoomFactor);
            int newHeight = (int)(loadedImage.Height * zoomFactor);

            Bitmap finalImage = new Bitmap(containerWidth, containerHeight);

            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float offsetX = (containerWidth - newWidth) / 2;
                float offsetY = (containerHeight - newHeight) / 2;

                g.TranslateTransform(containerWidth / 2f, containerHeight / 2f);
                g.RotateTransform(angle);
                g.TranslateTransform(-containerWidth / 2f, -containerHeight / 2f);

                g.DrawImage(loadedImage, offsetX, offsetY, newWidth, newHeight);
            }

            return finalImage;
        }
        public void SaveImage(Bitmap image, string filePath)
        {
            image.Save(filePath);
        }
        public void ShowCoordinateOfRectangleCenter(Graphics g)
        {
            if (SelectionRectangle == Rectangle.Empty) return;

            // 1. Вычисляем координаты центра прямоугольника.
            float centerX = SelectionRectangle.Left + (float)SelectionRectangle.Width / 2;
            float centerY = SelectionRectangle.Top + (float)SelectionRectangle.Height / 2;

            // 2. Создаем строку для отображения координат.
            string text = $"({centerX:F0}, {centerY:F0})";

            // 3. Настраиваем шрифт и цвет текста.
            Font font = new Font("Arial", 10);
            Brush brush = Brushes.White; 

            // 4. Вычисляем размеры текста.
            SizeF textSize = g.MeasureString(text, font);

            // 5. Вычисляем позицию для рисования текста, центрируя его в центре прямоугольника.
            float textX = centerX - textSize.Width / 2;
            float textY = centerY - textSize.Height / 2;

            // 6. Обводка текста
            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, new PointF(textX, textY), StringFormat.GenericDefault);

            // 7. Отображаем текст на Graphics.
            g.DrawString(text, font, brush, textX, textY);
        }
        public enum ResizeHandle
        {
            None,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
        
    }
}
[Serializable]
public class AppSettings
{
    public bool CenterLines { get; set; }
    public bool GridLines { get; set; }
    public bool HideRect { get; set; }
    public bool IsShowCoordinateOfRectangleCenter { get; set; }
    public int RotationValue { get; set; }
    public float ScaleFactor { get; set; }
    public float ZoomFactor { get; set; }
    public float Angle { get; set; }
    public bool IsMouseWheelZoom { get; set; }
    public Size FormSize { get; set; }
    public int ComboBoxAreaScaleIndex { get; set; }
    public Size ComboBoxSize { get; set; }
    public Rectangle Rect { get; set; }
    public float AspectRatio { get; set; }
    public Point DragStartPointForSelectionRectangle { get; set; }
    public bool IsDraggingRect { get; set; }
    public bool IsResizing { get; set; }
    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }


    public AppSettings()
    {
        CenterLines = false;
        GridLines = false;
        HideRect = false;
        IsShowCoordinateOfRectangleCenter = false;
        RotationValue = 0;
        ScaleFactor = 0;
        ZoomFactor = 1;
        Angle = 0;
        IsMouseWheelZoom = false;
        FormSize = new Size(0, 0);
        ComboBoxAreaScaleIndex = -1;
        ComboBoxSize = new Size(0, 0);
        Rect = new Rectangle(0, 0, 0, 0);
        AspectRatio = 1.0f;
        DragStartPointForSelectionRectangle = Point.Empty;
        IsDraggingRect = false;
        IsResizing = false;
        ImageWidth = 0;
        ImageHeight = 0;
    }
}

