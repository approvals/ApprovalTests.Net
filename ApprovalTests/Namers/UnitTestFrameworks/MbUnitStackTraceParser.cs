using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers
{
	public class MbUnitStackTraceParser : AttributeStackTraceParser
	{
		public const string Attribute = "MbUnit.Framework.TestAttribute";

		public override string ForTestingFramework
		{
			get { return "MbUnit"; }
		}

		protected override string GetAttributeType()
		{
			return Attribute;
		}
	}
}