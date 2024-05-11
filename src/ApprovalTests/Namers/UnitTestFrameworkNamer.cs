using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.Namers;

public class UnitTestFrameworkNamer : IApprovalNamer
{
    readonly StackTraceParser stackTraceParser;
    public string Subdirectory { get; }

    public UnitTestFrameworkNamer()
    {
        Approvals.SetCaller();
        stackTraceParser = new();
        stackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
        Subdirectory = GetSubdirectoryFromAttribute();
    }

    static string GetSubdirectoryFromAttribute()
    {
        var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
        return subdirectoryAttribute == null ? string.Empty : subdirectoryAttribute.Subdirectory;
    }

    public string Name => stackTraceParser.ApprovalName;

    public virtual string SourcePath => Path.Combine(stackTraceParser.SourcePath, Subdirectory);
}