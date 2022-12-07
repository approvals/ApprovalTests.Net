using ApprovalTests.Maintenance;

namespace ApprovalUtilities.Tests;

public class MaintenanceTest
{
    [Fact]
    public void EnsureNoAbandonedFiles()
    {
        ApprovalMaintenance.VerifyNoAbandonedFiles("Reflection");
    }
}