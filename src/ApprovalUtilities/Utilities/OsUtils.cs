using System;
using System.IO;
using System.Linq;
using System.Management;

namespace ApprovalUtilities.Utilities;

public enum ApprovalsPlatform
{
    Windows,
    Linux,
    Mac
}

public class OsUtils
{
    public static ApprovalsPlatform GetPlatformId()
    {
        var platformID = Environment.OSVersion.Platform;
        if (platformID == PlatformID.MacOSX || platformID == PlatformID.Unix)
        {
            if (Directory.Exists("/Applications") &&
                Directory.Exists("/Users") &&
                Directory.Exists("/Volumes") &&
                Directory.Exists("/System"))
            {
                return ApprovalsPlatform.Mac;
            }
            else
            {
                return ApprovalsPlatform.Linux;
            }
        }
        return ApprovalsPlatform.Windows;
    }

    public static string GetFullOsNameFromWmi()
    {
        var platformId = GetPlatformId();
        if (platformId == ApprovalsPlatform.Windows)
        {
            var caption =
                (from x in
                        new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                    select x.GetPropertyValue("Caption")).FirstOrDefault();

            var name = caption == null ? Environment.OSVersion.ToString() : caption.ToString();
            return name;
        }
        else
        {
            return platformId.ToString();
        }
    }
    public static bool IsWindowsOs()
    {
        return Path.DirectorySeparatorChar == '\\';
    }

    public static bool IsUnixOs()
    {
        return !IsWindowsOs();
    }
}