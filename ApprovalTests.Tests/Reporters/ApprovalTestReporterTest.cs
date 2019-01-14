using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    public class ApprovalTestReporterTest
    {
        [Test]
        public void TestApprovalTestIsWorkingForText()
        {
            Approvals.SetCaller();
            Assert.True(ApprovalTestReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
        }
        [Test]
        public void TestApprovalTestIsNotWorkingForImage()
        {
            Approvals.SetCaller();
            Assert.False(ApprovalTestReporter.INSTANCE.IsWorkingInThisEnvironment("default.png"));
        }

        [Test]
        public void TestApprovalTestReporterIsWorkingWithEqualsStrings()
        {
            ApprovalTestReporter.INSTANCE.ShouldIgnoreLineEndings = true;
            Assert.DoesNotThrow(() =>ApprovalTestReporter.INSTANCE.Report("Hello", "Hello"));
        }

        [Test]
        public void TestApprovalTestReporterIsDetectingNotEqualStrings()
        {
            var e = ExceptionUtilities.GetException(() => ApprovalTestReporter.INSTANCE.Report("Hello", "Hello2"));
            Assert.AreEqual(@"The string are not equal
               ↓ (pos 5)
Expected: Hello
Actual:   Hello2
               ↑ (pos 5)", e.Message);
        }
    }
}