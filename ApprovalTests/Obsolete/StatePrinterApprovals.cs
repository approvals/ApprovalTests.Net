using System;

namespace ApprovalTests.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public static class StatePrinterApprovals
    {
        const string ObsoleteError = "This class has been moved to the ApprovalTests.StatePrinter NuGet package (https://www.nuget.org/packages/ApprovalTests.StatePrinter)";
    }
}