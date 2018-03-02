using System;

namespace ApprovalTests.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class WinFormsApprovals
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.WinForms NuGet package (https://www.nuget.org/packages/ApprovalTests.WinForms)";
    }
    [Obsolete(WinFormsApprovals.ObsoleteError, true)]
    public class WinFormsUtils
    {
    }
}