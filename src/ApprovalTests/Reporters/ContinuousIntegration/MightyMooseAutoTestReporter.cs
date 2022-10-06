namespace ApprovalTests.Reporters.ContinuousIntegration;

public class MightyMooseAutoTestReporter : IEnvironmentAwareReporter
{
    public static readonly MightyMooseAutoTestReporter INSTANCE = new();

    public static bool? IsRunning;

    public void Report(string approved, string received)
    {
        // do nothing
    }

    public bool IsWorkingInThisEnvironment(string forFile)
    {
        if (IsRunning == null)
        {
            IsRunning = ParentProcessUtils.ProcessName.StartsWith("AutoTest.TestRunner");
            if (IsRunning.Value)
            {
                var message = "AutoTest support is being deprecated. It will be removed in V5.";
                Trace.WriteLine(message);
                Console.WriteLine(message);
                Debug.WriteLine(message);
            }
        }

        return IsRunning.Value;
    }
}