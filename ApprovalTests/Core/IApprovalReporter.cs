
namespace ApprovalTests.Core
{
    public interface IApprovalFailureReporter
    {
        void Report(string approved, string received);
        bool ShouldIgnoreLineEndings { set; }

    }
}