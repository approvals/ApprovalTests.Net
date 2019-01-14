using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using System;
using System.IO;
using System.Reflection;
using ApprovalTests.Asserts;

namespace ApprovalTests.Reporters
{
    public class AssertReporter : IEnvironmentAwareReporter
    {
        protected readonly string areEqual;
        private readonly string assertClass;
        private readonly string frameworkAttribute;

        public AssertReporter(string assertClass, string areEqual, string frameworkAttribute)
        {
            this.assertClass = assertClass;
            this.areEqual = areEqual;
            this.frameworkAttribute = frameworkAttribute;
        }

        public virtual void Report(string approved, string received)
        {
            AssertFileContents(approved, received);
        }

        public virtual bool IsWorkingInThisEnvironment(string forFile)
        {
            return GenericDiffReporter.IsTextFile(forFile) && IsFrameworkUsed();
        }

        public bool IsFrameworkUsed()
        {
            return AttributeStackTraceParser.GetFirstFrameForAttribute(Approvals.CurrentCaller, frameworkAttribute) !=
                   null;
        }

        public void AssertFileContents(string approved, string received)
        {
            var a = File.Exists(approved) ? File.ReadAllText(approved) : "";
            var r = File.ReadAllText(received);
            QuietReporter.DisplayCommandLineApproval(approved, received);

            AssertEqual(a, r);
        }

        public void AssertEqual(string approvedContent, string receivedContent)
        {
            try
            {
                var type = Type.GetType(assertClass);
                var parameters = new[] { approvedContent, receivedContent };
                InvokeEqualsMethod(type, parameters);
            }
            catch (NullReferenceException)
            {
                StringAssert.Equal(approvedContent, receivedContent);
            }
            catch (TargetInvocationException e)
            {
                throw e.GetBaseException();
            }
        }

        protected virtual void InvokeEqualsMethod(Type type, string[] parameters)
        {
            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
            type.InvokeMember(areEqual, bindingFlags, null, null, parameters);
        }
    }
}