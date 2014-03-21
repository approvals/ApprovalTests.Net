using ApprovalTests.Core.Exceptions;
using ApprovalTests.TheoryTests;
using NUnit.Framework;

namespace ApprovalTests.Tests.Core.Exceptions
{
	[TestFixture]
	public class SerializableExceptionsTest
	{
		[Test]
		public void TestSerializable()
		{
			string r = "recieved";
			string a = "approved";
			Verify(new ApprovalMissingException(r, a));
			Verify(new ApprovalMismatchException(r, a));
			Verify(new ApprovalException(r, a));
		}

		private void Verify(object o)
		{
			SerializableTheory.Verify(o, Assert.AreEqual);
		}
	}
}