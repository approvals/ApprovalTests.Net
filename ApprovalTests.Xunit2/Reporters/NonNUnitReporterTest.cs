namespace ApprovalTests.Xunit2.Reporters
{
    using ApprovalTests.Reporters;

    using ApprovalUtilities.Utilities;

    using Xunit;

    public class NonNUnitReporterTest
    {
        [Fact]
        public void TestNunitIsNotWorkingFromXUnit()
        {
            Approvals.SetCaller();
            Assert.False(NUnitReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
        }

        [Fact]
        public void TestXunitIsWorking()
        {
            Approvals.SetCaller();
            Assert.True(XUnitReporter.INSTANCE.IsWorkingInThisEnvironment("default.txt"));
        }

        [Fact]
        public void TestXunitReporterIsWorking()
        {
            var e = ExceptionUtilities.GetException(() => XUnit2Reporter.INSTANCE.AssertEqual("Hello", "Hello2"));
            Approvals.Verify(e.Message);
        }

        [Fact]
        public void TestXunitReporterIgnoreEndLines()
        {
            XUnit2Reporter.INSTANCE.ShouldIgnoreLineEndings = true;
            XUnit2Reporter.INSTANCE.AssertEqual("Hello\nHello", "Hello\r\nHello");
        }

        [Fact]
        public void TestXunitReporterIgnoreEndLinesAndFailAfter()
        {
            XUnit2Reporter.INSTANCE.ShouldIgnoreLineEndings = true;
            var e = ExceptionUtilities.GetException(() => XUnit2Reporter.INSTANCE.AssertEqual("Hello\nHello", "Hello\r\nHello2"));
            Approvals.Verify(e.Message);
        }
    }
}