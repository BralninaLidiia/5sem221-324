namespace wfaMinesweeper
{
    partial class FormForGame
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
            SuspendLayout();
            // 
            // FormForGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 224, 220);
            ClientSize = new Size(872, 449);
            MinimumSize = new Size(888, 488);
            Name = "FormForGame";
            Text = "Minesweeper";
            WindowState = FormWindowState.Maximized;
            SizeChanged += Form1_SizeChanged;
            ResumeLayout(false);
        }

        #endregion
    }
}
