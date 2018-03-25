using System;

namespace ApprovalUtilities.Persistence.EntityFramework
{
    [Obsolete(ObsoleteError, true)]
    public class ObjectContextAdaptor<T>
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.EntityFramework NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFramework)";
    }
}