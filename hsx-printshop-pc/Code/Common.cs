using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace MaSoft.Code
{
    public static class Common
    {
        #region LIst扩展
        /// <summary>
        /// 比较两个List是否相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ListA"></param>
        /// <param name="ListB"></param>
        /// <returns></returns>
        public static bool IsEqual<T>(this IList<T> ListA, IList<T> ListB)
        {
            if (ListA.Count() != ListB.Count())
                return false;
            foreach (var x in ListA)
            {
                if (!ListB.Contains(x))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        #endregion

        #region HTTP POST请求（可上传文件）

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="formItems">Post表单内容</param>
        /// <param name="cookieContainer">cookie列表</param>
        /// <param name="timeOut">超时 默认20秒</param>
        /// <param name="refererUrl">来源地址</param>
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>
        /// <returns></returns>
        public static string Post(string url, List<PostItem> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 60000)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            #region 初始化请求对象
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (!string.IsNullOrEmpty(refererUrl))
                request.Referer = refererUrl;
            if (cookieContainer != null)
                request.CookieContainer = cookieContainer;
            #endregion

            var boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
            request.ContentType = "multipart/form-data; boundary="+boundary;
            //请求流
            var postStream = new MemoryStream();

            #region 处理Form表单请求内容
            //是否用Form上传文件
            var formUploadFile = formItems != null && formItems.Count > 0;
            if (formUploadFile)
            {
                //文件数据模板
                var fileFormatTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
                    "\r\nContent-Type: application/octet-stream" +
                    "\r\n\r\n";
                //文本数据模板
                var dataFormatTemplate =
                    "\r\n--" + boundary +
                    "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                    "\r\n\r\n{1}";
                foreach (var item in formItems)
                {
                    string format = null;
                    format = item.IsFile ? string.Format(fileFormatTemplate, item.Key, item.FileName) : string.Format(dataFormatTemplate, item.Key, item.Value);
                    //统一处理
                    byte[] formdataBytes = null;
                    //第一行不需要换行
                    formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? format.Substring(2, format.Length - 2) : format);
                    postStream.Write(formdataBytes, 0, formdataBytes.Length);
                    //写入文件内容
                    if (item.FileContent == null || item.FileContent.Length <= 0) continue;
                    using (var stream = item.FileContent)
                    {
                        var buffer = new byte[1024];
                        var bytesRead = 0;
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            postStream.Write(buffer, 0, bytesRead);
                        }
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream.Length;

            #region 输入二进制流
            
            if (postStream.Length>0)
            {
                postStream.Position = 0;
                var requestStream = request.GetRequestStream();
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                ////debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = sr.ReadToEnd();
                postStream.Close();//关闭文件访问
            }
            #endregion

            var response = (HttpWebResponse)request.GetResponse();
            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (var responseStream = response.GetResponseStream())
            {
                using (var myStreamReader = new StreamReader(responseStream,Encoding.UTF8))
                {
                    var retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        #endregion

        #region 二维码处理

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="size">图片大小（默认200px）</param>
        /// <param name="margin">边距（默认2）</param>
        /// <returns></returns>
        public static Bitmap ToQrCode(string content, int size = 200,int margin = 2)
        {
            var writer = new BarcodeWriter {Format = BarcodeFormat.QR_CODE};
            var options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",//内容编码
                Width = size,//二维码宽度
                Height = size,//二维码高度
                Margin = margin//二维码边距
            };
            writer.Options = options;
            var pic = writer.Write(content);
            return pic;
        }

        /// <summary>
        /// 生成条形码
        /// 1、只支持数字
        /// 2、只支持偶数个
        /// 3、最大长度为80
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="margin">边距</param>
        /// <returns></returns>
        public static Bitmap ToBarCode(string content, int width = 150,int height = 50,int margin = 2)
        {
            //使用ITF格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            var writer = new BarcodeWriter {Format = BarcodeFormat.CODE_128};
            var options = new EncodingOptions()
            {
                Width = width,
                Height = height,
                Margin = margin
            };
            writer.Options = options;
            var pic = writer.Write(content);
            return pic;
        }

        /// <summary>
        /// 生成带logo的二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="logoPath">logo路径</param>
        /// <param name="moduleSize">二维码大小</param>
        /// <returns>返回二维码图片路径</returns>
        public static Bitmap ToQrCode(string content, string logoPath, int moduleSize = 5)
        {
            var logo = new Bitmap(logoPath);
            //构造二维码写码器
            var writer = new MultiFormatWriter();
            var hint = new Dictionary<EncodeHintType, object>
            {
                {EncodeHintType.CHARACTER_SET, "UTF-8"}, {EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H}
            };
            //生成二维码 
            var bm = writer.encode(content, BarcodeFormat.QR_CODE, 300, 300, hint);
            var barcodeWriter = new BarcodeWriter();
            var map = barcodeWriter.Write(bm);
            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            var rectangle = bm.getEnclosingRectangle();
            //计算插入图片的大小和位置
            var middleW = Math.Min((int)(rectangle[2] / 3.5), logo.Width);
            var middleH = Math.Min((int)(rectangle[3] / 3.5), logo.Height);
            var middleL = (map.Width - middleW) / 2;
            var middleT = (map.Height - middleH) / 2;
            //将img转换成bmp格式，否则后面无法创建Graphics对象
            var impinge = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(impinge))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0);
            }
            //将二维码插入图片
            var myGraphic = Graphics.FromImage(impinge);
            //白底
            myGraphic.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);
            return impinge;
        }

        /// <summary>
        /// 解析二维码或条码图片
        /// </summary>
        /// <param name="pic">图片</param>
        /// <returns></returns>
        public static string FromQrCode(Bitmap pic)
        {
            var reader = new BarcodeReader {Options = {CharacterSet = "UTF-8"}};
            var result = reader.Decode(pic);
            return result == null ? "" : result.Text;
        }

        #endregion

    }

    #region 表单数据项

    /// <summary>
    /// 表单数据项
    /// </summary>
    public class PostItem
    {
        /// <summary>
        /// 表单键，request["key"]
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// 表单值，上传文件时忽略，request["key"].value
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// 是否是文件
        /// </summary>
        public bool IsFile
        {
            get
            {
                if (FileContent == null || FileContent.Length == 0)
                    return false;

                if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                    throw new Exception("上传文件时 FileName 属性值不能为空");
                return true;
            }
        }
        /// <summary>
        /// 上传的文件名
        /// </summary>
        public string FileName { set; get; }
        /// <summary>
        /// 上传的文件内容
        /// </summary>
        public Stream FileContent { set; get; }
    }

    #endregion

}
