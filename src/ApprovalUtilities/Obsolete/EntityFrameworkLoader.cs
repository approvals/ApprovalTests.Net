namespace ApprovalUtilities.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public class EntityFrameworkUtils
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.EntityFramework NuGet package (https://www.nuget.org/packages/ApprovalTests.EntityFramework)";
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class ObjectContextAdaptor<T>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class Loaders
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public abstract class EntityFrameworkLoader<QueryType, LoaderType, DatabaseContextType>
    {

    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public static class EntityFrameworkLoadersExtensions
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class LambdaEnumerableLoader<T, C>
    {

    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class LambdaSingleLoader<T, C>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public abstract class MultiRowEntityFrameworkLoader<T, DatabaseContextType>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class FirstLoader<T>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class OrderedLoader<T, TKey>
    {
    }

    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = EntityFrameworkUtils.ObsoleteError)]
    public class PaginatedLoader<T>
    {
    }
}