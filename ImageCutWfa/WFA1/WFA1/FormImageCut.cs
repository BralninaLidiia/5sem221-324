using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection.Metadata;
using System.Windows.Forms;
using ImageCutLib;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static ImageCutLib.ImageCut;

namespace WFA1
{
    public partial class FormImageCut : Form
    {
        private ImageCut imageCut = new ImageCut();
        private Bitmap loadedImage;
        private Bitmap finalImage;
        float angle = 0;
        float aspectRatio;
        float scaleFactor = 0;
        float zoomFactor = 1;
        int pictureBoxForLoadImageWidth, pictureBoxForLoadImageHeight;
        private Bitmap originalImage;
        private bool isMouseWheelZoom = false;
        private int oldPictureBoxWidth;
        private int oldPictureBoxHeight;
        private bool isFirstResize = true;

        public FormImageCut()
        {
            InitializeComponent();
            pictureBoxForLoadImage.Paint += PictureBoxForLoadImage_Paint;
            pictureBoxForLoadImage.MouseDown += PictureBoxForLoadImage_MouseDown;
            pictureBoxForLoadImage.MouseMove += PictureBoxForLoadImage_MouseMove;
            pictureBoxForLoadImage.MouseUp += PictureBoxForLoadImage_MouseUp;
            pictureBoxForLoadImage.MouseWheel += new MouseEventHandler(pictureBoxForLoadImage_MouseWheel);
        }
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imageCut = new ImageCut();
                    // Очищаем PictureBox перед загрузкой нового изображения
                    pictureBoxForLoadImage.Image = null;
                    pictureBoxForLoadImage.Invalidate(); // Заставляем PictureBox перерисоваться, чтобы очиститься визуально

                    // Загружаем исходное изображение
                    originalImage = new Bitmap(openFileDialog.FileName);

                    // Устанавливаем изображение в PictureBox и режим масштабирования
                    pictureBoxForLoadImage.Image = originalImage;
                    pictureBoxForLoadImage.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBoxForLoadImage.BackColor = Color.White;


                    rotationTrackBar.Value = 0;
                    centerLinesBox.Checked = false;
                    gridLinesBox.Checked = false;
                    hideRectBox.Checked = false;
                    checkBoxShowCoordinateOfRectangleCenter.Checked = false;
                    scaleFactor = 0;
                    zoomFactor = 1;
                    angle = 0;
                    comboBoxAreaScale.SelectedIndex = -1;

