using System.Text;
using CsharpHttpHelper;

namespace MaSoft.Code
{
    public class NetLog
    {
        private static string url = "http://log.huixueseng.com/index.php?c=api&a=add";
        private static string appid = "5cfdc4dd1f5d6";
        private static int time = 2000;
        private static bool open = true;

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="info"></param>
        public static void Info(string terminal, string info)
        {
            if (!open) return;
            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = url,
                Method = "post",
                PostEncoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Postdata = string.Format("appid={0}&type=1&terminal={1}&info={2}", appid, terminal, info),
                Timeout = time
            };
            http.FastRequest(item);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="info"></param>
        public static void Error(string terminal, string info)
        {
            if (!open) return;
            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = url,
                Method = "post",
                PostEncoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Postdata = string.Format("appid={0}&type=2&terminal={1}&info={2}", appid, terminal, info),
                Timeout = time
            };
            http.FastRequest(item);
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="info"></param>
        public static void Debug(string terminal, string info)
        {
            if (!open) return;
            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = url,
                Method = "post",
                PostEncoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Postdata = string.Format("appid={0}&type=3&terminal={1}&info={2}", appid, terminal, info),
                Timeout = time
            };
            http.FastRequest(item);
        }

        /// <summary>
        /// 数据操作
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="info"></param>
        public static void Data(string terminal, string info)
        {
            if (!open) return;
            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = url,
                Method = "post",
                PostEncoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Postdata = string.Format("appid={0}&type=4&terminal={1}&info={2}", appid, terminal, info),
                Timeout = time
            };
            http.FastRequest(item);
        }

        /// <summary>
        /// 其他操作
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="info"></param>
        public static void Other(string terminal, string info)
        {
            if (!open) return;
            var http = new HttpHelper();
            var item = new HttpItem()
            {
                URL = url,
                Method = "post",
                PostEncoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Postdata = string.Format("appid={0}&type=0&terminal={1}&info={2}", appid, terminal, info),
                Timeout = time
            };
            http.FastRequest(item);
        }

    }
}
