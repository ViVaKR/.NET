namespace SpaceShooter
{
    partial class MainSceen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSceen));
            this.LblTitle = new System.Windows.Forms.Label();
            this.BtnReplay = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.PicTitleImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicTitleImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Britannic Bold", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.Location = new System.Drawing.Point(307, 85);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(930, 236);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "LABEL";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnReplay
            // 
            this.BtnReplay.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReplay.ForeColor = System.Drawing.Color.SaddleBrown;
            this.BtnReplay.Location = new System.Drawing.Point(449, 820);
            this.BtnReplay.Name = "BtnReplay";
            this.BtnReplay.Size = new System.Drawing.Size(400, 100);
            this.BtnReplay.TabIndex = 1;
            this.BtnReplay.Text = "Replay";
            this.BtnReplay.UseVisualStyleBackColor = true;
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Transparent;
            this.BtnExit.Font = new System.Drawing.Font("Impact", 24F);
            this.BtnExit.ForeColor = System.Drawing.Color.SaddleBrown;
            this.BtnExit.Location = new System.Drawing.Point(449, 939);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(400, 100);
            this.BtnExit.TabIndex = 2;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // PicTitleImage
            // 
            this.PicTitleImage.Location = new System.Drawing.Point(553, 371);
            this.PicTitleImage.Name = "PicTitleImage";
            this.PicTitleImage.Size = new System.Drawing.Size(456, 425);
            this.PicTitleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PicTitleImage.TabIndex = 3;
            this.PicTitleImage.TabStop = false;
            // 
            // MainSceen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 1388);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnReplay);
            this.Controls.Add(this.PicTitleImage);
            this.Controls.Add(this.LblTitle);
            this.Font = new System.Drawing.Font("IBM Plex Sans KR", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.Color.OrangeRed;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainSceen";
            this.Text = " Space Shooter";
            ((System.ComponentModel.ISupportInitialize)(this.PicTitleImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Button BtnReplay;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.PictureBox PicTitleImage;
    }
}

