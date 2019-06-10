namespace MaSoft.UI.MaMessage
{
    partial class CancelOrder
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
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.pictureBox_ico = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ico)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_msg
            // 
            this.textBox_msg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.textBox_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_msg.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_msg.Enabled = false;
            this.textBox_msg.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_msg.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_msg.Location = new System.Drawing.Point(22, 92);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.ShortcutsEnabled = false;
            this.textBox_msg.Size = new System.Drawing.Size(276, 72);
            this.textBox_msg.TabIndex = 10;
            this.textBox_msg.TabStop = false;
            this.textBox_msg.Text = "正在等待支付完成\r\n取消支付将会取消打印并删除打印任务\r\n您要取消打印当前订单吗？";
            this.textBox_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_cancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_cancel.ForeColor = System.Drawing.Color.Black;
            this.button_cancel.Location = new System.Drawing.Point(178, 167);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(100, 35);
            this.button_cancel.TabIndex = 14;
            this.button_cancel.Text = "取 消";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.Red;
            this.button_ok.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ok.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_ok.Location = new System.Drawing.Point(42, 167);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 35);
            this.button_ok.TabIndex = 13;
            this.button_ok.Text = "确 定";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Image = global::MaSoft.Properties.Resources.MessageBox_close;
            this.pictureBox_close.Location = new System.Drawing.Point(298, 7);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_close.TabIndex = 12;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            this.pictureBox_close.MouseLeave += new System.EventHandler(this.pictureBox_close_MouseLeave);
            this.pictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_close_MouseMove);
            // 
            // pictureBox_ico
            // 
            this.pictureBox_ico.Image = global::MaSoft.Properties.Resources.MessageBox_ask;
            this.pictureBox_ico.Location = new System.Drawing.Point(130, 18);
            this.pictureBox_ico.MaximumSize = new System.Drawing.Size(60, 60);
            this.pictureBox_ico.MinimumSize = new System.Drawing.Size(60, 60);
            this.pictureBox_ico.Name = "pictureBox_ico";
            this.pictureBox_ico.Size = new System.Drawing.Size(60, 60);
            this.pictureBox_ico.TabIndex = 11;
            this.pictureBox_ico.TabStop = false;
            // 
            // CancelOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_alert;
            this.ClientSize = new System.Drawing.Size(321, 230);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_msg);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.pictureBox_ico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(321, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(321, 230);
            this.Name = "CancelOrder";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CanelOrder";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_ico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_msg;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.PictureBox pictureBox_ico;
    }
}