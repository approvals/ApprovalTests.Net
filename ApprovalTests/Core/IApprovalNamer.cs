namespace ApprovalTests.Core
{
    public interface IApprovalNamer
    {
        /// <summary>
        /// Full directory path of the source file.
        /// </summary>
        string SourcePath { get; }
        /// <summary>
        /// Name without extension. For example 'MyClass.MyMethod'.
        /// </summary>
        string Name { get; }
    }
}