using ApprovalTests.Core;
using ApprovalTests.Reporters.ContinuousIntegration;

[TestFixture]
public class ReporterTest
{
    [Test]
    public void Testname()
    {
        var old = Environment.GetEnvironmentVariable(NCrunchReporter.EnvironmentVariable);
        Environment.SetEnvironmentVariable(NCrunchReporter.EnvironmentVariable, "1");
        Assert.IsTrue(NCrunchReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt"));
        Environment.SetEnvironmentVariable(NCrunchReporter.EnvironmentVariable, old);
    }

    [Test]
    public void TestInvalidReporterShouldThrow()
    {
        var attribute = new UseReporterAttribute(typeof(ReporterTest));
        VerifyReporterAttribute(attribute);
    }

    [Test]
    public void TestMultipleWithInvalidReporterShouldThrow()
    {
        var attribute = new UseReporterAttribute(typeof(ReporterTest), typeof(string));
        VerifyReporterAttribute(attribute);
    }

    static void VerifyReporterAttribute(UseReporterAttribute attribute)
    {
        var reporter = (IEnvironmentAwareReporter) attribute.Reporter;
        var reportException = Assert.Throws<Exception>(() => reporter.Report("a.txt", "a.txt"));
        var isWorkingException = Assert.Throws<Exception>(() => reporter.IsWorkingInThisEnvironment("a.txt"));

        Approvals.Verify($"""
                          {reportException.Message}

                          {isWorkingException.Message}
                          """);
    }
}