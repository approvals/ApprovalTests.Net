using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ApprovalTests.Namers;
using ApprovalTests.Namers.StackTraceParsers;
using Xunit;

namespace ApprovalTests.Xunit.Namer
{
	public class XunitStackTraceNamerTest
	{
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
		//[Fact]
		public async Task ProperFullAsyncTest()
		{
			await Task.Delay(10);
			// This is the stack trace, and needs to do MAGIC!
			//   at ApprovalTests.Xunit.Namer.XunitStackTraceNamerTest.<ProperFullAsyncTest>d__c.MoveNext()
			Approvals.Verify("Async with Delay");
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

		private static Task AnAsyncMethod()
		{
			return Task.FromResult(default(object));
		}

		private void AssertEquals<T>(string typeName)
		{
			Type instance = Type.GetType(typeName, false);
			Assert.Equal(typeof (T), instance);
		}
	}
}