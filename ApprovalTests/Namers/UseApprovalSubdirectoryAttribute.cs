using System;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Namers
{
    public class UseApprovalSubdirectoryAttribute : Attribute
    {
        public UseApprovalSubdirectoryAttribute(string subdirectory)
        {
            // begin-snippet: guard_usage
            Guard.AgainstNullAndEmpty(subdirectory, nameof(subdirectory));
            // end-snippet
            Subdirectory = subdirectory;
        }

        public string Subdirectory { get; }
    }
}