using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Utilities;
using ApprovalUtilities.Persistence;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Utilities;
using Xunit;

namespace ApprovalUtilities.Tests;

[UseReporter(typeof(DiffReporter))]
public class LoggerTest
{
    [Fact]
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
        logText = logText.ScrubStackTrace();
        Approvals.Verify(logText);
    }

    [Fact]
    public void TestShowMarker()
    {
        var log = Logger.LogToStringBuilder();
        Logger.Show(markerIn: false);
        using(Logger.MarkEntryPoints()){}
        Assert.Equal("", log.ToString());
    }

    [Fact]
    public void TestShowEvents()
    {
        var log = Logger.LogToStringBuilder();
        Logger.Show(events: false);
        Logger.Event("ignored event");

        Assert.Equal("", log.ToString());
    }

    [Fact]
    public void TestSql()
    {
        var log = Logger.LogToStringBuilder();
        Logger.Show(sql: false);
        Logger.Sql("ignored event");

        Assert.Equal("", log.ToString());
    }

    [Fact]
    public void TestShowVariables()
    {
        var log = Logger.LogToStringBuilder();
        Logger.Show(variables: false);
        Logger.Variable("name", "Llewellyn");

        Assert.Equal("", log.ToString());
    }

    [Fact]
    public void TestTimes()
    {
        CultureUtilities.ForceCulture();
        var log = Logger.LogToStringBuilder();
        Logger.UseTimer(new MockTimer());
        Logger.Show();
        Logger.Variable("name", "Llewellyn");
        Logger.Variable("name", "Llewellyn");
        Logger.Variable("name", "Llewellyn");

        Approvals.Verify(log.ToString());
    }
}

public class MockTimer : ILoader<DateTime>
{
    int ticks;

    public DateTime Load()
    {
        ticks += 10;
        ticks %= 999;
        return new DateTime(2011, 5, 6, 10, 30, 0, ticks);
    }
}