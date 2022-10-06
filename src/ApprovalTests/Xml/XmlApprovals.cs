namespace ApprovalTests.Xml;

public static class XmlApprovals
{
    public static void VerifyXml(string xml, Func<string, string> scrubber = null)
    {
        VerifyText(xml, "xml", true, scrubber);
    }

    /// <summary>
    /// 	Throws exception if Xml is incorrectly formatted
    /// </summary>
    public static void VerifyText(string text, string fileExtensionWithoutDot = "xml", bool safely = false, Func<string, string> scrubber = null)
    {
        if (scrubber == null)
        {
            scrubber = ScrubberUtils.NO_SCRUBBER;
        }

        text = XmlUtils.FormatXml(scrubber.Invoke(text), safe: safely);
        Approvals.Verify(WriterFactory.CreateTextWriter(text, fileExtensionWithoutDot));
    }

    public static void VerifyOrderedXml(string text, Func<string, string> scrubber = null)
    {
        if (scrubber == null)
        {
            scrubber = ScrubberUtils.NO_SCRUBBER;
        }

        text = XmlUtils.FormatXmlWithOrderedAttributes(scrubber.Invoke(text));

        Approvals.Verify(WriterFactory.CreateTextWriter(text, "xml"));
    }
}