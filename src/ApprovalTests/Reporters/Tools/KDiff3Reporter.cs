using DiffEngine;

namespace ApprovalTests.Reporters;

public class KDiff3Reporter() :
    DiffToolReporter(DiffTool.KDiff3)
{
    public static readonly KDiff3Reporter INSTANCE = new();
}