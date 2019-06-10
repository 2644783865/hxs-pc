using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace MaSoft.Code
{
    public class PrinterApi
    {
        /// <summary>
        /// 打印任务结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct JOB
        {
            public int JobId;
            public int MediaType;
            public int PageSize;
            public int Color;
            public int Orientation;
            public int Duplex;
            public int Copyes;
        };

        /// <summary>
        /// 获取打印任务详情
        /// </summary>
        /// <param name="jobinfo">ref 任务详情</param>
        /// <param name="name">打印机名称</param>
        /// <param name="jobid">任务标识ID</param>
        [DllImport("PrinterApi.dll", EntryPoint = "GetJobInfo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetJobInfo(ref JOB jobinfo, string name, int jobid);

        /// <summary>
        /// 纸张类型大小
        /// </summary>
        /// <param name="no">固定编号</param>
        /// <returns></returns>
        public static string GetPageSizeExplain(int no)
        {
            var ps = "A4";
            switch (no)
            {
                case 3002: ps = "B5"; break;
                case 3003: ps = "A3"; break;
                case 3004: ps = "A0"; break;
                case 3005: ps = "A1"; break;
                case 3006: ps = "A2"; break;
                case 3007: ps = "A5"; break;
                case 3008: ps = "A6"; break;
                case 3010: ps = "B4"; break;
                case 3011: ps = "letter"; break;
                case 3020: ps = "B2"; break;
                case 3021: ps = "B1"; break;
                case 3022: ps = "B6"; break;
                default: ps = "A4"; break;
            }
            return ps;
        }

        /// <summary>
        /// 打印类型
        /// </summary>
        /// <param name="no">固定编号</param>
        /// <returns></returns>
        public static string GetDuplexExplain(int no)
        {
            return no == 2001 ? "单面" : "双面";
        }

        /// <summary>
        /// 打印颜色
        /// </summary>
        /// <param name="no">固定编号</param>
        /// <returns></returns>
        public static string GetColorExplain(int no)
        {
            return no == 4001 ? "黑白" : "彩色";
        }

        /// <summary>
        /// 纸质
        /// </summary>
        /// <param name="no">固定编号</param>
        /// <returns></returns>
        public static string GetMediaTypeExplain(int no)
        {
            var ps = "普通";
            switch (no)
            {
                case 1013: ps = "透明"; break;
                case 1004: ps = "亮光"; break;
                default: ps = "普通"; break;
            }
            return ps;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class PRINTER_DEFAULTS
        {
            public IntPtr lpDataType;
            public IntPtr lpDevMode;
            [MarshalAs(UnmanagedType.U4)]
            public PrinterAccessRights DesiredAccess;
        }

        public enum PrinterAccessRights
        {
            READ_CONTROL = 0x20000,
            WRITE_DAC = 0x40000,
            WRITE_OWNER = 0x80000,
            SERVER_ACCESS_ADMINISTER = 0x1,
            SERVER_ACCESS_ENUMERATE = 0x2,
            PRINTER_ACCESS_ADMINISTER = 0x4,
            PRINTER_ACCESS_USE = 0x8,
            PRINTER_ALL_ACCESS = 0xF000C,
            SERVER_ALL_ACCESS = 0xF0003
        }

        public enum PrintJobControlCommands
        {
            JOB_CONTROL_SETJOB,
            JOB_CONTROL_PAUSE,
            JOB_CONTROL_RESUME,
            JOB_CONTROL_CANCEL,
            JOB_CONTROL_RESTART,
            JOB_CONTROL_DELETE,
            JOB_CONTROL_SENT_TO_PRINTER,
            JOB_CONTROL_LAST_PAGE_EJECTED
        }

        //打开打印机
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool OpenPrinter([In] string pPrinterName, out IntPtr phPrinter, [In] [MarshalAs(UnmanagedType.LPStruct)] PRINTER_DEFAULTS pDefault);

        //关闭打印机
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        public static extern bool ClosePrinter([In] IntPtr hPrinter);

        //修改打印作业设置
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetJob([In] IntPtr hPrinter, [In] int dwJobId, [In] int Level, [In] IntPtr lpJob, [In] [MarshalAs(UnmanagedType.U4)] PrintJobControlCommands dwCommand);

    }
    
}
