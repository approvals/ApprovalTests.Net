
namespace ApprovalTests.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ApprovalTests.Core;
    using System.Windows;

    public class AllFailingTestsClipboardReporter : IApprovalFailureReporter
    {
        static StringBuilder TOTAL = new StringBuilder();
        public static readonly AllFailingTestsClipboardReporter INSTANCE = new AllFailingTestsClipboardReporter();
        public void Report(string approved, string received)
        {
            string temp = QuietReporter.GetCommandLineForApproval(approved, received);
            TOTAL.AppendLine(temp);
            Clipboard.SetText(TOTAL.ToString());
        }

    }
}
