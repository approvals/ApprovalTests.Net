using ApprovalTests.Maintenance;
using Xunit;

namespace ApprovalUtilities.Tests
{
    public class MaintenanceTest
    {
        [Fact]
        public void EnsureNoAbandonedFiles()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles("Reflection");
        }
    }
}