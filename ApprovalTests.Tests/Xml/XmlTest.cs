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
	}
}