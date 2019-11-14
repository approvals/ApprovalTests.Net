using System;
using System.Diagnostics;

namespace ApprovalTests.Core
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = @"No longer used internally to approval tests. To replace use `Console.WriteLine(warning)` and `Debug.WriteLine(warning)`.")]
    public static class ConsoleUtilities
    {
        public static void WriteLine(string warning)
        {
            Console.WriteLine(warning);
            Debug.WriteLine(warning);
        }
    }
}