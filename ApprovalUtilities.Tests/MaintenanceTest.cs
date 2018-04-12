using ApprovalTests.Maintenance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests
{
    [TestClass]
    public class MaintenanceTest
    {
        [TestMethod]
        public void EnsureNoAbandonedFiles()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles();
        }
    }
}