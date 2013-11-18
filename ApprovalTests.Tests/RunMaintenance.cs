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
			ApprovalMaintenance.VerifyNoAbandonedFiles();
		}
	}
}