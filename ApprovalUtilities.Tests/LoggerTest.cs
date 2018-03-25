﻿using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Utilities;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalUtilities.Tests
{
	[UseReporter(typeof(DiffReporter))]
	public class LoggerTest
	{
		[Test]
		public void TestMainPath()
		{
			var log = Logger.LogToStringBuilder();
			using (Logger.MarkEntryPoints())
			{


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
			}
			var logText = log.ScrubPath(PathUtilities.GetDirectoryForCaller());
			logText = StackTraceScrubber.ScrubStackTrace(logText);
			Approvals.Verify(logText);
		}
		[Test]
		public void TestShowMarker()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(markerIn: false);
			Logger.MarkerIn();

			Logger.MarkerOut();
			Assert.AreEqual("", log.ToString());
		}
		[Test]
		public void TestShowEvents()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(events: false);
			Logger.Event("ignored event");

			Assert.AreEqual("", log.ToString());
		}
		[Test]
		public void TestSql()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(sql: false);
			Logger.Sql("ignored event");

			Assert.AreEqual("", log.ToString());
		}
		[Test]
		public void TestShowVariables()
		{
			var log = Logger.LogToStringBuilder();
			Logger.Show(variables: false);
			Logger.Variable("name", "Llewellyn");

			Assert.AreEqual("", log.ToString());
		}
		[Test]
		public void TestTimes()
		{
			CultureUtilities.ForceCulture();
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
		    ticks = ticks%999;
			return new DateTime(2011, 5, 6, 10, 30, 0, ticks);
		}
	}
}