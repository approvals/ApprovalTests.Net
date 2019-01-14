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

        [Test]
        public void TestApprovalTestReporterIgnoreEndLines()
        {
            ApprovalTestReporter.INSTANCE.ShouldIgnoreLineEndings = true;
            Assert.DoesNotThrow(() =>ApprovalTestReporter.INSTANCE.Report("Hello\nHello", "Hello\r\nHello"));
        }

        [Test]
        public void TestApprovalTestReporterIgnoreEndLinesAndFailAfter()
        {
            ApprovalTestReporter.INSTANCE.ShouldIgnoreLineEndings = true;
            var e = ExceptionUtilities.GetException(() => ApprovalTestReporter.INSTANCE.Report("Hello\nHello", "Hello\r\nHello2"));
            Assert.AreEqual(@"The string are not equal
                      ↓ (pos 11)
Expected: Hello\nHello
Actual:   Hello\r\nHello2
                        ↑ (pos 12)", e.Message);
        }
    }
}