using ApprovalTests.Xml;

[TestFixture]
public class XmlTest
{
    [Test]
    public void TestXml()
    {
        XmlApprovals.VerifyXml("<xml><hello/><start>hi</start></xml>");
    }

    [Test]
    public void TestOrderedXmlWithAttributes()
    {
        XmlApprovals.VerifyOrderedXml("<xml b=\"123\" a=\"456\"><hello x=\"y\"/><start>hi</start></xml>");
    }

    [Test]
    public void TestOrderedXmlWithDeepAttributes()
    {
        XmlApprovals.VerifyOrderedXml("<xml b=\"1\" a=\"1\"><branch1 b=\"1\" a=\"1\"/><branch2 b=\"1\" a=\"1\">hi</branch2></xml>");
    }
}