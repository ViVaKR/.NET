namespace ControlsManger
{
    partial class UniCodeForm
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
            groupBox1 = new GroupBox();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1656, 394);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Buttons";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(36F, 84F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1656, 394);
            Controls.Add(groupBox1);
            Font = new Font("IBM Plex Sans KR", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Margin = new Padding(8);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
    }
}
