using ApprovalTests.Maintenance;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	public class RunMaintenance
	{
		[Test]
		public void EnsureNoAbandonedFiles()
		{
#if NETCORE
            ApprovalMaintenance.VerifyNoAbandonedFiles("CustomNamerShouldBeSubstitutable.approved.txt", "EntityFramework", "Wpf", "EventApprovalsTest", "StatePrinterTests", "PowerShellClipboardReporterTest", "TestEnsureFileExist", "TortoiseImageDiffTest");
#else
            ApprovalMaintenance.VerifyNoAbandonedFiles("CustomNamerShouldBeSubstitutable.approved.txt");
#endif
        }
	}
}