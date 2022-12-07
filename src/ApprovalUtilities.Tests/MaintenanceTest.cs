using ApprovalTests.Maintenance;

public class MaintenanceTest
{
    [Fact]
    public void EnsureNoAbandonedFiles()
    {
        ApprovalMaintenance.VerifyNoAbandonedFiles("Reflection");
    }
}