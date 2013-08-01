namespace ApprovalTests.Xunit.Namer
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using ApprovalTests.Namers;
    using ApprovalTests.Reporters;
    using ApprovalTests.StackTraceParsers;

    using global::Xunit;

    public class XunitStackTraceNamerTest
    {
        //[Fact]
        [UseReporter(typeof(FileLauncherReporter))]
        public void ShowMeCallers()
        {
            Approvals.SetCaller();

            Approvals.VerifyAll(Approvals.CurrentCaller.NonLambdaCallers, "");
        }

        [Fact]
        public void TestApprovalName()
        {
            var name = new UnitTestFrameworkNamer().Name;
            Assert.Equal("XunitStackTraceNamerTest.TestApprovalName", name);
        }

        [Fact]
        public void TestApprovalNamerFailureMessage()
        {
            var parser = new StackTraceParser();
            var exception = Assert.Throws<Exception>(() => parser.Parse(new StackTrace(6)));

            Approvals.Verify(exception.Message);
        }

        [Fact]
        public async Task AsyncTestApprovalName()
        {
            var name = new UnitTestFrameworkNamer().Name;
            var path = new UnitTestFrameworkNamer().SourcePath;

            await AnAsyncMethod();

            Assert.Equal("XunitStackTraceNamerTest.AsyncTestApprovalName", name);
            Assert.True(File.Exists(path + "\\XunitStackTraceNamerTest.cs"));
        }

        [Fact]
        public async Task FullAsyncTest()
        {
            await AnAsyncMethod();

            Approvals.Verify("Async");
        }

        private static Task AnAsyncMethod()
        {
            return TaskEx.FromResult(default(object));
        }
    }
}