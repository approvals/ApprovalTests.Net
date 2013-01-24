using System;
using ApprovalTests.Core;

namespace ApprovalTests.Wpf
{
	public class ImageWriter : IApprovalWriter
	{
		private readonly Action<string> writer;


		public ImageWriter(Action<string> writer)
		{
			this.writer = writer;
		}


		public string GetApprovalFilename(string basename)
		{
			return basename + ".approved.png";
		}

		public string GetReceivedFilename(string basename)
		{
			return basename + ".received.png";
		}

		public string WriteReceivedFile(string received)
		{
			writer(received);
			return received;
		}
	}
}