using ApprovalTests.Asserts;
using ApprovalTests.Core;

namespace ApprovalTests.Reporters
{
    /// <summary>
    /// Reporter that work for string assertion without the need of an assert framework
    /// </summary>
    public class ApprovalTestReporter : IEnvironmentAwareReporter
    {
        public static readonly ApprovalTestReporter INSTANCE = new ApprovalTestReporter();

        public void Report(string approved, string received)
        {
            StringAssert.Equal(approved, received, false, ShouldIgnoreLineEndings);
        }

        public bool ShouldIgnoreLineEndings { get; set; }

        public bool IsWorkingInThisEnvironment(string forFile)
        {
            return GenericDiffReporter.IsTextFile(forFile);
        }
    }
}