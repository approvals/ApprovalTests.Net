using System.IO;

namespace ApprovalTests.Utilities
{
	public static class StringReporting
	{
		public static void DiffWith(this string expected, string actual)
		{
			if (expected != actual)
			{
				var expectedFile = Path.GetTempPath() + "Expected.Approvals.Temp.txt";
				var actualFile = Path.GetTempPath() + "Actual.Approvals.Temp.txt";

				File.WriteAllText(expectedFile, expected);
				File.WriteAllText(actualFile, actual);

				var reporter = Approvals.GetReporter();
				reporter.Report(expectedFile, actualFile);
			}
		}
	}
}