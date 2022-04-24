using DiffEngine;

namespace ApprovalTests.Reporters;

public class RiderReporter : DiffToolReporter
{
    public static readonly RiderReporter INSTANCE = new RiderReporter();

    public RiderReporter() : base(DiffTool.Rider)
    {
    }
}