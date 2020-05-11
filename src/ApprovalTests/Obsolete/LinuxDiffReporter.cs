namespace ApprovalTests.Reporters.Linux
{
    [ObsoleteEx(
        RemoveInVersion = "5.3",
        TreatAsErrorFromVersion = "5.0",
        ReplacementTypeOrMember = nameof(DiffReporter))]
    public class LinuxDiffReporter
    {
    }
}