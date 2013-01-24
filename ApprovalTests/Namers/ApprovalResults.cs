using System;
using System.Linq;
using System.Management;

namespace ApprovalTests.Namers
{
    public class ApprovalResults
    {
        public static void UniqueForDotNetVersion()
        {
            NamerFactory.AsMachineSpecificTest(GetDotNetVersion);
        }

        public static string GetDotNetVersion()
        {
            return "Net_v" + Environment.Version.ToString();
        }

        public static void UniqueForMachineName()
        {
            NamerFactory.AsMachineSpecificTest(GetMachineName);
        }

        public static string GetMachineName()
        {
            return "ForMachine." + Environment.MachineName;
        }

        public static string GetOsName()
        {
            var caption = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                           select x.GetPropertyValue("Caption")).FirstOrDefault();

            var name = caption == null ? Environment.OSVersion.ToString() : caption.ToString();
            return name.Trim().Replace(' ', '_');
        }

        public static void UniqueForOs()
        {
            NamerFactory.AsMachineSpecificTest(GetOsName);
        }
    }
}