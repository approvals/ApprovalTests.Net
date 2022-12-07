namespace ApprovalTests.Reporters;

public static class ReporterEvents
{
    public static readonly List<Action<string>> CreateNewFileEventListeners = new();

    public static void CreatedApprovedFile(string approved)
    {
        foreach (var listener in CreateNewFileEventListeners)
        {
            listener(approved);
        }
    }
}