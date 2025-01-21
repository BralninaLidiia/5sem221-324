namespace wfaMinesweeper
{
    partial class PlayerReadyForm
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
            label1 = new Label();
            buttonOK = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.FromArgb(51, 27, 23);
            label1.Location = new Point(577, 186);
            label1.Name = "label1";
            label1.Size = new Size(208, 86);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // buttonOK
            // 
            buttonOK.BackColor = Color.FromArgb(216, 165, 154);
            buttonOK.FlatAppearance.BorderColor = Color.FromArgb(92, 32, 24);
            buttonOK.FlatAppearance.BorderSize = 2;
            buttonOK.FlatStyle = FlatStyle.Flat;
            buttonOK.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonOK.ForeColor = Color.FromArgb(51, 27, 23);
            buttonOK.Location = new Point(646, 403);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(154, 44);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "Начать";
            buttonOK.UseVisualStyleBackColor = false;
            buttonOK.Click += buttonOK_Click;
            // 
            // PlayerReadyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 224, 220);
            ClientSize = new Size(1904, 1041);
            Controls.Add(buttonOK);
            Controls.Add(label1);
            MinimumSize = new Size(1918, 1038);
            Name = "PlayerReadyForm";
            Text = "Ready?";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button buttonOK;
    }
}