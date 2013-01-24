using ApprovalTests.Core;
using ApprovalTests.Reporters;
using ApprovalTests.Writers;

namespace ApprovalTests.Silverlight.Service
{
	public class ApprovalService : IApprovalService
	{
		public void Approve(string path, string testName, byte[] content)
		{
			IApprovalNamer namer = new SimpleNamer(path, testName);
			IApprovalWriter writer = new BinaryWriter(content, "png");
			IApprovalFailureReporter reporter = new ImageReporter();
			Approvals.Verify(writer, namer, reporter);
		}
	}
}