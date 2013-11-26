using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ApprovalTests.StackTraceParsers;
using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalTests.Xunit.Namer
{
	public class XunitStackTraceNamerTest
	{
		[Fact]
		public void TestMightyMoose()
		{
			Approvals.SetCaller();
			var m = new MightyMooseAutoTestReporter();
			var b = m.IsWorkingInThisEnvironment("a.txt");
			var f = PathUtilities.GetAdjacentFile("mightymooseresult.txt");
			File.WriteAllText(f, "{0}, MightyMoose was running = {1}".FormatWith(DateTime.Now, b));
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

		[Fact(Skip = "This is Hard")]
		public async Task ProperFullAsyncTest()
		{
			await Task.Delay(10);

			Approvals.Verify("Async with Delay");
		}

		private static Task AnAsyncMethod()
		{
			return Task.FromResult(default(object));
		}
	}
}