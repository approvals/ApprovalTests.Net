using ApprovalUtilities.Utilities;

namespace ApprovalTests.Core.Exceptions
{
	public class ApprovalMismatchException : ApprovalException
	{
		public ApprovalMismatchException(string received, string approved) : base(received, approved)
		{
		}

		public override string Message
		{
			get { return "Failed Approval: Received file {0} does not match approved file {1}.".FormatWith(Received, Approved); }
		}
	}
}