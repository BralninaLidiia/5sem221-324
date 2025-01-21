using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinesweeperLib;
using TestStack.White;

namespace wfaMinesweeper
{

    public partial class FormForStartGame : Form
    {
        // Делаем свойство Miner публичным
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Minesweeper Miner { get; set; } = new Minesweeper();
        private int _currentImageIndex1 = 0;
        private int _currentImageIndex2 = 0;
        private List<string> _imagePaths;

        public FormForStartGame()
        {
            //label1.Location = new Point((ClientSize.Width - label1.Width) / 2, 30);
            
            InitializeComponent();
            LoadImages();
            UpdatePictureBox(1);
            UpdatePictureBox(2);
        }
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender; // Получаем PictureBox
            int borderWidth = 5; // Толщина рамки

            // Рисуем прямоугольник, который будет рамкой
            // Учитываем что рисование по контуру, то есть 1px с одной стороны и 1px с другой стороны
            Rectangle rect = new Rectangle(0, 0, pb.Width - 1, pb.Height - 1);

            using (Pen pen = new Pen(ColorTranslator.FromHtml("#5C2018"), borderWidth))
            { // Черная рамка
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
        private void PictureBox2_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender; // Получаем PictureBox
            int borderWidth = 5; // Толщина рамки

            // Рисуем прямоугольник, который будет рамкой
            // Учитываем что рисование по контуру, то есть 1px с одной стороны и 1px с другой стороны
            Rectangle rect = new Rectangle(0, 0, pb.Width - 1, pb.Height - 1);

            using (Pen pen = new Pen(ColorTranslator.FromHtml("#5C2018"), borderWidth))
            { // Черная рамка
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
        private void LoadImages()
        {
            _imagePaths = new List<string>
            {
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\cat1.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\cat2.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\klown.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\klown2.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\voin1.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\рыцарь.png",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\Катя.jpg",
                "C:\\Users\\ЦИФРОСКУПКА\\Desktop\\5 сем\\Новая папка\\яя.png"
            };
        }
        private void UpdatePictureBox(int i)
        {
            if (i == 1)
            {
                pictureBox1.Image = Image.FromFile(_imagePaths[_currentImageIndex1]);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                Miner.FirstPlayerPicturePath = _imagePaths[_currentImageIndex1];
            }
            else
            {
                pictureBox2.Image = Image.FromFile(_imagePaths[_currentImageIndex2]);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                Miner.SecondPlayerPicturePath = _imagePaths[_currentImageIndex2];
            }


        }
        private void buttonToStartGame_Click(object sender, EventArgs e)
        {
            if (Miner.Gamemod == 0)
            {
                MessageBox.Show($"Выберите режим игры");
                return;
            }
            if (Miner.AreaSize == 0)
            {
                MessageBox.Show($"Выберите размер поля");
                return;
            }
            FormForGame formMain = new FormForGame(this.Miner);
            //this.Hide();
            formMain.Show();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Miner.Gamemod = 1;
            button7.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");
            //button1.BackColor = ColorTranslator.FromHtml("#1e90ff");
            button7.FlatAppearance.BorderSize = 4;
            //button1.FlatAppearance.MouseDownBackColor = button1.BackColor;
            //button1.FlatAppearance.MouseOverBackColor = button1.BackColor;
            button7.FlatStyle = FlatStyle.Flat;
            //// Код для убирания обводки button2
            button2.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button2.FlatAppearance.BorderSize = 2;
            //button2.FlatStyle = FlatStyle.Standard;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Miner.Gamemod = 2;
            button2.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");
            //button2.BackColor = ColorTranslator.FromHtml("#1e90ff");
            button2.FlatAppearance.BorderSize = 4;
            //button2.FlatAppearance.MouseDownBackColor = button2.BackColor;
            //button2.FlatAppearance.MouseOverBackColor = button2.BackColor;
            button2.FlatStyle = FlatStyle.Flat;
            //// Код для убирания обводки button1
            button7.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button7.FlatAppearance.BorderSize = 2;
            //button1.FlatStyle = FlatStyle.Standard;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Miner.AreaSize = 3;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 4;
            button3.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");

            button4.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button4.FlatAppearance.BorderSize = 2;
            button5.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button5.FlatAppearance.BorderSize = 2;
            button6.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button6.FlatAppearance.BorderSize = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Miner.AreaSize = 4;
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 4;
            button4.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");

            button3.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button3.FlatAppearance.BorderSize = 2;
            button5.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button5.FlatAppearance.BorderSize = 2;
            button6.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button6.FlatAppearance.BorderSize = 2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Miner.AreaSize = 5;
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 4;
            button5.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");

            button3.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button3.FlatAppearance.BorderSize = 2;
            button4.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button4.FlatAppearance.BorderSize = 2;
            button6.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button6.FlatAppearance.BorderSize = 2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Miner.AreaSize = 6;
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 4;
            button6.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#dc143c");

            button3.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button3.FlatAppearance.BorderSize = 2;
            button4.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button4.FlatAppearance.BorderSize = 2;
            button5.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#5C2018");
            button5.FlatAppearance.BorderSize = 2;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (_imagePaths.Count <= 0)
                return;

            _currentImageIndex1--;
            if (_currentImageIndex1 < 0)
                _currentImageIndex1 = _imagePaths.Count - 1;

            UpdatePictureBox(1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_imagePaths.Count <= 0)
                return;

            _currentImageIndex1++;
            if (_currentImageIndex1 >= _imagePaths.Count)
                _currentImageIndex1 = 0;

            UpdatePictureBox(1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (_imagePaths.Count <= 0)
                return;

            _currentImageIndex2--;
            if (_currentImageIndex2 < 0)
                _currentImageIndex2 = _imagePaths.Count - 1;

            UpdatePictureBox(2);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_imagePaths.Count <= 0)
                return;

            _currentImageIndex2++;
            if (_currentImageIndex2 >= _imagePaths.Count)
                _currentImageIndex2 = 0;

            UpdatePictureBox(2);
        }
    }
}
