namespace Minesweep
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
            this.btn_restart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFlag = new System.Windows.Forms.TextBox();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.explode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.explode)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_restart
            // 
            this.btn_restart.Location = new System.Drawing.Point(226, 51);
            this.btn_restart.Name = "btn_restart";
            this.btn_restart.Size = new System.Drawing.Size(94, 34);
            this.btn_restart.TabIndex = 0;
            this.btn_restart.Text = "Restart";
            this.btn_restart.UseVisualStyleBackColor = true;
            this.btn_restart.Click += new System.EventHandler(this.btn_restart_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 91);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 506);
            this.panel1.TabIndex = 1;
            // 
            // txtFlag
            // 
            this.txtFlag.Location = new System.Drawing.Point(102, 57);
            this.txtFlag.Name = "txtFlag";
            this.txtFlag.ReadOnly = true;
            this.txtFlag.Size = new System.Drawing.Size(85, 22);
            this.txtFlag.TabIndex = 2;
            // 
            // txtScore
            // 
            this.txtScore.Location = new System.Drawing.Point(361, 57);
            this.txtScore.Name = "txtScore";
            this.txtScore.ReadOnly = true;
            this.txtScore.Size = new System.Drawing.Size(85, 22);
            this.txtScore.TabIndex = 3;
            // 
            // explode
            // 
            this.explode.BackColor = System.Drawing.Color.Transparent;
            this.explode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.explode.Cursor = System.Windows.Forms.Cursors.Default;
            this.explode.Image = global::Minesweep.Properties.Resources.explosion;
            this.explode.Location = new System.Drawing.Point(20, -6);
            this.explode.Name = "explode";
            this.explode.Size = new System.Drawing.Size(76, 76);
            this.explode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.explode.TabIndex = 0;
            this.explode.TabStop = false;
            this.explode.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 601);
            this.Controls.Add(this.explode);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.txtFlag);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_restart);
            this.Name = "Form1";
            this.Text = "MineSweeper";
            ((System.ComponentModel.ISupportInitialize)(this.explode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_restart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFlag;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.PictureBox explode;
    }
}

