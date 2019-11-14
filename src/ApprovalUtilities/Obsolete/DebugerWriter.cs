using System.Diagnostics;

namespace ApprovalUtilities.SimpleLogger.Writers
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(DebuggerWriter))]
    public class DebugerWriter : IAppendable
    {
        public void AppendLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}