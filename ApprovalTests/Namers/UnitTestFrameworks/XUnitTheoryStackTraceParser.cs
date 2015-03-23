using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers
{
    public class XUnitTheoryStackTraceParser : AttributeStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.Extensions.TheoryAttribute";

        public override string ForTestingFramework
        {
            get { return "xUnit.extensions"; }
        }

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}