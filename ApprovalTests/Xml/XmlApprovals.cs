using System;
using System.Linq;
using System.Xml.Linq;
using ApprovalTests.Scrubber;
using ApprovalTests.Writers;
using ApprovalUtilities.SimpleLogger;
using ApprovalUtilities.Xml;

namespace ApprovalTests.Xml
{
	public class XmlApprovals
	{
		public static void VerifyXml(string xml)
		{
			VerifyText(xml, "xml", true, ScrubberUtils.NO_SCRUBBER);
		}

		/// <summary>
		/// 	Throws exception if Xml is incorrectly formatted
		/// </summary>
		public static void VerifyXmlStrict(string xml)
		{
			VerifyText(xml, "xml", false, ScrubberUtils.NO_SCRUBBER);
		}

		public static void VerifyText(string text, string fileExtensionWithoutDot, bool safely, Func<string, string> scrubber)
		{
			text = XmlUtils.FormatXml(scrubber.Invoke(text), safe: safely);
			ApprovalTests.Approvals.Verify(WriterFactory.CreateTextWriter(text, fileExtensionWithoutDot));
		}

		public static void VerifyOrderedXml(string text)
		{
			VerifyOrderedXml(text, ScrubberUtils.NO_SCRUBBER);
		}

		public static void VerifyOrderedXml(string text, Func<string, string> scrubber)
		{
			string xml = scrubber.Invoke(text);

		    var xElement = XElement.Parse(xml);
		    var orderedNodes = xElement.Attributes().OrderBy(e => e.ToString()).ToArray();
            xElement.RemoveAttributes();
		    foreach (var attribute in orderedNodes)
		    {
		        attribute.Log("xml", n => n.ToString());
		        xElement.SetAttributeValue(attribute.Name, attribute.Value);
		    }
		    text = xElement.ToString();

		    ApprovalTests.Approvals.Verify(WriterFactory.CreateTextWriter(text, "xml"));
		}
	}
}