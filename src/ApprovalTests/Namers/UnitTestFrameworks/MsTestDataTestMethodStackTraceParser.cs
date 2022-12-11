using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers;

public class MsTestDataTestMethodStackTraceParser : AttributeStackTraceParser
{
    public const string Attribute = "Microsoft.VisualStudio.TestTools.UnitTesting.DataTestMethodAttribute";

    public override string ForTestingFramework => "MsTest-DataTest";

    protected override string GetAttributeType() => Attribute;
}