using MaSoft.Code;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MaSoft.Code.Dao;
using SQLiteSugar;

namespace MaSoft.UI
{
    public partial class SelectPayWay : Form
    {
        public PayType PayType = PayType.OnlinePay;

        #region 初始化
        public SelectPayWay()
        {
            InitializeComponent();
        }

        private void SelectPayWay_Load(object sender, EventArgs e)
        {
            PayType = PayType.OnlinePay;
            textBox_msg.Text = "请呼叫店员输入操作码";
            textBox_msg.ForeColor = Color.Gray;
        }

        #endregion

        #region 方法

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_money_Click(object sender, EventArgs e)
        {
            SetBtnStyle(PayType.Money);
        }

        private void button_pos_Click(object sender, EventArgs e)
        {
            SetBtnStyle(PayType.Pos);
        }

        private void button_card_Click(object sender, EventArgs e)
        {
            SetBtnStyle(PayType.Card);
        }

        private void button_tally_Click(object sender, EventArgs e)
        {
            SetBtnStyle(PayType.Tally);
        }

        private void button_other_Click(object sender, EventArgs e)
        {
            SetBtnStyle(PayType.Other);
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (PayType != PayType.OnlinePay && PayType != PayType.WxPay && PayType != PayType.AliPay)
            {
                if (!textBox_password.Text.IsNullOrEmpty())
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        var model = db.Queryable<User>().FirstOrDefault();
                        if (model!=null)
                        {
                            if (model.SafePassWord == textBox_password.Text.GetMD5_32().GetMD5_32())
                            {
                                //认证成功
                                DialogResult = DialogResult.OK;
                            }
                            else
                            {
                                textBox_msg.Text = "操作码错误";
                                textBox_msg.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            textBox_msg.Text = "非法错误，请重新登录";
                            textBox_msg.ForeColor = Color.Red;
                        }
                    }
                }
                else
                {
                    textBox_msg.Text = "请输入操作码";
                    textBox_msg.ForeColor = Color.Red;
                }
            }
            else
            {
                textBox_msg.Text = "请选择线下支付方式";
                textBox_msg.ForeColor = Color.Red;
            }
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

        #region 按钮样式

        public void SetBtnStyle(PayType pt)
        {
            switch (pt)
            {
                case PayType.Money:
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(255, 254, 255);
                    button_pos.FlatAppearance.BorderColor = Color.Silver;
                    button_pos.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(255, 254, 255);
                    button_card.FlatAppearance.BorderColor = Color.Silver;
                    button_card.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(255, 254, 255);
                    button_tally.FlatAppearance.BorderColor = Color.Silver;
                    button_tally.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(255, 254, 255);
                    button_other.FlatAppearance.BorderColor = Color.Silver;
                    button_other.ForeColor = Color.FromArgb(64, 64, 64);
                    //现金
                    button_money.BackColor = Color.FromArgb(121, 52, 250);
                    button_money.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                    button_money.ForeColor = Color.Black;
                    break;
                case PayType.Pos:
                    //现金
                    button_money.BackColor = Color.FromArgb(255, 254, 255);
                    button_money.FlatAppearance.BorderColor = Color.Silver;
                    button_money.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(255, 254, 255);
                    button_card.FlatAppearance.BorderColor = Color.Silver;
                    button_card.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(255, 254, 255);
                    button_tally.FlatAppearance.BorderColor = Color.Silver;
                    button_tally.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(255, 254, 255);
                    button_other.FlatAppearance.BorderColor = Color.Silver;
                    button_other.ForeColor = Color.FromArgb(64, 64, 64);
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(121, 52, 250);
                    button_pos.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                    button_pos.ForeColor = Color.Black;
                    break;
                case PayType.Card:
                    //现金
                    button_money.BackColor = Color.FromArgb(255, 254, 255);
                    button_money.FlatAppearance.BorderColor = Color.Silver;
                    button_money.ForeColor = Color.FromArgb(64, 64, 64);
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(255, 254, 255);
                    button_pos.FlatAppearance.BorderColor = Color.Silver;
                    button_pos.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(255, 254, 255);
                    button_tally.FlatAppearance.BorderColor = Color.Silver;
                    button_tally.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(255, 254, 255);
                    button_other.FlatAppearance.BorderColor = Color.Silver;
                    button_other.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(121, 52, 250);
                    button_card.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                    button_card.ForeColor = Color.Black;
                    break;
                case PayType.Tally:
                    //现金
                    button_money.BackColor = Color.FromArgb(255, 254, 255);
                    button_money.FlatAppearance.BorderColor = Color.Silver;
                    button_money.ForeColor = Color.FromArgb(64, 64, 64);
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(255, 254, 255);
                    button_pos.FlatAppearance.BorderColor = Color.Silver;
                    button_pos.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(255, 254, 255);
                    button_card.FlatAppearance.BorderColor = Color.Silver;
                    button_card.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(255, 254, 255);
                    button_other.FlatAppearance.BorderColor = Color.Silver;
                    button_other.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(121, 52, 250);
                    button_tally.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                    button_tally.ForeColor = Color.Black;
                    break;
                case PayType.Other:
                    //现金
                    button_money.BackColor = Color.FromArgb(255, 254, 255);
                    button_money.FlatAppearance.BorderColor = Color.Silver;
                    button_money.ForeColor = Color.FromArgb(64, 64, 64);
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(255, 254, 255);
                    button_pos.FlatAppearance.BorderColor = Color.Silver;
                    button_pos.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(255, 254, 255);
                    button_card.FlatAppearance.BorderColor = Color.Silver;
                    button_card.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(255, 254, 255);
                    button_tally.FlatAppearance.BorderColor = Color.Silver;
                    button_tally.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(121, 52, 250);
                    button_other.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                    button_other.ForeColor = Color.Black;
                    break;
                default:
                    //现金
                    button_money.BackColor = Color.FromArgb(255, 254, 255);
                    button_money.FlatAppearance.BorderColor = Color.Silver;
                    button_money.ForeColor = Color.FromArgb(64, 64, 64);
                    //银行卡
                    button_pos.BackColor = Color.FromArgb(255, 254, 255);
                    button_pos.FlatAppearance.BorderColor = Color.Silver;
                    button_pos.ForeColor = Color.FromArgb(64, 64, 64);
                    //会员卡
                    button_card.BackColor = Color.FromArgb(255, 254, 255);
                    button_card.FlatAppearance.BorderColor = Color.Silver;
                    button_card.ForeColor = Color.FromArgb(64, 64, 64);
                    //记账
                    button_tally.BackColor = Color.FromArgb(255, 254, 255);
                    button_tally.FlatAppearance.BorderColor = Color.Silver;
                    button_tally.ForeColor = Color.FromArgb(64, 64, 64);
                    //其他
                    button_other.BackColor = Color.FromArgb(255, 254, 255);
                    button_other.FlatAppearance.BorderColor = Color.Silver;
                    button_other.ForeColor = Color.FromArgb(64, 64, 64);
                    break;
            }
            PayType = pt;
        }


        #endregion

        
    }
}
