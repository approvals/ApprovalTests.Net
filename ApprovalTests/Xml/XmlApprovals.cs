using ApprovalUtilities.Xml;

namespace ApprovalTests.Xml
{
	public class XmlApprovals
	{
		public static void VerifyXml(string xml)
		{
			VerifyText(xml, "xml", true);
		}

		public static void VerifyText(string text, string fileExtensionWithoutDot, bool safely)
		{
			text = XmlUtils.FormatXml(text, safe: safely);
			ApprovalTests.Approvals.Verify(new ApprovalTextWriter(text, fileExtensionWithoutDot));
		}


		/// <summary>
		/// 	Throws exception if Xml is incorrectly formatted
		/// </summary>
		public static void VerifyXmlStrict(string xml)
		{
			VerifyText(xml, "xml", false);
		}
	}
}