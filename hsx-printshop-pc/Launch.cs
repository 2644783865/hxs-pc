using CsharpHttpHelper;
using MaSoft.Code;
using MaSoft.UI;
using MaSoft.UI.MaMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using MaSoft.Code.Dao;
using MaSoft.Properties;
using SQLiteSugar;
using Common = MaSoft.Code.Common;
using HttpStatusCode = System.Net.HttpStatusCode;

namespace MaSoft
{
    public partial class Launch : Form
    {
        #region 全局变量

        /// <summary>
        /// 需要显示的打印任务
        /// </summary>
        public List<PrintJobModel> ShowJobs = new List<PrintJobModel>();

        /// <summary>
        /// 订单支付定时器
        /// </summary>
        private readonly System.Timers.Timer _bTimer = new System.Timers.Timer();

        /// <summary>
        /// 用户信息
        /// </summary>
        public User User;

        //是否支付中
        public bool IsPaying = false;

        //正在支付的订单
        public string PayingOrderNo = "";
        //微信支付链接
        public string WxPayUrl = "";
        //支付宝支付链接
        public string AliPayUrl = "";

        //倒计时关闭订单(秒)
        public int ClosePayOrderTime = 300;
        /// <summary>
        /// 订单关闭定时器
        /// </summary>
        private readonly System.Timers.Timer _closeOrderTimer = new System.Timers.Timer();

        /// <summary>
        /// 终端信息
        /// </summary>
        private string terminal = "";

        #endregion

        #region 初始化

        public Launch()
        {
            InitializeComponent();
            using (var db = SugarDao.GetInstance())
            {
                User = db.Queryable<User>().FirstOrDefault();
            }
            //设置当前进程优先级最高
            var process = Process.GetCurrentProcess();
            process.PriorityClass = ProcessPriorityClass.High;
        }

        private void Launch_Load(object sender, EventArgs e)
        {
            terminal = "店铺名称：" + User.PrintShopName + "<br/>" +
                       "登录账户：" + User.UserName + "<br/>" +
                       "终端编号：" + User.Guid + "<br/>" +
                       "终端前缀：" + User.Prefix + "<br/>" +
                       "机器名称：" + Environment.MachineName + "<br/>" +
                       "操作系统：" + Environment.OSVersion + "<br/>" +
                       "分辨率：" + Screen.PrimaryScreen.Bounds.Width + " * " + Screen.PrimaryScreen.Bounds.Height + "<br/>" +
                       "Framework版本：" + Environment.Version + "<br/>";

            var ps = new PrintServer();
            foreach (var pq in ps.GetPrintQueues())
            {
                //放行打印机不监听
                using (var db = SugarDao.GetInstance())
                {
                    if (db.Queryable<PassPrinter>().Where(m => m.PrintName == pq.Name).Count() != 0) continue;
                    var pqm = new PrintQueueMonitor(pq.Name);
                    pqm.OnJobStatusChange += GetPrintJobsBindingListView;
                }
                terminal += "打印机：" + pq.Name + "（" + pq.QueueDriver.Name + "）" + "<br/>";
            }

            Label_pringshopname.Text = User.PrintShopName;
            label_print_h2.Text = "欢迎再次光临 " + User.PrintShopName + "，记得带走您的U盘哦~";
            Label_pringshopname.Location = new Point(992 - Label_pringshopname.Width, 39);

            //初始化倒计时
            _closeOrderTimer.Elapsed += CloseOrder;
            _closeOrderTimer.Interval = 1000;

            _bTimer.Elapsed += OrderQuery;
        }