                    loadedImage = new Bitmap(pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                    //Создаем Graphics из нового Bitmap
                    using (Graphics g = Graphics.FromImage(loadedImage))
                    {
                        g.Clear(pictureBoxForLoadImage.BackColor);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        Rectangle destRect = GetScaledRect(originalImage.Size, pictureBoxForLoadImage.ClientSize);

                        //  Теперь рисуем originalImage В loadedImage
                        g.DrawImage(originalImage, destRect, new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
                    }

                    isMouseWheelZoom = false;
                    pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;//нужно запомнить чтобы при ресайзе окна мы могли корректно расчитать масштаб для прямоугольника
                    pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
                    finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                    pictureBoxForLoadImage.Image = finalImage;
                    pictureBoxForLoadImage.Invalidate();
                    ImageSizeLabel.Text = $"Размер изображения: \n{originalImage.Width}x{originalImage.Height} пикселей";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                }
            }

        }
        private void RefreshImage()
        {
            if (originalImage == null) return;
            //Создаем Bitmap для хранения масштабированного изображения
            loadedImage = new Bitmap(pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            //Создаем Graphics из нового Bitmap

            using (Graphics g = Graphics.FromImage(loadedImage))
            {
                g.Clear(pictureBoxForLoadImage.BackColor);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle destRect = GetScaledRect(originalImage.Size, pictureBoxForLoadImage.ClientSize);

                //  Теперь рисуем originalImage В loadedImage
                g.DrawImage(originalImage, destRect, new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
            }

            isMouseWheelZoom = false;

            finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            pictureBoxForLoadImage.Image = finalImage;
            pictureBoxForLoadImage.Invalidate();
        }
        private void PictureBoxForLoadImage_MouseDown(object sender, MouseEventArgs e)
        {
            imageCut.StartInteraction(e.Location);
        }

        private void PictureBoxForLoadImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (imageCut.IsDraggingRect)
                {
                    imageCut.Drag(e.Location, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                }
                else if (imageCut.IsResizing)
                {
                    imageCut.Resize(e.Location, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                    SelectedRectSizeLabel.Text = $"Размер области: \n{imageCut.SelectionRectangle.Width}x{imageCut.SelectionRectangle.Height} пикселей";
                    imageCut.StartPointForSelectionRectangle = e.Location;

                }
                pictureBoxForLoadImage.Invalidate();
            }
            if (imageCut.GetResizeHandle(e.Location) == ResizeHandle.TopLeft
                || imageCut.GetResizeHandle(e.Location) == ResizeHandle.BottomRight)
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (imageCut.GetResizeHandle(e.Location) == ResizeHandle.TopRight ||
                imageCut.GetResizeHandle(e.Location) == ResizeHandle.BottomLeft)
            {
                Cursor = Cursors.SizeNESW;
            }
            else if (imageCut.SelectionRectangle.Contains(e.Location))
            {
                Cursor = Cursors.Hand;

            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        private void pictureBoxForLoadImage_MouseWheel(object sender, MouseEventArgs e)
        {
            scaleFactor = e.Delta;
            isMouseWheelZoom = true;
            finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            pictureBoxForLoadImage.Image = finalImage;
            pictureBoxForLoadImage.Invalidate();
        }

        private void PictureBoxForLoadImage_MouseUp(object sender, MouseEventArgs e)
        {

            imageCut.StopInteraction();
        }

        private void centerLines_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxForLoadImage.Invalidate();
        }
        private void gridLinesBox_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxForLoadImage.Invalidate();
        }
        private void hideRectBox_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxForLoadImage.Invalidate();
        }

        private void buttonCrop_Click_1(object sender, EventArgs e)
        {
            if (finalImage != null)
            {
                try
                {
                    Bitmap croppedImage = imageCut.CropImage(
                        finalImage,
                        imageCut.SelectionRectangle,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height
                    );

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        imageCut.SaveImage(croppedImage, saveFileDialog.FileName);
                        MessageBox.Show("Изображение успешно сохранено!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обрезки изображения: " + ex.Message);
                }
            }
            else if (loadedImage != null)
            {
                try
                {
                    //if (rotationTrackBar.Value == 0)
                    //    finalBitmap = loadedImage;
                    Bitmap croppedImage = imageCut.CropImage(
                        loadedImage,
                        imageCut.SelectionRectangle,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height
                    );

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp"
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        imageCut.SaveImage(croppedImage, saveFileDialog.FileName);
                        MessageBox.Show("Изображение успешно сохранено!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка обрезки изображения: " + ex.Message);
                }
            }

        }

        private void comboBoxAreaScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Сохраняем текущее значение StartRectanglePoint
            Point currentStartRectanglePoint = imageCut.SelectionRectangle.Location;


            // Расчёт zoomFactorForRect для масштабирования выделенной области
            float zoomFactorForRect = Math.Min((float)pictureBoxForLoadImage.Width / originalImage.Width,
                                       (float)pictureBoxForLoadImage.Height / originalImage.Height);

            // Получаем отмасштабированные размеры изображения с учётом zoomFactor
            int scaledWidth = (int)(originalImage.Width * zoomFactorForRect);
            int scaledHeight = (int)(originalImage.Height * zoomFactorForRect);

            switch (comboBoxAreaScale.SelectedIndex)
            {
                case 0:
                    imageCut.InitializeSelectionRectangle(
                        scaledWidth,  // Используем отмасштабированную ширину
                        scaledHeight,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height, // Используем отмасштабированную высоту
                        currentStartRectanglePoint, // Используем текущую позицию (центр или предыдущую)
                        1f / 1f // Пропорция 1:1
                    );
                    aspectRatio = 1f / 1f;
                    break;
                case 1:
                    imageCut.InitializeSelectionRectangle(
                        scaledWidth,
                        scaledHeight,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height,
                        currentStartRectanglePoint,
                        4f / 3f // Пропорция 4:3
                    );
                    aspectRatio = 4f / 3f;
                    break;
                case 2:
                    imageCut.InitializeSelectionRectangle(
                        scaledWidth,
                        scaledHeight,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height,
                        currentStartRectanglePoint,
                        3f / 4f // Пропорция 3:4
                    );
                    aspectRatio = 3f / 4f;
                    break;
                case 3:
                    imageCut.InitializeSelectionRectangle(
                        scaledWidth,
                        scaledHeight,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height,
                        currentStartRectanglePoint,
                        9f / 16f // Пропорция 16:9
                    );
                    aspectRatio = 9f / 16f;
                    break;
                case 4:
                    imageCut.InitializeSelectionRectangle(
                        scaledWidth,
                        scaledHeight,
                        pictureBoxForLoadImage.Width,
                        pictureBoxForLoadImage.Height,
                        currentStartRectanglePoint,
                        16f / 9f // Пропорция 9:16
                    );
                    aspectRatio = 16f / 9f;
                    break;
            }

            // Обновляем информацию о размере области
            SelectedRectSizeLabel.Text = $"Размер области: \n{imageCut.SelectionRectangle.Width}x{imageCut.SelectionRectangle.Height} пикселей";

            // Перерисовываем PictureBox
            pictureBoxForLoadImage.Invalidate();
        }


