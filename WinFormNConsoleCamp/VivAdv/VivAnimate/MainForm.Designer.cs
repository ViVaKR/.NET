namespace VivAnimate
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
            AutoScaleDimensions = new SizeF(17F, 38F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1500, 793);
            Font = new Font("IBM Plex Sans KR", 10.875F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Name = "MainForm";
            Text = "PlayGround";
            ResumeLayout(false);
        }

        #endregion
    }
}
