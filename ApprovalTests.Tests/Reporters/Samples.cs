using ApprovalTests.Reporters;

namespace ApprovalTests.Tests.Reporters.Sample1
{
    // begin-snippet: custom_reporter
    internal class CustomReporter : GenericDiffReporter
    {
        // Allows performance optimizations
        public static readonly CustomReporter INSTANCE = new CustomReporter();

        public CustomReporter() :
            base(@"c:\path\to\application.exe")
        {
        }

    }

    // end-snippet
}

namespace ApprovalTests.Tests.Reporters.Sample2
{
    // begin-snippet: custom_reporter_robust
    public class CustomReporter : GenericDiffReporter
    {
        // Allows performance optimizations
        public static readonly CustomReporter INSTANCE = new CustomReporter();
        // begin-snippet: custom_reporter_diff_info
        public CustomReporter() :
            base(
                new DiffInfo(
                    @"c:\path\to\application.exe",
                    "--nosplash {0} {1}",
                    DiffPrograms.TEXT_AND_IMAGE))
        {
        }
        // end-snippet
    }
}