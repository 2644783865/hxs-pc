using System;
using System.Text;
using System.Text.RegularExpressions;

namespace MaSoft.Code
{
    public static class ValidateHelper
    {

        #region 数字字符串检查

        /// <summary>
        /// 是否是int
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值 bool</returns>
        public static bool IsInt(this string input)
        {
            return Regex.IsMatch(input, @"^[1-9]\d*\.?[0]*$");
        }

        /// <summary>
        /// 是否纯数字字符串(0~9的字符串)
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值 bool</returns>
        public static bool IsNum(this string input)
        {
            var regNumber = new Regex("^[0-9]+$");
            var m = regNumber.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 是否纯数字字符串 可带正负号
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值 bool</returns>
        public static bool IsNumberSign(this string input)
        {
            var regNumberSign = new Regex("^[+-]?[0-9]+$");
            var m = regNumberSign.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDecimal(this string input)
        {
            var regDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
            var m = regDecimal.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDecimalSign(this string input)
        {
            var regDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
            var m = regDecimalSign.Match(input);
            return m.Success;
        }

        #endregion

        #region 常用格式有效性

        /// <summary>
        /// 固定电话号码格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsPhone(this string input)
        {
            var rx = new Regex(@"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$", RegexOptions.None);
            var m = rx.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 手机号码格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsMobile(this string input)
        {
            var rx = new Regex(@"^(13|15|17|18)\d{9}$", RegexOptions.None);
            var m = rx.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 固话和手机格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsValidPhoneAndMobile(this string input)
        {
            var rx = new Regex(@"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}$|^(13|15|17|18)\d{9}$", RegexOptions.None);
            var m = rx.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 身份证格式有效性
        /// </summary>
        /// <returns>返回值</returns>
        public static bool IsIdCard(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            switch (input.Length)
            {
                case 15:
                    return Regex.IsMatch(input, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$");
                case 18:
                    return Regex.IsMatch(input,
                        @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[A-Z])$",
                        RegexOptions.IgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// 邮件地址格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsEmail(this string input)
        {
            var regEmail = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            var m = regEmail.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 邮编格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsZipCode(this string input)
        {
            var rx = new Regex(@"^\d{6}$", RegexOptions.None);
            var m = rx.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 网址格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsValidUrl(this string input)
        {
            return Regex.IsMatch(input,
                                 @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$");
        }

        /// <summary>
        /// 域名有效性
        /// </summary>
        /// <param name="input">域名</param>
        /// <returns>返回值</returns>
        public static bool IsValidDomain(this string input)
        {
            var r = new Regex(@"^\d+$");
            if (input.IndexOf(".", StringComparison.Ordinal) == -1)
            {
                return false;
            }
            return !r.IsMatch(input.Replace(".", string.Empty));
        }

        /// <summary>
        /// IP地址格式有效性
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsValidIp(this string input)
        {
            return Regex.IsMatch(input, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 检测是否为base64字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsBase64(this string input)
        {
            return Regex.IsMatch(input, @"[A-Za-z0-9\+\/\=]");
        }

        #endregion

        #region 日期格式有效性

        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDate(this string input)
        {
            return Regex.IsMatch(input,
                @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsDateHourMinute(this string input)
        {
            return Regex.IsMatch(input,
                @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }

        #endregion

        #region 文件格式有效性

        /// <summary>
        /// 是否是图片文件名
        /// </summary>
        /// <returns> </returns>
        public static bool IsImgFileName(this string fileName)
        {
            if (fileName.IndexOf(".", StringComparison.Ordinal) == -1)
                return false;

            string tempFileName = fileName.Trim().ToLower();
            string extension = tempFileName.Substring(tempFileName.LastIndexOf(".", StringComparison.Ordinal));
            return extension == ".png" || extension == ".bmp" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif";
        }



        #endregion

        #region 中文检测

        /// <summary>
        /// 检测是否只有中文字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsChzn(this string input)
        {
            var regChzn = new Regex("^[\u4e00-\u9fa5]+$");
            var m = regChzn.Match(input);
            return m.Success;
        }

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool IsHasChzn(this string input)
        {
            var regChzn = new Regex("[\u4e00-\u9fa5]+");
            var m = regChzn.Match(input);
            return m.Success;
        }


        #endregion

        #region UI友好性

        /// <summary>
        /// 检测用户名格式是否有效 用户名的长度（4-20个字符）及内容（只能是汉字、字母、下划线、数字）是否合法
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>返回值</returns>
        public static bool IsQualifiedUserName(this string userName)
        {
            var userNameLength = Encoding.Default.GetBytes(userName).Length;
            return userNameLength >= 4 && userNameLength <= 20 &&
                   Regex.IsMatch(userName, @"^([\u4e00-\u9fa5A-Za-z_0-9]{0,})$");
        }

        /// <summary>
        /// 密码格式有效性 大小写字母和数字 6至16位
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>返回值</returns>
        public static bool IsQualifiedPassword(this string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z_0-9]{6,16}$");
        }

        #endregion
    }
}
