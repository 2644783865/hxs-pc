namespace MaSoft.UI
{
    partial class UserLogout
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
            this.label_h1 = new System.Windows.Forms.Label();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.panel_money = new System.Windows.Forms.Panel();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.textBox_msg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.panel_money.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_h1
            // 
            this.label_h1.AutoSize = true;
            this.label_h1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_h1.Location = new System.Drawing.Point(128, 22);
            this.label_h1.Name = "label_h1";
            this.label_h1.Size = new System.Drawing.Size(74, 21);
            this.label_h1.TabIndex = 19;
            this.label_h1.Text = "操作确认";
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.Image = global::MaSoft.Properties.Resources.MessageBox_close;
            this.pictureBox_close.Location = new System.Drawing.Point(307, 7);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_close.TabIndex = 20;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            this.pictureBox_close.MouseLeave += new System.EventHandler(this.pictureBox_close_MouseLeave);
            this.pictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_close_MouseMove);
            // 
            // panel_money
            // 
            this.panel_money.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.panel_money.Controls.Add(this.textBox_password);
            this.panel_money.Location = new System.Drawing.Point(39, 89);
            this.panel_money.Name = "panel_money";
            this.panel_money.Size = new System.Drawing.Size(252, 36);
            this.panel_money.TabIndex = 28;
            // 
            // textBox_password
            // 
            this.textBox_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.textBox_password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_password.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_password.Location = new System.Drawing.Point(3, 7);
            this.textBox_password.Name = "textBox_password";
            this.textBox_password.Size = new System.Drawing.Size(246, 22);
            this.textBox_password.TabIndex = 1;
            this.textBox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_password.UseSystemPasswordChar = true;
            this.textBox_password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_password_KeyUp);
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(52)))), ((int)(((byte)(250)))));
            this.button_ok.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_ok.FlatAppearance.BorderSize = 0;
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ok.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_ok.Location = new System.Drawing.Point(115, 146);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 32);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "确 定";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // textBox_msg
            // 
            this.textBox_msg.BackColor = System.Drawing.Color.Lavender;
            this.textBox_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_msg.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_msg.Enabled = false;
            this.textBox_msg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_msg.ForeColor = System.Drawing.Color.Gray;
            this.textBox_msg.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_msg.Location = new System.Drawing.Point(12, 57);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.ShortcutsEnabled = false;
            this.textBox_msg.Size = new System.Drawing.Size(306, 26);
            this.textBox_msg.TabIndex = 31;
            this.textBox_msg.TabStop = false;
            this.textBox_msg.Text = "请输入操作码进行操作";
            this.textBox_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UserLogout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_330_200;
            this.ClientSize = new System.Drawing.Size(330, 200);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_msg);
            this.Controls.Add(this.panel_money);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.label_h1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(330, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(330, 200);
            this.Name = "UserLogout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UserLogout";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UserLogout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.panel_money.ResumeLayout(false);
            this.panel_money.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_h1;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.Panel panel_money;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.TextBox textBox_msg;
    }
}