using ApprovalTests.StackTraceParsers;
using System.Linq;

namespace ApprovalTests.Reporters
{
    public class XUnitReporter : AssertReporter
    {
        public readonly static XUnitReporter INSTANCE = new XUnitReporter();

        public XUnitReporter()
            : base("Xunit.Assert, xunit", "Equal", XUnitStackTraceParser.Attribute)
        {
        }

        protected override void InvokeEqualsMethod(System.Type type, string[] parameters)
        {
            var method = type.GetMethods().Where(m => m.Name == areEqual && m.IsGenericMethod && m.GetParameters().Count() == 2)
                .First();
            method.MakeGenericMethod(typeof(string)).Invoke(null, parameters);
        }
    }
}