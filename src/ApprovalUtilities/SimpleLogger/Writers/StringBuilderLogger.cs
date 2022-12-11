using System.Text;
using ApprovalUtilities.Utilities;

namespace ApprovalUtilities.SimpleLogger.Writers;

public class StringBuilderLogger : IAppendable
{
    StringBuilder builder = new();

    public void AppendLine(string text) =>
        builder.Append(text + "\n");

    public void Append(string text)
    {
        builder.Append(text);
    }

    public override string ToString() =>
        builder.ToString();

    public string ScrubPath(string path) =>
        ToString().ScrubPath(path);
}