namespace ApprovalTests.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public class WpfApprovals
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.Wpf NuGet package (https://www.nuget.org/packages/ApprovalTests.Wpf)";
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = WpfApprovals.ObsoleteError)]
    public class WpfBindingsAssert
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = WpfApprovals.ObsoleteError)]
    public class ApprovalWpfWindowWriter
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = WpfApprovals.ObsoleteError)]
    public class ImageWriter
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = WpfApprovals.ObsoleteError)]
    public class AssertNoBindingErrorsTraceListener
    {
    }
}