using System;
using System.Collections.Generic;

namespace ApprovalTests.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class RdlcApprovals
    {
        internal const string ObsoleteError =
                "This class has been moved to the ApprovalTests.Rdlc NuGet package (https://www.nuget.org/packages/ApprovalTests.Rdlc)"
            ;
    }

    [Obsolete(RdlcApprovals.ObsoleteError, true)]
    public class DataPairs : Dictionary<string, object>
    {
    }
    [Obsolete(RdlcApprovals.ObsoleteError, true)]
    public static class DataSetTestingUtilities
    {
    }
    [Obsolete(RdlcApprovals.ObsoleteError, true)]
    public class ColumnDefaults
    {
    }
}