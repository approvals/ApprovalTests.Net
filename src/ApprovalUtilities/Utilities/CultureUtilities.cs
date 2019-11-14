using System.Globalization;
using System.Threading;

namespace ApprovalUtilities.Utilities
{
    public static class CultureUtilities
    {
        public static void ForceCulture(string culture="en-US")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
        }
    }
}