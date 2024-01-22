using ApprovalTests.Combinations;

[TestFixture]
[UseReporter(typeof(DiffReporter))]
public class LockDownTests
{
    public string Echo(params int[] i) =>
        i.ToReadableString();

    [Test]
    public void TestLockDown()
    {
        int[] n = [1, 2];
        CombinationApprovals.VerifyAllCombinations((a, b, c, d, e, f, g, h, i) => Echo(a, b, c, d, e, f, g, h, i), n, n, n, n, n, n, n, n, n);
    }

    [Test]
    public void TestLockDown8()
    {
        int[] n = [1, 2];
        CombinationApprovals.VerifyAllCombinations((a, b, c, d, e, f, g, h) => Echo(a, b, c, d, e, f, g, h), n, n, n, n, n, n, n, n);
    }

    [Test]
    public void TestLockDown2()
    {
        int[] n = [1, 2];
        CombinationApprovals.VerifyAllCombinations((a, b) => Echo(a, b), n, n);
    }

    [Test]
    [UseReporter(typeof(MachineSpecificReporter))]
    public void TestExceptions()
    {
        using (ApprovalResults.UniqueForOs())
        {
            int[] n = [0, 2];
            CombinationApprovals.VerifyAllCombinations((a, b) => a / b, n, n);
        }
    }
}