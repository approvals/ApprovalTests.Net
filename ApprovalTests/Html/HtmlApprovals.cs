using System;
using ApprovalTests.Scrubber;
using ApprovalTests.Xml;

namespace ApprovalTests.Html
{
	public class HtmlApprovals
	{
		public static void VerifyHtml(string html)
		{
            VerifyHtml(html, ScrubberUtils.NO_SCRUBBER);
		}

        public static void VerifyHtml(string html, Func<string, string> scrubber)
		{
			XmlApprovals.VerifyText(html, "html", true, scrubber);
		}

		/// <summary>
		/// 	Throws exception if Html is incorrectly formatted
		/// </summary>
		public static void VerifyHtmlStrict(string html)
		{
			XmlApprovals.VerifyText(html, "html", false, ScrubberUtils.NO_SCRUBBER);
		}
	}
}