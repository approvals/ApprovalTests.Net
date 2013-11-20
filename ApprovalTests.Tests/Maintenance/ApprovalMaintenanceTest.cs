using ApprovalTests.Maintenance;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Maintenance
{
	[TestFixture]
	public class ApprovalMaintenanceTest
	{
		[Test]
		public void FindAbandonedApprovalFiles()
		{
			using (var t = new TempFile(PathUtilities.GetAdjacentFile("DeletedClass.AbandoneMethod.approved.txt")))
			{
				t.WriteAllText("Llewellyn was here");
				var path = PathUtilities.GetDirectoryForCaller();
				var list = ApprovalMaintenance.FindAbandonedFiles(path);
				Approvals.VerifyAll("Abandoned Files:",list, f=>f.Name);
			}
		}
	}
}