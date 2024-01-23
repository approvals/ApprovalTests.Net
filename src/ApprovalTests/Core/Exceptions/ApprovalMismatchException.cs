namespace ApprovalTests.Core.Exceptions;

public class ApprovalMismatchException(string received, string approved) :
    ApprovalException(received, approved)
{
    public override string Message => $"Failed Approval: Received file {Received} does not match approved file {Approved}.";
}