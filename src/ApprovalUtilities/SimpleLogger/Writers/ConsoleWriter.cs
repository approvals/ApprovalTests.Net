namespace ApprovalUtilities.SimpleLogger.Writers;

public class ConsoleWriter : IAppendable
{
    public void AppendLine(string text) => Console.WriteLine(text);
}