namespace ApprovalTests.Tests.Namer
{
    using System;
    using ApprovalTests.Namers;
    using ApprovalUtilities.Utilities;
    using NUnit.Framework;

    [TestFixture]
    public class ApprovalResultsTest
    {
        [Test]
        public void TestEasyNames()
        {
            Assert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));
        }

        [Test]
        public void TestUniqueNames()
        {
            ApprovalResults.UniqueForMachineName();
            var methods = new Func<string>[] {
                ApprovalResults.GetDotNetVersion,
                ApprovalResults.GetOsName,
                ApprovalResults.GetUserName
            };
            Approvals.VerifyAll(
                methods,
                m => "{0} => {1}".FormatWith(m.Method.Name, m.Invoke()));
        }
    }
}