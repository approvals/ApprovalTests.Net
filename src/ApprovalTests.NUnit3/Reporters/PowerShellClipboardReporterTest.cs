﻿[TestFixture]
public class PowerShellClipboardReporterTest
{
    [Test]
    public void TestCommand() =>
        Approvals.Verify(PowerShellClipboardReporter.GetCommandLineForApproval(@"c:\temp\approved.txt", @"c:\temp\recieved.txt"));
}