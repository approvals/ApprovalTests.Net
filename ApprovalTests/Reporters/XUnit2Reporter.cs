namespace ApprovalTests.Reporters
{
    using System.Linq;

    using ApprovalTests.StackTraceParsers;

    public class XUnit2Reporter : AssertReporter
    {
        public readonly static XUnit2Reporter INSTANCE = new XUnit2Reporter();

        public XUnit2Reporter()
            : base("Xunit.Assert, xunit.assert", "Equal", XUnitStackTraceParser.Attribute)
        {
        }

        protected override void InvokeEqualsMethod(System.Type type, string[] parameters)
        {
            var method = type.GetMethods().First(m => m.Name == this.areEqual && m.GetParameters().Count() == 2);
            method.Invoke(null, parameters);
        }
    }
}