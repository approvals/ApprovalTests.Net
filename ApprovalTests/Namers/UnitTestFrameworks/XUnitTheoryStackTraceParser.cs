using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers
{
    public class XUnitTheoryStackTraceParser : AttributeStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.Extensions.TheoryAttribute";

        public override string ForTestingFramework => "xUnit.extensions";

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}