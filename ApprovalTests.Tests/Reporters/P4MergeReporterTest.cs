using System.Drawing;
using System.IO;
using System.Reflection;
using ApprovalTests.Core.Exceptions;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using NUnit.Framework;

namespace ApprovalTests.Tests.Reporters
{
    [TestFixture]
    public class P4MergeReporterTest
    {
        public string GetBitmapFilePath()
        {
            var bitmapFile = Path.GetTempFileName().Replace("tmp", "png");
            using (var bitmap = new Bitmap(1, 1))
            {
                bitmap.Save(bitmapFile);
            }
            return bitmapFile;
        }

        [Test]
        [UseReporter(typeof(QuietReporter))]
        public void P4MergeLaunchesOnFirstTestRun()
        {
            var existingDefaultApprovalFileName = PathUtilities.GetDirectoryForCaller() + GetType().Name + "." + MethodBase.GetCurrentMethod().Name + "approved.png";
            File.Delete(existingDefaultApprovalFileName);
            Assert.Throws<ApprovalMissingException>(() => Approvals.VerifyFile(GetBitmapFilePath()));
        }
    }
}