using System;
using System.Text.RegularExpressions;

namespace ApprovalTests.Scrubber
{
	public static class HtmlScrubbers
	{
		public static string ScrubBrowserLink(string input)
		{
			string regex = "\r\n<!-- Visual Studio Browser Link -->(?s).*<!-- End Browser Link -->\r\n\r\n";
			return new Regex(regex).Replace(input, string.Empty);
		}

		public static string ScrubAspViewstate(string input)
		{
			string AspViewState = "<input type=\"hidden\" name=\"__VIEWSTATE.+/>";
			return Regex.Replace(input, AspViewState, "<!-- aspviewstate -->");
		}

		public static Func<string, string> ScrubAsp
		{
			get { return ScrubberUtils.Combine(ScrubAspViewstate, ScrubBrowserLink); }
		}

		public static Func<string, string> ScrubMvc
		{
			get { return ScrubBrowserLink; }
		}
	}
}