#pragma warning disable CS0169  

using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ApprovalTests;
using ApprovalTests.Tests.Events;
using ApprovalUtilities.Reflection;
using Xunit;
using ApprovalTests.Reporters;

namespace ApprovalUtilities.Tests.Reflection
{
    [UseReporter(typeof(DiffReporter))]
    public class ReflectionUtilitiesTest
    {
        [Fact]
        public void ControlWithLocalAndBaseKeys()
        {
            var checkBox = new CheckBox();

            checkBox.CheckedChanged += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.AnotherStandardCallback;
            checkBox.Click += TestingListener.StandardCallback;

            Approvals.VerifyAll(checkBox.GetEventHandlerListEvents(), string.Empty);
        }

        [Fact]
        public void ControlWithEmptyHandlers()
        {
            var checkBox = new CheckBox();

            Assert.Empty(checkBox.GetEventHandlerListEvents());
        }
            
        public class TargetPoco : TargetPocoBase
        {
            public string PublicInstanceField;
            string PrivateInstanceField;
            public static string PublicStaticField;
            string PrivateStaticField;
            public string PublicInstanceProperty{ get; set; }
            private string PrivateInstanceProperty{ get; set; }
            public static string PublicStaticProperty{ get; set; }
            private string PrivateStaticProperty { get; set; }
        }
        public class TargetPocoBase
        {
            public string BasePublicInstanceField;
            string BasePrivateInstanceField;
            public static string BasePublicStaticField;
            string BasePrivateStaticField;
            public string BasePublicInstanceProperty{ get; set; }
            private string BasePrivateInstanceProperty{ get; set; }
            public static string BasePublicStaticProperty{ get; set; }
            private string BasePrivateStaticProperty{ get; set; }
        }
        [Fact]
        public void GetControlNonPublicStaticFields()
        {
            Approvals.VerifyAll(
                new TargetPoco().NonPublicStaticFields(false).OrderBy(x=>x.Name),
                string.Empty);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GetInheritedNonPublicStaticFields()
        {
            Approvals.VerifyAll(new TargetPoco().NonPublicStaticFields(true),
                string.Empty);
        }

        [Fact]
        public void GetNonPublicInstanceFields()
        {
            Approvals.VerifyAll(
                new TestingEventPoco().GetInstanceFields(),
                string.Empty);
        }

        [Fact]
        public void GetNonPublicInstanceFieldsAssignableTo()
        {
            Func<FieldInfo, bool> selector = fi => typeof(MulticastDelegate).IsAssignableFrom(fi.FieldType);

            Approvals.VerifyAll(
                new TestingEventPoco().GetInstanceFields(selector),
                string.Empty);
        }

        [Fact]
        public void GetNonPublicInstanceFieldsNamed()
        {
            Func<FieldInfo, bool> selector = fi => string.Compare(fi.Name, "NonEventField", false) == 0;

            Approvals.VerifyAll(
                new TestingEventPoco().GetInstanceFields(selector),
                string.Empty);
        }

        [Fact]
        public void GetNonPublicInstanceProperties()
        {
            Approvals.VerifyAll(
                new TargetPoco().NonPublicInstanceProperties(),
                string.Empty);
        }

        [Fact]
        public void GetNonPublicInstancePropertiesNamed()
        {
            Func<PropertyInfo, bool> selector = pi => string.Compare(pi.Name, "Events", false) == 0;

            Approvals.VerifyAll(
                new CheckBox().NonPublicInstanceProperties(selector),
                string.Empty);
        }

        [Fact]
        public void GetPocoEvents()
        {
            var testingPoco = new TestingEventPoco();

            testingPoco.MyEvent += TestingListener.StandardCallback;
            testingPoco.PropertyChanged += TestingListener.PropertyChangedHandler;

            Approvals.VerifyAll(testingPoco.GetPocoEvents(), string.Empty);
        }

        [Fact]
        public void GetEmptyPocoEvents()
        {
            var testingPoco = new TestingEventPoco();

            Assert.Empty(testingPoco.GetPocoEvents());
        }

        [Fact]
        public void GetInheritedPocoEvents()
        {
            var value = new InheritsTestingEventPoco();

            value.MyEvent += TestingListener.StandardCallback;
            value.PropertyChanged += TestingListener.PropertyChangedHandler;

            Approvals.VerifyAll(value.GetPocoEvents(), string.Empty);
        }

        [Fact]
        public void GetPrivateBaseClassFields()
        {
            Approvals.VerifyAll("Private methods for Class B", ReflectionUtilities.GetAllFields(typeof(B)), "");
        }

        [Fact]
        public void GetLabelForChild()
        {
            var value = new C("a", "b", "c");

            Assert.Equal("B", ReflectionUtilities.GetFieldForChild(value, "b").Name);
            Assert.Equal("D", ReflectionUtilities.GetFieldForChild(value, "c").Name);
        }

        private class C
        {
            private string A;
            private string B;
            public string D;

            public C(string a, string b, string c)
            {
                A = a;
                B = b;
                D = c;
            }
        }

        public class A
        {
            private string Booya = null;
            public string GetBooya()
            {
                return Booya;
            }
        }

        private class B : A
        {
        }
    }
}
#pragma warning restore CS0169