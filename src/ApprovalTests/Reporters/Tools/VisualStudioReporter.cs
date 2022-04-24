using DiffEngine;

namespace ApprovalTests.Reporters;

public class VisualStudioReporter : DiffToolReporter
{
    public static readonly VisualStudioReporter INSTANCE = new();

    public VisualStudioReporter() : base(DiffTool.VisualStudio)
    {
    }
}