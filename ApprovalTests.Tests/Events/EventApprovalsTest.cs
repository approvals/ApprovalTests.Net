namespace ApprovalTests.Tests.Events
{
    using System.Windows.Forms;
    using ApprovalTests.Events;
    using NUnit.Framework;
using ApprovalTests.Reporters;

    [TestFixture]
    [UseReporter(typeof(TortoiseDiffReporter))]
    public class EventApprovalsTest
    {
        [Test]
        public void ComponentWithStandardEventHandler()
        {
            var timer = new Timer();
            timer.Tick += TestingListener.StandardCallback;
            timer.Tick += TestingListener.AnotherStandardCallback;
            EventApprovals.VerifyEvents(timer);
        }

        [Test]
        public void ControlWithEverything()
        {
            var testingControl = new TestingControl();
            testingControl.Click += TestingListener.StandardCallback;
            testingControl.Click += TestingListener.AnotherStandardCallback;
            testingControl.MyEvent += TestingListener.StandardCallback;
            testingControl.MyEvent += TestingListener.AnotherStandardCallback;
            testingControl.KeyEvent += TestingListener.StandardCallback;
            testingControl.KeyEvent += TestingListener.AnotherStandardCallback;
            EventApprovals.VerifyEvents(testingControl);
        }

        [Test]
        public void ControlWithLocalAndBaseKeys()
        {
            var checkBox = new CheckBox();
            checkBox.CheckedChanged += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.StandardCallback;
            EventApprovals.VerifyEvents(checkBox);
        }

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