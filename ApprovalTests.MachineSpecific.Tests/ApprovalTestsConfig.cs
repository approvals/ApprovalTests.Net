using System;
using System.Linq;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: UseReporter(typeof(DiffReporter))]

[TestClass]
public class SetupAssemblyInitializer
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var machinesToRun = new[]{ "LLEWELLYN-PC" };

        if (!machinesToRun.Contains(Environment.MachineName))
        {
            Assert.Inconclusive($"Machine name '{Environment.MachineName}' not in allowed list: {string.Join(", ", machinesToRun)}. See ApprovalTestsConfig.cs");
        }
    }
}