namespace MaSoft.UI
{
    partial class AboutUs
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
            this.textBox_msg = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_msg
            // 
            this.textBox_msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.textBox_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_msg.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_msg.Enabled = false;
            this.textBox_msg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_msg.ForeColor = System.Drawing.Color.Gray;
            this.textBox_msg.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_msg.Location = new System.Drawing.Point(56, 200);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.ShortcutsEnabled = false;
            this.textBox_msg.Size = new System.Drawing.Size(209, 116);
            this.textBox_msg.TabIndex = 32;
            this.textBox_msg.TabStop = false;
            this.textBox_msg.Text = "慧学僧自助打印助手\r\n\r\n远道科技（大连）有限公司 版权所有\r\n\r\n网址：www.huixueseng.cn\r\n\r\n电话：188-4260-9113\r\n\r\n邮箱：" +
                "1656145374@qq.com";
            this.textBox_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MaSoft.Properties.Resources.about_wxqcode;
            this.pictureBox1.Location = new System.Drawing.Point(81, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.Image = global::MaSoft.Properties.Resources.MessageBox_close;
            this.pictureBox_close.Location = new System.Drawing.Point(299, 7);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_close.TabIndex = 38;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            this.pictureBox_close.MouseLeave += new System.EventHandler(this.pictureBox_close_MouseLeave);
            this.pictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_close_MouseMove);
            // 
            // AboutUs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_320_323;
            this.ClientSize = new System.Drawing.Size(320, 323);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox_msg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 323);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 323);
            this.Name = "AboutUs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutUs";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_msg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox_close;
    }
}