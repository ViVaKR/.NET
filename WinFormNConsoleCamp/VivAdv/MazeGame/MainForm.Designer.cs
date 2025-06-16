namespace MazeGame
{
    partial class MainForm
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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(18F, 42F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 591);
            Font = new Font("IBM Plex Sans KR", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Margin = new Padding(4, 4, 4, 4);
            Name = "MainForm";
            Text = "미궁속으로";
            ResumeLayout(false);
        }

        #endregion
    }
}