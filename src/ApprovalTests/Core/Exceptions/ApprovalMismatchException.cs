using System.Runtime.Serialization;

namespace ApprovalTests.Core.Exceptions;

[Serializable]
public class ApprovalMismatchException : ApprovalException
{
    public ApprovalMismatchException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ApprovalMismatchException(string received, string approved) : base(received, approved)
    {
    }

    public override string Message => $"Failed Approval: Received file {Received} does not match approved file {Approved}.";
}