namespace WFA1
{
    partial class FormImageCut
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            centerLinesBox = new CheckBox();
            gridLinesBox = new CheckBox();
            button1 = new Button();
            buttonCrop = new Button();
            label1 = new Label();
            pictureBoxForLoadImage = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            checkBoxShowCoordinateOfRectangleCenter = new CheckBox();
            buttonResetSettings = new Button();
            buttonSaveSettings = new Button();
            buttonLoadSettings = new Button();
            SelectedRectSizeLabel = new Label();
            ImageSizeLabel = new Label();
            label3 = new Label();
            rotationTrackBar = new TrackBar();
            hideRectBox = new CheckBox();
            buToCenter = new Button();
            comboBoxAreaScale = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxForLoadImage).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rotationTrackBar).BeginInit();
            SuspendLayout();
            // 
            // centerLinesBox
            // 
            centerLinesBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            centerLinesBox.AutoSize = true;
            centerLinesBox.Font = new Font("Segoe UI", 12F);
            centerLinesBox.Location = new Point(6, 233);
            centerLinesBox.Name = "centerLinesBox";
            centerLinesBox.Size = new Size(131, 25);
            centerLinesBox.TabIndex = 1;
            centerLinesBox.Text = "Линии центра";
            centerLinesBox.UseVisualStyleBackColor = true;
            centerLinesBox.CheckedChanged += centerLines_CheckedChanged;
            // 
            // gridLinesBox
            // 
            gridLinesBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gridLinesBox.AutoSize = true;
            gridLinesBox.Font = new Font("Segoe UI", 12F);
            gridLinesBox.Location = new Point(6, 253);
            gridLinesBox.Name = "gridLinesBox";
            gridLinesBox.Size = new Size(119, 25);
            gridLinesBox.TabIndex = 2;
            gridLinesBox.Text = "Линии сетки";
            gridLinesBox.UseVisualStyleBackColor = true;
            gridLinesBox.CheckedChanged += gridLinesBox_CheckedChanged;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(0, 9);
            button1.Name = "button1";
            button1.Size = new Size(213, 50);
            button1.TabIndex = 7;
            button1.Text = "Загрузить изображение";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonLoad_Click;
            // 
            // buttonCrop
            // 
            buttonCrop.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCrop.Font = new Font("Segoe UI", 12F);
            buttonCrop.Location = new Point(1, 540);
            buttonCrop.Name = "buttonCrop";
            buttonCrop.Size = new Size(213, 35);
            buttonCrop.TabIndex = 8;
            buttonCrop.Text = "Сохранить изображение";
            buttonCrop.UseVisualStyleBackColor = true;
            buttonCrop.Click += buttonCrop_Click_1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(7, 77);
            label1.Name = "label1";
            label1.Size = new Size(134, 21);
            label1.TabIndex = 9;
            label1.Text = "Выбрать область:";
            // 
            // pictureBoxForLoadImage
            // 
            pictureBoxForLoadImage.BackColor = SystemColors.Control;
            pictureBoxForLoadImage.Dock = DockStyle.Fill;
            pictureBoxForLoadImage.Location = new Point(3, 3);
            pictureBoxForLoadImage.Name = "pictureBoxForLoadImage";
            pictureBoxForLoadImage.Size = new Size(640, 701);
            pictureBoxForLoadImage.TabIndex = 10;
            pictureBoxForLoadImage.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(pictureBoxForLoadImage, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(865, 707);
            tableLayoutPanel1.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxShowCoordinateOfRectangleCenter);
            panel1.Controls.Add(buttonResetSettings);
            panel1.Controls.Add(buttonSaveSettings);
            panel1.Controls.Add(buttonLoadSettings);
            panel1.Controls.Add(SelectedRectSizeLabel);
            panel1.Controls.Add(ImageSizeLabel);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(rotationTrackBar);
            panel1.Controls.Add(hideRectBox);
            panel1.Controls.Add(buToCenter);
            panel1.Controls.Add(comboBoxAreaScale);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(buttonCrop);
            panel1.Controls.Add(gridLinesBox);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(centerLinesBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(649, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 701);
            panel1.TabIndex = 11;
            // 
            // checkBoxShowCoordinateOfRectangleCenter
            // 
            checkBoxShowCoordinateOfRectangleCenter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkBoxShowCoordinateOfRectangleCenter.AutoSize = true;
            checkBoxShowCoordinateOfRectangleCenter.Font = new Font("Segoe UI", 12F);
            checkBoxShowCoordinateOfRectangleCenter.Location = new Point(6, 278);
            checkBoxShowCoordinateOfRectangleCenter.Name = "checkBoxShowCoordinateOfRectangleCenter";
            checkBoxShowCoordinateOfRectangleCenter.Size = new Size(177, 46);
            checkBoxShowCoordinateOfRectangleCenter.TabIndex = 22;
            checkBoxShowCoordinateOfRectangleCenter.Text = "Координаты центра \r\nобласти";
            checkBoxShowCoordinateOfRectangleCenter.UseVisualStyleBackColor = true;
            checkBoxShowCoordinateOfRectangleCenter.CheckedChanged += checkBoxShowCoordinateOfRectangleCenter_CheckedChanged;
            // 
            // buttonResetSettings
            // 
            buttonResetSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonResetSettings.Font = new Font("Segoe UI", 12F);
            buttonResetSettings.Location = new Point(1, 662);
            buttonResetSettings.Name = "buttonResetSettings";
            buttonResetSettings.Size = new Size(213, 35);
            buttonResetSettings.TabIndex = 21;
            buttonResetSettings.Text = "Сбросить настройки";
            buttonResetSettings.UseVisualStyleBackColor = true;
            buttonResetSettings.Click += buttonResetSettings_Click;
            // 
            // buttonSaveSettings
            // 
            buttonSaveSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSaveSettings.Font = new Font("Segoe UI", 12F);
            buttonSaveSettings.Location = new Point(1, 622);
            buttonSaveSettings.Name = "buttonSaveSettings";
            buttonSaveSettings.Size = new Size(213, 35);
            buttonSaveSettings.TabIndex = 20;
            buttonSaveSettings.Text = "Сохранить настройки";
            buttonSaveSettings.UseVisualStyleBackColor = true;
            buttonSaveSettings.Click += buttonSaveSettings_Click;
            // 
            // buttonLoadSettings
            // 
            buttonLoadSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonLoadSettings.Font = new Font("Segoe UI", 12F);
            buttonLoadSettings.Location = new Point(1, 581);
            buttonLoadSettings.Name = "buttonLoadSettings";
            buttonLoadSettings.Size = new Size(213, 35);
            buttonLoadSettings.TabIndex = 19;
            buttonLoadSettings.Text = "Загрузить настройки";
            buttonLoadSettings.UseVisualStyleBackColor = true;
            buttonLoadSettings.Click += buttonLoadSettings_Click;
            // 
            // SelectedRectSizeLabel
            // 
            SelectedRectSizeLabel.AutoSize = true;
            SelectedRectSizeLabel.Location = new Point(8, 490);
            SelectedRectSizeLabel.Name = "SelectedRectSizeLabel";
            SelectedRectSizeLabel.Size = new Size(0, 15);
            SelectedRectSizeLabel.TabIndex = 18;
            // 
            // ImageSizeLabel
            // 
            ImageSizeLabel.AutoSize = true;
            ImageSizeLabel.Location = new Point(8, 461);
            ImageSizeLabel.Name = "ImageSizeLabel";
            ImageSizeLabel.Size = new Size(0, 15);
            ImageSizeLabel.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 393);
            label3.Name = "label3";
            label3.Size = new Size(165, 15);
            label3.TabIndex = 16;
            label3.Text = "Угол поворота изображения";
            // 
            // rotationTrackBar
            // 
            rotationTrackBar.Location = new Point(11, 411);
            rotationTrackBar.Maximum = 90;
            rotationTrackBar.Minimum = -90;
            rotationTrackBar.Name = "rotationTrackBar";
            rotationTrackBar.Size = new Size(194, 45);
            rotationTrackBar.TabIndex = 15;
            rotationTrackBar.Scroll += rotationTrackBar_Scroll;
            // 
            // hideRectBox
            // 
            hideRectBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            hideRectBox.AutoSize = true;
            hideRectBox.Font = new Font("Segoe UI", 12F);
            hideRectBox.Location = new Point(6, 213);
            hideRectBox.Name = "hideRectBox";
            hideRectBox.Size = new Size(142, 25);
            hideRectBox.TabIndex = 14;
            hideRectBox.Text = "Скрыть область";
            hideRectBox.UseVisualStyleBackColor = true;
            hideRectBox.CheckedChanged += hideRectBox_CheckedChanged;
            // 
            // buToCenter
            // 
            buToCenter.Location = new Point(37, 330);
            buToCenter.Name = "buToCenter";
            buToCenter.Size = new Size(133, 43);
            buToCenter.TabIndex = 13;
            buToCenter.Text = "Переместить область в центр";
            buToCenter.UseVisualStyleBackColor = true;
            buToCenter.Click += buToCenter_Click;
            // 
            // comboBoxAreaScale
            // 
            comboBoxAreaScale.FormattingEnabled = true;
            comboBoxAreaScale.Items.AddRange(new object[] { "1x1 ", "4x3 ", "3x4 ", "16x9 ", "9x16" });
            comboBoxAreaScale.Location = new Point(7, 104);
            comboBoxAreaScale.Name = "comboBoxAreaScale";
            comboBoxAreaScale.Size = new Size(121, 23);
            comboBoxAreaScale.TabIndex = 12;
            comboBoxAreaScale.SelectedIndexChanged += comboBoxAreaScale_SelectedIndexChanged;
            // 
            // FormImageCut
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(865, 707);
            Controls.Add(tableLayoutPanel1);
            MinimumSize = new Size(881, 746);
            Name = "FormImageCut";
            Text = "ImageCut";
            WindowState = FormWindowState.Maximized;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBoxForLoadImage).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rotationTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private CheckBox centerLinesBox;
        private CheckBox gridLinesBox;
        private Button button1;
        private Button buttonCrop;
        private Label label1;
        private PictureBox pictureBoxForLoadImage;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ComboBox comboBoxAreaScale;
        private Button buToCenter;
        private CheckBox hideRectBox;
        private Label label3;
        private TrackBar rotationTrackBar;
        private Label ImageSizeLabel;
        private Label SelectedRectSizeLabel;
        private Button buttonSaveSettings;
        private Button buttonLoadSettings;
        private Button buttonResetSettings;
        private CheckBox checkBoxShowCoordinateOfRectangleCenter;
    }
}
