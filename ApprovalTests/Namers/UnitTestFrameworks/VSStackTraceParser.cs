using ApprovalTests.Namers.StackTraceParsers;

namespace ApprovalTests.StackTraceParsers
{
	public class VSStackTraceParser : AttributeStackTraceParser
	{
		public const string Attribute = "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute";

		public override string ForTestingFramework
		{
			get { return "MsTest"; }
		}

		protected override string GetAttributeType()
		{
			return Attribute;
		}
	}
}