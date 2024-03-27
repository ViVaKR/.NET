
namespace KeyHook
{
    partial class MainForm
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
            this.Grp_Register = new System.Windows.Forms.GroupBox();
            this.Btn_RemoveAll = new System.Windows.Forms.Button();
            this.Btn_D = new System.Windows.Forms.Button();
            this.Btn_C = new System.Windows.Forms.Button();
            this.Btn_B = new System.Windows.Forms.Button();
            this.Btn_Add_A = new System.Windows.Forms.Button();
            this.Grp_Status = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Lbx_Status = new System.Windows.Forms.ListBox();
            this.Lbl_Status = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Grp_Register.SuspendLayout();
            this.Grp_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grp_Register
            // 
            this.Grp_Register.Controls.Add(this.Btn_RemoveAll);
            this.Grp_Register.Controls.Add(this.Btn_D);
            this.Grp_Register.Controls.Add(this.Btn_C);
            this.Grp_Register.Controls.Add(this.Btn_B);
            this.Grp_Register.Controls.Add(this.Btn_Add_A);
            this.Grp_Register.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Grp_Register.Location = new System.Drawing.Point(0, 711);
            this.Grp_Register.Name = "Grp_Register";
            this.Grp_Register.Size = new System.Drawing.Size(1340, 158);
            this.Grp_Register.TabIndex = 0;
            this.Grp_Register.TabStop = false;
            this.Grp_Register.Text = "Hot Key Register";
            // 
            // Btn_RemoveAll
            // 
            this.Btn_RemoveAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_RemoveAll.Location = new System.Drawing.Point(967, 51);
            this.Btn_RemoveAll.Name = "Btn_RemoveAll";
            this.Btn_RemoveAll.Size = new System.Drawing.Size(370, 104);
            this.Btn_RemoveAll.TabIndex = 4;
            this.Btn_RemoveAll.Tag = "Z";
            this.Btn_RemoveAll.Text = "Remove All";
            this.Btn_RemoveAll.UseVisualStyleBackColor = true;
            this.Btn_RemoveAll.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_D
            // 
            this.Btn_D.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_D.Location = new System.Drawing.Point(726, 51);
            this.Btn_D.Name = "Btn_D";
            this.Btn_D.Size = new System.Drawing.Size(241, 104);
            this.Btn_D.TabIndex = 3;
            this.Btn_D.Tag = "D";
            this.Btn_D.Text = "Hot Key ( D )";
            this.Btn_D.UseVisualStyleBackColor = true;
            this.Btn_D.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_C
            // 
            this.Btn_C.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_C.Location = new System.Drawing.Point(485, 51);
            this.Btn_C.Name = "Btn_C";
            this.Btn_C.Size = new System.Drawing.Size(241, 104);
            this.Btn_C.TabIndex = 2;
            this.Btn_C.Tag = "C";
            this.Btn_C.Text = "Hot Key ( C )";
            this.Btn_C.UseVisualStyleBackColor = true;
            this.Btn_C.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_B
            // 
            this.Btn_B.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_B.Location = new System.Drawing.Point(244, 51);
            this.Btn_B.Name = "Btn_B";
            this.Btn_B.Size = new System.Drawing.Size(241, 104);
            this.Btn_B.TabIndex = 1;
            this.Btn_B.Tag = "B";
            this.Btn_B.Text = "Hot Key ( B )";
            this.Btn_B.UseVisualStyleBackColor = true;
            this.Btn_B.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Add_A
            // 
            this.Btn_Add_A.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_Add_A.Location = new System.Drawing.Point(3, 51);
            this.Btn_Add_A.Name = "Btn_Add_A";
            this.Btn_Add_A.Size = new System.Drawing.Size(241, 104);
            this.Btn_Add_A.TabIndex = 0;
            this.Btn_Add_A.Tag = "A";
            this.Btn_Add_A.Text = "Hot Key ( A )";
            this.Btn_Add_A.UseVisualStyleBackColor = true;
            this.Btn_Add_A.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Grp_Status
            // 
            this.Grp_Status.Controls.Add(this.splitContainer1);
            this.Grp_Status.Controls.Add(this.textBox1);
            this.Grp_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grp_Status.Location = new System.Drawing.Point(0, 0);
            this.Grp_Status.Name = "Grp_Status";
            this.Grp_Status.Size = new System.Drawing.Size(1340, 711);
            this.Grp_Status.TabIndex = 1;
            this.Grp_Status.TabStop = false;
            this.Grp_Status.Text = "Hot Key Status";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Lbx_Status);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Lbl_Status);
            this.splitContainer1.Size = new System.Drawing.Size(1334, 602);
            this.splitContainer1.SplitterDistance = 664;
            this.splitContainer1.TabIndex = 2;
            // 
            // Lbx_Status
            // 
            this.Lbx_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbx_Status.FormattingEnabled = true;
            this.Lbx_Status.ItemHeight = 42;
            this.Lbx_Status.Location = new System.Drawing.Point(0, 0);
            this.Lbx_Status.Name = "Lbx_Status";
            this.Lbx_Status.Size = new System.Drawing.Size(664, 602);
            this.Lbx_Status.TabIndex = 0;
            // 
            // Lbl_Status
            // 
            this.Lbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbl_Status.Location = new System.Drawing.Point(0, 0);
            this.Lbl_Status.Name = "Lbl_Status";
            this.Lbl_Status.Size = new System.Drawing.Size(666, 602);
            this.Lbl_Status.TabIndex = 0;
            this.Lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(3, 653);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1334, 55);
            this.textBox1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 42F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 869);
            this.Controls.Add(this.Grp_Status);
            this.Controls.Add(this.Grp_Register);
            this.Font = new System.Drawing.Font("IBM Plex Sans KR", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Grp_Register.ResumeLayout(false);
            this.Grp_Status.ResumeLayout(false);
            this.Grp_Status.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grp_Register;
        private System.Windows.Forms.Button Btn_RemoveAll;
        private System.Windows.Forms.Button Btn_D;
        private System.Windows.Forms.Button Btn_C;
        private System.Windows.Forms.Button Btn_B;
        private System.Windows.Forms.Button Btn_Add_A;
        private System.Windows.Forms.GroupBox Grp_Status;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox Lbx_Status;
        private System.Windows.Forms.Label Lbl_Status;
        private System.Windows.Forms.TextBox textBox1;
    }
}

