namespace wfaMinesweeper
{
    partial class FormForStartGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForStartGame));
            buttonToStartGame = new Button();
            label1 = new Label();
            button2 = new Button();
            label2 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top; // Убираем AnchorStyles.Right, чтобы центрирование работало
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(51, 27, 23);
            int clientWidth = 1084;
            // label1.Text = "Выберите режим игры"
            int labelWidth = 208; // Ориентировочная ширина текста, может немного отличаться, но мы её здесь зафиксируем

            label1.Location = new Point((clientWidth - labelWidth) / 2, 30);
            label1.Name = "label1";
            label1.Size = new Size(208, 25); // Сохраняем начальный размер, он будет скорректирован autosize
            label1.TabIndex = 1;
            label1.Text = "Выберите режим игры";
            int btnWidth2 = 150;
            int btnHeight2 = 50;
            int label2Width = 212; // Приблизительная ширина текста "Выберите размер поля\r\n"
            int label4Width = 384; // Приблизительная ширина текста "Выберите аватар для юнитов своей армии"

            // Вертикальные положения
            int vPosButtons = 80;
            int vPosLabel2 = 150;
            int vPosLabel4 = 260;

            // Позиции элементов
            int btn7X = (clientWidth - btnWidth2) / 3;
            int btn2X = (clientWidth - btnWidth2) / 3 * 2;
            int label2X = (clientWidth - label2Width) / 2;
            int label4X = (clientWidth - label4Width) / 2;


            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top; // Убираем AnchorStyles.Right
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button2.Location = new System.Drawing.Point(btn2X, vPosButtons);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(btnWidth2, btnHeight2);
            this.button2.TabIndex = 3;
            this.button2.Text = "2 Игрока";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.label2.Location = new System.Drawing.Point(label2X, vPosLabel2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Выберите размер поля\r\n";
            // 
            // button7
            // 
            this.button7.Anchor = System.Windows.Forms.AnchorStyles.Top; // Убираем AnchorStyles.Right
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button7.FlatAppearance.BorderSize = 2;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button7.Location = new System.Drawing.Point(btn7X, vPosButtons);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(btnWidth2, btnHeight2);
            this.button7.TabIndex = 9;
            this.button7.Text = "1 Игрок";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.label4.Location = new System.Drawing.Point(label4X, vPosLabel4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(384, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Выберите аватар для юнитов своей армии";


            int btnWidth = 105;
            int btnHeight = 33;
            int vPos = 200; // Вертикальное положение кнопок

            // Расстояние между кнопками
            int btnSpacing = (clientWidth - (btnWidth * 4)) / 5;

            // Позиции кнопок по X
            int btn3X = btnSpacing;
            int btn4X = btn3X + btnWidth + btnSpacing;
            int btn5X = btn4X + btnWidth + btnSpacing;
            int btn6X = btn5X + btnWidth + btnSpacing;

            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button3.Location = new System.Drawing.Point(btn3X, vPos);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.button3.TabIndex = 5;
            this.button3.Text = "3X3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button4.Location = new System.Drawing.Point(btn4X, vPos);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.button4.TabIndex = 6;
            this.button4.Text = "4X4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button5.FlatAppearance.BorderSize = 2;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button5.Location = new System.Drawing.Point(btn5X, vPos);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.button5.TabIndex = 7;
            this.button5.Text = "5X5";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.button6.FlatAppearance.BorderSize = 2;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button6.Location = new System.Drawing.Point(btn6X, vPos);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(btnWidth, btnHeight);
            this.button6.TabIndex = 8;
            this.button6.Text = "6X6";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);

            // Предположим, что высота buttonToStartGame известна и равна buttonHeight (замените на реальное значение)
            int buttonHeight = 60; // Замените на реальную высоту buttonToStartGame

            // Вычисляем размеры и позиции для pictureBox1 и pictureBox2
            int pictureBoxHeight = 724 - buttonHeight - 30 - 50 - 330;  // 724 - ClientSize.Height
            int pictureBoxWidth = pictureBoxHeight;
            int pictureBox1X = 1084 / 3 - pictureBoxWidth / 2;
            int pictureBox2X = 1084 / 3 * 2 - pictureBoxWidth / 2;
            int pictureBoxY = 310;


            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.None; // Изменено на None
            this.pictureBox1.Location = new System.Drawing.Point(pictureBox1X, pictureBoxY);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(pictureBoxWidth, pictureBoxHeight);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);

            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.None; // Изменено на None
            this.pictureBox2.Location = new System.Drawing.Point(pictureBox2X, pictureBoxY);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(pictureBoxWidth, pictureBoxHeight);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox2_Paint);
            // Фиксируем значения
            int clientHeight = 724;
            int buttonWidth = 40;
            int buttonHeight2 = 25;
            int startButtonWidth = 150;
            int startButtonHeight = 60;
            int margin = 10; // отступ между кнопками
            int verticalPos = 580; // вертикальная позиция для кнопок 1,8,9,10
            int startButtonVerticalPos = clientHeight - startButtonHeight - 30;

            // Вычисляем позиции кнопок
            int button1X = clientWidth / 3 - buttonWidth - margin;
            int button8X = button1X + buttonWidth + 20;
            int button9X = clientWidth / 3 * 2 - buttonWidth - margin;
            int button10X = button9X + buttonWidth + 20;
            int startButtonX = (clientWidth - startButtonWidth) / 2;

            // 
            // button1
            // 
            button1.BackgroundImage = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\ArrowLeft.jpg");
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(button1X, verticalPos);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(buttonWidth, buttonHeight2);
            this.button1.TabIndex = 14;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button8
            // 
            this.button8.BackgroundImage = (Image)resources.GetObject("button8.BackgroundImage");
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button8.FlatAppearance.BorderSize = 2;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(button8X, verticalPos);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(buttonWidth, buttonHeight2);
            this.button8.TabIndex = 15;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button9.BackgroundImage = Image.FromFile("C:\\Users\\ЦИФРОСКУПКА\\Desktop\\ArrowLeft.jpg");
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button9.FlatAppearance.BorderSize = 2;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Location = new System.Drawing.Point(button9X, verticalPos);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(buttonWidth, buttonHeight2);
            this.button9.TabIndex = 16;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            //
            // button10
            //
            this.button10.BackgroundImage = (Image)resources.GetObject("button8.BackgroundImage");
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.button10.FlatAppearance.BorderSize = 2;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Location = new System.Drawing.Point(button10X, verticalPos);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(buttonWidth, buttonHeight2);
            this.button10.TabIndex = 17;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // buttonToStartGame
            // 
            this.buttonToStartGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(165)))), ((int)(((byte)(154)))));
            this.buttonToStartGame.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(32)))), ((int)(((byte)(24)))));
            this.buttonToStartGame.FlatAppearance.BorderSize = 2;
            this.buttonToStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToStartGame.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonToStartGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(27)))), ((int)(((byte)(23)))));
            this.buttonToStartGame.Location = new System.Drawing.Point(startButtonX, startButtonVerticalPos);
            this.buttonToStartGame.Name = "buttonToStartGame";
            this.buttonToStartGame.Size = new System.Drawing.Size(startButtonWidth, startButtonHeight);
            this.buttonToStartGame.TabIndex = 0;
            this.buttonToStartGame.Text = "Начать игру";
            this.buttonToStartGame.UseVisualStyleBackColor = false;
            this.buttonToStartGame.Click += new System.EventHandler(this.buttonToStartGame_Click);
            // 
            // FormForStartGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 224, 220);
            ClientSize = new Size(1084, 724);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(buttonToStartGame);
            MaximumSize = new Size(1100, 763);
            MinimumSize = new Size(1100, 763);
            Name = "FormForStartGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Start Mainsweeper";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonToStartGame;
        private Label label1;
        private Button button2;
        private Label label2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private Button button8;
        private Button button9;
        private Button button10;
    }
}