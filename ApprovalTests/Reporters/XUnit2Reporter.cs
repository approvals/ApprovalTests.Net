using System;

namespace ApprovalTests.Reporters
{
    using System.Linq;

    using ApprovalTests.StackTraceParsers;

    public class XUnit2Reporter : AssertReporter
    {
        public readonly static XUnit2Reporter INSTANCE = new XUnit2Reporter();
        private static readonly Lazy<bool> isXunit2 = new Lazy<bool>(IsXunit2);

        private static bool IsXunit2()
        {
            return AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Any(a => a.FullName.Contains("xunit.assert"));
        }

        public XUnit2Reporter()
            : base("Xunit.Assert, xunit.assert", "Equal", XUnitStackTraceParser.Attribute)
        {
        }

        public override bool IsWorkingInThisEnvironment(string forFile)
        {
            return base.IsWorkingInThisEnvironment(forFile) && isXunit2.Value;
        }

        protected override void InvokeEqualsMethod(System.Type type, string[] parameters)
        {
            var method = type.GetMethods().First(m => m.Name == this.areEqual && m.GetParameters().Count() == 2);
            method.Invoke(null, parameters);
        }
    }
}