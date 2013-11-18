using System.IO;
using ApprovalTests.Core;
using ApprovalTests.StackTraceParsers;

namespace ApprovalTests.Namers
{
	public class UnitTestFrameworkNamer : IApprovalNamer
	{
		private readonly StackTraceParser stackTraceParser;
		private string subdirectory;

		public UnitTestFrameworkNamer()
		{
			Approvals.SetCaller();
			stackTraceParser = new StackTraceParser();
			stackTraceParser.Parse(Approvals.CurrentCaller.StackTrace);
			HandleSubdirectory();
		}

		private void HandleSubdirectory()
		{
			var subdirectoryAttribute = Approvals.CurrentCaller.GetFirstFrameForAttribute<UseApprovalSubdirectoryAttribute>();
			if (subdirectoryAttribute != null)
			{
				subdirectory = subdirectoryAttribute.Subdirectory;
			}
		}


		public string Name
		{
			get { return stackTraceParser.ApprovalName; }
		}

		public string SourcePath
		{
			get { return stackTraceParser.SourcePath + GetSubdirectory(); }
		}

		public string GetSubdirectory()
		{
			if (string.IsNullOrEmpty(subdirectory))
			{
				return string.Empty;
			}
			return Path.DirectorySeparatorChar + subdirectory;
		}
	}
}