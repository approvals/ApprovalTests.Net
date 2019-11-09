namespace ApprovalUtilities.Obsolete
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        Message = ObsoleteError)]
    public class NhQueryableAdaptor<T>
    {
        internal const string ObsoleteError = "This class has been moved to the ApprovalTests.NHibernate NuGet package (https://www.nuget.org/packages/ApprovalTests.NHibernate)";
    }
}