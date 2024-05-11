﻿[TestFixture]
// begin-snippet: use_MachineSpecificReporter
[UseReporter(typeof(MachineSpecificReporter))]
// end-snippet
public class ApprovalResultsTest
{
    [Test]
    public void TestUniqueNames()
    {
        var machinesToRun = new[] {"LLEWELLYN-PC", "LLEWELLYNWINDOW"};

        if (!machinesToRun.Contains(Environment.MachineName))
        {
            Assert.Inconclusive($"Machine name '{Environment.MachineName}' not in allowed list: {string.Join(", ", machinesToRun)}. See ApprovalTestsConfig.cs");
        }

        ApprovalResults.UniqueForMachineName();
        var methods = new[]
        {
            //ApprovalResults.GetDotNetVersion,
            ApprovalResults.GetOsName,
            ApprovalResults.GetUserName
        };
        Approvals.VerifyAll(
            methods,
            m => $"{m.Method.Name} => {m.Invoke()}");
    }

    [Test]
    public void TestEasyNames() =>
        ClassicAssert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));

    public void SampleUniqueForOs()
    {
        // begin-snippet: unique_for_os
        using (ApprovalResults.UniqueForOs())
        {
            Approvals.Verify("Data");
        }
        // end-snippet
    }
}