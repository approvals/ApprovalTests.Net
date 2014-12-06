using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class TortoiseImageDiffTest
    {
        [Test]
        public void TestIsImage()
        {
            var files = new[]
                            {
                                "image.png", "image.gif", "image.jpg", "image.jpeg", "image.tif", "image.tiff",
                                "movie.avi", "text.txt", "excel.xls"
                            };
            Approvals.VerifyAll(
                files,
                f => "{0}  => {1}".FormatWith(f, TortoiseImageDiffReporter.INSTANCE.IsWorkingInThisEnvironment(f)));
        }
    }
}