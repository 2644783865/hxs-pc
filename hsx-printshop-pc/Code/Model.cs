using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MaSoft.Code
{
    // 下面为枚举 -------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 支付方式
    /// </summary>
    public enum PayType
    {

        /// <summary>
        /// 线上支付
        /// </summary>
        OnlinePay = 0,

        /// <summary>
        /// 微信扫一扫
        /// </summary>
        WxPay = 5001,

        /// <summary>
        /// 支付宝扫一扫
        /// </summary>
        AliPay = 5002,

        /// <summary>
        /// 现金
        /// </summary>
        Money = 5003,

        /// <summary>
        /// POS刷卡
        /// </summary>
        Pos = 5004,

        /// <summary>
        /// 会员卡
        /// </summary>
        Card = 5005,

        /// <summary>
        /// 记账
        /// </summary>
        Tally = 5006,

        /// <summary>
        /// 其他
        /// </summary>
        Other = 5007
    }


    // 下面为常用对象 -------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 展示列表对象
    /// </summary>
    public class PrintJobModel
    {
        /// <summary>
        /// 打印任务标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 纸质
        /// </summary>
        public string PageType { get; set; }

        /// <summary>
        /// 打印类型（单面，双面）
        /// </summary>
        public string PrintType { get; set; }

        /// <summary>
        /// 纸张大小
        /// </summary>
        public string PageSize { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string PageColor { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 份数
        /// </summary>
        public int Copys { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }

        /// <summary>
        /// 删除图片
        /// </summary>
        public Image Action { get; set; }
    }

    /// <summary>
    /// 本地打印任务
    /// </summary>
    public class LocalJob
    {
        /// <summary>
        /// 任务标识
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 纸质固定编码
        /// </summary>
        public int MediaTypeNo { get; set; }

        /// <summary>
        /// 单双面固定编码
        /// </summary>
        public int DuplexNo { get; set; }

        /// <summary>
        /// 纸张大小固定编码
        /// </summary>
        public int PageSizeNo { get; set; }

        /// <summary>
        /// 打印颜色固定编码
        /// </summary>
        public int ColorNo { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 份数
        /// </summary>
        public int Copyes { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int SumPages { get; set; }

        /// <summary>
        /// 纸张方向（1-纵向，其他-横向）
        /// </summary>
        public int OrientationNo { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool IsPay { get; set; }

    }

    /// <summary>
    /// 待印任务价格
    /// </summary>
    public class TermPrice
    {
        /// <summary>
        /// 纸质固定编码
        /// </summary>
        public int MediaTypeNo { get; set; }

        /// <summary>
        /// 单双面固定编码
        /// </summary>
        public int DuplexNo { get; set; }

        /// <summary>
        /// 纸张大小固定编码
        /// </summary>
        public int PageSizeNo { get; set; }

        /// <summary>
        /// 打印颜色固定编码
        /// </summary>
        public int ColorNo { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 价格（分）
        /// </summary>
        public int Price { get; set; }

    }

    /// <summary>
    /// 本地打印机
    /// </summary>
    public class LocalPrint
    {
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string printName { get; set; }

        /// <summary>
        /// 打印机驱动名称
        /// </summary>
        public string printDrive { get; set; }
    }

    

    // 下面为数据库对象 -------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 安全密码
        /// </summary>
        public string SafePassWord { get; set; }

        /// <summary>
        /// 自动登录
        /// </summary>
        public bool IsAutoLogin { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 认证信息
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 电脑唯一标识
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 本机信息前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 打印店名称
        /// </summary>
        public string PrintShopName { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否开机运行
        /// </summary>
        public bool IsStartRun { get; set; }

    }

    /// <summary>
    /// 放行打印机
    /// </summary>
    public class PassPrinter
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 放行打印机名称
        /// </summary>
        public string PrintName { get; set; }

        /// <summary>
        /// 删除标识，初始化False，True为需要删除
        /// </summary>
        public bool NeedDel { get; set; }
    }

    /// <summary>
    /// 黑白打印机
    /// </summary>
    public class BlackPrinter
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 打印机驱动名称
        /// </summary>
        public string PrintDrive { get; set; }

        /// <summary>
        /// 删除标识，初始化False，True为需要删除
        /// </summary>
        public bool NeedDel { get; set; }
    }

    /// <summary>
    /// 彩色打印机
    /// </summary>
    public class ColorPrinter
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 打印机驱动名称
        /// </summary>
        public string PrintDrive { get; set; }

        /// <summary>
        /// 删除标识，初始化False，True为需要删除
        /// </summary>
        public bool NeedDel { get; set; }
    }

    /// <summary>
    /// 重复打印机
    /// </summary>
    public class RepeatPrinter
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 打印机驱动名称
        /// </summary>
        public string PrintDrive { get; set; }

        /// <summary>
        /// 删除标识，初始化False，True为需要删除
        /// </summary>
        public bool NeedDel { get; set; }
    }

    /// <summary>
    /// 问题打印机
    /// </summary>
    public class ProblematicPrinter
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 打印机驱动名称
        /// </summary>
        public string PrintDrive { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string KeyValues { get; set; }

        /// <summary>
        /// 启动入口
        /// </summary>
        public int  StartIndex { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public int  Length { get; set; }

        /// <summary>
        /// 修改偏移
        /// </summary>
        public int ReviseOffset { get; set; }

        /// <summary>
        /// 特殊SRC字符串值
        /// </summary>
        public string SpecialSrcValue  { get; set; }

        /// <summary>
        /// 特殊SRC入口
        /// </summary>
        public int  SpecialStart { get; set; }

        /// <summary>
        /// 特殊SRC长度
        /// </summary>
        public int SpecialLength { get; set; }

        /// <summary>
        /// 特殊SRC值
        /// </summary>
        public string SpecialValue { get; set; }

        /// <summary>
        /// 特殊SRC目标
        /// </summary>
        public int? SpecialTarget { get; set; }

        /// <summary>
        /// 启动入口2
        /// </summary>
        public int StartInex2 { get; set; }

        /// <summary>
        /// 强迫特殊逆转
        /// </summary>
        public int  ForceSpecialReverse { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        public int  DmModel { get; set; }
    }

    /// <summary>
    /// 打印价格表
    /// </summary>
    public class PrintPrice
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 纸质
        /// </summary>
        public int MediaType { get; set; }

        /// <summary>
        /// 单双面
        /// </summary>
        public int Duplex { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public int Color { get; set; }

        /// <summary>
        /// 价格（分）
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// 大于页数1
        /// </summary>
        public int SalePage1 { get; set; }

        /// <summary>
        /// 大于页数单价1
        /// </summary>
        public int SalePrice1 { get; set; }

        /// <summary>
        /// 大于页数2
        /// </summary>
        public int SalePage2 { get; set; }

        /// <summary>
        /// 大于页数单价2
        /// </summary>
        public int SalePrice2 { get; set; }

        /// <summary>
        /// 大于页数3
        /// </summary>
        public int SalePage3 { get; set; }

        /// <summary>
        /// 大于页数单价3
        /// </summary>
        public int SalePrice3 { get; set; }

        /// <summary>
        /// 删除标识，初始化False，True为需要删除
        /// </summary>
        public bool NeedDel { get; set; }

    }

    /// <summary>
    /// 打印任务
    /// </summary>
    public class Job
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrinterName { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 打印任务标识 JobId
        /// </summary>
        public int Identifier { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 提交名称
        /// </summary>
        public string UploadName { get; set; }

        /// <summary>
        /// 纸质固定编码
        /// </summary>
        public int MediaTypeNo { get; set; }

        /// <summary>
        /// 单双面固定编码
        /// </summary>
        public int DuplexNo { get; set; }

        /// <summary>
        /// 纸张大小固定编码
        /// </summary>
        public int PageSizeNo { get; set; }

        /// <summary>
        /// 打印颜色固定编码
        /// </summary>
        public int ColorNo { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 份数
        /// </summary>
        public int Copys { get; set; }

        /// <summary>
        /// 纸张方向（1-纵向，其他-横向）
        /// </summary>
        public int OrientationNo { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        [DefaultValue(false)]
        public bool IsPay { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [DefaultValue(true)]
        public bool IsShow { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(false)]
        public bool NeedDel { get; set; }

    }

    /// <summary>
    /// 打印订单额外费用
    /// </summary>
    public class JobExtra
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 额外费用 胶装
        /// </summary>
        [DefaultValue(false)]
        public bool ExtraGluing { get; set; }

        /// <summary>
        /// 额外费用 排版
        /// </summary>
        [DefaultValue(false)]
        public bool ExtraTypesetting { get; set; }

        /// <summary>
        /// 额外费用 复印
        /// </summary>
        [DefaultValue(false)]
        public bool ExtraCopy { get; set; }

        /// <summary>
        /// 额外费用 扫描
        /// </summary>
        [DefaultValue(false)]
        public bool ExtraScan { get; set; }

        /// <summary>
        /// 额外费用 其他
        /// </summary>
        [DefaultValue(false)]
        public bool ExtraOther { get; set; }

        /// <summary>
        /// 额外费用（元）
        /// </summary>
        [DefaultValue(0)]
        public double ExtraPrice { get; set; }
    }

}
