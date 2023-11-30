namespace Com.ViVaKR.Forms
{
    partial class Form1
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelStatusMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 1469);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(2181, 70);
            this.progressBar1.TabIndex = 0;
            // 
            // labelStatusMessage
            // 
            this.labelStatusMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelStatusMessage.Location = new System.Drawing.Point(0, 1431);
            this.labelStatusMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatusMessage.Name = "labelStatusMessage";
            this.labelStatusMessage.Size = new System.Drawing.Size(2181, 38);
            this.labelStatusMessage.TabIndex = 1;
            this.labelStatusMessage.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2181, 1539);
            this.Controls.Add(this.labelStatusMessage);
            this.Controls.Add(this.progressBar1);
            this.Font = new System.Drawing.Font("IBM Plex Sans KR", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelStatusMessage;
    }
}