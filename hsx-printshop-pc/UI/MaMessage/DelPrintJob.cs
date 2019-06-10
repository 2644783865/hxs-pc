using System;
using System.Windows.Forms;

namespace MaSoft.UI.MaMessage
{
    public partial class DelPrintJob : Form
    {

        #region 初始化

        public DelPrintJob()
        {
            InitializeComponent();
        }

        private void Del_PrintJob_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 关闭效果

        private void pictureBox_close_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_close.Image = Properties.Resources.MessageBox_close;
        }

        private void pictureBox_close_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox_close.Image = Properties.Resources.MessageBox_close_hover;
        }

        #endregion
        
    }
}