        private void buToCenter_Click(object sender, EventArgs e)
        {
            imageCut.AreaToCenter(pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            pictureBoxForLoadImage.Invalidate();
        }

        private void rotationTrackBar_Scroll(object sender, EventArgs e)
        {
            if (loadedImage != null)
            {
                angle = rotationTrackBar.Value;
                finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height); // Передаем zoomFactor по ссылке
                pictureBoxForLoadImage.Image = finalImage;
                pictureBoxForLoadImage.Invalidate();
                isMouseWheelZoom = false;
            }
        }

        private Rectangle GetScaledRect(Size imageSize, Size containerSize)
        {
            float aspectRatioImage = (float)imageSize.Width / imageSize.Height;
            float aspectRatioContainer = (float)containerSize.Width / containerSize.Height;
            Rectangle rect;

            if (aspectRatioImage > aspectRatioContainer)
            {
                int width = containerSize.Width;
                int height = (int)(width / aspectRatioImage);
                int y = (containerSize.Height - height) / 2;
                rect = new Rectangle(0, y, width, height);
            }
            else
            {
                int height = containerSize.Height;
                int width = (int)(height * aspectRatioImage);
                int x = (containerSize.Width - width) / 2;
                rect = new Rectangle(x, 0, width, height);
            }

            return rect;
        }


