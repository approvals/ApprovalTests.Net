[TestFixture]
public class InlineTextReporterTest
{
    [Test]
    public void TestComment()
    {
        var actual = "Hello";
        var expected = "Hello";
        Approvals.AssertText(expected, actual);
    }

    [Test]
    public void CSharpStrings()
    {
        var example1 = new[]
        {
            "**************",

            "I am ten chars",
            "**************",
        };
        var samples = new[] { "boo","with \"quotes\".", example1.JoinWith("\n"), };

        Approvals.VerifyAll("C# Strings", samples, InlineTextReporter.ConvertToCSharp);
    }

    [Test]
    public void MakerHeadertest()
    {
        // begin-snippet: assert_text
        var header = new Header();
        var actual = header.MakeHeading("I am ten chars");
        var expected = new[]{
            "**************",
            "I am ten chars",
            "**************",

        };
        Approvals.AssertText(expected, actual);
        // end-snippet
    }

    public void MakerHeaderBefore()
    {
        // begin-snippet: assert_text_before
        var header = new Header();
        var actual = header.MakeHeading("I am ten chars");
        var expected = "";
        Approvals.AssertText(expected, actual);
        // end-snippet
    }
}

public class Header
{
    public string MakeHeading(string iAmTenChars) =>
        new[]
        {
            "**************",

            "I am ten chars",
            "**************",
        }.JoinWith("\n");
}