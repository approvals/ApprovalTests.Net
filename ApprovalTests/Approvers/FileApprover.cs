using System.Collections.Generic;
using System.IO;

using ApprovalTests.Core;
using ApprovalTests.Core.Exceptions;
using ApprovalTests.Reporters;

namespace ApprovalTests.Approvers
{
    public class FileApprover : IApprovalApprover
    {
        private readonly IApprovalNamer namer;
        private readonly bool normalizeLineEndingsForTextFiles;
        private readonly IApprovalWriter writer;
        private string approved;
        private ApprovalException failure;
        private string received;

        public FileApprover(IApprovalWriter writer, IApprovalNamer namer)
            : this(writer, namer, false)
        {
        }

        public FileApprover(IApprovalWriter writer, IApprovalNamer namer, bool normalizeLineEndingsForTextFiles)
        {
            this.writer = writer;
            this.namer = namer;
            this.normalizeLineEndingsForTextFiles = normalizeLineEndingsForTextFiles;
        }

        public virtual bool Approve()
        {
            string basename = Path.Combine(this.namer.SourcePath, this.namer.Name);
            this.approved = Path.GetFullPath(this.writer.GetApprovalFilename(basename));
            this.received = Path.GetFullPath(this.writer.GetReceivedFilename(basename));
            this.received = this.writer.WriteReceivedFile(this.received);

            this.failure = this.Approve(this.approved, this.received);
            return this.failure == null;
        }

        public virtual ApprovalException Approve(string approvedPath, string receivedPath)
        {
            if (!File.Exists(approvedPath))
            {
                return new ApprovalMissingException(receivedPath, approvedPath);
            }

            if (this.normalizeLineEndingsForTextFiles && GenericDiffReporter.IsTextFile(approvedPath))
            {
                var receivedText = File.ReadAllText(receivedPath).Replace("\r\n", "\n");
                var approvedText = File.ReadAllText(approvedPath).Replace("\r\n", "\n");

                return !Compare(receivedText.ToCharArray(), approvedText.ToCharArray()) ?
                    new ApprovalMismatchException(receivedPath, approvedPath) :
                    null;
            }

            return !Compare(File.ReadAllBytes(receivedPath), File.ReadAllBytes(approvedPath)) ?
                new ApprovalMismatchException(receivedPath, approvedPath) :
                null;
        }

        public void Fail()
        {
            throw this.failure;
        }

        public void ReportFailure(IApprovalFailureReporter reporter)
        {
            reporter.Report(this.approved, this.received);
        }

        public void CleanUpAfterSuccess(IApprovalFailureReporter reporter)
        {
            File.Delete(this.received);
            var withCleanUp = reporter as IApprovalReporterWithCleanUp;
            if (withCleanUp != null)
            {
                withCleanUp.CleanUp(this.approved, this.received);
            }
        }

        private static bool Compare(ICollection<char> chars1, ICollection<char> chars2)
        {
            if (chars1.Count != chars2.Count)
            {
                return false;
            }

            IEnumerator<char> e1 = chars1.GetEnumerator();
            IEnumerator<char> e2 = chars2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                if (e1.Current != e2.Current)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool Compare(ICollection<byte> bytes1, ICollection<byte> bytes2)
        {
            if (bytes1.Count != bytes2.Count)
            {
                return false;
            }

            IEnumerator<byte> e1 = bytes1.GetEnumerator();
            IEnumerator<byte> e2 = bytes2.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                if (e1.Current != e2.Current)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
