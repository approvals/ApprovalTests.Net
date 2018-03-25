
using System;
using System.Linq;
using System.Reflection;
#if !NETCORE
using System.Windows.Forms;
#endif
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Tests.Events;
using ApprovalUtilities.Reflection;
using NUnit.Framework;
using ApprovalTests.Reporters;

namespace ApprovalUtilities.Tests.Reflection
{
    [UseReporter(typeof(DiffReporter))]
    public class ReflectionUtilitiesTest
    {
#if !NETCORE
        [Test]
        public void ControlWithLocalAndBaseKeys()
        {
            var checkBox = new CheckBox();

            checkBox.CheckedChanged += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.StandardCallback;

            Approvals.VerifyAll(checkBox.GetEventHandlerListEvents(), string.Empty);
        }

        [Test]
        public void ControlWithEmptyHandlers()
        {
            var checkBox = new CheckBox();

            Assert.AreEqual(0, checkBox.GetEventHandlerListEvents().Count());
        }

        [Test]
        public void GetControlNonPublicStaticFields()
        {
            Approvals.VerifyAll(
                new CheckBox().NonPublicStaticFields(false),
                string.Empty);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void GetInheritedNonPublicStaticFields()
        {
            try
            {
                Approvals.VerifyAll("For " + ApprovalResults.GetDotNetVersion(),
                    new CheckBox().NonPublicStaticFields(true),
                    string.Empty);
            }
            catch (Exception)
            {
                //Thread.Sleep(9000);
                throw;
            }
        }
#endif

        [Test]
        public void GetNonPublicInstanceFields()
        {
            Approvals.VerifyAll(
                new TestingPoco().GetInstanceFields(),
                string.Empty);
        }

        [Test]
        public void GetNonPublicInstanceFieldsAssignableTo()
        {
            Func<FieldInfo, bool> selector = fi => typeof(MulticastDelegate).IsAssignableFrom(fi.FieldType);

            Approvals.VerifyAll(
                new TestingPoco().GetInstanceFields(selector),
                string.Empty);
        }

        [Test]
        public void GetNonPublicInstanceFieldsNamed()
        {
            Func<FieldInfo, bool> selector = fi => string.Compare(fi.Name, "NonEventField", false) == 0;

            Approvals.VerifyAll(
                new TestingPoco().GetInstanceFields(selector),
                string.Empty);
        }
#if !NETCORE
        [Test]
        public void GetNonPublicInstanceProperties()
        {
            Approvals.VerifyAll(
                new CheckBox().NonPublicInstanceProperties(),
                string.Empty);
        }

        [Test]
        public void GetNonPublicInstancePropertiesNamed()
        {
            Func<PropertyInfo, bool> selector = pi => string.Compare(pi.Name, "Events", false) == 0;

            Approvals.VerifyAll(
                new CheckBox().NonPublicInstanceProperties(selector),
                string.Empty);
        }
#endif

        [Test]
        public void GetPocoEvents()
        {
            var testingPoco = new TestingPoco();

            testingPoco.MyEvent += TestingListener.StandardCallback;
            testingPoco.PropertyChanged += TestingListener.PropertyChagnedHandler;

            Approvals.VerifyAll(testingPoco.GetPocoEvents(), string.Empty);
        }

        [Test]
        public void GetEmptyPocoEvents()
        {
            var testingPoco = new TestingPoco();

            Assert.AreEqual(0, testingPoco.GetPocoEvents().Count());
        }

        [Test]
        public void GetInheritedPocoEvents()
        {
            var value = new InheritsTestingPoco();

            value.MyEvent += TestingListener.StandardCallback;
            value.PropertyChanged += TestingListener.PropertyChagnedHandler;

            Approvals.VerifyAll(value.GetPocoEvents(), string.Empty);
        }

        [Test]
        public void GetPrivateBaseClassFields()
        {
            Approvals.VerifyAll("Private methods for Class B", ReflectionUtilities.GetAllFields(typeof(B)), "");
        }

        [Test]
        public void GetLabelForChild()
        {
            var value = new C("a", "b", "c");

            Assert.AreEqual("B", ReflectionUtilities.GetFieldForChild(value, "b").Name);
            Assert.AreEqual("D", ReflectionUtilities.GetFieldForChild(value, "c").Name);
        }

        private class C
        {
            private string A;
            private string B;
            public string D;

            public C(string a, string b, string c)
            {
                this.A = a;
                this.B = b;
                this.D = c;
            }
        }

        private class A
        {
            private string Booya;
        }

        private class B : A
        {
        }
    }
}