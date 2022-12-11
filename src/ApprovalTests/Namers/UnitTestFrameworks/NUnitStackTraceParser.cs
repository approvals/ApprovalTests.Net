using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers;

public class NUnitStackTraceParser : AttributeStackTraceParser
{
    public const string Attribute = "NUnit.Framework.TestAttribute";

    public override string ForTestingFramework => "NUnit";

    protected override string GetAttributeType() => Attribute;
}