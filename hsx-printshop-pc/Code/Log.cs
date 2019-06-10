using System;
using System.IO;
using System.Text;
using System.Threading;

namespace MaSoft.Code
{

    public enum Level
    {
        All = int.MinValue,
        Verbose = 10000,
        Trace = 20000,
        Finer = 20000,
        Debug = 30000,
        Fine = 30000,
        Info = 40000,
        Notice = 50000,
        Warn = 60000,
        Error = 70000,
        Severe = 80000,
        Critical = 90000,
        Finest = 10000,
        Alert = 100000,
        Fatal = 110000,
        Emergency = 120000,
        Off = int.MaxValue
    }


    public static class Log
    {
        private static bool bool_0;

        private static bool bool_1;

        private static bool bool_2;

        private static bool bool_3;

        private static bool bool_4;

        private static string string_0;

        private static int ohIbbMetra;

        private static void smethod_0(string string_1, Level level_0, string string_2, bool bool_5)
        {
            DateTime now = DateTime.Now;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(string_2))
                {
                    stringBuilder.Append("\r\n");
                }
                else if (string_1.IndexOf("_NT") > -1)
                {
                    stringBuilder.Append(string.Format("{0}\r\n", string_2));
                }
                else
                {
                    stringBuilder.Append(string.Format("{0}\t{1}\t{2}\t{3}\r\n", now.ToString("yyyy-MM-dd HH:mm:ss.fff"), Thread.CurrentThread.Name, level_0, string_2));
                }
            }
            catch (Exception ex)
            {
                Error("WriteLogError", ex);
            }
            if (string.IsNullOrEmpty(string_1))
            {
                string_1 = "log";
            }
            if (stringBuilder.Length > ohIbbMetra || bool_5)
            {
                string text = string_0;
                try
                {
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                }
                catch
                {
                }
                try
                {
                    text += string.Format("{0}{1}.log", string_1, now.ToString("yyMMdd"));
                    if (stringBuilder.Length > 0)
                    {
                        Console.Out.Write(stringBuilder);
                    }
                    using (StreamWriter streamWriter = new StreamWriter(text, true, Encoding.GetEncoding("GB2312")))
                    {
                        streamWriter.Write(stringBuilder.ToString());
                    }
                }
                catch
                {
                }
                finally
                {
                    stringBuilder.Length = 0;
                    text = string.Empty;
                }
            }
        }

        internal static void Clear(int holdDays, string extName = ".log")
        {
            try
            {
                if (Directory.Exists(string_0))
                {
                    FileInfo[] files = new DirectoryInfo(string_0).GetFiles("*" + extName);
                    int num = int.Parse(DateTime.Now.AddDays(-holdDays).ToString("yyMMdd"));
                    int length = num.ToString().Length;
                    FileInfo[] array = files;
                    foreach (FileInfo fileInfo in array)
                    {
                        if (fileInfo.Name.Length >= length && int.Parse(fileInfo.Name.Substring(fileInfo.Name.Length - num.ToString().Length - extName.Length, num.ToString().Length)) < num)
                        {
                            fileInfo.Delete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Error("ClearLogError", ex);
            }
        }

        private static void CdlbAvXbYe(string string_1, Level level_0, string string_2, params object[] paras)
        {
            string string_3 = string.Format(string_2, paras);
            smethod_0(string_1, level_0, string_3, false);
        }

        public static void Debug(string msg)
        {
            if (bool_0)
            {
                smethod_0("Debug", Level.Debug, msg, false);
            }
        }

        public static void Debug(string format, params object[] paras)
        {
            if (bool_0)
            {
                CdlbAvXbYe("Debug", Level.Debug, format, paras);
            }
        }

        public static void Error(string msg)
        {
            if (bool_1)
            {
                smethod_0("Error", Level.Error, msg, false);
            }
        }

        public static void Error(Exception ex)
        {
            if (bool_1 && ex != null)
            {
                LocationInfo locationInfo = new LocationInfo(typeof(Log));
                string string_ = "出现错误：{0} ，{1} 定位：{2}";
                string text = string.Empty;
                string text2 = string.Empty;
                if (ex != null)
                {
                    text = ex.GetType().Name;
                    text2 = ex.Message + "," + ex.StackTrace;
                }
                CdlbAvXbYe("Error", Level.Error, string_, text, text2, locationInfo.FullInfo);
            }
        }

        public static void Error(string msg, Exception ex, string errorName = "")
        {
            if (bool_1)
            {
                try
                {
                    LocationInfo locationInfo = new LocationInfo(typeof(Log));
                    string string_ = "{0}出现错误：{1} ，{2} 定位：{3}";
                    string text = string.Empty;
                    string text2 = string.Empty;
                    if (ex != null)
                    {
                        text = ex.GetType().Name;
                        text2 = ex.Message + "," + ex.StackTrace;
                    }
                    CdlbAvXbYe("Error", Level.Error, string_, msg, text, text2, locationInfo.FullInfo);
                    if (!string.IsNullOrEmpty(errorName))
                    {
                        Dialog.Error(errorName, ex);
                    }
                }
                catch (Exception ex2)
                {
                    Warn("记录错误的时候出现错误:{0} ,跟踪信息：{1}", ex2.Message, ex2.StackTrace);
                }
            }
        }

        public static void Error(string format, params object[] paras)
        {
            if (bool_1)
            {
                CdlbAvXbYe("Error", Level.Error, format, paras);
            }
        }

        public static void Info(string msg)
        {
            if (bool_3)
            {
                smethod_0("Info", Level.Info, msg, false);
            }
        }

        public static void Info(string format, params object[] paras)
        {
            if (bool_3)
            {
                CdlbAvXbYe("Info", Level.Info, format, paras);
            }
        }

        public static void Warn(string msg)
        {
            if (bool_2)
            {
                smethod_0("Warn", Level.Warn, msg, false);
            }
        }

        public static void Warn(string format, params object[] paras)
        {
            if (bool_2)
            {
                CdlbAvXbYe("Warn", Level.Warn, format, paras);
            }
        }

        public static void Trace(string name, string msg)
        {
            if (bool_4)
            {
                smethod_0(name, Level.Notice, msg, false);
            }
        }

        public static void Trace(string name, string msg, Exception ex)
        {
            if (bool_1)
            {
                try
                {
                    LocationInfo locationInfo = new LocationInfo(typeof(Log));
                    string string_ = "{0}出现错误：{1} ，{2} 定位：{3}";
                    string text = string.Empty;
                    string text2 = string.Empty;
                    if (ex != null)
                    {
                        text = ex.GetType().Name;
                        text2 = ex.Message;
                    }
                    CdlbAvXbYe(name, Level.Error, string_, msg, text, text2, locationInfo.FullInfo);
                }
                catch (Exception ex2)
                {
                    Warn("记录错误的时候出现错误:{0} ,跟踪信息：{1}", ex2.Message, ex2.StackTrace);
                }
            }
        }

        static Log()
        {

            bool_0 = true;
            bool_1 = true;
            bool_2 = true;
            bool_3 = true;
            bool_4 = true;
            string_0 = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";
            ohIbbMetra = 0;
        }
    }
}
