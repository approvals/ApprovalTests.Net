namespace ApprovalUtilities.SimpleLogger.Writers;

public class NullWriter : IAppendable
{
    public static readonly NullWriter Instance = new NullWriter();
    public void AppendLine(string text)
    {
    }
}