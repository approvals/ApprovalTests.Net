[TestFixture]
public class EventApprovalsTest
{
    [Test]
    public void MulticastPoco()
    {
        var testingPoco = new TestingEventPoco();
        testingPoco.MyEvent += TestingListener.AnotherStandardCallback;
        testingPoco.MyEvent += TestingListener.StandardCallback;
        EventApprovals.VerifyEvents(testingPoco);
    }

    [Test]
    public void UnicastPoco()
    {
        var testingPoco = new TestingEventPoco();
        testingPoco.MyEvent += TestingListener.StandardCallback;
        testingPoco.PropertyChanged += TestingListener.PropertyChangedCallback;
        EventApprovals.VerifyEvents(testingPoco);
    }
}