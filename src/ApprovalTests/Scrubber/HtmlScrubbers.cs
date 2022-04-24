using System;
using System.Text.RegularExpressions;

namespace ApprovalTests.Scrubber;

public static class HtmlScrubbers
{
    public static string ScrubBrowserLink(string input)
    {
        var regex = "\r\n<!-- Visual Studio Browser Link -->(?s).*<!-- End Browser Link -->\r\n\r\n";
        return new Regex(regex).Replace(input, string.Empty);
    }

    public static string ScrubAspViewstate(string input)
    {
        var AspViewState = "<input type=\"hidden\" name=\"__VIEWSTATE.+/>";
        return Regex.Replace(input, AspViewState, "<!-- aspviewstate -->");
    }

    public static Func<string, string> ScrubAsp => ScrubberUtils.Combine(ScrubAspViewstate, ScrubBrowserLink);

    public static Func<string, string> ScrubMvc => ScrubBrowserLink;
}