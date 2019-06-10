using CsharpHttpHelper;
using MaSoft.Code;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using MaSoft.Code.Dao;
using MaSoft.Properties;
using SQLiteSugar;

namespace MaSoft
{
    public partial class Login : Form
    {
        #region 初始化

        /// <summary>
        /// 终端信息
        /// </summary>
        private string terminal = "";

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            terminal = "机器名称：" + Environment.MachineName +"<br/>" +
                       "操作系统：" + Environment.OSVersion + "<br/>" +
                       "分辨率：" + Screen.PrimaryScreen.Bounds.Width + " * " + Screen.PrimaryScreen.Bounds.Height + "<br/>" +
                       "Framework版本：" + Environment.Version + "<br/>";


            #region 设置壁纸
            var filePath = Directory.GetCurrentDirectory() + "\\hxs.bmp";
            if (System.IO.File.Exists(filePath))
            {
                SystemParametersInfo(20, 0, filePath, 0x2);
            }
            #endregion

            TextBox_username.SetWatermark("用户名");
            TextBox_password.SetWatermark("登录密码");
            Label_info.Text = "v" + ProductVersion;

            using (var db = SugarDao.GetInstance())
            {
                var user = db.Queryable<User>().SingleOrDefault();
                if (user != null)
                {
                    TextBox_username.Text = user.UserName;
                    TextBox_password.Text = user.PassWord;

                    if (user.IsStartRun)
                    {
                        SetMeAutoStart(true);
                    }

                    if (user.IsAutoLogin)
                    {
                        Button_login.Enabled = false;
                        Button_login.Text = "正在登录";
                        timer_login.Start();
                    }
                }
                else
                {
                    SetMeAutoStart(true);
                }
            }
        }

        private void timer_login_Tick(object sender, EventArgs e)
        {
            timer_login.Stop();
            AutoLogin();
        }

