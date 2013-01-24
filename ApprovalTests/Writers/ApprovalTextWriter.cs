using System.IO;
using ApprovalTests.Core;
using System;

namespace ApprovalTests
{
	public class ApprovalTextWriter : IApprovalWriter
	{
		public ApprovalTextWriter(string data) : this(data, "txt")
		{
			Data = data;
		}

		public ApprovalTextWriter(string data, string extensionWithoutDot)
		{
			Data = data;
			ExtensionWithDot = EnsureDoc(extensionWithoutDot);
		}

		private string EnsureDoc(string extension)
		{
			string extensionWithDot = String.Format(".{0}", extension);
			return extension.StartsWith(".") ? extension : extensionWithDot;
		}
        public string Data { get; set; }
		public string ExtensionWithDot { get; set; }

		#region IApprovalWriter Members

		public string GetApprovalFilename(string basename)
		{
			return String.Format("{0}.approved{1}", basename, ExtensionWithDot);
		}

		public string GetReceivedFilename(string basename)
		{
			return String.Format("{0}.received{1}", basename, ExtensionWithDot);
		}

		public string WriteReceivedFile(string received)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(received));
			File.WriteAllText(received, Data);
			return received;
		}

		#endregion
	}
}