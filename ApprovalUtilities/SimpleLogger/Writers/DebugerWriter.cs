using System.Diagnostics;

namespace ApprovalUtilities.SimpleLogger.Writers
{
    public class DebugerWriter : IAppendable
    {
        public void AppendLine(string text)
        {
            Debug.WriteLine(text);
        }
    }
}