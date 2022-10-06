namespace ApprovalUtilities.SimpleLogger.Writers;

public class DebuggerWriter : IAppendable
{
    public void AppendLine(string text)
    {
        Debug.WriteLine(text);
    }
}