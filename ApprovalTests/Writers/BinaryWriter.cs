using System;
using System.IO;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Writers
{
	public class BinaryWriter : IApprovalWriter
	{
		public BinaryWriter(byte[] data)
		{
			Data = data;
		}

		public BinaryWriter(Stream content, string extensionWithoutDot)
		{
			Data = new byte[content.Length];
			content.Read(Data, 0, Data.Length);
			ExtensionWithDot = EnsureDoc(extensionWithoutDot);
		}

		public BinaryWriter(byte[] data, string extensionWithoutDot)
		{
			Data = data;
			ExtensionWithDot = EnsureDoc(extensionWithoutDot);
		}

		private string EnsureDoc(string extension)
		{
			var extensionWithDot = ".{0}".FormatWith(extension);
			return extension.StartsWith(".") ? extension : extensionWithDot;
		}

		public byte[] Data { get; set; }
		public string ExtensionWithDot { get; set; }


		public virtual string GetApprovalFilename(string basename)
		{
			return String.Format("{0}{1}{2}", basename, WriterUtils.Approved, ExtensionWithDot);
		}

		public virtual string GetReceivedFilename(string basename)
		{
			return String.Format("{0}{1}{2}", basename, WriterUtils.Received, ExtensionWithDot);
		}

		public string WriteReceivedFile(string received)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(received));
			File.WriteAllBytes(received, Data);
			return received;
		}
	}
}