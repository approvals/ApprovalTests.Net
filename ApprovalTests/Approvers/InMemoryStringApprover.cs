using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApprovalTests.Core;
using ApprovalTests.Core.Exceptions;

namespace ApprovalTests.Approvers {
    public class InMemoryStringApprover : IApprovalApprover {
        private readonly string _approved;
        private ApprovalException _failure;
        private readonly string _received;

        public InMemoryStringApprover(string approved, string receivedData) 
        {
            _approved = Path.GetFullPath(approved);
            _received = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            Directory.CreateDirectory(Path.GetDirectoryName(_received));
            File.WriteAllText(_received, receivedData, Encoding.UTF8);
        }

        public virtual bool Approve()
        {
            _failure = Approve(_approved, _received);
            return _failure == null;
        }

        public virtual ApprovalException Approve(string approved, string received)
        {
            if (!File.Exists(approved))
            {
                return new ApprovalMissingException(received, approved);
            }

            if (!Compare(File.ReadAllBytes(received), File.ReadAllBytes(approved)))
            {
                return new ApprovalMismatchException(received, approved);
            }

            return null;
        }

        public void Fail()
        {
            throw _failure;
        }

        public void ReportFailure(IApprovalFailureReporter reporter)
        {
            reporter.Report(_approved, _received);
        }

        public void CleanUpAfterSucess(IApprovalFailureReporter reporter)
        {
            File.Delete(_received);
            if (reporter is IApprovalReporterWithCleanUp)
            {
                ((IApprovalReporterWithCleanUp)reporter).CleanUp(_approved, _received);
            }
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