using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using System.Linq;
using ApprovalTests.Namers.StackTraceParsers;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void SetUp()
    {
        AttributeStackTraceParser.ExcludeFileInfoFromApprovalTests = caller => true;
        FixCurrentDirectory();
        var machinesToRun = new[] { "LLEWELLYN-PC", "LLEWELLYNWINDOW" };

        if (!machinesToRun.Contains(Environment.MachineName))
        {
            Assert.Inconclusive($"Machine name '{Environment.MachineName}' not in allowed list: {string.Join(", ", machinesToRun)}. See ApprovalTestsConfig.cs");
        }
    }
    void FixCurrentDirectory([CallerFilePath] string callerFilePath = "")
    {
        Environment.CurrentDirectory = Directory.GetParent(callerFilePath).FullName;
    }
}