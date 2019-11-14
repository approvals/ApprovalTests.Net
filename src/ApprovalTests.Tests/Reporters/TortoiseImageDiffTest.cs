using ApprovalTests.Reporters.Windows;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class TortoiseImageDiffTest
    {
        [Test]
        public void TestIsImage()
        {
            if (!TortoiseTextDiffReporter.INSTANCE.IsWorkingInThisEnvironment("a.txt"))
            {
                return;
            }

            var files = new[]
            {
                "image.png", "image.gif", "image.jpg", "image.jpeg", "image.tif", "image.tiff",
                "movie.avi", "text.txt", "excel.xls"
            };
            Approvals.VerifyAll(
                files,
                f => $"{f}  => {TortoiseImageDiffReporter.INSTANCE.IsWorkingInThisEnvironment(f)}");
        }
    }
}