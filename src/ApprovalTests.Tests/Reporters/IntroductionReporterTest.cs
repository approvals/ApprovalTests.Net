[TestFixture]
public class IntroductionReporterTest
{
    [Test]
    public void TestComment()
    {
        Approvals.Verify(new IntroductionReporter().GetFriendlyWelcomeMessage());
    }
}