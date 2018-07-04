
using System;

namespace ApprovalUtilities.Obsolete
{
    [Obsolete(ObsoleteError, true)]
    public class EntityFrameworkUtils
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTestsApprovalTests.EntityFrameworkUtilities NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFrameworkUtilities)";
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class ObjectContextAdaptor<T>
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class Loaders
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public abstract class EntityFrameworkLoader<QueryType, LoaderType, DatabaseContextType>
    {

    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public static class EntityFrameworkLoadersExtensions
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class LambdaEnumerableLoader<T, C>
    {

    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class LambdaSingleLoader<T, C>
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public abstract class MultiRowEntityFrameworkLoader<T, DatabaseContextType>
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class FirstLoader<T>
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class OrderedLoader<T, TKey>
    {
    }

    [Obsolete(EntityFrameworkUtils.ObsoleteError, true)]
    public class PaginatedLoader<T>
    {
    }
}