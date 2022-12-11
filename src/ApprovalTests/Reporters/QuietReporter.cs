using System.Diagnostics;
using System.Runtime.InteropServices;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters;

public class QuietReporter : IEnvironmentAwareReporter
{
    public static readonly QuietReporter INSTANCE = new();

    public void Report(string approved, string received) =>
        DisplayCommandLineApproval(approved, received);

    public static void DisplayCommandLineApproval(string approved, string received)
    {
        var message = GetCommandLineForApproval(approved, received);
        Debug.WriteLine(message);
        Console.WriteLine(message);
    }

    public static string GetCommandLineForApproval(string approved, string received)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return $"cmd /c move /Y \"{received}\" \"{approved}\"";
        }

        return $"mv -f \"{received}\" \"{approved}\"";
    }

    public bool IsWorkingInThisEnvironment(string forFile) => true;
}