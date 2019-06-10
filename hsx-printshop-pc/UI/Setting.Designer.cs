namespace MaSoft.UI
{
    partial class Setting
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
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.groupBox_set = new System.Windows.Forms.GroupBox();
            this.checkBox_top = new System.Windows.Forms.CheckBox();
            this.label_a3 = new System.Windows.Forms.Label();
            this.checkBox_login = new System.Windows.Forms.CheckBox();
            this.label_a2 = new System.Windows.Forms.Label();
            this.checkBox_power = new System.Windows.Forms.CheckBox();
            this.label_a1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.groupBox_set.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.Image = global::MaSoft.Properties.Resources.MessageBox_close;
            this.pictureBox_close.Location = new System.Drawing.Point(309, 6);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_close.TabIndex = 40;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            this.pictureBox_close.MouseLeave += new System.EventHandler(this.pictureBox_close_MouseLeave);
            this.pictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_close_MouseMove);
            // 
            // groupBox_set
            // 
            this.groupBox_set.Controls.Add(this.checkBox_top);
            this.groupBox_set.Controls.Add(this.label_a3);
            this.groupBox_set.Controls.Add(this.checkBox_login);
            this.groupBox_set.Controls.Add(this.label_a2);
            this.groupBox_set.Controls.Add(this.checkBox_power);
            this.groupBox_set.Controls.Add(this.label_a1);
            this.groupBox_set.Location = new System.Drawing.Point(12, 8);
            this.groupBox_set.Name = "groupBox_set";
            this.groupBox_set.Size = new System.Drawing.Size(306, 180);
            this.groupBox_set.TabIndex = 41;
            this.groupBox_set.TabStop = false;
            this.groupBox_set.Text = "设置";
            // 
            // checkBox_top
            // 
            this.checkBox_top.AutoSize = true;
            this.checkBox_top.Location = new System.Drawing.Point(165, 116);
            this.checkBox_top.Name = "checkBox_top";
            this.checkBox_top.Size = new System.Drawing.Size(48, 16);
            this.checkBox_top.TabIndex = 46;
            this.checkBox_top.Text = "启用";
            this.checkBox_top.UseVisualStyleBackColor = true;
            this.checkBox_top.CheckedChanged += new System.EventHandler(this.checkBox_top_CheckedChanged);
            // 
            // label_a3
            // 
            this.label_a3.AutoSize = true;
            this.label_a3.Location = new System.Drawing.Point(94, 117);
            this.label_a3.Name = "label_a3";
            this.label_a3.Size = new System.Drawing.Size(65, 12);
            this.label_a3.TabIndex = 45;
            this.label_a3.Text = "窗口置顶：";
            // 
            // checkBox_login
            // 
            this.checkBox_login.AutoSize = true;
            this.checkBox_login.Location = new System.Drawing.Point(165, 81);
            this.checkBox_login.Name = "checkBox_login";
            this.checkBox_login.Size = new System.Drawing.Size(48, 16);
            this.checkBox_login.TabIndex = 44;
            this.checkBox_login.Text = "启用";
            this.checkBox_login.UseVisualStyleBackColor = true;
            this.checkBox_login.CheckedChanged += new System.EventHandler(this.checkBox_login_CheckedChanged);
            // 
            // label_a2
            // 
            this.label_a2.AutoSize = true;
            this.label_a2.Location = new System.Drawing.Point(94, 82);
            this.label_a2.Name = "label_a2";
            this.label_a2.Size = new System.Drawing.Size(65, 12);
            this.label_a2.TabIndex = 43;
            this.label_a2.Text = "自动登录：";
            // 
            // checkBox_power
            // 
            this.checkBox_power.AutoSize = true;
            this.checkBox_power.Location = new System.Drawing.Point(165, 46);
            this.checkBox_power.Name = "checkBox_power";
            this.checkBox_power.Size = new System.Drawing.Size(48, 16);
            this.checkBox_power.TabIndex = 42;
            this.checkBox_power.Text = "启用";
            this.checkBox_power.UseVisualStyleBackColor = true;
            this.checkBox_power.CheckedChanged += new System.EventHandler(this.checkBox_power_CheckedChanged);
            // 
            // label_a1
            // 
            this.label_a1.AutoSize = true;
            this.label_a1.Location = new System.Drawing.Point(94, 47);
            this.label_a1.Name = "label_a1";
            this.label_a1.Size = new System.Drawing.Size(65, 12);
            this.label_a1.TabIndex = 41;
            this.label_a1.Text = "开机启动：";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(254)))));
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_330_200;
            this.ClientSize = new System.Drawing.Size(330, 200);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.groupBox_set);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(330, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(330, 200);
            this.Name = "Setting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.groupBox_set.ResumeLayout(false);
            this.groupBox_set.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.GroupBox groupBox_set;
        private System.Windows.Forms.CheckBox checkBox_login;
        private System.Windows.Forms.Label label_a2;
        private System.Windows.Forms.CheckBox checkBox_power;
        private System.Windows.Forms.Label label_a1;
        private System.Windows.Forms.CheckBox checkBox_top;
        private System.Windows.Forms.Label label_a3;
    }
}