        /// <summary>
        /// 自动登录 程序启动后2秒后执行
        /// </summary>
        public void AutoLogin()
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    //【第一步】程序自检之后更新本地用户信息。
                    var model = db.Queryable<User>().SingleOrDefault();
                    if (model != null)
                    {
                        //自动登录
                        if (model.IsAutoLogin)
                        {
                            //请求 用户登录
                            var url = Resources.ServiceUrl + Resources.ServiceUserLogin;
                            var identification = (HardwareTool.GetCpuId() + HardwareTool.GetHardDiskId()).GetMD5_32();
                            var request = new
                            {
                                username = model.UserName,
                                password = model.PassWord,
                                uniqueid = identification
                            };
                            var http = new HttpHelper();
                            var item = new HttpItem()
                            {
                                URL = url,
                                Method = "post",
                                PostEncoding = Encoding.UTF8,
                                ContentType = "application/json",
                                Postdata = request.ToJson(),
                                Timeout = 10000
                            };
                            var result = http.GetHtml(item);
                            if (result.StatusCode == HttpStatusCode.OK)
                            {
                                var json = DynamicJson.Parse(result.Html);
                                if (json.error_code() && json.error_code == 0)
                                {

                                    //更新本地 用户登录信息[User]表
                                    db.Update<User>(new
                                    {
                                        SafePassWord = json.data.safepassword,
                                        LoginTime = DateTime.Now,
                                        Token = json.data.token,
                                        Guid = identification,
                                        Prefix = json.data.prefix,
                                        PrintShopName = json.data.printshopname
                                    }, it => it.Id == model.Id);

                                    //更新本地 放行打印机[PassPrinter]表
                                    if (json.data.pass())
                                    {
                                        if (db.Queryable<PassPrinter>().Count()>0)
                                        {
                                            db.ExecuteCommand("UPDATE PassPrinter SET NeedDel=1");
                                            foreach (var pass in json.data.pass)
                                            {
                                                string printName = pass.printName;
                                                var modelPassPrint = db.Queryable<PassPrinter>().Where(c => c.PrintName == printName).SingleOrDefault();
                                                if (modelPassPrint != null)
                                                {
                                                    db.Update<PassPrinter>(new { NeedDel = false }, it => it.Id == modelPassPrint.Id); 
                                                }
                                                else
                                                {
                                                    db.Insert(new PassPrinter()
                                                    {
                                                        PrintName = printName,
                                                        NeedDel = false
                                                    });
                                                }
                                            }

                                        }
                                        else
                                        {
                                            foreach (var pass in json.data.pass)
                                            {
                                                db.Insert(new PassPrinter()
                                                {
                                                    PrintName = pass.printName,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }

                                    //更新本地 打印价格表[PrintPrice]表。
                                    if (json.data.price())
                                    {
                                        if (db.Queryable<PrintPrice>().Count() > 0)
                                        {
                                            db.ExecuteCommand("UPDATE PrintPrice SET NeedDel=1");
                                            foreach (var price in json.data.price)
                                            {
                                                int mediaType = (int)price.mediatype;
                                                int duplex = (int)price.duplex;
                                                int pageSize = (int)price.pagesize;
                                                int color = (int)price.colour;

                                                var modelPrintPrice = db.Queryable<PrintPrice>().Where(m => m.MediaType == mediaType &&
                                                                                                            m.Duplex == duplex &&
                                                                                                            m.PageSize == pageSize &&
                                                                                                            m.Color == color).SingleOrDefault();
                                                if (modelPrintPrice != null)
                                                {
                                                    db.Update<PrintPrice>(new
                                                    {
                                                        Money = (int)price.price ,
                                                        SalePage1 = (int)price.salepage1,
                                                        SalePrice1 = (int)price.saleprice1,
                                                        SalePage2 = (int)price.salepage2,
                                                        SalePrice2 = (int)price.saleprice2,
                                                        SalePage3 = (int)price.salepage3,
                                                        SalePrice3 = (int)price.saleprice3,
                                                        NeedDel = false

                                                    }, it => it.Id == modelPrintPrice.Id);
                                                }
                                                else
                                                {
                                                    db.Insert(new PrintPrice()
                                                    {
                                                        MediaType = mediaType,
                                                        Duplex = duplex,
                                                        PageSize = pageSize,
                                                        Color = color,
                                                        Money = (int)price.price,
                                                        SalePage1 = (int)price.salepage1,
                                                        SalePrice1 = (int)price.saleprice1,
                                                        SalePage2 = (int)price.salepage2,
                                                        SalePrice2 = (int)price.saleprice2,
                                                        SalePage3 = (int)price.salepage3,
                                                        SalePrice3 = (int)price.saleprice3,
                                                        NeedDel = false
                                                    });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var price in json.data.price)
                                            {
                                                db.Insert(new PrintPrice()
                                                {
                                                    MediaType = (int)price.mediatype,
                                                    Duplex = (int)price.duplex,
                                                    PageSize = (int)price.pagesize,
                                                    Color = (int)price.colour,
                                                    Money = (int)price.price,
                                                    SalePage1 = (int)price.salepage1,
                                                    SalePrice1 = (int)price.saleprice1,
                                                    SalePage2 = (int)price.salepage2,
                                                    SalePrice2 = (int)price.saleprice2,
                                                    SalePage3 = (int)price.salepage3,
                                                    SalePrice3 = (int)price.saleprice3,
                                                    NeedDel = false
                                                });
                                            }
                                        }
                                    }

                                    //更新黑白打印机
                                    if (json.data.blackPrintersPclist())
                                    {
                                        if (db.Queryable<BlackPrinter>().Count() > 0)
                                        {
                                            db.ExecuteCommand("UPDATE BlackPrinter SET NeedDel=1");
                                            foreach (var black in json.data.blackPrintersPclist)
                                            {
                                                string printDrive = black.printDrive;
                                                var modelBlackPrint = db.Queryable<BlackPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                                if (modelBlackPrint != null)
                                                {
                                                    db.Update<BlackPrinter>(new { NeedDel = false }, it => it.Id == modelBlackPrint.Id); 
                                                }
                                                else
                                                {
                                                    db.Insert(new BlackPrinter()
                                                    {
                                                        PrintDrive = printDrive,
                                                        NeedDel = false
                                                    });
                                                }
                                            }

                                        }
                                        else
                                        {
                                            foreach (var black in json.data.blackPrintersPclist)
                                            {
                                                db.Insert(new BlackPrinter()
                                                {
                                                    PrintDrive = black.printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }

                                    //更新彩色打印机
                                    if (json.data.colourPrintersPclist())
                                    {
                                        if (db.Queryable<ColorPrinter>().Count() > 0)
                                        {
                                            db.ExecuteCommand("UPDATE ColorPrinter SET NeedDel=1");
                                            foreach (var color in json.data.colourPrintersPclist)
                                            {
                                                string printDrive = color.printDrive;
                                                var modelColorPrint = db.Queryable<ColorPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                                if (modelColorPrint != null)
                                                {
                                                    db.Update<ColorPrinter>(new { NeedDel = false }, it => it.Id == modelColorPrint.Id); 
                                                }
                                                else
                                                {
                                                    db.Insert(new ColorPrinter()
                                                    {
                                                        PrintDrive = printDrive,
                                                        NeedDel = false
                                                    });
                                                }
                                            }

                                        }
                                        else
                                        {
                                            foreach (var color in json.data.colourPrintersPclist)
                                            {
                                                db.Insert(new ColorPrinter()
                                                {
                                                    PrintDrive = color.printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }

                                    //重复打印机
                                    if (json.data.repeatThePrintersPclsit())
                                    {
                                        if (db.Queryable<RepeatPrinter>().Count() > 0)
                                        {
                                            db.ExecuteCommand("UPDATE RepeatPrinter SET NeedDel=1");
                                            foreach (var repeat in json.data.repeatThePrintersPclsit)
                                            {
                                                string printDrive = repeat.printDrive;
                                                var modelRepeatPrint = db.Queryable<RepeatPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                                if (modelRepeatPrint != null)
                                                {
                                                    db.Update<RepeatPrinter>(new { NeedDel = false }, it => it.Id == modelRepeatPrint.Id); 
                                                }
                                                else
                                                {
                                                    db.Insert(new RepeatPrinter()
                                                    {
                                                        PrintDrive = printDrive,
                                                        NeedDel = false
                                                    });
                                                }
                                            }

                                        }
                                        else
                                        {
                                            foreach (var repeat in json.data.repeatThePrintersPclsit)
                                            {
                                                db.Insert(new RepeatPrinter()
                                                {
                                                    PrintDrive = repeat.printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }

                                    //问题打印机
                                    db.ExecuteCommand(@"delete from ProblematicPrinter");
                                    if (json.data.problematicPrintersPclist())
                                    {
                                        foreach (var problematic in json.data.problematicPrintersPclist)
                                        {
                                            int? x = null;
                                            if (problematic.special_target != null && problematic.special_target != "" && problematic.special_target != "null")
                                            {
                                                x = int.Parse(problematic.special_target);
                                            }
                                            db.Insert(new ProblematicPrinter()
                                            {
                                                PrintDrive = problematic.printDrive,
                                                DataType = int.Parse(problematic.dataType),
                                                KeyValues = problematic.key_values,
                                                StartIndex = int.Parse(problematic.start_index),
                                                Length = int.Parse(problematic.length),
                                                ReviseOffset = int.Parse(problematic.revise_offset),
                                                SpecialSrcValue = problematic.special_src_value,
                                                SpecialStart = int.Parse(problematic.special_start),
                                                SpecialLength = int.Parse(problematic.special_length),
                                                SpecialValue = problematic.special_value,
                                                SpecialTarget = x,
                                                StartInex2 = int.Parse(problematic.start_index2),
                                                ForceSpecialReverse = int.Parse(problematic.force_special_reverse),
                                                DmModel = int.Parse(problematic.dm_mode)
                                            });
                                        }
                                    }

                                    //打印订单额外费用 初始化
                                    db.ExecuteCommand(@"delete from JobExtra");
                                    db.Insert(new JobExtra()
                                    {
                                        ExtraGluing = false,
                                        ExtraTypesetting = false,
                                        ExtraCopy = false,
                                        ExtraScan = false,
                                        ExtraOther = false,
                                        ExtraPrice = 0
                                    });

                                    //清空 打印任务[Job]表
                                    db.ExecuteCommand(@"delete from Job");

                                    var printers = new List<LocalPrint>();
                                    //移除所有打印作业
                                    using (var ps = new LocalPrintServer())
                                    {
                                        var queue = ps.GetPrintQueues();
                                        foreach (var pq in queue)
                                        {
                                            printers.Add(new LocalPrint()
                                            {
                                                printName = pq.Name,
                                                printDrive = pq.QueueDriver.Name
                                            });

                                            pq.Refresh();
                                            var jobs = pq.GetPrintJobInfoCollection();
                                            if (pq.NumberOfJobs <= 0) continue;
                                            foreach (var job in jobs)
                                            {
                                                job.Cancel();
                                            }
                                        }
                                    }
                                    //删除 放行打印机[PassPrinter]表 多余记录
                                    db.ExecuteCommand(@"delete from PassPrinter WHERE NeedDel==1");
                                    //删除 打印价格表[PrintPrice]表 多余记录
                                    db.ExecuteCommand(@"delete from PrintPrice WHERE NeedDel==1");
                                    //删除 黑白打印机[BlackPrinter]表 多余记录
                                    db.ExecuteCommand(@"delete from BlackPrinter WHERE NeedDel==1");
                                    //删除 彩色打印机[ColorPrinter]表 多余记录
                                    db.ExecuteCommand(@"delete from ColorPrinter WHERE NeedDel==1");
                                    //删除 重复打印机[RepeatPrinter]表 多余记录
                                    db.ExecuteCommand(@"delete from RepeatPrinter WHERE NeedDel==1");

                                    //更新打印店打印机
                                    if (printers.Any())
                                    {
                                        var data = new SignData();
                                        data.SetValue("token", json.data.token.ToString());
                                        var sign = SignHelper.GetSign(data, identification);
                                        var requestA = new
                                        {
                                            token = json.data.token.ToString(),
                                            list = printers,
                                            sgin = sign
                                        };
                                        var urlA = Resources.ServiceUrl + Resources.ServiceUpdatePrinters;
                                        var httpA = new HttpHelper();
                                        var itemA = new HttpItem()
                                        {
                                            URL = urlA,
                                            Method = "post",
                                            PostEncoding = Encoding.UTF8,
                                            ContentType = "application/json",
                                            Postdata = requestA.ToJson(),
                                            Timeout = 10000
                                        };
                                        httpA.GetHtml(itemA);
                                    }

                                    terminal += "本地打印机：" + printers.ToJson();

                                    var launch = new Launch();
                                    Hide();
                                    launch.Show();
                                }
                                else
                                {
                                    var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                                    msg.Show(json.error_msg() ? json.error_msg : "网络连接超时", 400, 200);
                                    Button_login.Enabled = true;
                                }
                            }
                            else
                            {
                                var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                                msg.Show("网络连接超时", 400, 200);
                                Button_login.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【自动登录】" + exception);
            }
        }

        #endregion

        #region 鼠标移动

        private Point _movePoint;
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_bar_MouseDown(object sender, MouseEventArgs e)
        {
            _movePoint = new Point(e.X, e.Y);
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Location = new Point(Location.X + e.X - _movePoint.X, Location.Y + e.Y - _movePoint.Y);
            }
        }

        #endregion

        #region 关闭、最小化

        private void PictureBox_close_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox_close.Image = Properties.Resources.all_close_yes;
        }

        private void PictureBox_close_MouseLeave(object sender, EventArgs e)
        {
            PictureBox_close.Image = Properties.Resources.all_close_no;
        }

        private void PictureBox_min_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox_min.Image = Properties.Resources.all_min_yes;
        }

        private void PictureBox_min_MouseLeave(object sender, EventArgs e)
        {
            PictureBox_min.Image = Properties.Resources.all_min_no;
        }

        private void PictureBox_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PictureBox_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region 登录按钮特效

        private void Button_login_MouseMove(object sender, MouseEventArgs e)
        {
            Button_login.BackColor = Color.FromArgb(235, 234, 234);
        }

        private void Button_login_MouseLeave(object sender, EventArgs e)
        {
            Button_login.BackColor = Color.FromArgb(249, 249, 249);
        }

        #endregion

        #region 登录

        private void TextBox_password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Control || e.KeyCode == Keys.Enter)
            {
                Button_login_Click(sender, e);
            }
        }

        private void Button_login_Click(object sender, EventArgs e)
        {
            Button_login.Text = "正在登录";
            Task.Factory.StartNew(SetData);
        }
        //声明委托
        private delegate void SetDataDelegate();
        private void SetData()
        {
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new SetDataDelegate(SetData));
                }
                else
                {
                    var username = TextBox_username.Text.Trim();
                    var password = TextBox_password.Text.Trim();
                    if (string.IsNullOrEmpty(username))
                    {
                        var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                        msg.Show("请输入用户名", 400, 200);
                        TextBox_username.Focus();
                        Button_login.Text = "登 录";
                        return;
                    }
                    if (string.IsNullOrEmpty(password))
                    {
                        var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                        msg.Show("请输入登录密码", 400, 200);
                        TextBox_password.Focus();
                        Button_login.Text = "登 录";
                        return;
                    }
                    //请求 用户登录
                    var url = Properties.Resources.ServiceUrl + Properties.Resources.ServiceUserLogin;
                    var identification = (HardwareTool.GetCpuId() + HardwareTool.GetHardDiskId()).GetMD5_32();
                    var request = new
                    {
                        username = username,
                        password = password,
                        uniqueid = identification
                    };
                    var http = new HttpHelper();
                    var item = new HttpItem()
                    {
                        URL = url,
                        Method = "post",
                        PostEncoding = Encoding.UTF8,
                        ContentType = "application/json",
                        Postdata = request.ToJson(),
                        Timeout = 5000
                    };
                    var result = http.GetHtml(item);
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var json = DynamicJson.Parse(result.Html);
                        if (json.error_code() && json.error_code == 0)
                        {
                            using (var db = SugarDao.GetInstance())
                            {
                                //更新本地 用户登录信息[User]表
                                var model = db.Queryable<User>().FirstOrDefault();
                                if (model != null)
                                {
                                    db.Update<User>(new
                                    {
                                        UserName = username,
                                        PassWord = password,
                                        SafePassWord = json.data.safepassword,
                                        LoginTime = DateTime.Now,
                                        Token = json.data.token,
                                        Guid = identification,
                                        Prefix = json.data.prefix,
                                        PrintShopName = json.data.printshopname
                                    }, it => it.Id == model.Id);
                                }
                                else
                                {
                                    //插入
                                    db.Insert(new User()
                                    {
                                        UserName = username,
                                        PassWord = password,
                                        SafePassWord = json.data.safepassword,
                                        IsAutoLogin = true,
                                        LoginTime = DateTime.Now,
                                        Token = json.data.token,
                                        Guid = identification,
                                        Prefix = json.data.prefix,
                                        PrintShopName = json.data.printshopname,
                                        IsTop = true,
                                        IsStartRun = true
                                    });
                                }

                                //更新本地 放行打印机[PassPrinter]表
                                if (json.data.pass())
                                {
                                    if (db.Queryable<PassPrinter>().Count() > 0)
                                    {
                                        db.ExecuteCommand("UPDATE PassPrinter SET NeedDel=1");
                                        foreach (var pass in json.data.pass)
                                        {
                                            string printName = pass.printName;
                                            var modelPassPrint = db.Queryable<PassPrinter>().Where(c => c.PrintName == printName).SingleOrDefault();
                                            if (modelPassPrint != null)
                                            {
                                                db.Update<PassPrinter>(new { NeedDel = false }, it => it.Id == modelPassPrint.Id);
                                            }
                                            else
                                            {
                                                db.Insert(new PassPrinter()
                                                {
                                                    PrintName = printName,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var pass in json.data.pass)
                                        {
                                            db.Insert(new PassPrinter()
                                            {
                                                PrintName = pass.printName,
                                                NeedDel = false
                                            });
                                        }
                                    }

                                }

                                //更新本地 打印价格表[PrintPrice]表。
                                if (json.data.price())
                                {
                                    if (db.Queryable<PrintPrice>().Count() > 0)
                                    {
                                        db.ExecuteCommand("UPDATE PrintPrice SET NeedDel=1");
                                        foreach (var price in json.data.price)
                                        {
                                            int mediaType = (int)price.mediatype;
                                            int duplex = (int)price.duplex;
                                            int pageSize = (int)price.pagesize;
                                            int color = (int)price.colour;

                                            var modelPrintPrice = db.Queryable<PrintPrice>().Where(m => m.MediaType == mediaType &&
                                                                                                        m.Duplex == duplex &&
                                                                                                        m.PageSize == pageSize &&
                                                                                                        m.Color == color).SingleOrDefault();
                                            if (modelPrintPrice != null)
                                            {
                                                db.Update<PrintPrice>(new
                                                {
                                                    Money = (int)price.price,
                                                    SalePage1 = (int)price.salepage1,
                                                    SalePrice1 = (int)price.saleprice1,
                                                    SalePage2 = (int)price.salepage2,
                                                    SalePrice2 = (int)price.saleprice2,
                                                    SalePage3 = (int)price.salepage3,
                                                    SalePrice3 = (int)price.saleprice3,
                                                    NeedDel = false

                                                }, it => it.Id == modelPrintPrice.Id);
                                            }
                                            else
                                            {
                                                db.Insert(new PrintPrice()
                                                {
                                                    MediaType = mediaType,
                                                    Duplex = duplex,
                                                    PageSize = pageSize,
                                                    Color = color,
                                                    Money = (int)price.price,
                                                    SalePage1 = (int)price.salepage1,
                                                    SalePrice1 = (int)price.saleprice1,
                                                    SalePage2 = (int)price.salepage2,
                                                    SalePrice2 = (int)price.saleprice2,
                                                    SalePage3 = (int)price.salepage3,
                                                    SalePrice3 = (int)price.saleprice3,
                                                    NeedDel = false
                                                });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var price in json.data.price)
                                        {
                                            db.Insert(new PrintPrice()
                                            {
                                                MediaType = (int)price.mediatype,
                                                Duplex = (int)price.duplex,
                                                PageSize = (int)price.pagesize,
                                                Color = (int)price.colour,
                                                Money = (int)price.price,
                                                SalePage1 = (int)price.salepage1,
                                                SalePrice1 = (int)price.saleprice1,
                                                SalePage2 = (int)price.salepage2,
                                                SalePrice2 = (int)price.saleprice2,
                                                SalePage3 = (int)price.salepage3,
                                                SalePrice3 = (int)price.saleprice3,
                                                NeedDel = false
                                            });
                                        }
                                    }
                                }

                                //更新黑白打印机
                                if (json.data.blackPrintersPclist())
                                {
                                    if (db.Queryable<BlackPrinter>().Count() > 0)
                                    {
                                        db.ExecuteCommand("UPDATE BlackPrinter SET NeedDel=1");
                                        foreach (var black in json.data.blackPrintersPclist)
                                        {
                                            string printDrive = black.printDrive;
                                            var modelBlackPrint = db.Queryable<BlackPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                            if (modelBlackPrint != null)
                                            {
                                                db.Update<BlackPrinter>(new { NeedDel = false }, it => it.Id == modelBlackPrint.Id);
                                            }
                                            else
                                            {
                                                db.Insert(new BlackPrinter()
                                                {
                                                    PrintDrive = printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var black in json.data.blackPrintersPclist)
                                        {
                                            db.Insert(new BlackPrinter()
                                            {
                                                PrintDrive = black.printDrive,
                                                NeedDel = false
                                            });
                                        }
                                    }

                                }

                                //更新彩色打印机
                                if (json.data.colourPrintersPclist())
                                {
                                    if (db.Queryable<ColorPrinter>().Count() > 0)
                                    {
                                        db.ExecuteCommand("UPDATE ColorPrinter SET NeedDel=1");
                                        foreach (var color in json.data.colourPrintersPclist)
                                        {
                                            string printDrive = color.printDrive;
                                            var modelColorPrint = db.Queryable<ColorPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                            if (modelColorPrint != null)
                                            {
                                                db.Update<ColorPrinter>(new { NeedDel = false }, it => it.Id == modelColorPrint.Id);
                                            }
                                            else
                                            {
                                                db.Insert(new ColorPrinter()
                                                {
                                                    PrintDrive = printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var color in json.data.colourPrintersPclist)
                                        {
                                            db.Insert(new ColorPrinter()
                                            {
                                                PrintDrive = color.printDrive,
                                                NeedDel = false
                                            });
                                        }
                                    }

                                }

                                //重复打印机
                                if (json.data.repeatThePrintersPclsit())
                                {
                                    if (db.Queryable<RepeatPrinter>().Count() > 0)
                                    {
                                        db.ExecuteCommand("UPDATE RepeatPrinter SET NeedDel=1");
                                        foreach (var repeat in json.data.repeatThePrintersPclsit)
                                        {
                                            string printDrive = repeat.printDrive;
                                            var modelRepeatPrint = db.Queryable<RepeatPrinter>().Where(c => c.PrintDrive == printDrive).SingleOrDefault();
                                            if (modelRepeatPrint != null)
                                            {
                                                db.Update<RepeatPrinter>(new { NeedDel = false }, it => it.Id == modelRepeatPrint.Id);
                                            }
                                            else
                                            {
                                                db.Insert(new RepeatPrinter()
                                                {
                                                    PrintDrive = printDrive,
                                                    NeedDel = false
                                                });
                                            }
                                        }

                                    }
                                    else
                                    {
                                        foreach (var repeat in json.data.repeatThePrintersPclsit)
                                        {
                                            db.Insert(new RepeatPrinter()
                                            {
                                                PrintDrive = repeat.printDrive,
                                                NeedDel = false
                                            });
                                        }
                                    }

                                }

                                //问题打印机
                                db.ExecuteCommand(@"delete from ProblematicPrinter");
                                if (json.data.problematicPrintersPclist())
                                {
                                    foreach (var problematic in json.data.problematicPrintersPclist)
                                    {
                                        int? x = null;
                                        if (problematic.special_target != null && problematic.special_target != "" && problematic.special_target != "null")
                                        {
                                            x = int.Parse(problematic.special_target);
                                        }
                                        db.Insert(new ProblematicPrinter()
                                        {
                                            PrintDrive = problematic.printDrive,
                                            DataType = int.Parse(problematic.dataType),
                                            KeyValues = problematic.key_values,
                                            StartIndex = int.Parse(problematic.start_index),
                                            Length = int.Parse(problematic.length),
                                            ReviseOffset = int.Parse(problematic.revise_offset),
                                            SpecialSrcValue = problematic.special_src_value,
                                            SpecialStart = int.Parse(problematic.special_start),
                                            SpecialLength = int.Parse(problematic.special_length),
                                            SpecialValue = problematic.special_value,
                                            SpecialTarget = x,
                                            StartInex2 = int.Parse(problematic.start_index2),
                                            ForceSpecialReverse = int.Parse(problematic.force_special_reverse),
                                            DmModel = int.Parse(problematic.dm_mode)
                                        });
                                    }
                                }

                                //打印订单额外费用 初始化
                                db.ExecuteCommand(@"delete from JobExtra");
                                db.Insert(new JobExtra()
                                {
                                    ExtraGluing = false,
                                    ExtraTypesetting = false,
                                    ExtraCopy = false,
                                    ExtraScan = false,
                                    ExtraOther = false,
                                    ExtraPrice = 0
                                });

                                //清空 打印任务[Job]表
                                db.ExecuteCommand(@"delete from Job");

                                var printers = new List<LocalPrint>();
                                //移除所有打印作业
                                using (var ps = new LocalPrintServer())
                                {
                                    var queue = ps.GetPrintQueues();
                                    foreach (var pq in queue)
                                    {
                                        printers.Add(new LocalPrint()
                                        {
                                            printName = pq.Name,
                                            printDrive = pq.QueueDriver.Name
                                        });

                                        pq.Refresh();
                                        var jobs = pq.GetPrintJobInfoCollection();
                                        if (pq.NumberOfJobs <= 0) continue;
                                        foreach (var job in jobs)
                                        {
                                            job.Cancel();
                                        }
                                    }
                                }
                                //删除 放行打印机[PassPrinter]表 多余记录
                                db.ExecuteCommand(@"delete from PassPrinter WHERE NeedDel==1");
                                //删除 打印价格表[PrintPrice]表 多余记录
                                db.ExecuteCommand(@"delete from PrintPrice WHERE NeedDel==1");
                                //删除 黑白打印机[BlackPrinter]表 多余记录
                                db.ExecuteCommand(@"delete from BlackPrinter WHERE NeedDel==1");
                                //删除 彩色打印机[ColorPrinter]表 多余记录
                                db.ExecuteCommand(@"delete from ColorPrinter WHERE NeedDel==1");
                                //删除 重复打印机[RepeatPrinter]表 多余记录
                                db.ExecuteCommand(@"delete from RepeatPrinter WHERE NeedDel==1");

                                //更新打印店打印机
                                if (printers.Any())
                                {
                                    var data = new SignData();
                                    data.SetValue("token", json.data.token.ToString());
                                    var sign = SignHelper.GetSign(data, identification);
                                    var requestA = new
                                    {
                                        token = json.data.token.ToString(),
                                        list = printers,
                                        sgin = sign
                                    };
                                    var urlA = Properties.Resources.ServiceUrl + Properties.Resources.ServiceUpdatePrinters;
                                    var httpA = new HttpHelper();
                                    var itemA = new HttpItem()
                                    {
                                        URL = urlA,
                                        Method = "post",
                                        PostEncoding = Encoding.UTF8,
                                        ContentType = "application/json",
                                        Postdata = requestA.ToJson()
                                    };
                                    httpA.GetHtml(itemA);
                                }

                                terminal += "本地打印机：" + printers.ToJson();

                                var launch = new Launch();
                                Hide();
                                launch.Show();

                            }
                        }
                        else
                        {
                            var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                            msg.Show(json.error_msg() ? json.error_msg : "网络连接超时", 400, 200);
                            Button_login.Text = "登 录";
                        }
                    }
                    else
                    {
                        var msg = new Msg { TopLevel = false, Parent = PictureBox_bar };
                        msg.Show("网络连接超时", 400, 200);
                        Button_login.Text = "登 录";
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【手动登录异常】" + exception.ToString());
            }
        }

        #endregion

        #region 开机自启动

        /// <summary>
        /// 快捷方式名称-任意自定义
        /// </summary>
        private const string QuickName = "慧学僧";

        /// <summary>
        /// 自动获取系统自动启动目录
        /// </summary>
        private string systemStartPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Startup); } }

        /// <summary>
        /// 自动获取程序完整路径
        /// </summary>
        private string appAllPath { get { return Process.GetCurrentProcess().MainModule.FileName; } }

        /// <summary>
        /// 自动获取桌面目录
        /// </summary>
        private string desktopPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); } }

        /// <summary>
        /// 设置开机自动启动-只需要调用改方法就可以了参数里面的bool变量是控制开机启动的开关的，默认为开启自启启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        public void SetMeAutoStart(bool onOff = true)
        {
            if (onOff)//开机启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在2个以快捷方式则保留一个快捷方式-避免重复多于
                if (shortcutPaths.Count >= 2)
                {
                    for (int i = 1; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
                else if (shortcutPaths.Count < 1)//不存在则创建快捷方式
                {
                    CreateShortcut(systemStartPath, QuickName, appAllPath, "慧学僧");
                }
            }
            else//开机不启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在快捷方式则遍历全部删除
                if (shortcutPaths.Count > 0)
                {
                    for (int i = 0; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
            }
            //创建桌面快捷方式-如果需要可以取消注释
            //CreateDesktopQuick(desktopPath, QuickName, appAllPath);
        }

        /// <summary>
        ///  向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">目标目录</param>
        /// <param name="shortcutName">快捷方式名字</param>
        /// <param name="targetPath">文件完全路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标地址</param>
        /// <returns>成功或失败</returns>
        private bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            try
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);                         //目录不存在则创建
                //添加引用 Com 中搜索 Windows Script Host Object Model
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));          //合成路径
                WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);    //创建快捷方式对象
                shortcut.TargetPath = targetPath;                                                               //指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);                                  //设置起始位置
                shortcut.WindowStyle = 1;                                                                       //设置运行方式，默认为常规窗口
                shortcut.Description = description;                                                             //设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;    //设置图标路径
                shortcut.Save();                                                                                //保存快捷方式
                return true;
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【开机自启动】" + exception);
            }
            return false;
        }

        /// <summary>
        /// 获取指定文件夹下指定应用程序的快捷方式路径集合
        /// </summary>
        /// <param name="directory">文件夹</param>
        /// <param name="targetPath">目标应用程序路径</param>
        /// <returns>目标应用程序的快捷方式</returns>
        private List<string> GetQuickFromFolder(string directory, string targetPath)
        {
            List<string> tempStrs = new List<string>();
            tempStrs.Clear();
            string tempStr = null;
            string[] files = Directory.GetFiles(directory, "*.lnk");
            if (files == null || files.Length < 1)
            {
                return tempStrs;
            }
            for (int i = 0; i < files.Length; i++)
            {
                //files[i] = string.Format("{0}\\{1}", directory, files[i]);
                tempStr = GetAppPathFromQuick(files[i]);
                if (tempStr == targetPath)
                {
                    tempStrs.Add(files[i]);
                }
            }
            return tempStrs;
        }

        /// <summary>
        /// 获取快捷方式的目标文件路径-用于判断是否已经开启了自动启动
        /// </summary>
        /// <param name="shortcutPath"></param>
        /// <returns></returns>
        private string GetAppPathFromQuick(string shortcutPath)
        {
            //快捷方式文件的路径 = @"d:\Test.lnk";
            if (System.IO.File.Exists(shortcutPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortct = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                //快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;
                //快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                return shortct.TargetPath;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据路径删除文件-用于取消自启时从计算机自启目录删除程序的快捷方式
        /// </summary>
        /// <param name="path">路径</param>
        private void DeleteFile(string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// 在桌面上创建快捷方式-如果需要可以调用
        /// </summary>
        /// <param name="desktopPath">桌面地址</param>
        /// <param name="appPath">应用路径</param>
        public void CreateDesktopQuick(string desktopPath = "", string quickName = "", string appPath = "")
        {
            List<string> shortcutPaths = GetQuickFromFolder(desktopPath, appPath);
            //如果没有则创建
            if (shortcutPaths.Count < 1)
            {
                CreateShortcut(desktopPath, quickName, appPath, "慧学僧自助打印助手");
            }
        }

        #endregion

        #region 设置壁纸

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        #endregion
    }
}
