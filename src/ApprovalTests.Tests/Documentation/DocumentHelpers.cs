using System.Collections.Generic;
using System.IO;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
    [TestFixture]
    public class DocumentHelpers
    {

        [Test]
        public void ListAllVerifyFunctions()
        {
            Approvals.VerifyWithExtension("[Approvals.Verify(String text)](Approvals.cs)",".include.md");
        }

    }
}