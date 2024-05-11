using DiffEngine;

namespace ApprovalTests.Reporters;

public class BeyondCompareReporter() :
    DiffToolReporter(DiffTool.BeyondCompare)
{
    public static readonly BeyondCompareReporter INSTANCE = new();
}