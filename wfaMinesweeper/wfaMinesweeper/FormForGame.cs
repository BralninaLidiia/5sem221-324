using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Windows.Forms;
using MinesweeperLib; // Добавляем using для нашей библиотеки
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;


namespace wfaMinesweeper
{
    public partial class FormForGame : Form
    {
        Panel areaPanel = new Panel();
        Panel firstArmyPanel;
        Panel secondArmyPanel;
        private Button finishPlacementButton;
        private Button startButton;
        private Minesweeper _miner;

        public FormForGame(Minesweeper miner)
        {
            InitializeComponent();
            _miner = miner;
            CreateArea();
        }
        private void CreateStartButton(int Height)
        {
            startButton = new Button();
            startButton.Text = "Начать раунд";
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Size = new Size(220, 60);
            startButton.Font = new Font(startButton.Font.FontFamily, 12);
            startButton.FlatAppearance.BorderSize = 2;
            startButton.ForeColor = ColorTranslator.FromHtml("#331b17");
            startButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            startButton.BackColor = ColorTranslator.FromHtml("#D4A59A");
            startButton.Location = new Point((this.ClientSize.Width - startButton.Width) / 8 - startButton.Width/2, Height+ startButton.Height/2);
            startButton.Click += StartButton_Click;
            firstArmyPanel.Controls.Add(startButton);
        }
        private async void StartButton_Click(object sender, EventArgs e)
        {
            _miner.PayerNumber = 1;
            if (_miner.Gamemod == 2 || (_miner.Gamemod == 1 && _miner.RoundCounter % 2 == 0))
            {
                PlayerReadyForm playerReadyForm = new PlayerReadyForm(_miner);
                playerReadyForm.Opacity = 1;
                playerReadyForm.Show();

            }
                foreach (Control control in this.Controls.OfType<Control>().Reverse())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                CreateArea();

                
        }
        private void CreateArea()
        {
            if (startButton!= null)
                startButton.Visible = false;
            if (_miner.FirstArmy == null) 
                _miner.StartGame();

            areaPanel = new Panel();
            int imageWidth = (this.Width / 2) / _miner.AreaSize;
            areaPanel.Size = new Size(imageWidth * _miner.AreaSize, this.Height);
            areaPanel.Location = new Point((this.Width - areaPanel.Width) / 2 + (this.Width / 2 - areaPanel.Width) / 2 - 8, 0);
            if (_miner.AreaSize == 5)
                areaPanel.Location = new Point((this.Width - areaPanel.Width) / 2 + (this.Width / 2 - areaPanel.Width) / 2 - 9, 0);
            this.Controls.Add(areaPanel);
            int imageHeight = 0;
            int pictureBoxesPerRow = _miner.AreaSize;

            for (int i = 0; i < _miner.AreaSize * _miner.AreaSize; i++)
            {
                PictureBox pb = new PictureBox();

                pb.SizeMode = PictureBoxSizeMode.Zoom;
                if (_miner.FirstArmy!=null && _miner.FirstArmy[i] == -1 && _miner.PayerNumber == 1 && _miner.RoundCounter % 2 == 1)
                {
                    pb.BackColor = ColorTranslator.FromHtml("#a0203a");
                    pb.Enabled = false;
                }
                else if (_miner.SecondArmy != null && _miner.SecondArmy[i] == -1 && _miner.PayerNumber == 2 && _miner.RoundCounter % 2 == 1)
                {
                    pb.BackColor = ColorTranslator.FromHtml("#1773cd");
                    pb.Enabled = false;
                }
                else if (_miner.FirstArmy != null && _miner.FirstArmy[i] == -1 && _miner.PayerNumber == 1 && _miner.RoundCounter % 2 != 1)
                {
                    pb.BackColor = ColorTranslator.FromHtml("#1773cd");
                    pb.Enabled = false;
                }
                else if (_miner.SecondArmy != null && _miner.SecondArmy[i] == -1 && _miner.PayerNumber == 2 && _miner.RoundCounter % 2 != 1)
                {
                    pb.BackColor = ColorTranslator.FromHtml("#a0203a");
                    pb.Enabled = false;
                }
                else
                    pb.BackColor = ColorTranslator.FromHtml("#dcc3e8");

                imageHeight = imageWidth;

                pb.Width = imageWidth;
                pb.Height = imageHeight;

                int row = i / pictureBoxesPerRow;
                int col = i % pictureBoxesPerRow;

                int x = col * (imageWidth);
                int y = row * (imageHeight);

                pb.Location = new Point(x, y);
                pb.Tag = i; // Сохраняем порядковый номер в Tag
                pb.Paint += (sender, e) =>
                {
                    int borderWidth = 1; // Толщина 2 мм 
                    Color borderColor = ColorTranslator.FromHtml("#5C2018"); // Цвет границы (можно изменить)

                    // Переводим мм в пиксели (приблизительно) - можно уточнить на разных DPI
                    float pixelsPerMM = e.Graphics.DpiX / 25.4f;
                    int borderPixels = /*(int)(borderWidth * pixelsPerMM)*/2;


                    using (Pen pen = new Pen(borderColor, borderPixels))
                    {
                        Rectangle rect = pb.ClientRectangle;
                        rect.Inflate(-borderPixels / 2, -borderPixels / 2);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                };
                pb.Click += PictureBox_Click; // Подписываемся на событие Click
                areaPanel.Controls.Add(pb);
            }

            CreatePanelsForArmy(1);
            CreatePanelsForArmy(2);
        }


        private async void StartChecking()
        {
            if (_miner.PayerNumber == 2)
            {
                for (int i = 0; i < _miner.FirstArmy.Length; i++)
                {
                    // Найдем PictureBox по индексу
                    PictureBox pb = areaPanel.Controls.OfType<PictureBox>().FirstOrDefault(p => (int)p.Tag == i);
                    pb.Enabled = false;
                    if (pb != null)
                    {
                        if ((_miner.FirstArmy[i] <= 0 && _miner.RoundCounter % 2 == 1) 
                            || (_miner.SecondArmy[i] <= 0 && _miner.RoundCounter % 2 != 1)
                            || (_miner.SecondArmy[i] <= 0 && _miner.RoundCounter % 2 == 1) 
                            || (_miner.FirstArmy[i] <= 0 && _miner.RoundCounter % 2 != 1))
                        {
                            pb.BackColor = ColorTranslator.FromHtml("#dcc3e8");// Отображение хода первого игрока
                        }
                        if ((_miner.FirstArmy[i] > 0 && _miner.RoundCounter % 2 == 1) || (_miner.SecondArmy[i] > 0 && _miner.RoundCounter % 2 != 1))
                        {
                            pb.BackColor = ColorTranslator.FromHtml("#1e90ff"); // Отображение хода первого игрока
                        }
                        if ((_miner.SecondArmy[i] > 0 && _miner.RoundCounter % 2 == 1) || (_miner.FirstArmy[i] > 0 && _miner.RoundCounter % 2 != 1))
                        {
                            pb.BackColor = ColorTranslator.FromHtml("#dc143c");
                        }

                             // Отображение хода второго игрока

                    }
                }
            }
            
            for (int i = 0; i < _miner.FirstArmy.Length; i++)
            {
                PictureBox pb = areaPanel.Controls.OfType<PictureBox>().FirstOrDefault(p => (int)p.Tag == i);
                if (_miner.FirstArmy[i] !=0 )
                {
                    
                    bool battleResult = _miner.Check(i);
                    if (battleResult)
                    {
                        pb.BackColor = Color.Purple;
                        //MessageBox.Show($"Бум! Осталось {_miner.FightersSecondArmy} юнитов у второго игрока и {_miner.FightersFirstArmy} юнитов у первого игрока");
                        if (_miner.FightersFirstArmy == 0)
                            break;
                    }
                }

                if (_miner.SecondArmy[i] != 0 )
                    _miner.UpdateArmyArray(i);

            }
            // Обработка оставшихся бомб после всех раундов
            for (int i = 0; i < _miner.FirstArmy.Length; i++)
            {
                if (_miner.FirstArmy[i] > 0)
                    _miner.FirstArmy[i] = -1;
                if (_miner.SecondArmy[i] > 0)
                    _miner.SecondArmy[i] = -1;
            }
            UpdateArmyPicture();
            if (_miner.FightersFirstArmy == 0 || _miner.FightersSecondArmy == 0)
            {
                this.Controls.Remove(finishPlacementButton);
                firstArmyPanel.Controls.Remove(startButton);
                EndGameForm formEnd = new EndGameForm(_miner);
                formEnd.Show();
                await Task.Delay(10000);
                this.Close();
            }
        }
        private void UpdateArmyPicture()
        {
            var pictureBoxes1 = firstArmyPanel.Controls.OfType<PictureBox>().OrderByDescending(p => (int)p.Tag).ToList();
            var pictureBoxes2 = secondArmyPanel.Controls.OfType<PictureBox>().OrderByDescending(p => (int)p.Tag).ToList();
            if (_miner.RoundCounter % 2 != 1)
            {
                int index1 = 0;
                for (int i = _miner.FightersCount - _miner.FightersFirstArmy - 1; i >= 0; i--)
                {
                    if (index1 < pictureBoxes1.Count)
                    {
                        PictureBox pb1 = pictureBoxes1[index1];
                        if (pb1 != null)
                        {
                            pb1.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                        }
                        index1++;

                    }
                }
                int index2 = 0;
                for (int i = _miner.FightersCount - _miner.FightersSecondArmy - 1; i >= 0; i--)
                {
                    if (index2 < pictureBoxes2.Count)
                    {
                        PictureBox pb2 = pictureBoxes2[index2];
                        if (pb2 != null)
                        {
                            pb2.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                        }
                        index2++;

                    }
                }
            }
            if (_miner.RoundCounter % 2 == 1)
            {
                int index1 = 0;
                for (int i = _miner.FightersCount - _miner.FightersSecondArmy - 1; i >= 0; i--)
                {
                    if (index1 < pictureBoxes1.Count)
                    {
                        PictureBox pb1 = pictureBoxes1[index1];
                        if (pb1 != null)
                        {
                            pb1.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                        }
                        index1++;

                    }
                }
                int index2 = 0;
                for (int i = _miner.FightersCount - _miner.FightersFirstArmy - 1; i >= 0; i--)
                {
                    if (index2 < pictureBoxes2.Count)
                    {
                        PictureBox pb2 = pictureBoxes2[index2];
                        if (pb2 != null)
                        {
                            pb2.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                        }
                        index2++;

                    }
                }
            }
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (_miner.PayerNumber == 1)
            {
                if (sender is PictureBox pb)
                {
                    int index = (int)pb.Tag;
                    if (_miner.FirstArmy[index] > 0) // Если юнит уже установлен
                    {
                        int removedClickNumber = _miner.FirstArmy[index];
                        pb.BackColor = ColorTranslator.FromHtml("#dcc3e8");
                        _miner.FirstArmy[index] = 0;
                        pb.Enabled = true;
                        _miner.UpdateArmyArray(1, removedClickNumber);

                        if (finishPlacementButton != null && finishPlacementButton.Visible)
                        {
                            finishPlacementButton.Visible = false;
                        }
                    }
                    
                    else if (_miner.FirstArmy[index] == 0) // Если юнит не установлен
                    {
                        if (_miner.FirstArmy.Where(x => x > 0).Count() < _miner.FightersFirstArmy)
                        {
                            int clickNumber = _miner.GetNextClickNumber(1);
                            _miner.FirstArmy[index] = clickNumber;
                            if (_miner.RoundCounter % 2 == 1)
                                pb.BackColor = ColorTranslator.FromHtml("#dc143c");
                            else
                                pb.BackColor = ColorTranslator.FromHtml("#1e90ff");
                            _miner.UpdateArmyArray(1, clickNumber);
                            if (_miner.FirstArmy.Where(x => x > 0).Count() == _miner.FightersFirstArmy)
                                finishPlacementButton.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show($"Достигнут лимит юнитов.");
                        }
                    }
                }
            }
            else if (_miner.PayerNumber == 2)
            {
                if (sender is PictureBox pb)
                {
                    int index = (int)pb.Tag;
                    if (_miner.SecondArmy[index] > 0) // Если юнит уже установлен
                    {
                        int removedClickNumber = _miner.SecondArmy[index];
                        pb.BackColor = ColorTranslator.FromHtml("#dcc3e8");
                        _miner.SecondArmy[index] = 0;
                        pb.Enabled = true;
                        _miner.UpdateArmyArray(2, removedClickNumber);
                        if (finishPlacementButton != null && finishPlacementButton.Visible)
                        {
                            finishPlacementButton.Visible = false;
                        }
                    }
                    else if (_miner.SecondArmy[index] == 0) // Если юнит не установлен
                    {
                        if (_miner.SecondArmy.Where(x => x > 0).Count() < _miner.FightersSecondArmy)
                        {
                            int clickNumber = _miner.GetNextClickNumber(2);
                            _miner.SecondArmy[index] = clickNumber;
                            if (_miner.RoundCounter % 2 == 1)
                                pb.BackColor = ColorTranslator.FromHtml("#1e90ff");
                            else
                                pb.BackColor = ColorTranslator.FromHtml("#dc143c");
                            _miner.UpdateArmyArray(2, clickNumber);
                            if (_miner.SecondArmy.Where(x => x > 0).Count() == _miner.FightersSecondArmy)
                                finishPlacementButton.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show($"Достигнут лимит юнитов.");
                        }
                    }
                }
            }
        }

        private void CreatePanelsForArmy(int armyNumber)
        {
            Panel panel = new Panel();
            panel.AutoScroll = true;
            panel.Size = new Size((this.Width - areaPanel.Width) / 2 - 10, this.Height);

            if (armyNumber == 1)
            {
                panel.Location = new Point(10, 10);
                firstArmyPanel = panel;
            }
            else if (armyNumber == 2)
            {
                panel.Location = new Point(areaPanel.Location.X + areaPanel.Width + 12, 10);
                if (_miner.AreaSize == 5)
                {
                    panel.Location = new Point(areaPanel.Location.X + areaPanel.Width + 13, 10);
                }
                panel.Size = firstArmyPanel.Size;
                secondArmyPanel = panel;
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный номер армии");
                return;
            }

            this.Controls.Add(panel);

            // Создание PictureBox
            int imageWidth = panel.Width / 2 - 15;
            int imageHeight = 0;
            int spacing = 10;
            int pictureBoxesPerRow = 2;
            int row = 0;

            for (int i = 0; i < _miner.FightersCount; i++)
            {
                PictureBox pb = new PictureBox();
                try
                {   
                    if (armyNumber == 1)
                        pb.Image = Image.FromFile(_miner.FirstPlayerPicturePath);
                    else
                        pb.Image = Image.FromFile(_miner.SecondPlayerPicturePath);
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    Console.WriteLine("Ошибка: Не удалось загрузить изображение:" + ex.Message);
                    pb.BackColor = Color.LightGray; // Устанавливаем серый фон, если не удалось загрузить изображение
                }
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Tag =(i);

                imageHeight = (int)((double)imageWidth / pb.Image.Width * pb.Image.Height);

                pb.Width = imageWidth;
                pb.Height = imageHeight;

                row = i / pictureBoxesPerRow;
                int col = i % pictureBoxesPerRow;

                int x = col * (imageWidth + spacing);
                int y = row * (imageHeight + spacing);

                pb.Location = new Point(x, y);
                pb.BorderStyle = BorderStyle.None;  // Убираем дефолтную рамку
                pb.Paint += PictureBox_Paint;

                panel.Controls.Add(pb);
            }


            int heightForButton = (imageHeight * row + spacing * row) + (panel.Height - (imageHeight * row + spacing * row)) / 2 + 30;


            if (armyNumber == 1)
            {
                CreateStartButton(heightForButton);
            }
            else if (armyNumber == 2)
            {
                CreateFinishPlacementButton(heightForButton+10);
                var pictureBoxes1 = firstArmyPanel.Controls.OfType<PictureBox>().OrderByDescending(p => (int)p.Tag).ToList();
                var pictureBoxes2 = secondArmyPanel.Controls.OfType<PictureBox>().OrderByDescending(p => (int)p.Tag).ToList();
                if (_miner.RoundCounter % 2 == 1)
                {
                    int index1 = 0;
                    for (int i = _miner.FightersCount - _miner.FightersFirstArmy - 1; i >= 0; i--)
                    {
                        if (index1 < pictureBoxes1.Count)
                        {
                            PictureBox pb1 = pictureBoxes1[index1];
                            if (pb1 != null)
                            {
                                pb1.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                            }
                            index1++;

                        }
                    }
                    int index2 = 0;
                    for (int i = _miner.FightersCount - _miner.FightersSecondArmy - 1; i >= 0; i--)
                    {
                        if (index2 < pictureBoxes2.Count)
                        {
                            PictureBox pb2 = pictureBoxes2[index2];
                            if (pb2 != null)
                            {
                                pb2.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                            }
                            index2++;

                        }
                    }
                }
                if (_miner.RoundCounter % 2 != 1)
                {
                    int index1 = 0;
                    for (int i = _miner.FightersCount - _miner.FightersSecondArmy - 1; i >= 0; i--)
                    {
                        if (index1 < pictureBoxes1.Count)
                        {
                            PictureBox pb1 = pictureBoxes1[index1];
                            if (pb1 != null)
                            {
                                pb1.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                            }
                            index1++;

                        }
                    }
                    int index2 = 0;
                    for (int i = _miner.FightersCount - _miner.FightersFirstArmy - 1; i >= 0; i--)
                    {
                        if (index2 < pictureBoxes2.Count)
                        {
                            PictureBox pb2 = pictureBoxes2[index2];
                            if (pb2 != null)
                            {
                                pb2.Image = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\тестирование\\погиб.png");
                            }
                            index2++;

                        }
                    }
                }

            }
            
        }

        private void CreateFinishPlacementButton(int y)
        {
            finishPlacementButton = new Button();
            finishPlacementButton.FlatStyle = FlatStyle.Flat;
            finishPlacementButton.Size = new Size(220, 60);
            finishPlacementButton.Font = new Font(startButton.Font.FontFamily, 12);
            finishPlacementButton.FlatAppearance.BorderSize = 2;
            finishPlacementButton.ForeColor = ColorTranslator.FromHtml("#331b17");
            finishPlacementButton.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            finishPlacementButton.BackColor = ColorTranslator.FromHtml("#D4A59A");
            finishPlacementButton.Text = "Закончить расстановку";
            int x = secondArmyPanel.Location.X + secondArmyPanel.Width / 2 - finishPlacementButton.Width / 2;
            finishPlacementButton.Location = new Point(x, y+ finishPlacementButton.Height/2);
            finishPlacementButton.Click += FinishPlacementButton_Click;
            this.Controls.Add(finishPlacementButton);
            finishPlacementButton.BringToFront();// Перемещаем кнопку на передний план
            finishPlacementButton.Visible = false;
            this.Invalidate();
        }
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender; // Получаем PictureBox
            int borderWidth = 4; // Толщина рамки

            //Рисуем прямоугольник
            Rectangle rect = new Rectangle(0, 0, pb.Width - 1, pb.Height - 1);

            using (Pen pen = new Pen(ColorTranslator.FromHtml("#5C2018"), borderWidth))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private async void FinishPlacementButton_Click(object sender, EventArgs e)
        {
            if(_miner.Gamemod ==2)
            {
                int num = _miner.PayerNumber;
                if (num == 1)
                {
                    _miner.PayerNumber = 2;
                }
                else if (num == 2)
                {
                    _miner.RoundCounter += 1;
                    StartChecking();
                    _miner.PayerNumber = 1;
                    _miner.Swap();
                    finishPlacementButton.Visible = false;
                    return;
                }

                PlayerReadyForm playerReadyForm = new PlayerReadyForm(_miner);
                playerReadyForm.Show();
                await Task.Delay(500);
                foreach (Control control in this.Controls.OfType<Control>().Reverse())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                CreateArea();
            }
            else
            {
                int num = _miner.PayerNumber;
                if (num == 1)
                {
                    _miner.PayerNumber = 2;
                    if ( _miner.RoundCounter % 2 != 0)
                    {
                        _miner.ComputerPlayer();
                        finishPlacementButton.PerformClick();
                        foreach (Control control in this.Controls.OfType<Control>().Reverse())
                        {
                            this.Controls.Remove(control);
                            control.Dispose();
                        }
                        CreateArea();
                    }
                }
                else if (num == 2)
                {
                    _miner.RoundCounter += 1;
                    StartChecking();
                    _miner.PayerNumber = 1;
                    _miner.Swap();
                    //await Task.Delay(5000);
                    //startButton.PerformClick();
                    if (_miner.RoundCounter % 2 == 0)
                    {
                        _miner.ComputerPlayer();
                        finishPlacementButton.PerformClick();
                        //finishPlacementButton.PerformClick();
                        //_miner.PayerNumber = 2;
                        string arrayContent = string.Join(", ", _miner.FirstArmy);
                        MessageBox.Show($"Содержимое firstArmy массива: {arrayContent}");
                        string arrayConten2t = string.Join(", ", _miner.SecondArmy);
                        MessageBox.Show($"Содержимое secondArmy массива: {arrayConten2t}");
                        foreach (Control control in this.Controls.OfType<Control>().Reverse())
                        {
                            this.Controls.Remove(control);
                            control.Dispose();
                        }
                        CreateArea();
                    }
                    finishPlacementButton.Visible = false;
                    return;
                }
                if (_miner.RoundCounter % 2 != 0)
                {
                    PlayerReadyForm playerReadyForm = new PlayerReadyForm(_miner);
                    playerReadyForm.Show();
                }
                foreach (Control control in this.Controls.OfType<Control>().Reverse())
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
                CreateArea();
            }
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.SuspendLayout(); // Замораживаем перерисовку формы

            // Цикл для прохода и удаления всех элементов управления
            while (this.Controls.Count > 0)
            {
                Control control = this.Controls[0]; // Получаем первый элемент
                this.Controls.Remove(control);       // Удаляем его из коллекции
                control.Dispose();                    // Освобождаем ресурсы
            }

            this.ResumeLayout(false);
            CreateArea();
            Invalidate();
        }
    }

}

