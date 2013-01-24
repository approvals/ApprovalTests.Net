namespace ApprovalTests.Tests.Namer
{
    using NUnit.Framework;
    using ApprovalTests.Namers;
    using ApprovalUtilities.Utilities;
    using System;

    public class NamerFactoryTest
    {
        [Test]
        public void TestUniqueNames() {
            ApprovalResults.UniqueForMachineName();
            var methods = new Func<string>[] { ApprovalResults.GetDotNetVersion, ApprovalResults.GetOsName };
            Approvals.VerifyAll(methods, m => "{0} => {1}".FormatWith(m.Method.Name, m.Invoke()));
        }

        
    }
}