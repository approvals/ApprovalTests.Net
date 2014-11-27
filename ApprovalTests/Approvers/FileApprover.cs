using System.Collections.Generic;
using System.IO;
using ApprovalTests.Core;
using ApprovalTests.Core.Exceptions;

namespace ApprovalTests.Approvers
{
	public class FileApprover : IApprovalApprover
	{
		private readonly IApprovalNamer namer;
		private readonly bool normaliseLineEndingsForTextFiles;
		private readonly IApprovalWriter writer;
		private string approved;
		private ApprovalException failure;
		private string received;

		public FileApprover(IApprovalWriter writer, IApprovalNamer namer) : this(writer, namer, false)
		{
		}

		public FileApprover(IApprovalWriter writer, IApprovalNamer namer, bool normaliseLineEndingsForTextFiles)
		{
			this.writer = writer;
			this.namer = namer;
			this.normaliseLineEndingsForTextFiles = normaliseLineEndingsForTextFiles;
		}

		public virtual bool Approve()
		{
			string basename = string.Format(@"{0}\{1}", namer.SourcePath, namer.Name);
			approved = Path.GetFullPath(writer.GetApprovalFilename(basename));
			received = Path.GetFullPath(writer.GetReceivedFilename(basename));
			received = writer.WriteReceivedFile(received);

			failure = Approve(approved, received);
			return failure == null;
		}

		public virtual ApprovalException Approve(string approved, string received)
		{
			if (!File.Exists(approved))
			{
				return new ApprovalMissingException(received, approved);
			}

			if (normaliseLineEndingsForTextFiles && Path.GetExtension(approved).EndsWith(".txt"))
			{
				var receivedText = File.ReadAllText(received).Replace("\r\n", "\n");
				var approvedText = File.ReadAllText(received).Replace("\r\n", "\n");


				if (!Compare(receivedText.ToCharArray(), approvedText.ToCharArray()))
				{
					return new ApprovalMismatchException(received, approved);
				}

				return null;
			}

			if (!Compare(File.ReadAllBytes(received), File.ReadAllBytes(approved)))
			{
				return new ApprovalMismatchException(received, approved);
			}

			return null;
		}

		public void Fail()
		{
			throw failure;
		}

		public void ReportFailure(IApprovalFailureReporter reporter)
		{
			reporter.Report(approved, received);
		}

		public void CleanUpAfterSucess(IApprovalFailureReporter reporter)
		{
			File.Delete(received);
			if (reporter is IApprovalReporterWithCleanUp)
			{
				((IApprovalReporterWithCleanUp)reporter).CleanUp(approved, received);
			}
		}

		private static bool Compare(ICollection<char> chars1, ICollection<char> chars2)
		{
			if (chars1.Count != chars2.Count)
				return false;

			IEnumerator<char> e1 = chars1.GetEnumerator();
			IEnumerator<char> e2 = chars2.GetEnumerator();

			while (e1.MoveNext() && e2.MoveNext())
			{
				if (e1.Current != e2.Current)
					return false;
			}

			return true;
		}

		private static bool Compare(ICollection<byte> bytes1, ICollection<byte> bytes2)
		{
			if (bytes1.Count != bytes2.Count)
				return false;

			IEnumerator<byte> e1 = bytes1.GetEnumerator();
			IEnumerator<byte> e2 = bytes2.GetEnumerator();

			while (e1.MoveNext() && e2.MoveNext())
			{
				if (e1.Current != e2.Current)
					return false;
			}

			return true;
		}
	}
}