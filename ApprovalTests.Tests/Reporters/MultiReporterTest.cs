using System;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
	[TestFixture]
	public class MultiReporterTest
	{
		[Test]
		public void TestMultiReporter()
		{
			var a = new RecordingReporter();
			var b = new RecordingReporter();
			var multi = new MultiReporter(a, b);
			multi.Report("a", "r");
			Assert.AreEqual("a,r", a.CalledWith);
			Assert.AreEqual("a,r", b.CalledWith);
		}
		[Test]
		public void TestCallAFterException()
		{
			var a = new NUnitReporter();
			var b = new RecordingReporter();
			var multi = new MultiReporter(a, b);
			var exception = ExceptionUtilities.GetException(() => multi.Report("a", "r"));
			Assert.AreEqual("a,r", b.CalledWith);
			Assert.IsInstanceOf<Exception>(exception);
		}
	}
}