namespace ApprovalTests.Core.Exceptions;

public class ApprovalMissingException(string received, string approved) :
    ApprovalException(received, approved)
{
    public override string Message => $"Failed Approval: Approval File \"{Approved}\" Not Found.";
}