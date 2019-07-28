using System.IO;
using ApprovalTests.Reporters;
using ApprovalTests.Scrubber;
using ApprovalUtilities.Utilities;
using NUnit.Framework;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Assert = NUnit.Framework.Assert;

namespace ApprovalTests.Tests.Pdf
{
    [TestFixture]

    public class PdfTest
    {
        [Test]
        [UseReporter(typeof(FileLauncherReporter), typeof(ClipboardReporter))]
        public void TestPdf_New()
        {
            var pdf = PathUtilities.GetAdjacentFile("temp.pdf");

            using (var fileStream = File.Create(pdf))
            using (var writer = new PdfWriter(fileStream))
            using (var pdfDocument = new PdfDocument(writer))
            {
                pdfDocument.SetTagged();
                var document = new Document(pdfDocument);
                document.Add(new Paragraph("Test"));
                document.Close();
            }
            Approvals.VerifyPdfFile(pdf);

            File.Delete(pdf);
        }

        [Test]
        [UseReporter(typeof(FileLauncherReporter), typeof(ClipboardReporter))]
        public void TestPdf_Sample()
        {
            var pdfOriginal = PathUtilities.GetAdjacentFile("sample.pdf");
            var pdf = PathUtilities.GetAdjacentFile("temp.pdf");

            File.Copy(pdfOriginal, pdf, true);
            Approvals.VerifyPdfFile(pdf);

            File.Delete(pdf);
        }

        [TestCase("xxx(D:20191230235959+23'59')xxx")]
        [TestCase("xxx(D:20191231235959+23'59)xxx")]
        [TestCase("xxx(D:20191231235959+23')xxx")]
        [TestCase("xxx(D:20191231235959+23)xxx")]
        [TestCase("xxx(D:20191231235959+)xxx")]
        [TestCase("xxx(D:20191231235959)xxx")]
        [TestCase("xxx(D:201912312359)xxx")]
        [TestCase("xxx(D:2019123123)xxx")]
        [TestCase("xxx(D:20191231)xxx")]
        [TestCase("xxx(D:201912)xxx")]
        [TestCase("xxx(D:2019)xxx")]
        public void TestPdf_ScrubberDateMatch(string input)
        {
            var matchPositions = PdfScrubber.FindDates(input).ToList();
            Assert.IsNotEmpty(matchPositions);

            var pos = matchPositions[0];
            Assert.AreEqual( '(', input[pos.start - 3]); // the start of token: (D:
            Assert.AreEqual( ')', input[pos.start + pos.length]); // the end of token: )
        }

        [TestCase("xxx(D:20191230235959+23'59'59)xxx")]
        [TestCase("xxx(D:20191231235959+23'5)xxx")]
        [TestCase("xxx(D:20191231235959+2)xxx")]
        [TestCase("xxx(D:2019123123595)xxx")]
        [TestCase("xxx(D:20191231235)xxx")]
        [TestCase("xxx(D:201912312)xxx")]
        [TestCase("xxx(D:2019123)xxx")]
        [TestCase("xxx(D:20191)xxx")]
        [TestCase("xxx(D:201)xxx")]
        [TestCase("xxx(D:20)xxx")]
        [TestCase("xxx(D:)xxx")]
        public void TestPdf_ScrubberDateNotMatch(string input)
        {
            var matchPositions = PdfScrubber.FindDates(input);
            Assert.IsEmpty(matchPositions);
        }

        [Test]
        public void TestPdf_ScrubberFindAllReplacementsInFile()
        {
            var pdf = PathUtilities.GetAdjacentFile("sample.pdf");

            using (var fileStream = File.OpenRead(pdf))
            {
                var matches = PdfScrubber.FindReplacements(fileStream);
                Assert.AreEqual(3, matches.Count());
            }
        }

[TestCase(@"
arbitrary content
trailer
<</ID [<c9e0f709eee32d7280bf971d9a36032a><c9e0f709eee32d7280bf971d9a36032a>]/Info 3 0 R/Root 1 0 R/Size 13>>
%iText-7.1.7 for .NET
startxref
1110
%%EOF")]
[TestCase(@"
arbitrary content
trailer
<< /Size 63 /Root 28 0 R /Info 1 0 R /ID [ <4653becaf7588b39b76ee669e3e88e21>
<4653becaf7588b39b76ee669e3e88e21> ] >>
startxref
69143
%%EOF")]
        public void TestPdf_ScrubberIdsMatch(string input)
        {
            var matchPositions = PdfScrubber.FindIds(input).ToList();
            Assert.AreEqual(2, matchPositions.Count);

            Assert.IsTrue(matchPositions.All(pos => input[pos.start - 1] == '<' && input[pos.start + pos.length] == '>'));
        }

        [TestCase(@"
NO TRAILER DECLARED
<</ID [<c9e0f709eee32d7280bf971d9a36032a><c9e0f709eee32d7280bf971d9a36032a>]/Info 3 0 R/Root 1 0 R/Size 13>>
%iText-7.1.7 for .NET
startxref
1110
%%EOF")]
        [TestCase(@"
NO IDS DECLARED
trailer
<< /Size 63 /Root 28 0 R /Info 1 0 R >>
startxref
69143
%%EOF")]
        public void TestPdf_ScrubberIdsNotMatch(string input)
        {
            var matchPositions = PdfScrubber.FindIds(input).ToList();
            Assert.AreEqual(0, matchPositions.Count);
        }
    }
}