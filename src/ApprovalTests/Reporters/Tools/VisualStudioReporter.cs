namespace ApprovalTests.Reporters;

public class VisualStudioReporter() :
    DiffToolReporter(DiffTool.VisualStudio)
{
    public static readonly VisualStudioReporter INSTANCE = new();
}