        private void PictureBoxForLoadImage_Paint(object sender, PaintEventArgs e)
        {
            // Очищаем область рисования (фон PictureBox)
            e.Graphics.Clear(pictureBoxForLoadImage.BackColor);

            // Отрисовываем изображение, если оно есть.
            if (finalImage != null)
            {
                e.Graphics.DrawImage(finalImage, 0, 0, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            }

            //Отрисовка центральных линий
            if (centerLinesBox.Checked && loadedImage != null)
            {
                imageCut.CentralLines_Paint(e.Graphics, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            }
            if (gridLinesBox.Checked && loadedImage != null)
            {
                imageCut.GridLines_Paint(e.Graphics, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height, imageCut.StartPointForSelectionRectangle);
            }
            if(checkBoxShowCoordinateOfRectangleCenter.Checked && loadedImage != null)
            {
                imageCut.ShowCoordinateOfRectangleCenter(e.Graphics);
            }
            //Отрисовка выделенной области
            if (loadedImage != null && !hideRectBox.Checked)
            {
                imageCut.DrawSelectionRectangle(e.Graphics);
            }
        }

        //private void Form1_Resize(object sender, EventArgs e) //этот вариант будто бы из-за перевода во флоат теряет точность, что-то не так если после ресайза пытаемся поменять размер окна, неправильно расчитывается перемещение области
        //{
        //    if (originalImage == null || imageCut.SelectionRectangle == Rectangle.Empty)
        //    {
        //        return;
        //    }
        //    int scaledWidth, scaledHeight;
        //    Point currentStartPoint = new Point();
        //    float zoomFactorForRectX = (float)pictureBoxForLoadImage.Width / pictureBoxForLoadImageWidth;
        //    float zoomFactorForRectY = (float)pictureBoxForLoadImage.Height / pictureBoxForLoadImageHeight;

        //    if (originalImage.Width > originalImage.Height)
        //    {
        //        scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectX);
        //        scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectX);
        //    }
        //    else
        //    {
        //        scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectY);
        //        scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectY);
        //    }

        //    // Пересчитываем размеры выделенного прямоугольника
        //    currentStartPoint.X = (int)(imageCut.StartPointForSelectionRectangle.X * zoomFactorForRectX);// Текущая точка начала прямоугольника
        //    currentStartPoint.Y = (int)(imageCut.StartPointForSelectionRectangle.Y * zoomFactorForRectY);
        //    imageCut.SelectionRectangle = new Rectangle(currentStartPoint.X, currentStartPoint.Y, scaledWidth, scaledHeight);
        //    imageCut.StartPointForSelectionRectangle = currentStartPoint;
        //    // Обновляем PictureBox
        //    RefreshImage();
        //    pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;
        //    pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
        //    SelectedRectSizeLabel.Text = $"Размер области: \n{imageCut.SelectionRectangle.Width}x{imageCut.SelectionRectangle.Height} пикселей";
        //    pictureBoxForLoadImage.Invalidate();
        //}
        //private void Form1_Resize(object sender, EventArgs e) //этот вариант будто бы из-за перевода во флоат теряет точность, что-то не так если после ресайза пытаемся поменять размер окна, неправильно расчитывается перемещение области
        //{
        //    if (originalImage == null || imageCut.SelectionRectangle == Rectangle.Empty)
        //    {
        //        return;
        //    }
        //    int scaledWidth, scaledHeight;
        //    Point currentStartPoint = new Point();
        //    float zoomFactorForRectX = (float)pictureBoxForLoadImage.Width / pictureBoxForLoadImageWidth;
        //    float zoomFactorForRectY = (float)pictureBoxForLoadImage.Height / pictureBoxForLoadImageHeight;

        //    if (originalImage.Width > originalImage.Height)
        //    {
        //        scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectX);
        //        scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectX);
        //    }
        //    else
        //    {
        //        scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectY);
        //        scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectY);
        //    }

        //    // Пересчитываем размеры выделенного прямоугольника
        //    currentStartPoint.X = (int)(imageCut.StartPointForSelectionRectangle.X * zoomFactorForRectX);// Текущая точка начала прямоугольника
        //    currentStartPoint.Y = (int)(imageCut.StartPointForSelectionRectangle.Y * zoomFactorForRectY);
        //    imageCut.SelectionRectangle = new Rectangle(currentStartPoint.X, currentStartPoint.Y, scaledWidth, scaledHeight);
        //    imageCut.StartPointForSelectionRectangle = currentStartPoint;
        //    // Обновляем PictureBox
        //    RefreshImage();
        //    pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;
        //    pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
        //    SelectedRectSizeLabel.Text = $"Размер области: \n{imageCut.SelectionRectangle.Width}x{imageCut.SelectionRectangle.Height} пикселей";
        //    pictureBoxForLoadImage.Invalidate();
        //}
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                return;
            }

            if (imageCut.SelectionRectangle == Rectangle.Empty)
            {
                // Если нет выделенного прямоугольника, просто перерисовываем изображение
                RefreshImage();
                pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;
                pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
                return;
            }

            // Код для пересчета положения и размеров прямоугольника, если он есть
            int scaledWidth, scaledHeight;
            Point currentStartPoint = new Point();
            float zoomFactorForRectX = (float)pictureBoxForLoadImage.Width / pictureBoxForLoadImageWidth;
            float zoomFactorForRectY = (float)pictureBoxForLoadImage.Height / pictureBoxForLoadImageHeight;

            if (originalImage.Width > originalImage.Height)
            {
                scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectX);
                scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectX);
            }
            else
            {
                scaledWidth = (int)(imageCut.SelectionWidth * zoomFactorForRectY);
                scaledHeight = (int)(imageCut.SelectionHeight * zoomFactorForRectY);
            }

            currentStartPoint.X = (int)(imageCut.StartPointForSelectionRectangle.X * zoomFactorForRectX);
            currentStartPoint.Y = (int)(imageCut.StartPointForSelectionRectangle.Y * zoomFactorForRectY);
            imageCut.SelectionRectangle = new Rectangle(currentStartPoint.X, currentStartPoint.Y, scaledWidth, scaledHeight);
            imageCut.StartPointForSelectionRectangle = currentStartPoint;

            RefreshImage(); // Обновляем изображение

            pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;
            pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
            SelectedRectSizeLabel.Text = $"Размер области: \n{imageCut.SelectionRectangle.Width}x{imageCut.SelectionRectangle.Height} пикселей";
            pictureBoxForLoadImage.Invalidate();
        }
        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
                DefaultExt = "txt",
                AddExtension = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveSettings(saveFileDialog.FileName);
            }
        }
        private void SaveSettings(string filePath)
        {
            AppSettings settings = new AppSettings();
            settings.CenterLines = centerLinesBox.Checked;
            settings.GridLines = gridLinesBox.Checked;
            settings.HideRect = hideRectBox.Checked;
            settings.IsShowCoordinateOfRectangleCenter = checkBoxShowCoordinateOfRectangleCenter.Checked;
            settings.RotationValue = rotationTrackBar.Value;
            settings.ScaleFactor = scaleFactor;
            settings.ZoomFactor = zoomFactor;
            settings.Angle = angle;
            settings.IsMouseWheelZoom = isMouseWheelZoom;
            settings.FormSize = this.Size;
            settings.ComboBoxAreaScaleIndex = comboBoxAreaScale.SelectedIndex;
            settings.ComboBoxSize = comboBoxAreaScale.Size;
            settings.Rect = imageCut.SelectionRectangle;
            settings.AspectRatio = aspectRatio;
            settings.DragStartPointForSelectionRectangle = imageCut.StartPointForSelectionRectangle;
            settings.IsDraggingRect = imageCut.IsDraggingRect;
            settings.IsResizing = imageCut.IsResizing;
            settings.ImageWidth = pictureBoxForLoadImage.Width;
            settings.ImageHeight = pictureBoxForLoadImage.Height;



            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"CenterLines: {settings.CenterLines}");
                    writer.WriteLine($"GridLines: {settings.GridLines}");
                    writer.WriteLine($"HideRect: {settings.HideRect}");
                    writer.WriteLine($"IsShowCoordinateOfRectangleCenter: {settings.IsShowCoordinateOfRectangleCenter}");
                    writer.WriteLine($"RotationValue: {settings.RotationValue}");
                    writer.WriteLine($"ScaleFactor: {settings.ScaleFactor}");
                    writer.WriteLine($"ZoomFactor: {settings.ZoomFactor}");
                    writer.WriteLine($"Angle: {settings.Angle}");
                    writer.WriteLine($"IsMouseWheelZoom: {settings.IsMouseWheelZoom}");
                    writer.WriteLine($"FormSize: {settings.FormSize.Width},{settings.FormSize.Height}");
                    writer.WriteLine($"ComboBoxAreaScaleIndex: {settings.ComboBoxAreaScaleIndex}");
                    writer.WriteLine($"ComboBoxSize: {settings.ComboBoxSize.Width},{settings.ComboBoxSize.Height}");
                    writer.WriteLine($"Rect: {settings.Rect.X},{settings.Rect.Y},{settings.Rect.Width},{settings.Rect.Height}");
                    writer.WriteLine($"AspectRatio: {settings.AspectRatio}");
                    writer.WriteLine($"DragStartPointForSelectionRectangle: {settings.DragStartPointForSelectionRectangle.X},{settings.DragStartPointForSelectionRectangle.Y}");
                    writer.WriteLine($"IsDraggingRect: {settings.IsDraggingRect}");
                    writer.WriteLine($"IsResizing: {settings.IsResizing}");
                    writer.WriteLine($"ImageSize: {settings.ImageWidth},{settings.ImageHeight}");
                }

                MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonLoadSettings_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadSettings(openFileDialog.FileName);
            }
        }
        private void LoadSettings(string filePath)
        {
            AppSettings settings = new AppSettings();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("CenterLines:"))
                        {
                            settings.CenterLines = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("GridLines:"))
                        {
                            settings.GridLines = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("HideRect:"))
                        {
                            settings.HideRect = bool.Parse(line.Split(':')[1].Trim());
                        }else if (line.StartsWith("IsShowCoordinateOfRectangleCenter:"))
                        {
                            settings.IsShowCoordinateOfRectangleCenter = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("RotationValue:"))
                        {
                            settings.RotationValue = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("ScaleFactor:"))
                        {
                            settings.ScaleFactor = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("ZoomFactor:"))
                        {
                            settings.ZoomFactor = float.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("Angle:"))
                        {
                            settings.Angle = float.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("IsMouseWheelZoom:"))
                        {
                            settings.IsMouseWheelZoom = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("FormSize:"))
                        {
                            string[] sizeValues = line.Split(':')[1].Trim().Split(',');
                            if (sizeValues.Length == 2)
                                settings.FormSize = new Size(int.Parse(sizeValues[0]), int.Parse(sizeValues[1]));
                        }
                        else if (line.StartsWith("ComboBoxAreaScaleIndex:"))
                        {
                            settings.ComboBoxAreaScaleIndex = int.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("ComboBoxSize:"))
                        {
                            string[] sizeValues = line.Split(':')[1].Trim().Split(',');
                            if (sizeValues.Length == 2)
                                settings.ComboBoxSize = new Size(int.Parse(sizeValues[0]), int.Parse(sizeValues[1]));
                        }
                        else if (line.StartsWith("Rect:"))
                        {
                            string[] rectValues = line.Split(':')[1].Trim().Split(',');
                            if (rectValues.Length == 4)
                                settings.Rect = new Rectangle(int.Parse(rectValues[0]), int.Parse(rectValues[1]), int.Parse(rectValues[2]), int.Parse(rectValues[3]));
                        }
                        else if (line.StartsWith("AspectRatio:"))
                        {
                            settings.AspectRatio = float.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("DragStartPointForSelectionRectangle:"))
                        {
                            string[] pointValues = line.Split(':')[1].Trim().Split(',');
                            if (pointValues.Length == 2)
                                settings.DragStartPointForSelectionRectangle = new Point(int.Parse(pointValues[0]), int.Parse(pointValues[1]));
                        }
                        else if (line.StartsWith("IsDraggingRect:"))
                        {
                            settings.IsDraggingRect = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("IsResizing:"))
                        {
                            settings.IsResizing = bool.Parse(line.Split(':')[1].Trim());
                        }
                        else if (line.StartsWith("ImageSize:"))
                        {
                            string[] imageSizeValues = line.Split(':')[1].Trim().Split(',');
                            if (imageSizeValues.Length == 2)
                            {
                                settings.ImageWidth = int.Parse(imageSizeValues[0]);
                                settings.ImageHeight = int.Parse(imageSizeValues[1]);
                            }

                        }
                    }
                }

                centerLinesBox.Checked = settings.CenterLines;
                gridLinesBox.Checked = settings.GridLines;
                hideRectBox.Checked = settings.HideRect;
                checkBoxShowCoordinateOfRectangleCenter.Checked = settings.IsShowCoordinateOfRectangleCenter;
                rotationTrackBar.Value = settings.RotationValue;
                scaleFactor = settings.ScaleFactor;
                zoomFactor = settings.ZoomFactor;
                angle = settings.Angle;
                isMouseWheelZoom = settings.IsMouseWheelZoom;
                this.Size = settings.FormSize;
                comboBoxAreaScale.SelectedIndex = settings.ComboBoxAreaScaleIndex;
                comboBoxAreaScale.Size = settings.ComboBoxSize;

                pictureBoxForLoadImage.Size = new Size(settings.ImageWidth, settings.ImageHeight);
                pictureBoxForLoadImageWidth = settings.ImageWidth;
                pictureBoxForLoadImageHeight = settings.ImageHeight;
                if (pictureBoxForLoadImage.Image != null)
                {
                    loadedImage = new Bitmap(pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                    using (Graphics g = Graphics.FromImage(loadedImage))
                    {
                        g.Clear(pictureBoxForLoadImage.BackColor);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        Rectangle destRect = GetScaledRect(originalImage.Size, pictureBoxForLoadImage.ClientSize);
                        g.DrawImage(originalImage, destRect, new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
                    }
                    if (settings.Rect.Width > 0 && settings.Rect.Height > 0)
                    {
                        imageCut.InitializeSelectionRectangle(loadedImage.Width, loadedImage.Height, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height, settings.DragStartPointForSelectionRectangle, settings.AspectRatio);
                        imageCut.SelectionRectangle = settings.Rect;
                    }
                    imageCut.IsDraggingRect = settings.IsDraggingRect;
                    imageCut.IsResizing = settings.IsResizing;
                }

                finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
                pictureBoxForLoadImage.Image = finalImage;
                pictureBoxForLoadImage.Invalidate();

                MessageBox.Show("Settings loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonResetSettings_Click(object sender, EventArgs e)
        {
            imageCut = new ImageCut();
            pictureBoxForLoadImage.Image = finalImage;
            rotationTrackBar.Value = 0;
            centerLinesBox.Checked = false;
            gridLinesBox.Checked = false;
            hideRectBox.Checked = false;
            checkBoxShowCoordinateOfRectangleCenter.Checked = false;
            scaleFactor = 0;
            zoomFactor = 1;
            angle = 0;
            comboBoxAreaScale.SelectedIndex = -1;
            isMouseWheelZoom = false;
            using (Graphics g = Graphics.FromImage(loadedImage))
            {
                g.Clear(pictureBoxForLoadImage.BackColor);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle destRect = GetScaledRect(originalImage.Size, pictureBoxForLoadImage.ClientSize);

                //  Теперь рисуем originalImage В loadedImage
                g.DrawImage(originalImage, destRect, new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
            }
            pictureBoxForLoadImageWidth = pictureBoxForLoadImage.Width;//нужно запомнить чтобы при ресайзе окна мы могли корректно расчитать масштаб для прямоугольника
            pictureBoxForLoadImageHeight = pictureBoxForLoadImage.Height;
            finalImage = imageCut.ChangeImage(loadedImage, scaleFactor, angle, ref zoomFactor, isMouseWheelZoom, pictureBoxForLoadImage.Width, pictureBoxForLoadImage.Height);
            pictureBoxForLoadImage.Image = finalImage;
            pictureBoxForLoadImage.Invalidate();
        }

        private void checkBoxShowCoordinateOfRectangleCenter_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxForLoadImage.Invalidate();
        }
    }
}
