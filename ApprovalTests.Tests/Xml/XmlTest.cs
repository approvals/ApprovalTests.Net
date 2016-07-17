using ApprovalTests.Xml;
using NUnit.Framework;

namespace ApprovalTests.Tests.Xml
{
	[TestFixture]
	public class XmlTest
	{
		[Test]
		public static void TestXml()
		{
			XmlApprovals.VerifyXml("<xml><hello/><start>hi</start></xml>");
		}

	    [Test]
	    public void TestXmlWithAttributes()
	    {
			XmlApprovals.VerifyXml("<xml b=\"123\" a=\"456\"><hello/><start>hi</start></xml>");
	    }
	}
}