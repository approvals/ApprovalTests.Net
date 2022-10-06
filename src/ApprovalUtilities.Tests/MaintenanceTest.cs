using ApprovalTests.Maintenance;
using Xunit;

public class MaintenanceTest
{
    [Fact]
    public void EnsureNoAbandonedFiles()
    {
        ApprovalMaintenance.VerifyNoAbandonedFiles("Reflection");
    }
}