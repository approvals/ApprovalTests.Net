namespace ApprovalTests.Namers.UnitTestFrameworks
{
    using ApprovalTests.Namers.StackTraceParsers;

    public class XUnit2TheoryStackTraceParser : AttributeStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.TheoryAttribute";

        public override string ForTestingFramework
        {
            get { return "xUnit2.net"; }
        }

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}