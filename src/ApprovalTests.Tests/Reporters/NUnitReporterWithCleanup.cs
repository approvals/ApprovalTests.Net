public class NUnitReporterWithCleanup : NUnit4Reporter
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