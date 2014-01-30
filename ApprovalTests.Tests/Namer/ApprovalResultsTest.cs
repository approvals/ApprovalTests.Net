namespace ApprovalTests.Tests.Namer
{
    using NUnit.Framework;
    using Namers;
    using ApprovalUtilities.Utilities;
    using System;

    [TestFixture]
    public class ApprovalResultsTest
    {
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

	    [Test]
	    public void TestEasyNames()
	    {
				Assert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));
	    }
    }
}