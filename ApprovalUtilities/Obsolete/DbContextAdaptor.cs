using System;

namespace ApprovalUtilities.Persistence.EntityFramework.Version5
{
    [Obsolete(ObsoleteError, true)]
    public class DbContextAdaptor<T>
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.EntityFramework NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFramework)";
    }
}