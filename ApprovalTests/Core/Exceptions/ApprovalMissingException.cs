using System;
using System.Runtime.Serialization;

namespace ApprovalTests.Core.Exceptions
{
    [Serializable]
    public class ApprovalMissingException : ApprovalException
    {
        public ApprovalMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ApprovalMissingException(string received, string approved) : base(received, approved)
        {
        }

        public override string Message => $"Failed Approval: Approval File \"{Approved}\" Not Found.";
    }
}