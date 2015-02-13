using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using ApprovalTests.Namers.StackTraceParsers;
using NUnit.Framework;

namespace ApprovalTests.Tests.Namer
{
    [TestFixture]
    public class AsyncNamerTests
    {
        [Test]
        public async void TestSourcePath()
        {
            await Task.Delay(1);
            var stackTraceParser = new StackTraceParser();
            var stackTrace = new StackTrace();

            stackTraceParser.Parse(stackTrace);
            Debug.WriteLine(stackTraceParser.ApprovalName);
        }

        [Test]
        public async void MethodWithDuplicate()
        {
            await Task.Delay(1);
            var stackTraceParser = new StackTraceParser();
            var stackTrace = new StackTrace();

            var exception = Assert.Throws<Exception>(() => stackTraceParser.Parse(stackTrace));
            Assert.IsTrue(exception.Message.Contains("Could Not Detect Test Framework"));
        }

        public async void MethodWithDuplicate(int foo)
        {
            await Task.Delay(1);
        }
    }

}