using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MaSoft.Code
{

    public static class Dialog
    {
        public static object lockObj;

        private static NotifyIcon notifyIcon_0;

        public static void Warn(string message, bool logWarn = false)
        {
            MessageBox.Show(message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (logWarn)
            {
                Log.Warn(message);
            }
        }

        public static void Warn(string msg, Form owner, MessageBoxOptions options)
        {
            MessageBox.Show(msg, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, options);
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void Info(string msg, Form owner, MessageBoxOptions options)
        {
            MessageBox.Show(msg, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, options);
        }

        public static DialogResult Confirm(string message)
        {
            return MessageBox.Show(message, "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void Error(Exception ex, bool logError = true)
        {
            string str = string.Empty;
            if (ex != null)
            {
                str = ex.Message;
            }
            MessageBox.Show("出现错误: " + str, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            if (logError)
            {
                Log.Error(ex);
            }
        }

        public static void Error(Exception ex, MethodBase methodInfo, bool logError = true)
        {
            string str = string.Empty;
            if (ex != null)
            {
                str = ex.Message;
            }
            if (logError)
            {
                string str2 = string.Empty;
                if (methodInfo != null)
                {
                    str2 = string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name, methodInfo);
                }
                Log.Error("出现错误:" + str2 + "," + str);
            }
        }

        public static void Error(string msg, Exception ex, MethodBase methodInfo, bool logError = true)
        {
            string text = string.Empty;
            if (ex != null)
            {
                text = ex.Message;
            }
            if (logError)
            {
                string text2 = string.Empty;
                if (methodInfo != null)
                {
                    text2 = string.Format("{0}.{1}", methodInfo.DeclaringType.Name, methodInfo.Name, methodInfo);
                }
                Log.Error("出现错误:" + msg + "," + text2 + "," + text);
            }
        }

        public static void Error(string title, Exception ex)
        {
            MessageBox.Show(title + "时出现错误: " + ex.GetType().Name, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static DialogResult ShowDialog(Form frm, string successMsg, string failMsg)
        {
            DialogResult dialogResult = DialogResult.No;
            try
            {
                dialogResult = frm.ShowDialog();
                if (dialogResult != DialogResult.OK)
                {
                    return dialogResult;
                }
                if (string.IsNullOrEmpty(successMsg))
                {
                    if (string.IsNullOrEmpty(failMsg))
                    {
                        return dialogResult;
                    }
                    Warn(failMsg);
                    return dialogResult;
                }
                Info(successMsg);
                return dialogResult;
            }
            catch (Exception ex)
            {
                Error(ex, MethodBase.GetCurrentMethod());
                return dialogResult;
            }
        }

        public static NotifyIcon SetNotify(string text, Form form = null, ContextMenuStrip menu = null)
        {
            notifyIcon_0 = new NotifyIcon();
            notifyIcon_0.Icon = ((form == null) ? new Icon("logo.ico") : form.Icon);
            notifyIcon_0.Visible = true;
            notifyIcon_0.Text = text;
            notifyIcon_0.MouseClick += delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    smethod_0(form);
                }
            };
            if (menu == null)
            {
                menu = new ContextMenuStrip();
                menu.Items.Add("显示主界面", null, delegate
                {
                    if (form != null)
                    {
                        form.WindowState = FormWindowState.Maximized;
                        form.Activate();
                        form.ShowInTaskbar = true;
                    }
                });
                menu.Items.Add("退出", null, delegate
                {
                    notifyIcon_0.Visible = false;
                    notifyIcon_0.Dispose();
                    notifyIcon_0 = null;
                    Application.Exit();
                });
                notifyIcon_0.ContextMenuStrip = menu;
            }
            return notifyIcon_0;
        }

        public static void ClearNotify()
        {
            if (notifyIcon_0 != null)
            {
                notifyIcon_0.ContextMenuStrip = null;
                notifyIcon_0.Visible = false;
                notifyIcon_0.Dispose();
            }
        }

        private static void smethod_0(Form form_0)
        {
            if (form_0 != null)
            {
                if (form_0.WindowState == FormWindowState.Minimized)
                {
                    form_0.WindowState = FormWindowState.Maximized;
                    form_0.Activate();
                    form_0.ShowInTaskbar = true;
                }
                else
                {
                    form_0.WindowState = FormWindowState.Minimized;
                    form_0.ShowInTaskbar = false;
                }
            }
        }

        public static Form InvokeForm(Type formType, bool isDialog = true, bool isTopMost = true, bool isShow = true, params object[] paras)
        {
            Form form = null;
            if (formType != null)
            {
                form = (Form)formType.Assembly.CreateInstance(formType.FullName, true, BindingFlags.CreateInstance, null, paras, null, null);
                if (form != null)
                {
                    if (isShow)
                    {
                        if (isDialog)
                        {
                            form.ShowDialog();
                        }
                        else
                        {
                            form.Show();
                        }
                    }
                    form.TopMost = isTopMost;
                }
            }
            return form;
        }

        static Dialog()
        {

            lockObj = new object();
            notifyIcon_0 = null;
        }
    }

}
