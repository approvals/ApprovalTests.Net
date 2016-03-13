using System.IO;

namespace ApprovalUtilities.Utilities
{
    public class OsUtils
    {
        public static bool IsWindowsOs()
        {
            return Path.DirectorySeparatorChar == '\\';
        }

        public static bool IsUnixOs()
        {
            return !IsWindowsOs();
        }
    }
}