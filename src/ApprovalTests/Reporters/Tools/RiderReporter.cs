using DiffEngine;

namespace ApprovalTests.Reporters;

public class RiderReporter() :
    DiffToolReporter(DiffTool.Rider)
{
    public static readonly RiderReporter INSTANCE = new();
}