namespace ApprovalUtilities.SimpleLogger.Writers;

public class StringBuilderLogger : IAppendable
{
    StringBuilder sb = new();

    public void AppendLine(string text)
    {
        sb.Append(text + "\n");
    }

    public void Append(string text)
    {
        sb.Append(text);
    }

    public override string ToString()
    {
        return sb.ToString();
    }

    public string ScrubPath(string path)
    {
        return ToString().ScrubPath(path);
    }
}