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
			XmlApprovals.VerifyOrderedXml("<xml b=\"123\" a=\"456\"><hello x=\"y\"/><start>hi</start></xml>");
	    } 
        
        [Test]
	    public void TestOrderXmlWithDeepAttributes()
	    {
            XmlApprovals.VerifyOrderedXml("<xml b=\"1\" a=\"1\"><branch1 b=\"1\" a=\"1\"/><branch2 b=\"1\" a=\"1\">hi</branch2></xml>");
	    }
	}
}