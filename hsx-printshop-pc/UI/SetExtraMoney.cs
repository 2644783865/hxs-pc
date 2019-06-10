using MaSoft.Code;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using MaSoft.Code.Dao;
using SQLiteSugar;

namespace MaSoft.UI
{
    public partial class SetExtraMoney : Form
    {
        private bool _gluing = false;
        private bool _typesetting = false;
        private bool _copy = false;
        private bool _scan = false;
        private bool _other = false;

        #region 窗口初始化

        public SetExtraMoney()
        {
            InitializeComponent();
        }

        private void SetExtraMoney_Load(object sender, EventArgs e)
        {
            textBox_msg.Text = "请选择附加费用类型";
            textBox_money.Focus();

            using (var db = SugarDao.GetInstance())
            {
                var model = db.Queryable<JobExtra>().FirstOrDefault();
                if (model == null) return;
                _gluing = model.ExtraGluing;
                SetBtnStyle(button_gluing, _gluing);
                _typesetting = model.ExtraTypesetting;
                SetBtnStyle(button_typesetting, _typesetting);
                _copy = model.ExtraCopy;
                SetBtnStyle(button_copy, _copy);
                _scan = model.ExtraScan;
                SetBtnStyle(button_scan, _scan);
                _other = model.ExtraOther;
                SetBtnStyle(button_other, _other);
                textBox_money.Text = model.ExtraPrice>0?model.ExtraPrice.ToString(CultureInfo.InvariantCulture):"";
            }
        }

        #endregion

        #region 方法

        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            using (var db = SugarDao.GetInstance())
            {
                if (_gluing || _typesetting || _copy || _scan || _other)
                {
                    if (textBox_money.Text.IsNull())
                    {
                        textBox_msg.Text = "请输入附加费用金额";
                    }
                    else
                    {
                        var model = db.Queryable<JobExtra>().FirstOrDefault();
                        if (model != null)
                        {
                            db.Update<JobExtra>(new
                            {
                                ExtraGluing = _gluing,
                                ExtraTypesetting = _typesetting,
                                ExtraCopy = _copy,
                                ExtraScan = _scan,
                                ExtraOther = _other,
                                ExtraPrice = textBox_money.Text.TryDouble()
                            }, it => it.Id == model.Id); 
                        }
                        else
                        {
                            db.Insert(new JobExtra()
                            {
                                ExtraGluing = _gluing,
                                ExtraTypesetting = _typesetting,
                                ExtraCopy = _copy,
                                ExtraScan = _scan,
                                ExtraOther = _other,
                                ExtraPrice = textBox_money.Text.TryDouble()
                            });
                        }
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void button_gluing_Click(object sender, EventArgs e)
        {
            _gluing = !_gluing;
            SetBtnStyle(button_gluing, _gluing);
        }

        private void button_typesetting_Click(object sender, EventArgs e)
        {
            _typesetting = !_typesetting;
            SetBtnStyle(button_typesetting, _typesetting);
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            _copy = !_copy;
            SetBtnStyle(button_copy, _copy);
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
            _scan = !_scan;
            SetBtnStyle(button_scan, _scan);
        }

        private void button_other_Click(object sender, EventArgs e)
        {
            _other = !_other;
            SetBtnStyle(button_other, _other);
        }

        private void button_no_Click(object sender, EventArgs e)
        {
            _gluing = false;
            SetBtnStyle(button_gluing, _gluing);
            _typesetting = false;
            SetBtnStyle(button_typesetting, _typesetting);
            _copy = false;
            SetBtnStyle(button_copy, _copy);
            _scan = false;
            SetBtnStyle(button_scan, _scan);
            _other = false;
            SetBtnStyle(button_other, _other);
            textBox_money.Text = "";
            using (var db =SugarDao.GetInstance())
            {
                var model = db.Queryable<JobExtra>().FirstOrDefault();
                if (model == null) return;
                db.Update<JobExtra>(new
                {
                    ExtraGluing = false,
                    ExtraTypesetting = false,
                    ExtraCopy = false,
                    ExtraScan = false,
                    ExtraOther = false,
                    ExtraPrice = 0
                }, it => it.Id == model.Id); 
            }
        }

        private void textBox_money_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                button_ok_Click(sender, e);
            }
        }

        //价格输入框控制
        private void textBox_money_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.') && e.KeyChar != (char)('-'))
            {
                e.Handled = true;
            }
            if (e.KeyChar == (char)('-'))
            {
                if ((sender as TextBox).Text != "")
                {
                    e.Handled = true;
                }
            }
            //第1位是负号时候、第2位小数点不可
            if (((TextBox)sender).Text == "-" && e.KeyChar == (char)('.'))
            {
                e.Handled = true;
            }
            //负号只能1次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                e.Handled = true;
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }
            //小数点（最大到2位）   
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 2 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                e.Handled = true;
            //光标在小数点右侧时候判断  
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
            }
        }

        #endregion

        #region 按钮样式

        private void SetBtnStyle(Button button, bool flag)
        {
            if (flag)
            {
                button.BackColor = Color.FromArgb(121, 52, 250);
                button.FlatAppearance.BorderColor = Color.FromArgb(121, 52, 250);
                button.ForeColor = Color.Black;
            }
            else
            {
                button.BackColor = Color.FromArgb(255, 254, 255);
                button.FlatAppearance.BorderColor = Color.DarkGray;
                button.ForeColor = Color.DarkGray;
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
