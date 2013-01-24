using MbUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	public class ExampleUsages
	{
		[Test, Parallelizable]
		public void ApprovingText()
		{
			Approvals.Verify("A piece of text");
		}

		[Test, Parallelizable]
		public void ApprovingAnEnumerble()
		{
			Approvals.VerifyAll(new[] { "item1", "item2", "item3" }, "items");
		}


	}
}