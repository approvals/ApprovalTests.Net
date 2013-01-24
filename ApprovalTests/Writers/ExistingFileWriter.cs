using System;
using System.IO;
using ApprovalTests.Core;

namespace ApprovalTests.Writers
{
	public class ExistingFileWriter : IApprovalWriter
	{
		private string file;

		public ExistingFileWriter(string file)
		{
			this.file = file;
			if (!File.Exists(file))
			{
				throw new Exception("Existing File is required: '" + file + "'");
			}
		}

		public string GetApprovalFilename(string basename)
		{
			return basename + WriterUtils.Approved + new FileInfo(file).Extension;
		}

		public string GetReceivedFilename(string basename)
		{
			return file;
		}

		public string WriteReceivedFile(string received)
		{
			return file;
		}
	}
}