using System;

namespace ApprovalTests.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class EntityFrameworkApprovals
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.EntityFramework NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFramework)";
    }
}