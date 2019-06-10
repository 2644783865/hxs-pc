using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MaSoft.Code
{
    public static class StringHelper
    {
        #region 对象操作

        /// <summary>
        /// 对象是空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        /// <summary>
        /// 对象不为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToStr(this object input)
        {
            return input.IsNull() ? null : input.ToString();
        }

        #endregion

        #region 字符串是否为空
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string inputStr)
        {
            return string.IsNullOrEmpty(inputStr);
        }
        /// <summary>
        /// 判断字符串是否为Null和空白字符
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string inputStr)
        {
            return string.IsNullOrWhiteSpace(inputStr);
        }

        #endregion

        #region 字符串截取和替换
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string Sub(this string inputStr, int length)
        {
            if (inputStr.IsNullOrEmpty())
                return null;

            return inputStr.Length >= length ? inputStr.Substring(0, length) : inputStr;
        }
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="inputStr">字符串</param>
        /// <param name="oldStr">原字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public static string TryReplace(this string inputStr, string oldStr, string newStr)
        {
            return inputStr.IsNullOrEmpty() ? inputStr : inputStr.Replace(oldStr, newStr);
        }
        /// <summary>
        /// 使用正则替换字符串
        /// </summary>
        /// <param name="inputStr">字符串</param>
        /// <param name="pattern">正则</param>
        /// <param name="replacement">新字符串</param>
        /// <returns></returns>
        public static string RegexReplace(this string inputStr, string pattern, string replacement)
        {
            return inputStr.IsNullOrEmpty() ? inputStr : Regex.Replace(inputStr, pattern, replacement);
        }

        #endregion

        #region 字符串分割

        /// <summary>
        /// 把字符串按照英文逗号分割为String数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] GetStrArray(this string str)
        {
            return str.Split(',');
        }

        /// <summary>
        /// 把字符串按照英文逗号分割为String数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> GetIntArray(this string str)
        {
            var s = GetStrArray(str);
            List<int> m = null;
            foreach (var item in s)
            {
                m.Add(item.IsInt() ? int.Parse(item) : 0);
            }
            return m;
        }

        /// <summary>
        /// 分割字符串 按着指定分隔符分割字符串
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="strSplit">分隔符</param>
        /// <returns></returns>
        public static string[] SplitString(this string strContent, string strSplit)
        {
            string[] strArray = null;
            if (!string.IsNullOrEmpty(strContent))
            {
                strArray = new Regex(strSplit).Split(strContent);
            }
            return strArray;
        }

        /// <summary>
        /// 把字符串按照分隔符转换成 List
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换为小写</param>
        /// <returns></returns>
        public static List<string> GetStrArray(this string str, char speater, bool toLower)
        {
            var list = new List<string>();
            var ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (string.IsNullOrEmpty(s) || s == speater.ToString()) continue;
                var strVal = s;
                if (toLower)
                {
                    strVal = s.ToLower();
                }
                list.Add(strVal);
            }
            return list;
        }

        /// <summary>
        /// 把字符串按照指定分隔符装成List去除重复
        /// </summary>
        /// <param name="oStr"></param>
        /// <param name="sepeater"></param>
        /// <returns></returns>
        public static List<string> GetSubStringList(this string oStr, char sepeater)
        {
            var list = new List<string>();
            var ss = oStr.Split(sepeater);
            foreach (var s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }

        #endregion

        #region 字符串合并

        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayStr(this List<int> list)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i].ToString());
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 把 List按照分隔符组装成string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArrayStr(this List<string> list, string speater)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(this Dictionary<int, int> list)
        {
            var sb = new StringBuilder();
            foreach (var kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            if (list.Count > 0)
            {
                var str = sb.ToString();

                return str.Substring(0, str.LastIndexOf(",", StringComparison.Ordinal));
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(this Dictionary<int, int> list, string speater)
        {
            var sb = new StringBuilder();
            foreach (var kvp in list)
            {
                sb.Append(kvp.Value + speater);
            }
            if (list.Count > 0)
            {
                var str = sb.ToString();

                return str.Substring(0, str.LastIndexOf(speater, StringComparison.Ordinal));
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region 字符串格式化

        /// <summary>
        /// 字符串格式化
        /// </summary>
        /// <param name="input"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string Format(this string input, params object[] param)
        {
            if (input.IsNullOrWhiteSpace())
                return null;

            var result = string.Format(input, param);
            return result;
        }
        /// <summary>
        /// 格式化手机号码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string FormatMobile(this string mobile)
        {
            if (!mobile.IsNullOrEmpty() && mobile.Length > 7)
            {
                var regx = new Regex(@"(?<=\d{3}).+(?=\d{4})", RegexOptions.IgnoreCase);
                mobile = regx.Replace(mobile, "****");
            }
            return mobile;
        }

        /// <summary>
        /// 格式化身份证号码
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static string FormatIdCard(this string idCard)
        {
            if (!idCard.IsNullOrEmpty() && idCard.Length > 10)
            {
                var regx = new Regex(@"(?<=\w{6}).+(?=\w{4})", RegexOptions.IgnoreCase);
                idCard = regx.Replace(idCard, "********");
            }
            return idCard;
        }

        /// <summary>
        /// 格式化银行卡号
        /// </summary>
        /// <param name="bankCark"></param>
        /// <returns></returns>
        public static string FormatBankCard(this string bankCark)
        {
            if (!bankCark.IsNullOrEmpty() && bankCark.Length > 4)
            {
                var regx = new Regex(@"(?<=\d{4})\d+(?=\d{4})", RegexOptions.IgnoreCase);
                bankCark = regx.Replace(bankCark, " **** **** ");
            }
            return bankCark;
        }

        /// <summary>
        /// 格式化真实姓名
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public static string FormatRealName(this string realName)
        {
            var l = realName.Length;
            var h = "";
            for (var i = 0; i < l - 1; i++)
            {
                h += "*";
            }
            realName = realName.Substring(0, 1) + h;
            return realName;
        }

        #endregion

        #region 类型转换
        /// <summary>
        /// string转int
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认</param>
        /// <returns></returns>
        public static int TryInt(this string inputStr, int defaultNum = 0)
        {
            try{
                return int.Parse(inputStr);
            }
            catch{
                return defaultNum;
            }
        }

        /// <summary>
        /// string转long
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认</param>
        /// <returns></returns>
        public static long TryLong(this string inputStr, long defaultNum = 0)
        {
            try{
                return long.Parse(inputStr);
            }
            catch{
                return defaultNum;
            }
        }

        /// <summary>
        /// string转double
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static double TryDouble(this string inputStr, double defaultNum = 0)
        {
            try{
                return double.Parse(inputStr);
            }
            catch{
                return defaultNum;
            }
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static decimal TryDecimal(this string inputStr, decimal defaultNum = 0)
        {
            try{
                return decimal.Parse(inputStr);
            }
            catch{
                return defaultNum;
            }
        }

        /// <summary>
        /// string转decimal
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="defaultNum">转换失败默认值</param>
        /// <returns></returns>
        public static float TryFloat(this string inputStr, float defaultNum = 0)
        {
            try{
               return  float.Parse(inputStr);
            }
            catch{
                return defaultNum;
            }
        }


        /// <summary>
        /// 值类型转string
        /// </summary>
        /// <param name="inputObj">输入</param>
        /// <param name="defaultStr">转换失败默认值</param>
        /// <returns></returns>
        public static string TryString(this ValueType inputObj, string defaultStr = "")
        {
            var output = inputObj.IsNull() ? defaultStr : inputObj.ToString();
            return output;
        }

        /// <summary>
        /// 字符串去空格
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <returns></returns>
        public static string TryTrim(this string inputStr)
        {
            var output = inputStr.IsNullOrEmpty() ? inputStr : inputStr.Trim();
            return output;
        }

        /// <summary>
        /// 将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        public static byte[] StringToBytes(this string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        /// <summary>
        /// 使用指定编码将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(this string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        /// <summary>
        /// 将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        public static string BytesToString(this byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 使用指定编码将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(this byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }
            //定义要返回的整数
            var num = 0;
            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length < 4) return num;
            //创建一个临时缓冲区
            var tempBuffer = new byte[4];
            //将传入的字节数组的前4个字节复制到临时缓冲区
            Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);
            //将临时缓冲区的值转换成整数，并赋给num
            num = BitConverter.ToInt32(tempBuffer, 0);
            //返回整数
            return num;
        }

        /// <summary>
        /// 将对象转换为指定类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        /// <returns>返回值</returns>
        public static object ConvertTo(this object data, Type targetType)
        {
            if (data == null || Convert.IsDBNull(data))
            {
                return null;
            }
            Type type2 = data.GetType();
            if (targetType == type2)
            {
                return data;
            }
            if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(data.ToString()))
                {
                    return null;
                }
                return new Guid(data.ToString());
            }

            if (targetType.IsEnum)
            {
                try
                {
                    return Enum.Parse(targetType, data.ToString(), true);
                }
                catch
                {
                    return Enum.ToObject(targetType, data);
                }
            }

            if (targetType.IsGenericType)
            {
                targetType = targetType.GetGenericArguments()[0];
            }

            return Convert.ChangeType(data, targetType);
        }

        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        /// <returns>返回值</returns>
        public static T ConvertTo<T>(this object data)
        {
            if (data == null || Convert.IsDBNull(data))
                return default(T);

            object obj = ConvertTo(data, typeof(T));
            if (obj == null)
            {
                return default(T);
            }
            return (T)obj;
        }

        #endregion

        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(this string text, int limitedLength)
        {
            //补足0的字符串
            var temp = "";
            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }
            //连接text
            temp += text;
            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 进制判断及转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(this string value, int from, int to)
        {
            if (!IsBaseNumber(from))
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");

            if (!IsBaseNumber(to))
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");

            var intValue = Convert.ToInt32(value, from);  //先转成10进制
            var result = Convert.ToString(intValue, to);  //再转成目标进制
            if (to != 2) return result;
            var resultLength = result.Length;  //获取二进制的长度
            switch (resultLength)
            {
                case 7:
                    result = "0" + result;
                    break;
                case 6:
                    result = "00" + result;
                    break;
                case 5:
                    result = "000" + result;
                    break;
                case 4:
                    result = "0000" + result;
                    break;
                case 3:
                    result = "00000" + result;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 判断是否是  2 8 10 16 进制
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <returns></returns>
        private static bool IsBaseNumber(this int baseNumber)
        {
            return baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16;
        }

        #endregion

        #region 时间格式

        /// <summary>  
        /// 秒转换 mm:ss
        /// </summary>  
        /// <param name="time"></param>  
        /// <returns></returns>  
        public static string SecondToTimer(float time)
        {
            var str = new StringBuilder();
            var hour = 0;
            var minute = 0;
            var second = 0;
            second = Convert.ToInt32(time);

            if (second > 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
                if (hour > 0 && hour < 10)
                {
                    str.Append("0" + hour);
                }
                else if (hour >= 10)
                {
                    str.Append(hour);
                }
                else
                {
                    str.Append("00");
                }
                str.Append(":");
            }
            if (minute > 0 && minute < 10)
            {
                str.Append("0" + minute);
            }
            else if (minute >= 10)
            {
                str.Append(minute);
            }
            else
            {
                str.Append("00");
            }
            str.Append(":");
            if (second > 0 && second < 10)
            {
                str.Append("0" + second);
            }
            else if (second >= 10)
            {
                str.Append(second);
            }
            else
            {
                str.Append("00");
            }
            return str.ToString();
        }

        #endregion

    }
}
