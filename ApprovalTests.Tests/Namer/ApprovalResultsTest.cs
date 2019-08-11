using ApprovalTests.Namers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    // begin-snippet: use_MachineSpecificReporter
    [UseReporter(typeof(MachineSpecificReporter))]
    // end-snippet
    public class ApprovalResultsTest
    {
        [Test]
        public void TestEasyNames()
        {
            Assert.AreEqual("Windows 7", ApprovalResults.TransformEasyOsName("Microsoft Windows 7 Professional N"));
        }
        
        public void SampleUniqueForOs()
        {
            // begin-snippet: unique_for_os
            using (ApprovalResults.UniqueForOs())
            {
                Approvals.Verify("Data");
            }
            // end-snippet
        }
    }
}
