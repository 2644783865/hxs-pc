using System;
using System.Management;

namespace MaSoft.Code
{
    public class HardwareTool
    {
        /// <summary>
        /// 获取CPU序列号代码
        /// </summary>
        /// <returns></returns>
        public static string GetCpuId()
        {
            try
            {
                var cpuInfo = "";
                var mc = new ManagementClass("Win32_Processor");
                var moc = mc.GetInstances();
                foreach (var o in moc)
                {
                    var mo = (ManagementObject)o;
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                return cpuInfo;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        public static string GetHardDiskId()
        {
            try
            {
                string hDid = null;
                using (var cimobject1 = new ManagementClass("Win32_DiskDrive"))
                {
                    var moc1 = cimobject1.GetInstances();
                    foreach (var o in moc1)
                    {
                        var mo = (ManagementObject)o;
                        hDid = (string)mo.Properties["Model"].Value;
                        mo.Dispose();
                    }
                }
                return hDid;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacId()
        {
            try
            {
                string moAddress = null;
                using (var mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
                {
                    var moc2 = mc.GetInstances();
                    foreach (var o in moc2)
                    {
                        var mo = (ManagementObject)o;
                        if ((bool)mo["IPEnabled"] == true)
                            moAddress = mo["MacAddress"].ToString();
                        mo.Dispose();
                    }
                }
                return moAddress;
            }
            catch (Exception)
            {
                return "";
            }
        }



    }
}
