using System;
using ApprovalTests.Asserts;

namespace ApprovalTests.Reporters
{
    using System.Linq;

    using StackTraceParsers;
    /// <summary>
    /// Reporter to use when using Xunit >=2
    /// </summary>
    public class XUnit2Reporter : AssertReporter
    {
        public readonly static XUnit2Reporter INSTANCE = new XUnit2Reporter();
        private static readonly Lazy<bool> isXunit2 = new Lazy<bool>(IsXunit2);
        public bool ShouldIgnoreLineEndings { get; set; }

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

        protected override void InvokeEqualsMethod(Type type, string[] parameters)
        {
            var xunitAssertMethods = type.GetMethods();
            var method = xunitAssertMethods.First(m => m.Name == areEqual && m.GetParameters().Count() == 5);
            if (method != null)
            {
                var ignoreEndLineParameters = new object[]
                {
                    parameters[0],
                    parameters[1],
                    false, //ignoreCase
                    ShouldIgnoreLineEndings, //ignoreLineEndingDifferences
                    false //ignoreWhiteSpaceDifferences
                };
                method.Invoke(null, ignoreEndLineParameters.ToArray());
                return;
            }

            method = xunitAssertMethods.First(m => m.Name == areEqual && m.GetParameters().Count() == 2);
            if (method != null)
            {
                method.Invoke(null, parameters);
            }

            StringAssert.Equal(parameters[0], parameters[1], false, ShouldIgnoreLineEndings);
        }
    }
}