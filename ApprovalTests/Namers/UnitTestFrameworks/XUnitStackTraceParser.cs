using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers
{
    public class XUnitStackTraceParser : AttributeStackTraceParser
    {
        public const string Attribute = "Xunit.FactAttribute";

        public override string ForTestingFramework
        {
            get { return "xUnit.net"; }
        }

        protected override string GetAttributeType()
        {
            return Attribute;
        }
    }
}