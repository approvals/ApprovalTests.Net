using System.Linq;
using ApprovalTests.Combinations;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace ApprovalDemos.Data
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	public class CombinationTest
	{
	 
		[Test]
		public void TestMultiplyAndAdd()
		{
			var range = Enumerable.Range(1,10);
			CombinationApprovals.VerifyAllCombinations((a,b,c) => MultiplyAndAdd(a,b,c), range, range,range);
		}

		public double MultiplyAndAdd(int a, int b, int c)
		{
			return a*b + c;
		}
	}

}