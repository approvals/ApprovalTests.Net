using ApprovalTests.Approvers;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{

	[TestFixture]
	public class FileApproverTests
	{

		[Test]
		public void TestFailureDueToMissingApproval()
		{

			AssertApprover("a.txt", "non_existing_file.txt", false);
		}

		[Test]
		public void TestFailureDueToMismatch()
		{
			AssertApprover("a.txt", "b.txt", false);
		}

		[Test]
		public void TestSuccess()
		{
			AssertApprover("a.txt", "a.txt", true);
		}

		private static void AssertApprover(string receivedFile, string approvedFile, bool expected)
		{
			var basePath = PathUtilities.GetDirectoryForCaller();
			var exception = FileApprover.Approve(basePath + approvedFile, basePath + receivedFile);
			Assert.AreEqual(expected, exception == null);
		}
	}
}