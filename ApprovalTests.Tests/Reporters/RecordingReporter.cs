using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Tests.Reporters
{
	public class RecordingReporter : IEnvironmentAwareReporter
	{
		private readonly bool working;
		public RecordingReporter()
		{
			this.working = true;
		}
		public RecordingReporter(bool working)
		{
			this.working = working;
		}
		public void Report(string approved, string received)
		{
			this.CalledWith = "{0},{1}".FormatWith(approved, received);
		}
		public bool IsWorkingInThisEnvironment(string forFile)
		{
			return working;
		}

		public string CalledWith { get; set; }
	}
}