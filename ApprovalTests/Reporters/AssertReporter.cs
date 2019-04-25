using ApprovalTests.Core;
using ApprovalTests.Namers.StackTraceParsers;
using System;
using System.IO;
using System.Reflection;

namespace ApprovalTests.Reporters
{
    public class AssertReporter : IEnvironmentAwareReporter
    {
        protected readonly string areEqual;
        private readonly string assertClass;
        private readonly string[] assertSearchAssemblies;
        private readonly string frameworkAttribute;

        public AssertReporter(string assertClass, string areEqual, string frameworkAttribute)
        {
            this.assertClass = assertClass;
            this.areEqual = areEqual;
            this.frameworkAttribute = frameworkAttribute;
        }

        public AssertReporter(string assertClass, string areEqual, string frameworkAttribute, params string[] assertSearchAssemblies)
        {
            this.assertClass = assertClass;
            this.assertSearchAssemblies = assertSearchAssemblies;
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
                var type = FindAssertionType();
                var parameters = new[] { approvedContent, receivedContent };
                InvokeEqualsMethod(type, parameters);
            }
            catch (TargetInvocationException e)
            {
                throw e.GetBaseException();
            }
        }

        private Type FindAssertionType()
        {
            // no search assemblies given, assume assembly given in as assertClass
            if (assertSearchAssemblies == null)
            {
                var found = Type.GetType(assertClass);
                if (found != null) return found;

                throw new ArgumentException($"Found no matching types in class [{assertClass}]");
            }

            foreach (var searchAssembly in assertSearchAssemblies)
            {
                var found = Type.GetType($"{assertClass}, {searchAssembly}");
                if (found != null) return found;
            }
              
            throw new ArgumentException($"Found no matching types in class [{assertClass}] using " +
                                        $"searchAssemblies [{string.Join(",", assertSearchAssemblies)}]");
        }

        protected virtual void InvokeEqualsMethod(Type type, string[] parameters)
        {
            var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
            type.InvokeMember(areEqual, bindingFlags, null, null, parameters);
        }
    }
}