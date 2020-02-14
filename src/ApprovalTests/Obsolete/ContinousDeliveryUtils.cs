namespace ApprovalTests.Reporters.ContinuousIntegration
{
    [ObsoleteEx(
        RemoveInVersion = "5.0",
        ReplacementTypeOrMember = nameof(ContinuousDeliveryUtils))]
    public class ContinousDeliveryUtils
    {
        public static void ReportOnServer(string approved, string received)
        {
            ContinuousDeliveryUtils.ReportOnServer(approved, received);
        }
    }
}