        private void GetPrintJobsBindingListView(object source, PrintJobChangeEventArgs e)
        {
            var isUpdateDate = false;
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var job = db.Queryable<Job>().Where(m => m.Identifier == e.JobID).FirstOrDefault();
                    if (job == null)
                    {
                        //【新添加任务】 正常任务-添加中-未暂停 直接暂停
                        if (e.JobStatus == JOBSTATUS.JOB_STATUS_SPOOLING && e.JobStatus != JOBSTATUS.JOB_STATUS_PAUSED)
                        {
                            try
                            {
                                var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                                IntPtr phPrinter;
                                if (PrinterApi.OpenPrinter(e.PrintName, out phPrinter, pDefault))
                                {
                                    PrinterApi.SetJob(phPrinter, e.JobID, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_PAUSE);
                                    PrinterApi.ClosePrinter(phPrinter);
                                }
                            }
                            catch (Exception exception)
                            {
                                NetLog.Error(terminal, "【打印任务暂停错误】" + exception);
                            }
                        }
                        //数据库处理 新增加任务 删除中/已删除/打印中/已打印 不进行处理
                        if (e.JobStatus == JOBSTATUS.JOB_STATUS_PAUSED &&
                            e.JobStatus != JOBSTATUS.JOB_STATUS_SPOOLING &&
                            e.JobStatus != JOBSTATUS.JOB_STATUS_DELETED &&
                            e.JobStatus != JOBSTATUS.JOB_STATUS_DELETING &&
                            e.JobStatus != JOBSTATUS.JOB_STATUS_PRINTING &&
                            e.JobStatus != JOBSTATUS.JOB_STATUS_PRINTED)
                        {
                            if (e.JobSize > 1)
                            {
                                var jobInfo = new PrinterApi.JOB();
                                PrinterApi.GetJobInfo(ref jobInfo, e.PrintName, e.JobID);

                                var fileName = GetJobFileName(e.JobName);
                                db.Insert(new Job()
                                {
                                    PrinterName = e.PrintName,
                                    OrderNo = "",
                                    Identifier = e.JobID,
                                    FileName = fileName,
                                    UploadName = GetJobUploadFileName(fileName),
                                    MediaTypeNo = jobInfo.MediaType,
                                    DuplexNo = jobInfo.Duplex,
                                    PageSizeNo = jobInfo.PageSize,
                                    ColorNo = jobInfo.Color,
                                    Pages = e.JobInfo.NumberOfPages,
                                    Copys = jobInfo.Copyes,
                                    OrientationNo = jobInfo.Orientation,
                                    Price = 0,
                                    Money = 0,
                                    IsPay = false,
                                    IsShow = true,
                                });
                                isUpdateDate = true;
                            }
                        }
                    }
                    else
                    {
                        //【未支付启动的任务】 继续-重启-删除  任务直接删除
                        if (!job.IsPay)
                        {
                            if (e.JobStatus != JOBSTATUS.JOB_STATUS_PAUSED)
                            {
                                var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                                IntPtr phPrinter;
                                if (PrinterApi.OpenPrinter(e.PrintName, out phPrinter, pDefault))
                                {
                                    PrinterApi.SetJob(phPrinter, e.JobID, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_CANCEL);
                                    PrinterApi.ClosePrinter(phPrinter);
                                }

                                db.Delete<Job>(m => m.Id == job.Id);
                                isUpdateDate = true;
                            }

                            if (e.JobStatus == JOBSTATUS.JOB_STATUS_DELETING)
                            {
                                db.Delete<Job>(m => m.Id == job.Id);
                                isUpdateDate = true;
                            }
                        }
                    }
                    Invoke(new Action(() =>
                    {
                        if (!isUpdateDate) return;
                        UpdateDataBaseJobs();

                        UpdateUiJob();
                        ShowUi();

                        //弹出主窗体并获取焦点
                        WindowState = FormWindowState.Normal;
                        Show();
                        Activate();
                    }));
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【监听打印任务】【打印机：" + e.PrintName + "】" + exception);
            }
        }

        #endregion

        #region 更新数据库打印任务信息

