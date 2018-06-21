using System;

namespace ApprovalTests.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class WpfApprovals
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.Wpf NuGet package (https://www.nuget.org/packages/ApprovalTests.Wpf)";
    }

    [Obsolete(WpfApprovals.ObsoleteError, true)]
    public class WpfBindingsAssert
    {
    }

    [Obsolete(WpfApprovals.ObsoleteError, true)]
    public class ApprovalWpfWindowWriter
    {
    }

    [Obsolete(WpfApprovals.ObsoleteError, true)]
    public class ImageWriter
    {
    }

    [Obsolete(WpfApprovals.ObsoleteError, true)]
    public class AssertNoBindingErrorsTraceListener
    {
    }
}