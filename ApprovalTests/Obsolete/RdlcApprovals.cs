using System.Collections.Generic;

namespace ApprovalTests.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public class RdlcApprovals
    {
        internal const string ObsoleteError =
            "This class has been moved to the ApprovalTests.Rdlc NuGet package (https://www.nuget.org/packages/ApprovalTests.Rdlc)";
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = RdlcApprovals.ObsoleteError)]
    public class DataPairs : Dictionary<string, object>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = RdlcApprovals.ObsoleteError)]
    public static class DataSetTestingUtilities
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = RdlcApprovals.ObsoleteError)]
    public class ColumnDefaults
    {
    }
}