using ApprovalTests.StatePrinter;
using NUnit.Framework;

namespace ApprovalTests.Tests.StatePrinter
{
	[TestFixture]
	public class StatePrinterTests
	{
		[Test]
		public void TestVerifyCircleReferences()
		{
			var turtle = new Turtle("jim", new Turtle("kasper", new Turtle("llewellyn", null)));
			turtle.On.On.On = turtle;
			StatePrinterApprovals.Verify(turtle);
		}
	}

	public class Turtle
	{
		public string Name { get; set; }
		public Turtle On { get; set; }

		public Turtle(string name, Turtle onTurtle)
		{
			Name = name;
			On = onTurtle;
		}
	}
}