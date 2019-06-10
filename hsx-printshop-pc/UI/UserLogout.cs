using MaSoft.Code;
using System;
using System.Linq;
using System.Windows.Forms;
using MaSoft.Code.Dao;
using SQLiteSugar;

namespace MaSoft.UI
{
    public partial class UserLogout : Form
    {
        #region 初始化

        public UserLogout()
        {
            InitializeComponent();
        }

        private void UserLogout_Load(object sender, EventArgs e)
        {
            textBox_msg.Text = "请输入操作码进行操作";
        }

        #endregion

        #region 方法

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (!textBox_password.Text.IsNullOrEmpty())
            {
                using (var db = SugarDao.GetInstance())
                {
                    var model = db.Queryable<User>().FirstOrDefault();
                    if (model != null)
                    {
                        if (model.SafePassWord == textBox_password.Text.GetMD5_32().GetMD5_32())
                        {
                            //认证成功
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            textBox_msg.Text = "操作码错误";
                        }
                    }
                    else
                    {
                        textBox_msg.Text = "非法错误，请重新登录";
                    }
                }
            }
            else
            {
                textBox_msg.Text = "请输入操作码";
            }
        }

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void textBox_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                button_ok_Click(sender, e);
            }
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
