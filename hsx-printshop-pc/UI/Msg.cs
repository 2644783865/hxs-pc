using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaSoft
{
    public partial class Msg : Form
    {
        public Msg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 弹窗提示
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="x">父级宽度</param>
        /// <param name="y">父级高度</param>
        /// <param name="time">消失时间</param>
        public void Show(string msg, int x, int y, int time = 2)
        {
            Label_msg.Text = msg;
            Width = Label_msg.Width + 20;
            Timer_close.Interval = time * 1000;
            Timer_close.Start();
            Location = new Point(x - Width / 2, y - 40);
            Show();
        }

        private void Timer_close_Tick(object sender, EventArgs e)
        {
            Hide();
        }

    }
}
