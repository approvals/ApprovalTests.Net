using DiffEngine;

namespace ApprovalTests.Reporters.Windows
{
    public class VisualStudioReporter : DiffToolReporter
    {
        public static readonly VisualStudioReporter INSTANCE = new VisualStudioReporter();

        public VisualStudioReporter()
            : base(DiffTool.VisualStudio)
        {
        }
    }
}