public class NUnitReporterWithCleanup : NUnit3Reporter
{
    public override void Report(string approved, string received)
    {
        try
        {
            base.Report(approved, received);
        }
        finally
        {
            File.Delete(received);
        }
    }
}