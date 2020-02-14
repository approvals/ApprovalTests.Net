namespace ApprovalTests.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public static class StatePrinterApprovals
    {
        const string ObsoleteError = "This class has been moved to the ApprovalTests.StatePrinter NuGet package (https://www.nuget.org/packages/ApprovalTests.StatePrinter)";
    }
}