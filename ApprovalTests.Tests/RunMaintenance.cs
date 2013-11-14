using ApprovalTests.Core.Exceptions;
using ApprovalTests.Maintenance;
using ApprovalTests.Reporters;
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