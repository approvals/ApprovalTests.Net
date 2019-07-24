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
        public void TestPdf()
        {

//            var pdfOriginal = PathUtilities.GetAdjacentFile("sample.pdf");
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

//            File.Copy(pdfOriginal, pdf, true);
            Approvals.VerifyPdfFile(pdf);
        }

        [Test]
        public void TestNotFound()
        {
            var stream = new MemoryStream("abcdefg".Select(x => (byte) x).ToArray());
            var find = PdfScrubber.Find("zoo", stream);
            Assert.AreEqual(-1, find);
        }

        [Test]
        public void TestFound()
        {
            var stream = new MemoryStream("abcdefg".Select(x => (byte) x).ToArray());
            var find = PdfScrubber.Find("cde", stream);
            Assert.AreEqual(2, find);
        }

        [Test]
        public void TestPartial()
        {
            var stream = new MemoryStream("abcdefg".Select(x => (byte) x).ToArray());
            var find = PdfScrubber.Find("cdf", stream);
            Assert.AreEqual(-1, find);
        }
    }
}