using System;

namespace ApprovalUtilities.Obsolete
{
    [Obsolete(EntityFrameworkLoadersExtensions.ObsoleteError, true)]
    public abstract class EntityFrameworkLoader<QueryType, LoaderType, DatabaseContextType>
    {
    }

    [Obsolete(EntityFrameworkLoadersExtensions.ObsoleteError, true)]
    public class ObjectContextAdaptor<T>
    {
    }

    [Obsolete(EntityFrameworkLoadersExtensions.ObsoleteError, true)]
    public class EntityFrameworkUtils
    {
    }

    [Obsolete(EntityFrameworkLoadersExtensions.ObsoleteError, true)]
    public abstract class MultiRowEntityFrameworkLoader<T, DatabaseContextType>
    {
    }

    [Obsolete(ObsoleteError, true)]
    public static class EntityFrameworkLoadersExtensions
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.EntityFramework NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFramework)";
    }
}