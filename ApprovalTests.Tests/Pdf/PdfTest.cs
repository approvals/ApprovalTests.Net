using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace ApprovalTests.Tests.Pdf
{
	[TestFixture]
	
	public class PdfTest
	{
		[Test]
		public void TestNotFound()
		{
			var stream = new MemoryStream("abcdefg".Select(x => (byte) x).ToArray());
			long find = PdfScrubber.Find("zoo", stream);
			Assert.AreEqual(-1, find);
		}

		[Test]
		public void TestFound()
		{
			var stream = new MemoryStream("abcdefg".Select(x => (byte)x).ToArray());
			long find = PdfScrubber.Find("cde", stream);
			Assert.AreEqual(2, find);
		}
		
		[Test]
		public void TestPartial()
		{
			var stream = new MemoryStream("abcdefg".Select(x => (byte)x).ToArray());
			long find = PdfScrubber.Find("cdf", stream);
			Assert.AreEqual(-1, find);
		}
	}
}
