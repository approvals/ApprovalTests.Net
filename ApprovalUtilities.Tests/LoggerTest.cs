using System;
using System.Globalization;
using System.Threading;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApprovalUtilities.Tests
{
	[TestClass]
	[UseReporter(typeof(DiffReporter))]
	public class LoggerTest
	{
		[TestMethod]
		public void TestMainPath()
		{
			var log = Logger.LogToStringBuilder();
			Logger.MarkerIn();
			Logger.Event("Starting");
			var name = "Llewellyn";
			Logger.Variable("name", name);
			Logger.Message("I Got here");
			Logger.Sql("Select * From table_name");
			try
			{
				throw new Exception(" Problem");
			}
			catch (Exception e)
			{
				Logger.Warning(e);
			}
			Logger.MarkerOut();
			Approvals.Verify(log.ScrubPath(PathUtilities.GetDirectoryForCaller()));
		}
		[TestMethod]
		public void TestShowMarker()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(markerIn: false);
			Logger.MarkerIn();

			Logger.MarkerOut();
			Assert.AreEqual("", log.ToString());
		}
		[TestMethod]
		public void TestShowEvents()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(events: false);
			Logger.Event("ignored event");

			Assert.AreEqual("", log.ToString());
		}
		[TestMethod]
		public void TestSql()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(sql: false);
			Logger.Sql("ignored event");

			Assert.AreEqual("", log.ToString());
		}
		[TestMethod]
		public void TestShowVariables()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(variables: false);
			Logger.Variable("name", "Llewellyn");

			Assert.AreEqual("", log.ToString());
		}
		[TestMethod]
		public void TestTimes()
		{
            ApprovalUtilities.Culture.CultureUtilities.ForceCulture();
			var log = Logger.LogToStringBuilder();
			Logger.UseTimer(new MockTimer());
			Logger.Show(timestamp: true, timeDifference: true);
			Logger.Variable("name", "Llewellyn");
			Logger.Variable("name", "Llewellyn");
			Logger.Variable("name", "Llewellyn");

			Approvals.Verify(log.ToString());
		}
	}

	public class MockTimer : ILoader<DateTime>
	{
		private int ticks;

		public DateTime Load()
		{
			ticks += 10;
			return new DateTime(2011, 5, 6, 10, 30, 0, ticks);
		}
	}
}