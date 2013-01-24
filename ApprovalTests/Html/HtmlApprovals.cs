using ApprovalTests.Xml;

namespace ApprovalTests.Html
{
	public class HtmlApprovals
	{
		public static void VerifyHtml(string html)
		{
			XmlApprovals.VerifyText(html, "html", true);
		}

		/// <summary>
		/// 	Throws exception if Html is incorrectly formatted
		/// </summary>
		public static void VerifyHtmlStrict(string html)
		{
			XmlApprovals.VerifyText(html, "html", false);
		}
	}
}