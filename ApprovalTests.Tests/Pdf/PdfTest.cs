using System.IO;
using System.Linq;
using ApprovalTests.Reporters;
using ApprovalTests.Scrubber;
using ApprovalUtilities.Utilities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using NUnit.Framework;

namespace ApprovalTests.Tests.Pdf
{
    [TestFixture]
    public class PdfTest
    {
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

        [Test]
        [UseReporter(typeof(ClipboardReporter))]
        public void TestPdf_New()
        {
            var pdf = PathUtilities.GetAdjacentFile("new_temp.pdf");

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
        public void TestPdf_Replacements()
        {
            var pdfOriginal = PathUtilities.GetAdjacentFile("sample.pdf");
            using (var fileStream = File.Open(pdfOriginal, FileMode.Open))
            {
                var replacements = PdfScrubber.FindReplacements(fileStream);
                Approvals.VerifyAll("Replacements", replacements, r => r.ToString());
            }
        }

        [Test]
        [UseReporter(typeof(ClipboardReporter))]
        public void TestPdf_Sample()
        {
            var pdfOriginal = PathUtilities.GetAdjacentFile("sample.pdf");
            var pdf = PathUtilities.GetAdjacentFile("sample_temp.pdf");

            File.Copy(pdfOriginal, pdf, true);
            Approvals.VerifyPdfFile(pdf);

            File.Delete(pdf);
        }

        [Test]
        public void TestPdf_ScrubberDateMatch()
        {
            var cases = new[] {"xxx(D:20191230235959+23'59')xxx", "xxx(D:20191231235959+23'59)xxx", "xxx(D:20191231235959+23')xxx", "xxx(D:20191231235959+23)xxx", "xxx(D:20191231235959+)xxx", "xxx(D:20191231235959)xxx", "xxx(D:201912312359)xxx", "xxx(D:2019123123)xxx", "xxx(D:20191231)xxx", "xxx(D:201912)xxx", "xxx(D:2019)xxx"};
            cases = cases.Reverse().ToArray();
            Approvals.VerifyAll("PDF Dates", cases, c => $@"{PdfScrubber.FindDates(c).ToList().ToReadableString()} For {c}");
        }

        [Test]
        public void TestPdf_ScrubberDateNotMatch()
        {
            var cases = new[] {"xxx(D:)xxx", "xxx(D:20)xxx", "xxx(D:201)xxx", "xxx(D:20191)xxx", "xxx(D:2019123)xxx", "xxx(D:201912312)xxx", "xxx(D:20191231235)xxx", "xxx(D:2019123123595)xxx", "xxx(D:20191231235959+2)xxx", "xxx(D:20191231235959+23'5)xxx", "xxx(D:20191230235959+23'59'59)xxx"};
            Approvals.VerifyAll("Dates", cases, c => $"{PdfScrubber.FindDates(c).ToReadableString()} for {c}");
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
    }
}