namespace MaSoft.UI
{
    partial class SelectPayWay
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
            this.button_other = new System.Windows.Forms.Button();
            this.button_tally = new System.Windows.Forms.Button();
            this.button_card = new System.Windows.Forms.Button();
            this.button_pos = new System.Windows.Forms.Button();
            this.button_money = new System.Windows.Forms.Button();
            this.textBox_password = new System.Windows.Forms.TextBox();
            this.panel_money = new System.Windows.Forms.Panel();
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
            this.label_h1.Location = new System.Drawing.Point(125, 25);
            this.label_h1.Name = "label_h1";
            this.label_h1.Size = new System.Drawing.Size(74, 21);
            this.label_h1.TabIndex = 18;
            this.label_h1.Text = "线下支付";
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.Image = global::MaSoft.Properties.Resources.MessageBox_close;
            this.pictureBox_close.Location = new System.Drawing.Point(297, 7);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_close.TabIndex = 19;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Click += new System.EventHandler(this.pictureBox_close_Click);
            this.pictureBox_close.MouseLeave += new System.EventHandler(this.pictureBox_close_MouseLeave);
            this.pictureBox_close.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_close_MouseMove);
            // 
            // button_other
            // 
            this.button_other.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_other.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_other.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_other.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_other.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_other.Location = new System.Drawing.Point(122, 118);
            this.button_other.Name = "button_other";
            this.button_other.Size = new System.Drawing.Size(80, 30);
            this.button_other.TabIndex = 20;
            this.button_other.TabStop = false;
            this.button_other.Text = "其他";
            this.button_other.UseVisualStyleBackColor = false;
            this.button_other.Click += new System.EventHandler(this.button_other_Click);
            // 
            // button_tally
            // 
            this.button_tally.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_tally.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_tally.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_tally.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_tally.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_tally.Location = new System.Drawing.Point(36, 118);
            this.button_tally.Name = "button_tally";
            this.button_tally.Size = new System.Drawing.Size(80, 30);
            this.button_tally.TabIndex = 20;
            this.button_tally.TabStop = false;
            this.button_tally.Text = "记账";
            this.button_tally.UseVisualStyleBackColor = false;
            this.button_tally.Click += new System.EventHandler(this.button_tally_Click);
            // 
            // button_card
            // 
            this.button_card.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_card.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_card.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_card.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_card.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_card.Location = new System.Drawing.Point(208, 82);
            this.button_card.Name = "button_card";
            this.button_card.Size = new System.Drawing.Size(80, 30);
            this.button_card.TabIndex = 20;
            this.button_card.TabStop = false;
            this.button_card.Text = "会员卡";
            this.button_card.UseVisualStyleBackColor = false;
            this.button_card.Click += new System.EventHandler(this.button_card_Click);
            // 
            // button_pos
            // 
            this.button_pos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_pos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_pos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pos.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_pos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_pos.Location = new System.Drawing.Point(122, 82);
            this.button_pos.Name = "button_pos";
            this.button_pos.Size = new System.Drawing.Size(80, 30);
            this.button_pos.TabIndex = 20;
            this.button_pos.TabStop = false;
            this.button_pos.Text = "银行卡";
            this.button_pos.UseVisualStyleBackColor = false;
            this.button_pos.Click += new System.EventHandler(this.button_pos_Click);
            // 
            // button_money
            // 
            this.button_money.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.button_money.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button_money.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_money.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_money.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_money.Location = new System.Drawing.Point(36, 82);
            this.button_money.Name = "button_money";
            this.button_money.Size = new System.Drawing.Size(80, 30);
            this.button_money.TabIndex = 20;
            this.button_money.TabStop = false;
            this.button_money.Text = "现金";
            this.button_money.UseVisualStyleBackColor = false;
            this.button_money.Click += new System.EventHandler(this.button_money_Click);
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
            // panel_money
            // 
            this.panel_money.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            this.panel_money.Controls.Add(this.textBox_password);
            this.panel_money.Location = new System.Drawing.Point(36, 194);
            this.panel_money.Name = "panel_money";
            this.panel_money.Size = new System.Drawing.Size(252, 36);
            this.panel_money.TabIndex = 25;
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
            this.button_ok.Location = new System.Drawing.Point(112, 259);
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
            this.textBox_msg.Location = new System.Drawing.Point(12, 165);
            this.textBox_msg.Multiline = true;
            this.textBox_msg.Name = "textBox_msg";
            this.textBox_msg.ReadOnly = true;
            this.textBox_msg.ShortcutsEnabled = false;
            this.textBox_msg.Size = new System.Drawing.Size(296, 23);
            this.textBox_msg.TabIndex = 33;
            this.textBox_msg.TabStop = false;
            this.textBox_msg.Text = "请呼叫店员输入操作码";
            this.textBox_msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SelectPayWay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImage = global::MaSoft.Properties.Resources.bg_320_323;
            this.ClientSize = new System.Drawing.Size(320, 323);
            this.ControlBox = false;
            this.Controls.Add(this.textBox_msg);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.panel_money);
            this.Controls.Add(this.button_other);
            this.Controls.Add(this.button_tally);
            this.Controls.Add(this.button_card);
            this.Controls.Add(this.button_pos);
            this.Controls.Add(this.button_money);
            this.Controls.Add(this.pictureBox_close);
            this.Controls.Add(this.label_h1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 323);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 323);
            this.Name = "SelectPayWay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectPayWay";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SelectPayWay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.panel_money.ResumeLayout(false);
            this.panel_money.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_h1;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.Button button_other;
        private System.Windows.Forms.Button button_tally;
        private System.Windows.Forms.Button button_card;
        private System.Windows.Forms.Button button_pos;
        private System.Windows.Forms.Button button_money;
        private System.Windows.Forms.TextBox textBox_password;
        private System.Windows.Forms.Panel panel_money;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.TextBox textBox_msg;
    }
}