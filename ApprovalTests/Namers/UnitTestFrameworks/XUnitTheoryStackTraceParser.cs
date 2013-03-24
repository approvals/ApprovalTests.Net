namespace ApprovalTests.StackTraceParsers
{
    public class XUnitTheoryStackTraceParser : XUnitStackTraceParser
    {
        public const string TheoryAttribute = "Xunit.Extensions.TheoryAttribute";

        protected override string GetAttributeType()
        {
            return TheoryAttribute;
        }
    }
}