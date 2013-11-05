using System;
using System.IO;

namespace ApprovalTests.Writers
{
	public class ConfigurableTempTextFileWriter : ApprovalTextWriter
	{
		private string receivedFilePath;
		private string approvedFilePath;
		public ConfigurableTempTextFileWriter(string approvedFilePath, string data)
			: base(data, Path.GetExtension(approvedFilePath))
		{
			this.approvedFilePath = Path.GetFullPath(approvedFilePath);
		}


		public override string GetApprovalFilename(string basename)
		{
			return approvedFilePath;
		}

		public override string GetReceivedFilename(string basename)
		{
			if (String.IsNullOrEmpty(receivedFilePath))
			{
				receivedFilePath = Path.ChangeExtension(Path.Combine(Path.GetTempPath(), Path.GetTempFileName()), ExtensionWithDot);
			}
			return receivedFilePath;
		}
	}
}