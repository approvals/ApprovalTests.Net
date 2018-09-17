using System;

namespace ApprovalTests.Reporters
{
    using System.Linq;

    using StackTraceParsers;

    public class XUnit2Reporter : AssertReporter
    {
        public readonly static XUnit2Reporter INSTANCE = new XUnit2Reporter();
        private static readonly Lazy<bool> isXunit2 = new Lazy<bool>(IsXunit2);

        private static bool IsXunit2()
        {
            var firstCheck = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Any(a => a.FullName.Contains("xunit.assert"));

            if (!firstCheck)
            {
                var secondCheck = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Any(a => a.FullName.Contains("xunit.core"));

                if (secondCheck)
                {
                    try
                    {
                        var entry = AppDomain.CurrentDomain.Load("xunit.assert");
                    }
                    catch (Exception)
                    {
                        // xunit.assert wasn't found - fail quietly so that next check can continue
                    }
                }
            }

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
            var method = type.GetMethods().First(m => m.Name == areEqual && m.GetParameters().Count() == 2);
            method.Invoke(null, parameters);
        }
    }
}