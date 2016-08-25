namespace ApprovalTests.Core
{
    public interface IApprovalApprover
    {
        /// <summary>
        ///     Called to verify.
        ///     Should save the received resource and compare.
        /// </summary>
        /// <remarks>
        ///     Note: This is part 1 of a 2 part call : if (Approve()) {Fail()}
        ///     To allow for the reporter to interject and approve in the interm
        /// </remarks>
        /// <returns>  true if Matching, false if not</returns>
        bool Approve();

        /// <summary>
        ///     Called if Approve() returned false and
        ///     the reporter did not Self-Approve via  .ApprovedWhenReported()
        ///     Usually throws an some sort of Exception
        /// </summary>
        /// <remarks>
        ///     Note: This is part 1 of a 2 part call : if (Approve()) {Fail()}
        ///     To allow for the reporter to interject and approve in the interm
        /// </remarks>
        void Fail();

        void ReportFailure(IApprovalFailureReporter reporter);
        void CleanUpAfterSuccess(IApprovalFailureReporter reporter);
    }
}