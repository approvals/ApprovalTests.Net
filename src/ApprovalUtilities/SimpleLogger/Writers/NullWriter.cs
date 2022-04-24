namespace ApprovalUtilities.SimpleLogger.Writers;

public class NullWriter : IAppendable
{
    public static readonly NullWriter Instance = new();
    public void AppendLine(string text)
    {
    }
}