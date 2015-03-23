namespace ApprovalTests.Xunit2.Reporters
{
    using ApprovalTests.Reporters;

    using ApprovalUtilities.Utilities;

    using global::Xunit;

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
    }
}