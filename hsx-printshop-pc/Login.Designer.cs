namespace MaSoft
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Label_info = new System.Windows.Forms.Label();
            this.TextBox_username = new System.Windows.Forms.TextBox();
            this.TextBox_password = new System.Windows.Forms.TextBox();
            this.Button_login = new System.Windows.Forms.Button();
            this.Label_copyright = new System.Windows.Forms.Label();
            this.PictureBox_min = new System.Windows.Forms.PictureBox();
            this.PictureBox_close = new System.Windows.Forms.PictureBox();
            this.PictureBox_bar = new System.Windows.Forms.PictureBox();
            this.PictureBox_username = new System.Windows.Forms.PictureBox();
            this.PictureBox_password = new System.Windows.Forms.PictureBox();
            this.Panel_mask = new System.Windows.Forms.Panel();
            this.timer_login = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_username)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_password)).BeginInit();
            this.Panel_mask.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_info
            // 
            this.Label_info.AutoSize = true;
            this.Label_info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.Label_info.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_info.ForeColor = System.Drawing.Color.Gray;
            this.Label_info.Location = new System.Drawing.Point(10, 236);
            this.Label_info.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_info.Name = "Label_info";
            this.Label_info.Size = new System.Drawing.Size(51, 17);
            this.Label_info.TabIndex = 0;
            this.Label_info.Text = "v1.0.0.0";
            // 
            // TextBox_username
            // 
            this.TextBox_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.TextBox_username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_username.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TextBox_username.Location = new System.Drawing.Point(329, 242);
            this.TextBox_username.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.TextBox_username.MaxLength = 11;
            this.TextBox_username.Name = "TextBox_username";
            this.TextBox_username.Size = new System.Drawing.Size(184, 18);
            this.TextBox_username.TabIndex = 2;
            // 
            // TextBox_password
            // 
            this.TextBox_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.TextBox_password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBox_password.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextBox_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TextBox_password.Location = new System.Drawing.Point(329, 305);
            this.TextBox_password.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.TextBox_password.MaxLength = 30;
            this.TextBox_password.Name = "TextBox_password";
            this.TextBox_password.Size = new System.Drawing.Size(184, 18);
            this.TextBox_password.TabIndex = 3;
            this.TextBox_password.UseSystemPasswordChar = true;
            this.TextBox_password.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_password_KeyUp);
            // 
            // Button_login
            // 
            this.Button_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Button_login.FlatAppearance.BorderSize = 0;
            this.Button_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_login.ForeColor = System.Drawing.Color.Black;
            this.Button_login.Location = new System.Drawing.Point(270, 358);
            this.Button_login.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Button_login.Name = "Button_login";
            this.Button_login.Size = new System.Drawing.Size(260, 41);
            this.Button_login.TabIndex = 4;
            this.Button_login.Text = "登 录";
            this.Button_login.UseVisualStyleBackColor = false;
            this.Button_login.Click += new System.EventHandler(this.Button_login_Click);
            this.Button_login.MouseLeave += new System.EventHandler(this.Button_login_MouseLeave);
            this.Button_login.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_login_MouseMove);
            // 
            // Label_copyright
            // 
            this.Label_copyright.AutoSize = true;
            this.Label_copyright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.Label_copyright.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_copyright.ForeColor = System.Drawing.Color.Gray;
            this.Label_copyright.Location = new System.Drawing.Point(598, 438);
            this.Label_copyright.Name = "Label_copyright";
            this.Label_copyright.Size = new System.Drawing.Size(200, 17);
            this.Label_copyright.TabIndex = 8;
            this.Label_copyright.Text = "2019 @ 远道科技（大连）有限公司";
            // 
            // PictureBox_min
            // 
            this.PictureBox_min.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(30)))), ((int)(((byte)(216)))));
            this.PictureBox_min.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_min.Image")));
            this.PictureBox_min.Location = new System.Drawing.Point(760, 5);
            this.PictureBox_min.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PictureBox_min.Name = "PictureBox_min";
            this.PictureBox_min.Size = new System.Drawing.Size(14, 16);
            this.PictureBox_min.TabIndex = 3;
            this.PictureBox_min.TabStop = false;
            this.PictureBox_min.Click += new System.EventHandler(this.PictureBox_min_Click);
            this.PictureBox_min.MouseLeave += new System.EventHandler(this.PictureBox_min_MouseLeave);
            this.PictureBox_min.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_min_MouseMove);
            // 
            // PictureBox_close
            // 
            this.PictureBox_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(30)))), ((int)(((byte)(216)))));
            this.PictureBox_close.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_close.Image")));
            this.PictureBox_close.Location = new System.Drawing.Point(781, 5);
            this.PictureBox_close.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PictureBox_close.Name = "PictureBox_close";
            this.PictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.PictureBox_close.TabIndex = 2;
            this.PictureBox_close.TabStop = false;
            this.PictureBox_close.Click += new System.EventHandler(this.PictureBox_close_Click);
            this.PictureBox_close.MouseLeave += new System.EventHandler(this.PictureBox_close_MouseLeave);
            this.PictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_close_MouseMove);
            // 
            // PictureBox_bar
            // 
            this.PictureBox_bar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(1)))), ((int)(((byte)(255)))));
            this.PictureBox_bar.Image = global::MaSoft.Properties.Resources.login_bar;
            this.PictureBox_bar.Location = new System.Drawing.Point(1, 1);
            this.PictureBox_bar.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PictureBox_bar.Name = "PictureBox_bar";
            this.PictureBox_bar.Size = new System.Drawing.Size(800, 200);
            this.PictureBox_bar.TabIndex = 0;
            this.PictureBox_bar.TabStop = false;
            this.PictureBox_bar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_bar_MouseDown);
            this.PictureBox_bar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_bar_MouseMove);
            // 
            // PictureBox_username
            // 
            this.PictureBox_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.PictureBox_username.Image = global::MaSoft.Properties.Resources.login_username;
            this.PictureBox_username.Location = new System.Drawing.Point(270, 231);
            this.PictureBox_username.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PictureBox_username.Name = "PictureBox_username";
            this.PictureBox_username.Size = new System.Drawing.Size(260, 41);
            this.PictureBox_username.TabIndex = 6;
            this.PictureBox_username.TabStop = false;
            // 
            // PictureBox_password
            // 
            this.PictureBox_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.PictureBox_password.Image = global::MaSoft.Properties.Resources.login_password;
            this.PictureBox_password.Location = new System.Drawing.Point(270, 294);
            this.PictureBox_password.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PictureBox_password.Name = "PictureBox_password";
            this.PictureBox_password.Size = new System.Drawing.Size(260, 41);
            this.PictureBox_password.TabIndex = 7;
            this.PictureBox_password.TabStop = false;
            // 
            // Panel_mask
            // 
            this.Panel_mask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.Panel_mask.Controls.Add(this.Label_info);
            this.Panel_mask.Location = new System.Drawing.Point(1, 202);
            this.Panel_mask.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_mask.Name = "Panel_mask";
            this.Panel_mask.Size = new System.Drawing.Size(800, 257);
            this.Panel_mask.TabIndex = 9;
            // 
            // timer_login
            // 
            this.timer_login.Interval = 2000;
            this.timer_login.Tick += new System.EventHandler(this.timer_login_Tick);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_login;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(802, 462);
            this.ControlBox = false;
            this.Controls.Add(this.Label_copyright);
            this.Controls.Add(this.Button_login);
            this.Controls.Add(this.TextBox_password);
            this.Controls.Add(this.TextBox_username);
            this.Controls.Add(this.PictureBox_min);
            this.Controls.Add(this.PictureBox_close);
            this.Controls.Add(this.PictureBox_bar);
            this.Controls.Add(this.PictureBox_username);
            this.Controls.Add(this.PictureBox_password);
            this.Controls.Add(this.Panel_mask);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(802, 462);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(802, 462);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "慧学僧自助打印助手";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_username)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_password)).EndInit();
            this.Panel_mask.ResumeLayout(false);
            this.Panel_mask.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_bar;
        private System.Windows.Forms.Label Label_info;
        private System.Windows.Forms.PictureBox PictureBox_close;
        private System.Windows.Forms.PictureBox PictureBox_min;
        private System.Windows.Forms.TextBox TextBox_username;
        private System.Windows.Forms.PictureBox PictureBox_username;
        private System.Windows.Forms.PictureBox PictureBox_password;
        private System.Windows.Forms.TextBox TextBox_password;
        private System.Windows.Forms.Button Button_login;
        private System.Windows.Forms.Label Label_copyright;
        private System.Windows.Forms.Panel Panel_mask;
        private System.Windows.Forms.Timer timer_login;
    }
}