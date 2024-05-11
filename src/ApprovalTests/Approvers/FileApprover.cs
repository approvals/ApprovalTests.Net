using ApprovalTests.Core;
using ApprovalTests.Core.Exceptions;
using EmptyFiles;

namespace ApprovalTests.Approvers;

public class FileApprover(IApprovalWriter writer, IApprovalNamer namer, bool normalizeLineEndingsForTextFiles = false)
    : IApprovalApprover
{
    public readonly IApprovalNamer namer = namer;
    public readonly bool normalizeLineEndingsForTextFiles = normalizeLineEndingsForTextFiles;
    public readonly IApprovalWriter writer = writer;
    public string approved;
    public ApprovalException failure;
    public string received;

    public virtual bool Approve()
    {
        var basename = Path.Combine(namer.SourcePath, namer.Name);
        approved = Path.GetFullPath(writer.GetApprovalFilename(basename));
        received = Path.GetFullPath(writer.GetReceivedFilename(basename));
        received = writer.WriteReceivedFile(received);

        failure = Approve(approved, received);
        return failure == null;
    }

    public virtual ApprovalException Approve(string approvedPath, string receivedPath)
    {
        if (!File.Exists(approvedPath))
        {
            return new ApprovalMissingException(receivedPath, approvedPath);
        }

        if (normalizeLineEndingsForTextFiles && FileExtensions.IsTextFile(approvedPath))
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
        throw failure;
    }

    public void ReportFailure(IApprovalFailureReporter reporter)
    {
        reporter.Report(approved, received);
    }

    public void CleanUpAfterSuccess(IApprovalFailureReporter reporter)
    {
        File.Delete(received);
        var withCleanUp = reporter as IApprovalReporterWithCleanUp;
        withCleanUp?.CleanUp(approved, received);
    }

    static bool Compare(ICollection<char> chars1, ICollection<char> chars2)
    {
        if (chars1.Count != chars2.Count)
        {
            return false;
        }

        using var e1 = chars1.GetEnumerator();
        using var e2 = chars2.GetEnumerator();

        while (e1.MoveNext() && e2.MoveNext())
        {
            if (e1.Current != e2.Current)
            {
                return false;
            }
        }

        return true;
    }

    static bool Compare(ICollection<byte> bytes1, ICollection<byte> bytes2)
    {
        if (bytes1.Count != bytes2.Count)
        {
            return false;
        }

        using var e1 = bytes1.GetEnumerator();
        using var e2 = bytes2.GetEnumerator();

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