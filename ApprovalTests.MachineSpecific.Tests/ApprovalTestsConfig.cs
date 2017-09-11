
using System;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

[assembly: UseReporter(typeof(DiffReporter))]

[TestClass]
public class SetupAssemblyInitializer
{
    [AssemblyInitialize]
    public static void AssemblyInit(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext t)
    {
        var machinesToRun = new[]{ "LLEWELLYN-PC" };

        if (!machinesToRun.Contains(Environment.MachineName))
        {
            Assert.Inconclusive($"Machine name '{Environment.MachineName}' not in allowed list: {string.Join(", ", machinesToRun)}. See ApprovalTestsConfig.cs");
        }
    }
}