        public void UpdateDataBaseJobs()
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var waitPrintJobs = db.Queryable<Job>().Where(m => string.IsNullOrEmpty(m.OrderNo) && !m.IsPay && m.IsShow).ToList();
                    if (!waitPrintJobs.Any()) return;
                    //价格查询
                    var termPrices = GetTermPrices(waitPrintJobs);
                    foreach (var item in waitPrintJobs)
                    {
                        var priceModel = termPrices.FirstOrDefault(m => m.MediaTypeNo == item.MediaTypeNo &&
                                                                        m.DuplexNo == item.DuplexNo &&
                                                                        m.PageSizeNo == item.PageSizeNo &&
                                                                        m.ColorNo == item.ColorNo);
                        //分转元
                        double price = 0.00;
                        if (priceModel != null)
                        {
                            price = priceModel.Price;
                        }
                        if (price > 0)
                        {
                            price = double.Parse((price / 100).ToString("#0.00"));
                        }
                        if (item.Price == price) continue;

                        var pages = item.Pages;
                        if (item.DuplexNo == 2002)
                        {
                            pages = pages % 2 > 0 ? (pages - 1) / 2 + 1 : pages / 2;
                        }
                        db.Update<Job>(new
                        {
                            Price = price,
                            Money = double.Parse((price * pages * item.Copys).ToString("#0.00"))

                        }, it => it.Id == item.Id);
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【更新数据库打印任务信息】" + exception);
            }
        }

        #endregion

        #region 更新前台打印任务

        public void UpdateUiJob()
        {
            using (var db = SugarDao.GetInstance())
            {
                var list = db.Queryable<Job>().Where(m => m.IsShow).OrderBy(m => m.Identifier).ToList();
                if (list.Any())
                {
                    ShowJobs.Clear();
                    var i = 1;
                    foreach (var m in list)
                    {
                        ShowJobs.Add(new PrintJobModel()
                        {
                            No = i,
                            Id = m.Identifier,
                            FileName = m.FileName,
                            PageType = PrinterApi.GetMediaTypeExplain(m.MediaTypeNo),
                            PrintType = PrinterApi.GetDuplexExplain(m.DuplexNo),
                            PageSize = PrinterApi.GetPageSizeExplain(m.PageSizeNo),
                            PageColor = PrinterApi.GetColorExplain(m.ColorNo),
                            Pages = DuplexPage(m.DuplexNo, m.Pages),
                            Copys = m.Copys,
                            Price = m.Price,
                            Money = m.Money,
                            Action = null
                        });
                        i++;
                    }
                    UpdateMoney();
                }
                else
                {
                    ShowJobs.Clear();
                }
                Invoke(new Action(() =>
                {
                    try
                    {
                        DataGridView_job.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
                        DataGridView_job.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        DataGridView_job.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        DataGridView_job.Columns[2].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        DataGridView_job.Columns[9].DefaultCellStyle.Format = "0.00";
                        DataGridView_job.Columns[10].DefaultCellStyle.Format = "0.00";
                        DataGridView_job.DataSource = new BindingList<PrintJobModel>(ShowJobs);
                        DataGridView_job.ClearSelection();
                    }
                    catch (Exception exception)
                    {
                        NetLog.Error(terminal, "【打印列表刷新错误】" + exception);
                    }
                }));
            }
        }

        public int DuplexPage(int duplex, int page)
        {
            if (duplex == 2002)
            {
                return page % 2 > 0 ? (page + 1) / 2 : page / 2;
            }
            else
            {
                return page;
            }
        }

        #endregion

        #region 更新支付价格

        /// <summary>
        /// 更新支付价格
        /// 新打印队列获取成功之后使用
        /// </summary>
        public void UpdateMoney()
        {
            using (var db = SugarDao.GetInstance())
            {
                var money = ShowJobs.Sum(m => m.Money);
                var model = db.Queryable<JobExtra>().FirstOrDefault();
                if (model != null && model.ExtraPrice > 0)
                {
                    money += model.ExtraPrice;
                }
                Invoke(new Action(() =>
                {
                    Label_files.Text = ShowJobs.Count.ToString();
                    Label_price.Text = money + "元";
                }));
            }
        }

        /// <summary>
        /// 获取订单支付价格
        /// </summary>
        public int GetOrderMoney_Int(List<Job> jobs)
        {
            using (var db = SugarDao.GetInstance())
            {
                var money = jobs.Sum(m => m.Money);
                var model = db.Queryable<JobExtra>().FirstOrDefault();
                if (model != null && model.ExtraPrice > 0)
                {
                    money += model.ExtraPrice;
                }
                return (int)(money * 100);
            }
        }

        /// <summary>
        /// 获取订单支付价格
        /// </summary>
        public double GetOrderMoney_Double(List<Job> jobs)
        {
            using (var db = SugarDao.GetInstance())
            {
                var money = jobs.Sum(m => m.Money);
                var model = db.Queryable<JobExtra>().FirstOrDefault();
                if (model != null && model.ExtraPrice > 0)
                {
                    money += model.ExtraPrice;
                }
                return money;
            }
        }

        #endregion

        #region 集成方法

        /// <summary>
        /// 获取待印任务个条件对应价格
        /// </summary>
        /// <param name="waitJobs">待印任务</param>
        /// <returns></returns>
        public List<TermPrice> GetTermPrices(List<Job> waitJobs)
        {
            if (waitJobs.Any())
            {
                using (var db = SugarDao.GetInstance())
                {
                    var terms = new List<TermPrice>();
                    var oneTerm = waitJobs.DistinctBy(m => new { m.MediaTypeNo, m.DuplexNo, m.PageSizeNo, m.ColorNo });
                    foreach (var item in oneTerm)
                    {
                        //相同条件打印任务
                        var sameJobs = waitJobs.Where(m => m.MediaTypeNo == item.MediaTypeNo &&
                                                           m.DuplexNo == item.DuplexNo &&
                                                           m.PageSizeNo == item.PageSizeNo &&
                                                           m.ColorNo == item.ColorNo).ToList();
                        //当前条件打印价格
                        var model = db.Queryable<PrintPrice>().Where(m => m.MediaType == item.MediaTypeNo &&
                                                                          m.Duplex == item.DuplexNo &&
                                                                          m.PageSize == item.PageSizeNo &&
                                                                          m.Color == item.ColorNo).FirstOrDefault();
                        //总页数
                        var sumPage = sameJobs.Sum(m => (m.Pages * m.Copys));

                        if (model != null)
                        {
                            //获取区间价格
                            var newPrice = model.Money;
                            if (sumPage >= model.SalePage1 && model.SalePrice1 > 0) { newPrice = model.SalePrice1; }
                            if (sumPage >= model.SalePage2 && model.SalePrice2 > 0) { newPrice = model.SalePrice2; }
                            if (sumPage >= model.SalePage3 && model.SalePrice3 > 0) { newPrice = model.SalePrice3; }

                            terms.Add(new TermPrice()
                            {
                                MediaTypeNo = item.MediaTypeNo,
                                DuplexNo = item.DuplexNo,
                                PageSizeNo = item.PageSizeNo,
                                ColorNo = item.ColorNo,
                                Pages = sumPage,
                                Price = newPrice
                            });
                        }
                        else
                        {
                            terms.Add(new TermPrice()
                            {
                                MediaTypeNo = item.MediaTypeNo,
                                DuplexNo = item.DuplexNo,
                                PageSizeNo = item.PageSizeNo,
                                ColorNo = item.ColorNo,
                                Pages = sumPage,
                                Price = 0
                            });
                        }
                    }
                    return terms;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 打印任务文件名格式化
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public string GetJobFileName(string name)
        {
            var n = name.Split('\\');
            var tempName = name;
            if (n.Length > 0)
            {
                tempName = n[n.Length - 1];
            }
            return tempName;
        }

        /// <summary>
        /// 上传文件名去JSON二义性
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        public string GetJobUploadFileName(string name)
        {
            return name.Replace('{', '_').Replace('}', '_').Replace('[', '_').Replace(']', '_').Replace('"', '_').Replace(':', '_').Replace(',', '_').Replace('=', '_');
        }

        /// <summary>
        /// 删除打印机任务
        /// </summary>
        /// <param name="jobList"></param>
        public void DelPrintJob(List<Job> jobList)
        {
            foreach (var job in jobList)
            {
                var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                IntPtr phPrinter;
                if (PrinterApi.OpenPrinter(job.PrinterName, out phPrinter, pDefault))
                {
                    PrinterApi.SetJob(phPrinter, job.Identifier, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_CANCEL);
                    PrinterApi.ClosePrinter(phPrinter);
                }
            }
        }

        /// <summary>
        /// 启动打印任务
        /// </summary>
        /// <param name="jobList"></param>
        public void StartPrintJob(List<Job> jobList)
        {
            foreach (var job in jobList)
            {
                var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                IntPtr phPrinter;
                if (PrinterApi.OpenPrinter(job.PrinterName, out phPrinter, pDefault))
                {
                    PrinterApi.SetJob(phPrinter, job.Identifier, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_RESUME);
                    PrinterApi.ClosePrinter(phPrinter);
                }
            }
        }

        /// <summary>
        /// 获取打印机名称集合
        /// </summary>
        /// <param name="jobs"></param>
        /// <returns></returns>
        public string GetPrintNames(List<Job> jobs)
        {
            if (jobs.Any())
            {
                var ps = jobs.DistinctBy(m => m.PrinterName);
                var sb = new StringBuilder();
                foreach (var job in ps)
                {
                    sb.Append(job.PrinterName + "、");
                }
                return sb.ToString().TrimEnd('、');
            }
            else
            {
                return "指定";
            }
        }

        #endregion

        #region 界面显示

        public void ShowUi()
        {
            if (IsPaying) return;
            using (var db = SugarDao.GetInstance())
            {
                if (db.Queryable<Job>().Where(m => m.IsShow).Count() > 0)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            PanelA.Visible = false;
                            PanelB.Visible = true;
                            panelC.Visible = false;
                            panelD.Visible = false;
                            panelE.Visible = false;
                        }));
                    }
                    else
                    {
                        PanelA.Visible = false;
                        PanelB.Visible = true;
                        panelC.Visible = false;
                        panelD.Visible = false;
                        panelE.Visible = false;
                    }
                }
                else
                {
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            PanelA.Visible = true;
                            PanelB.Visible = false;
                            panelC.Visible = false;
                            panelD.Visible = false;
                            panelE.Visible = false;
                            Label_files.Text = "0";
                            Label_price.Text = "0元";
                            ExtraSetLoad();
                        }));
                    }
                    else
                    {
                        PanelA.Visible = true;
                        PanelB.Visible = false;
                        panelC.Visible = false;
                        panelD.Visible = false;
                        panelE.Visible = false;
                        Label_files.Text = "0";
                        Label_price.Text = "0元";
                        ExtraSetLoad();
                    }
                }
            }
        }

        #endregion

        #region 打印额外费用初始化

        public void ExtraSetLoad()
        {
            using (var db = SugarDao.GetInstance())
            {
                //打印订单额外费用 初始化
                var modelExtra = db.Queryable<JobExtra>().FirstOrDefault();
                if (modelExtra != null)
                {
                    db.Update<JobExtra>(new
                    {
                        ExtraGluing = false,
                        ExtraTypesetting = false,
                        ExtraCopy = false,
                        ExtraScan = false,
                        ExtraOther = false,
                        ExtraPrice = 0
                    }, it => it.Id == modelExtra.Id);
                }
                else
                {
                    db.Insert(new JobExtra()
                    {
                        ExtraGluing = false,
                        ExtraTypesetting = false,
                        ExtraCopy = false,
                        ExtraScan = false,
                        ExtraOther = false,
                        ExtraPrice = 0
                    });
                }
            }
        }

        #endregion

        #region 激活窗体

        //调用API
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow(); //获得本窗体的句柄
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

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
            //托盘图标
            Hide();
            ShowInTaskbar = false;
            notifyIcon_hxs.ShowBalloonTip(3000, "慧学僧", "打印助手后台运行中，直接打开文档文件进行打印即可。", ToolTipIcon.Info);
        }

        private void PictureBox_min_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region 列表删除
        private void DataGridView_job_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex == 11)
                    {
                        var mask = new DelPrintJob();
                        if (mask.ShowDialog() == DialogResult.OK)
                        {
                            var jobId = int.Parse(DataGridView_job.Rows[e.RowIndex].Cells[0].Value.ToString());
                            var model = db.Queryable<Job>().Where(m => m.Identifier == jobId).FirstOrDefault();
                            if (model != null)
                            {
                                var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                                IntPtr phPrinter;
                                if (PrinterApi.OpenPrinter(model.PrinterName, out phPrinter, pDefault))
                                {
                                    PrinterApi.SetJob(phPrinter, jobId, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_CANCEL);
                                    PrinterApi.ClosePrinter(phPrinter);
                                    db.Delete<Job>(it => it.Id == model.Id);
                                }
                            }
                        }
                    }
                    UpdateDataBaseJobs();
                    UpdateMoney();
                    UpdateUiJob();
                    ShowUi();
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【删除打印任务】" + exception);
            }
        }

        #endregion

        #region 继续添加

        private void button2_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region 附加费用
        private void button1_Click(object sender, EventArgs e)
        {
            var mask = new SetExtraMoney();
            if (mask.ShowDialog() == DialogResult.OK)
            {
                UpdateMoney();
            }
        }

        #endregion

        #region 付款

        private void Button_pay_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var jobs = db.Queryable<Job>().Where(m => m.IsShow).ToList();
                    if (jobs.Count > 0)
                    {
                        if (jobs.Count(m => m.Price == 0 && m.Money == 0) == 0)
                        {
                            if (!string.IsNullOrEmpty(User.Prefix))
                            {
                                //生成订单号
                                var orderNo = "H" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + User.Prefix;
                                PayingOrderNo = orderNo;
                                //提交订单
                                var orderDetails = jobs.Select(m => new
                                {
                                    id = m.Id,
                                    fileName = m.UploadName,
                                    mediatype = m.MediaTypeNo,
                                    duplex = m.DuplexNo,
                                    pagesize = m.PageSizeNo,
                                    colour = m.ColorNo,
                                    pageNumber = m.Pages,
                                    shareNumber = m.Copys,
                                    money = (int)(m.Money * 100)
                                }).ToList();
                                var modelExtra = db.Queryable<JobExtra>().FirstOrDefault();
                                var extraDetails = new
                                {
                                    gluing = modelExtra.ExtraGluing ? 1 : 0,
                                    typesetting = modelExtra.ExtraTypesetting ? 1 : 0,
                                    copy = modelExtra.ExtraCopy ? 1 : 0,
                                    scan = modelExtra.ExtraScan ? 1 : 0,
                                    other = modelExtra.ExtraOther ? 1 : 0
                                };
                                //sign
                                var extraMoney = (int)(modelExtra.ExtraPrice * 100);
                                var money = GetOrderMoney_Int(jobs);

                                var data = new SignData();
                                data.SetValue("token", User.Token);
                                data.SetValue("orderno", orderNo);
                                data.SetValue("filecount", jobs.Count);
                                data.SetValue("extramoney", extraMoney);
                                data.SetValue("money", money);
                                var sign = SignHelper.GetSign(data, User.Guid);

                                var request = new
                                {
                                    token = User.Token,
                                    orderno = orderNo,
                                    filecount = jobs.Count,
                                    orderdetails = orderDetails,
                                    extradetails = extraDetails,
                                    extramoney = extraMoney,
                                    money = money,
                                    sgin = sign
                                };
                                var url = Resources.ServiceUrl + Resources.ServiceReferOrder;
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

                                NetLog.Info(terminal, "【订单提交请求】" + request.ToJson());

                                if (result.StatusCode == HttpStatusCode.OK)
                                {
                                    var json = DynamicJson.Parse(result.Html);

                                    NetLog.Info(terminal, "【订单提交响应】" + json.ToString());

                                    if (json.error_code() && json.error_code == 0)
                                    {
                                        label_pay_price.Text = GetOrderMoney_Double(jobs) + "元";
                                        label_pay_no.Text = orderNo;

                                        //WxPayUrl = 微信支付
                                        //WxPayUrl = json.wxpay.ToString();
                                        WxPayUrl = Resources.ServiceUrl + Resources.ScanPayUrl + orderNo;
                                        //AliPayUrl = 支付宝支付
                                        //AliPayUrl = json.alipay.ToString();

                                        //启动定时查询订单支付接口
                                        _bTimer.Interval = 2000;
                                        _bTimer.Enabled = true;

                                        //设置订单编号
                                        foreach (var job in jobs)
                                        {
                                            db.Update<Job>(new { OrderNo = orderNo }, it => it.Id == job.Id);
                                        }

                                        //显示扫码界面
                                        IsPaying = true;
                                        //生成支付二维码
                                        //微信
                                        pictureBox_payqcode.Image = Common.ToQrCode(WxPayUrl);

                                        PanelA.Visible = false;
                                        PanelB.Visible = false;
                                        panelC.Visible = true;
                                        panelD.Visible = false;
                                        panelE.Visible = false;

                                        //扫码倒计时
                                        ClosePayOrderTime = 300;
                                        label_closepay_time.Text = StringHelper.SecondToTimer(ClosePayOrderTime);
                                        _closeOrderTimer.Enabled = true;
                                    }
                                    else
                                    {
                                        var msg = new Msg { TopLevel = false, Parent = DataGridView_job };
                                        msg.Show("订单提交失败，请联系维护人员！", 485, 250);
                                    }
                                }
                                else
                                {
                                    var msg = new Msg { TopLevel = false, Parent = DataGridView_job };
                                    msg.Show("订单提交失败！", 485, 250);
                                }
                            }
                            else
                            {
                                var msg = new Msg { TopLevel = false, Parent = DataGridView_job };
                                msg.Show("系统配置文件错误，请重新登录！", 485, 250);
                            }
                        }
                        else
                        {
                            var msg = new Msg { TopLevel = false, Parent = DataGridView_job };
                            msg.Show("请删除价格为0的打印任务", 485, 250);
                        }
                    }
                    else
                    {
                        var msg = new Msg { TopLevel = false, Parent = DataGridView_job };
                        msg.Show("请先添加打印任务！", 485, 250);
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【付款】" + exception);
            }
        }

        /// <summary>
        /// 计时器Invoke 订单查询
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OrderQuery(object source, ElapsedEventArgs e)
        {
            EndInvoke(BeginInvoke(new Action(OrderQuery)));
        }

        /// <summary>
        /// 订单查询
        /// </summary>
        private void OrderQuery()
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    var data = new SignData();
                    data.SetValue("token", User.Token);
                    data.SetValue("orderno", PayingOrderNo);
                    var sign = SignHelper.GetSign(data, User.Guid);
                    var url = Resources.ServiceUrl + Resources.ServiceOrderQuery;
                    var request = new
                    {
                        token = User.Token,
                        orderno = PayingOrderNo,
                        sgin = sign
                    };
                    var http = new HttpHelper();
                    var item = new HttpItem()
                    {
                        URL = url,
                        Method = "post",
                        PostEncoding = Encoding.UTF8,
                        ContentType = "application/json",
                        Postdata = request.ToJson(),
                        Timeout = 2000
                    };
                    NetLog.Info(terminal, "【订单查询提交参数】" + request);
                    var result = http.GetHtml(item);
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        var json = DynamicJson.Parse(result.Html);
                        if (json.error_code() && json.error_code == 0)
                        {
                            string resultOrderNo = json.orderno;
                            var resultPayState = (int)json.paystate;
                            if (resultPayState == 1 && resultOrderNo == PayingOrderNo)
                            {
                                //设置订单为已支付
                                var jobs = db.Queryable<Job>().Where(m => m.OrderNo == resultOrderNo).ToList();
                                foreach (var job in jobs)
                                {
                                    db.Update<Job>(new
                                    {
                                        IsPay = true,
                                        IsShow = false
                                    }, it => it.Id == job.Id);
                                }
                                //打印订单额外费用 初始化
                                var modelExtra = db.Queryable<JobExtra>().FirstOrDefault();
                                if (modelExtra != null)
                                {
                                    db.Update<JobExtra>(new
                                    {
                                        ExtraGluing = false,
                                        ExtraTypesetting = false,
                                        ExtraCopy = false,
                                        ExtraScan = false,
                                        ExtraOther = false,
                                        ExtraPrice = 0
                                    }, it => it.Id == modelExtra.Id);

                                }
                                else
                                {
                                    db.Insert(new JobExtra()
                                    {
                                        ExtraGluing = false,
                                        ExtraTypesetting = false,
                                        ExtraCopy = false,
                                        ExtraScan = false,
                                        ExtraOther = false,
                                        ExtraPrice = 0
                                    });
                                }
                                PayingOrderNo = "";
                                //停止定时器
                                _bTimer.Enabled = false;
                                _bTimer.Close();
                                //支付空值
                                WxPayUrl = "";
                                AliPayUrl = "";
                                //执行打印
                                StartPrintJob(jobs);
                                //显示打印界面
                                PanelA.Visible = false;
                                PanelB.Visible = false;
                                panelC.Visible = false;
                                panelE.Visible = false;
                                textBox_print_msg.Text = "文件打印中，请到 " + GetPrintNames(jobs) + " 打印机 领取文件！";
                                panelD.Visible = true;

                                if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                                {
                                    UpdateDataBaseJobs();
                                    UpdateUiJob();
                                }

                                // 5秒后设置  IsPaying=false
                                timer_toPanelA.Interval = 10000;
                                timer_toPanelA.Start();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【订单查询】" + exception);
            }
        }

        /// <summary>
        /// 计时器Invoke 订单取消
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void CloseOrder(object source, ElapsedEventArgs e)
        {
            EndInvoke(BeginInvoke(new Action(ScanOrderDo)));
        }

        /// <summary>
        /// 扫码支付倒计时及到期关闭订单
        /// </summary>
        private void ScanOrderDo()
        {
            if (ClosePayOrderTime == 0)
            {
                _closeOrderTimer.Enabled = false;
                _closeOrderTimer.Close();
                //取消订单
                using (var db = SugarDao.GetInstance())
                {
                    //Jobs表设置为不显示
                    var jobs = db.Queryable<Job>().Where(m => m.OrderNo == PayingOrderNo).ToList();
                    if (jobs.Any())
                    {
                        foreach (var job in jobs)
                        {
                            db.Update<Job>(new { IsShow = false }, it => it.Id == job.Id);
                        }
                        //取消打印任务
                        DelPrintJob(jobs);
                    }
                    //订单号设置为空
                    PayingOrderNo = "";
                    //显示取消成功界面
                    PanelA.Visible = false;
                    PanelB.Visible = false;
                    panelC.Visible = false;
                    panelD.Visible = false;
                    panelE.Visible = true;
                    //关闭订单查询接口
                    _bTimer.Enabled = false;
                    _bTimer.Close();
                    if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                    {
                        UpdateDataBaseJobs();
                        UpdateUiJob();
                    }
                    // 3秒后设置  IsPaying=false
                    timer_toPanelA.Interval = 3000;
                    timer_toPanelA.Start();
                }
            }
            else
            {
                ClosePayOrderTime--;
                label_closepay_time.Text = StringHelper.SecondToTimer(ClosePayOrderTime);
            }
        }

        #endregion

        #region 付款上一步

        private void button_pay_last_Click(object sender, EventArgs e)
        {
            using (var db = SugarDao.GetInstance())
            {
                var mask = new DoLast();
                if (mask.ShowDialog() == DialogResult.OK)
                {
                    //取消Jobs表中的订单号
                    var jobs = db.Queryable<Job>().Where(m => m.OrderNo == PayingOrderNo).ToList();
                    if (jobs.Any())
                    {
                        foreach (var job in jobs)
                        {
                            db.Update<Job>(new { OrderNo = "" }, it => it.Id == job.Id);
                        }
                    }
                    //刷新显示界面
                    if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                    {
                        UpdateDataBaseJobs();
                        UpdateUiJob();
                    }

                    //显示上一步界面
                    IsPaying = false;
                    //关闭订单查询接口
                    _bTimer.Enabled = false;
                    _bTimer.Close();
                    //关闭倒计时
                    _closeOrderTimer.Enabled = false;
                    _closeOrderTimer.Close();
                    //订单号设置为空
                    PayingOrderNo = "";
                    PanelA.Visible = false;
                    PanelB.Visible = true;
                    panelC.Visible = false;
                    panelD.Visible = false;
                    panelE.Visible = false;
                }
            }
        }

        #endregion

        #region 取消支付

        private void button_pay_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                var mask = new CancelOrder();
                if (mask.ShowDialog() == DialogResult.OK)
                {
                    using (var db = SugarDao.GetInstance())
                    {
                        //Jobs表设置为不显示
                        var jobs = db.Queryable<Job>().Where(m => m.OrderNo == PayingOrderNo).ToList();
                        if (jobs.Any())
                        {
                            foreach (var job in jobs)
                            {
                                db.Delete<Job>(it => it.Id == job.Id);
                            }

                            //取消打印任务
                            DelPrintJob(jobs);
                        }
                        if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                        {
                            UpdateDataBaseJobs();
                            UpdateUiJob();
                        }
                        //关闭倒计时
                        _closeOrderTimer.Enabled = false;
                        _closeOrderTimer.Close();

                        //关闭订单查询接口
                        _bTimer.Enabled = false;
                        _bTimer.Stop();

                        // 3秒后设置  IsPaying=false
                        timer_toPanelA.Interval = 3000;
                        timer_toPanelA.Start();

                        //订单号设置为空
                        PayingOrderNo = "";
                        //显示取消成功界面
                        PanelA.Visible = false;
                        PanelB.Visible = false;
                        panelC.Visible = false;
                        panelD.Visible = false;
                        panelE.Visible = true;
                    }
                }
            }
            catch (Exception exception)
            {

                NetLog.Error(terminal, "【取消支付】" + exception);

                //关闭倒计时
                _closeOrderTimer.Enabled = false;
                _closeOrderTimer.Close();

                //关闭订单查询接口
                _bTimer.Enabled = false;
                _bTimer.Stop();

                using (var db = SugarDao.GetInstance())
                {
                    if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                    {
                        UpdateDataBaseJobs();
                        UpdateUiJob();
                    }
                }

                // 3秒后设置  IsPaying=false
                timer_toPanelA.Interval = 3000;
                timer_toPanelA.Start();

                //订单号设置为空
                PayingOrderNo = "";
                //显示取消成功界面
                PanelA.Visible = false;
                PanelB.Visible = false;
                panelC.Visible = false;
                panelD.Visible = false;
                panelE.Visible = true;

            }
        }

        private void timer_toPanelA_Tick(object sender, EventArgs e)
        {
            timer_toPanelA.Stop();
            IsPaying = false;
            ShowUi();
        }

        #endregion

        #region 线下支付

        private void button_pay_other_Click(object sender, EventArgs e)
        {
            try
            {
                //暂停支付倒计时
                _closeOrderTimer.Enabled = false;

                //隐藏二维码
                //微信
                pictureBox_payqcode.Image = Resources.pay_noqcode;

                using (var db = SugarDao.GetInstance())
                {
                    var mask = new SelectPayWay();
                    if (mask.ShowDialog() == DialogResult.OK)
                    {
                        var payType = (int)mask.PayType;

                        var data = new SignData();
                        data.SetValue("token", User.Token);
                        data.SetValue("orderno", PayingOrderNo);
                        data.SetValue("paytype", payType);
                        var sign = SignHelper.GetSign(data, User.Guid);
                        var url = Resources.ServiceUrl + Resources.ServiceReferPayType;
                        var request = new
                        {
                            token = User.Token,
                            orderno = PayingOrderNo,
                            paytype = payType,
                            sgin = sign
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
                                //设置打印订单为支付成功
                                var jobs = db.Queryable<Job>().Where(m => m.OrderNo == PayingOrderNo).ToList();
                                foreach (var job in jobs)
                                {
                                    db.Update<Job>(new
                                    {
                                        IsPay = true,
                                        IsShow = false
                                    }, it => it.Id == job.Id);
                                }
                                //打印订单额外费用 初始化
                                var modelExtra = db.Queryable<JobExtra>().FirstOrDefault();
                                if (modelExtra != null)
                                {
                                    db.Update<JobExtra>(new
                                    {
                                        ExtraGluing = false,
                                        ExtraTypesetting = false,
                                        ExtraCopy = false,
                                        ExtraScan = false,
                                        ExtraOther = false,
                                        ExtraPrice = 0
                                    }, it => it.Id == modelExtra.Id);

                                }
                                else
                                {
                                    db.Insert(new JobExtra()
                                    {
                                        ExtraGluing = false,
                                        ExtraTypesetting = false,
                                        ExtraCopy = false,
                                        ExtraScan = false,
                                        ExtraOther = false,
                                        ExtraPrice = 0
                                    });
                                }
                                PayingOrderNo = "";
                                //关闭订单查询接口
                                _bTimer.Enabled = false;
                                _bTimer.Stop();
                                //支付空值
                                WxPayUrl = "";
                                AliPayUrl = "";
                                //执行打印
                                StartPrintJob(jobs);
                                //显示打印界面
                                PanelA.Visible = false;
                                PanelB.Visible = false;
                                panelC.Visible = false;
                                panelE.Visible = false;
                                textBox_print_msg.Text = "文件打印中，请到 " + GetPrintNames(jobs) + " 打印机 领取文件！";
                                panelD.Visible = true;

                                if (db.Queryable<Job>().Where(m => m.IsShow && m.IsPay == false).Count() > 0)
                                {
                                    UpdateDataBaseJobs();
                                    UpdateUiJob();
                                }

                                // 5秒后设置  IsPaying=false
                                timer_toPanelA.Interval = 10000;
                                timer_toPanelA.Start();
                            }
                            else
                            {
                                var msg = new Msg { TopLevel = false, Parent = panelC };
                                msg.Show("接口服务500错误", 498, 300);
                            }
                        }
                        else
                        {
                            var msg = new Msg { TopLevel = false, Parent = panelC };
                            msg.Show("网络连接超时", 498, 300);
                        }
                    }
                    else
                    {
                        //继续支付倒计时
                        _closeOrderTimer.Enabled = true;

                        //显示二维码
                        //微信
                        pictureBox_payqcode.Image = Common.ToQrCode(WxPayUrl);
                    }
                }
            }
            catch (Exception exception)
            {
                var msg = new Msg { TopLevel = false, Parent = panelC };
                msg.Show("程序错误，请重试！", 498, 300);

                NetLog.Error(terminal, "【线下支付订单提交错误】" + exception);
            }
        }

        #endregion

        #region 托盘图标双击

        private void notifyIcon_hxs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Show();
            Activate();
            SetForegroundWindow(Handle);
        }

        #endregion

        #region 退出系统

        private void tool_logout_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Activate();
            ShowInTaskbar = true;
            var mask = new UserLogout();
            if (mask.ShowDialog() == DialogResult.OK)
            {
                notifyIcon_hxs.Dispose();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void Launch_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Activate();
            ShowInTaskbar = true;
            var mask = new UserLogout();
            if (mask.ShowDialog() == DialogResult.OK)
            {
                notifyIcon_hxs.Dispose();
                Environment.Exit(Environment.ExitCode);
                //Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }


        #endregion

        #region 同步数据

        private void tool_dataSync_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = SugarDao.GetInstance())
                {
                    //【第一步】程序自检之后更新本地用户信息。
                    var model = db.Queryable<User>().FirstOrDefault();
                    if (model != null)
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
                            Postdata = request.ToJson()
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
                                var printers = new List<LocalPrint>();
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
                                    }
                                }
                                if (printers.Any())
                                {
                                    var data = new SignData();
                                    data.SetValue("token", json.data.token.ToString());
                                    //data.SetValue("list", printers.ToJson());
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
                                        Postdata = requestA.ToJson()
                                    };
                                    httpA.FastRequest(itemA);
                                }

                                //更新内存本地账户
                                User = model;

                                //更新列表
                                UpdateDataBaseJobs();
                                UpdateMoney();

                                //通知数据更新成功
                                notifyIcon_hxs.ShowBalloonTip(5000, "慧学僧", "数据同步成功！", ToolTipIcon.Info);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                NetLog.Error(terminal, "【同步数据错误】" + exception);
            }
        }

        #endregion

        #region 禁止任务管理器

        private void timer_killProcess_Tick(object sender, EventArgs e)
        {
            KillProcess();
        }

        public void KillProcess()
        {
            var pro = Process.GetProcessesByName("taskmgr");
            foreach (var item in pro)
            {
                try { item.Kill(); }
                catch
                {
                    // ignored
                }
            }
        }

        #endregion

        #region 打开

        private void tool_open_Click(object sender, EventArgs e)
        {
            ShowInTaskbar = true;
            WindowState = FormWindowState.Normal;
            Show();
            Activate();
            SetForegroundWindow(Handle);
        }

        #endregion

        #region 设置

        private void tool_setting_Click(object sender, EventArgs e)
        {
            var mask = new UserLogout { StartPosition = FormStartPosition.CenterScreen };
            if (mask.ShowDialog() == DialogResult.OK)
            {
                var maskA = new Setting { StartPosition = FormStartPosition.CenterScreen };
                maskA.Show();
            }
        }

        #endregion

        #region 关于我们

        private void tool_about_Click(object sender, EventArgs e)
        {
            var mask = new AboutUs { StartPosition = FormStartPosition.CenterScreen };
            mask.Show();
        }

        #endregion

    }
}
