namespace ApprovalTests.Namers.UnitTestFrameworks
{
    using ApprovalTests.Namers.StackTraceParsers;

    public class XUnit2TheoryStackTraceParser : AttributeStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.TheoryAttribute";

        public override string ForTestingFramework => "xUnit2.net";

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}