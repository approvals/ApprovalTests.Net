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
        if (platformID is PlatformID.MacOSX or PlatformID.Unix)
        {
            if (Directory.Exists("/Applications") &&
                Directory.Exists("/Users") &&
                Directory.Exists("/Volumes") &&
                Directory.Exists("/System"))
            {
                return ApprovalsPlatform.Mac;
            }

            return ApprovalsPlatform.Linux;
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

        return platformId.ToString();
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