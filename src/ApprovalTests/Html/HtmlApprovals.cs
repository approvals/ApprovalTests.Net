namespace ApprovalTests.Html;

public static class HtmlApprovals
{
    public static void VerifyHtml(string html, Func<string, string> scrubber = null)
    {
        XmlApprovals.VerifyText(html, "html", true, scrubber);
    }

    /// <summary>
    /// Throws exception if Html is incorrectly formatted
    /// </summary>
    public static void VerifyHtmlStrict(string html)
    {
        XmlApprovals.VerifyText(html, "html");
    }
}