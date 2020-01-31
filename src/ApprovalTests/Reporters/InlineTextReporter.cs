using System.IO;
using System.Linq;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;
using TextCopy;

namespace ApprovalTests.Reporters
{
    public class InlineTextReporter : IApprovalFailureReporter
    {
        public static readonly InlineTextReporter INSTANCE = new InlineTextReporter();

        public void Report(string approved, string received)
        {
            var text = File.ReadAllText(received);
             Clipboard.SetText(ConvertToCSharp(text));
        }

        public static string ConvertToCSharp(string text)
        {
            text = text.Replace("\r\n", "\n");
            if (!text.Contains("\n"))
            {
                return $"var expected = \"{HandleEscapeChars(text)}\";";
            }

            return
                "                var expected = new[]{\n" +
                text.Split('\n').Select(s => "                \"" + HandleEscapeChars(s) + "\"," ).JoinWith("\n") +
                "                };";
        }

        private static string HandleEscapeChars(string text)
        {
            return text.Replace("\"", "\\\"");
        }
    }
}