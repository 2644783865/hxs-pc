namespace MaSoft
{
    partial class Msg
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
            this.Label_msg = new System.Windows.Forms.Label();
            this.Timer_close = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Label_msg
            // 
            this.Label_msg.AutoSize = true;
            this.Label_msg.BackColor = System.Drawing.Color.Transparent;
            this.Label_msg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_msg.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Label_msg.Location = new System.Drawing.Point(11, 11);
            this.Label_msg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_msg.Name = "Label_msg";
            this.Label_msg.Size = new System.Drawing.Size(77, 12);
            this.Label_msg.TabIndex = 0;
            this.Label_msg.Text = "显示提示信息";
            // 
            // Timer_close
            // 
            this.Timer_close.Tick += new System.EventHandler(this.Timer_close_Tick);
            // 
            // Msg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(49)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(100, 34);
            this.ControlBox = false;
            this.Controls.Add(this.Label_msg);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Msg";
            this.Opacity = 0.5D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Msg";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(49)))), ((int)(((byte)(245)))));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_msg;
        private System.Windows.Forms.Timer Timer_close;
    }
}