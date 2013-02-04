using System.Text;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests
{
	[TestFixture]
	[UseReporter(typeof(TortoiseDiffReporter),typeof(ClipboardReporter))]
	public class StringEncodingTest
	{
		[Test]
		public void TestUtf8()
		{
			var text = "UƬf8";
			Approvals.Verify(text);
		}	 
		[Test]
		public void TestAscii()
		{
			var text = "ascii";
			Approvals.Verify(text);
		}	 
    [Test]
		public void TestUnicode()
		{
			string text = UnicodeEncoding.Default.GetString(new byte[] { 101, 235, 110, 116 });
			Approvals.Verify(text);
		}
		
		[Test]
		public void TestDetectUtf8ByteOrderMark()
		{
			string with = PathUtilities.GetAdjacentFile("FileWithUtf8ByteOrderMark.txt");
			string without = PathUtilities.GetAdjacentFile("FileWithoutUtf8ByteOrderMark.txt");
			Assert.IsTrue(ApprovalTextWriter.IsUft8ByteOrderMarkPresent(with));
			Assert.IsFalse(ApprovalTextWriter.IsUft8ByteOrderMarkPresent(without));
		}
	}
}