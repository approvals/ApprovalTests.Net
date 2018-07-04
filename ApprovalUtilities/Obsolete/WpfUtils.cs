using System;

namespace ApprovalUtilities.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class WpfUtils
    {
        const string ObsoleteError = "This class has been moved to the ApprovalTests.Wpf NuGet package (https://www.nuget.org/packages/ApprovalTests.Wpf)";
    }
}