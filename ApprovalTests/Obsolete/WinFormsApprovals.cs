namespace ApprovalTests.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public class WinFormsApprovals
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.WinForms NuGet package (https://www.nuget.org/packages/ApprovalTests.WinForms)";
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = WinFormsApprovals.ObsoleteError)]
    public class WinFormsUtils
    {
    }
}