using System.IO;
using ApprovalTests.Core;

namespace ApprovalTests.Tests
{
	public class CleanupReporter : IApprovalFailureReporter
	{
		public void Report(string approved, string received)
		{
			File.Delete(received);
		}

		public bool ApprovedWhenReported()
		{
			return false;
		}
	}
}