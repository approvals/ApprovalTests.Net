namespace ApprovalTests.Tests.Events
{
    using ApprovalTests.Events;
    using NUnit.Framework;

    [TestFixture]
    public class EventApprovalsTest
    {
        [Test]
        public void MulticastPoco()
        {
            var testingPoco = new TestingPoco();
            testingPoco.MyEvent += TestingListener.AnotherStandardCallback;
            testingPoco.MyEvent += TestingListener.StandardCallback;
            EventApprovals.VerifyEvents(testingPoco);
        }

        [Test]
        public void UnicastPoco()
        {
            var testingPoco = new TestingPoco();
            testingPoco.MyEvent += TestingListener.StandardCallback;
            testingPoco.PropertyChanged += TestingListener.PropertyChangedCallback;
            EventApprovals.VerifyEvents(testingPoco);
        }
    }
}