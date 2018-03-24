using System.Text;
using ApprovalUtilities.Utilities;

namespace ApprovalUtilities.SimpleLogger.Writers
{
    public class StringBuilderLogger : IAppendable
    {
        private StringBuilder sb = new StringBuilder();

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
}