using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using System.Linq;

[SetUpFixture]
public class SetUpFixture
{
    [OneTimeSetUp]
    public void SetUp()
    {
        FixCurrentDirectory();
        var machinesToRun = new[] { "LLEWELLYN-PC" };

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