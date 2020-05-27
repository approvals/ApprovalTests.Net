using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;

[UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
public class ExistInNugetTests
{
    [Test]
    public void ShouldResolveCorrectPath()
    {
        Approvals.UseAssemblyLocationForApprovedFiles();
        Approvals.Verify("text");
    }
}